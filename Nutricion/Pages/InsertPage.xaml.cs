using Newtonsoft.Json;
using Nutricion.Models;
using System.Diagnostics.Tracing;
using System.Text;

namespace Nutricion;

public partial class InsertPage : ContentPage
{
    string current_user;
    private HttpClient client = new HttpClient();
    public InsertPage(string input_user)
	{
        current_user = input_user;
        InitializeComponent();
	}

	bool verifyDurationFormat(string duration)
	{
		if(duration.Length > 7 || duration.Length == 0 || duration.Length == 3 || duration.Length == 6) return false; 

		for(int i = duration.Length - 1, j = 0 ; i >= 0; i--, j++) 
		{
			if(j == 2 || j == 5)
			{
				if (duration[i] != ':')
				{
                    return false;
				}
			}
			else if (!char.IsNumber(duration[i])) return false;
			if(j == 1 || j == 4)
			{
				if (duration[i] - '0' >= 6)
				{
					return false;
				}
			}
		}
		return true;
	}

	private async void AddBtnClicked(object sender, EventArgs e)
	{
        if (!verifyDurationFormat(songDuration.Text))
		{
            await DisplayAlert("Error", "El formato para la duración de la canción debe de ser x:xx:xx", "ok");
			return;
        }

		if(!Uri.TryCreate(albumCoverLink.Text, UriKind.Absolute, out Uri albumCoverUri))
		{
            await DisplayAlert("Error", "Link de imagen erróneo", "ok");
            return;
        }

		if(songName.Text.Length == 0) 
		{
            await DisplayAlert("Error", "Falta incluir nombre de la canción", "ok");
            return;
        }

        if (artistName.Text.Length == 0)
        {
            await DisplayAlert("Error", "Falta incluir nombre del artista", "ok");
            return;
        }

        Song song = new Song
        {
            user = current_user,
            albumCoverLink = albumCoverLink.Text,
            artistName = artistName.Text,
            displayArtist = "Artista: " + artistName.Text,
			songName = songName.Text,
			songDuration = songDuration.Text,
            displayDuration = "Duración: " + songDuration.Text
        };

        string url = "https://nutricionramon.azurewebsites.net/api/Song/registrar";
        string jsonSong = JsonConvert.SerializeObject(song);

        StringContent content = new StringContent(jsonSong, Encoding.UTF8, "application/json");

        var respuesta = await client.PostAsync(url, content);



        int t = 5;


        if (respuesta.IsSuccessStatusCode)
        {
            await DisplayAlert("Agregar", "Se ha agregado correctamente", "Ok");
        }
        else
        {
            await DisplayAlert("Error", "No se pudo agregar", "Ok");
        }

        albumCoverLink.Text = "";
        artistName.Text = "";
        songName.Text = "";
        songDuration.Text = "";
    }

    private async void backToPlaylist_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlaylistPage(current_user));
    }
}


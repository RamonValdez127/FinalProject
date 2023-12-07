using Newtonsoft.Json;
using Nutricion.Models;
using System.Diagnostics.Tracing;
using System.Text;

namespace Nutricion;

public partial class EditPage : ContentPage
{
    Song song { get; set; }
    private HttpClient client = new HttpClient();
    public EditPage(ref Song song)
	{
        songName.Text = song.songName;
        albumCoverLink.Text = song.albumCoverLink;
        artistName.Text = song.artistName;
        songDuration.Text = song.songDuration;

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

	private async void EditBtnClicked(object sender, EventArgs e)
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

        song.albumCoverLink = albumCoverLink.Text;
        song.artistName = artistName.Text;
        song.displayArtist = "Artista: " + artistName.Text;
		song.songName = songName.Text;
		song.songDuration = songDuration.Text;
        song.displayDuration = "Duración: " + songDuration.Text;

        string url = "https://nutricionramon.azurewebsites.net/api/Song/registrar";
        string jsonSong = JsonConvert.SerializeObject(song);

        StringContent content = new StringContent(jsonSong, Encoding.UTF8, "application/json");

        var respuesta = await client.PutAsync(url, content);


        if (respuesta.IsSuccessStatusCode)
        {
            await DisplayAlert("Cancion", "Se ha editado correctamente", "Ok");
            //Navigation.PushAsync(new PlaylistPage(current_user));
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "No se pudo editar", "Ok");
        }

        albumCoverLink.Text = "";
        artistName.Text = "";
        songName.Text = "";
        songDuration.Text = "";
    }

    private async void backToPlaylist_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlaylistPage(song.user));
    }
}


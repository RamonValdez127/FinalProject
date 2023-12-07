using Newtonsoft.Json;
using Nutricion.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
namespace Nutricion;

public partial class PlaylistPage : ContentPage
{
    ObservableCollection<Song> OCSongList;

    private readonly HttpClient client = new HttpClient();
    string current_user;
    public PlaylistPage(string username)
	{
        current_user = username;
        InitializeComponent();
        ObtenerDatos(current_user);
    }

    private async void ObtenerDatos(string username)
    {
        string url = "https://nutricionramon.azurewebsites.net/api/Song/lista";

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync("token"));

        var respuesta = client.GetAsync(url);

        if (!respuesta.Result.IsSuccessStatusCode)
        {
            await DisplayAlert("Error", "No se pudo obtener los datos", "Ok");
            return;
        }
        else
        {
            var json = await respuesta.Result.Content.ReadAsStringAsync();
            List<Song> lista = JsonConvert.DeserializeObject<List<Song>>(json);
            var userSongs = lista.Where(r => r.user == username).ToList();
            OCSongList = new ObservableCollection<Song>(userSongs);
            playlist.ItemsSource = OCSongList;
        }
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InsertPage(current_user));
    }

    private async void EditButton_Clicked(object sender, EventArgs e)
    {
		//var song = ((ImageButton)sender).BindingContext as Song;
        //await Navigation.PushAsync(new EditPage(ref ((ImageButton)sender).BindingContext));
    }
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Atención!", "Realmente quieres eliminar la canción?", "Sí", "No");

        if(!result) return;

        var song = ((ImageButton)sender).BindingContext as Song;

        
        string url = "https://nutricionramon.azurewebsites.net/api/Song/delete/" + song.id.ToString();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync("token"));

        var respuesta = await client.DeleteAsync(url);

        if (respuesta.IsSuccessStatusCode)
        {
            OCSongList.Remove(song);
            await DisplayAlert("Eliminación", "Se ha eliminado correctamente", "Ok");

        }
        else
        {
            await DisplayAlert("Error", "No se pudo eliminar los datos", "Ok");
        }




    }
}
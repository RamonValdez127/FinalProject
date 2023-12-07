using Newtonsoft.Json;
using Nutricion.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Nutricion.Pages;

public partial class LoginPage : ContentPage
{	
	private readonly HttpClient client = new HttpClient();
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void btnLogin_Clicked(object sender, EventArgs e) 
	{
        string url = "https://nutricionramon.azurewebsites.net/api/Cuentas/login";

        User usuario = new User();
        usuario.UserName = txtUsuario.Text;
        usuario.Email = txtUsuario.Text;
        usuario.Password = txtPassword.Text;

        string jsonUser = JsonConvert.SerializeObject(usuario);

        StringContent content = new StringContent(
            jsonUser, Encoding.UTF8, "application/json");

        var respuesta = await client.PostAsync(url, content);

        if (respuesta.IsSuccessStatusCode)
        {
            var tokenString = respuesta.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<UserToken>(tokenString.Result);

            await SecureStorage.SetAsync("token", json.Token);

            await Navigation.PushAsync(new PlaylistPage(txtUsuario.Text));
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "Ok");
        }
    }

    private async void btnRegistro_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroPage());
    }
}
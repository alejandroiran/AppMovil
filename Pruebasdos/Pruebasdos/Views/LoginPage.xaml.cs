using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pruebasdos.Entidades;
using Pruebasdos.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pruebasdos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private IAuthService _authService => DependencyService.Get<IAuthService>();
        public LoginPage()
        {
            if (_authService.ValidarSesion())
            {
                Shell.Current.GoToAsync($"//{nameof(PantallaPrincipal)}");
            }
            
            InitializeComponent();
        }

        public async void Button_ClickedAsync(System.Object sender, System.EventArgs e)
        {
            Usuario user = new Usuario()
            {
                UsuarioNombre = entryUsuario.Text,
                Contrasena = entryContrasena.Text
            };
            var res = await _authService.AuthWithCredentialsAsync(user);
            if (res)
            {
                await Shell.Current.GoToAsync($"//{nameof(PantallaPrincipal)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Mensaje", "Usuario Incorrecto", "Ok");
            }
            
        }

        public async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CrearCuenta());
        }
        private void LimpiarText()
        {
            entryUsuario.Text = "";
            entryContrasena.Text = "";
        }

    }
}

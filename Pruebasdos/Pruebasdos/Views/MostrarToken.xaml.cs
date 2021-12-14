using System;
using System.Collections.Generic;
using Pruebasdos.Services;
using Xamarin.Forms;

namespace Pruebasdos.Views
{
    public partial class MostrarToken : ContentPage
    {
        public IAuthService _authService = DependencyService.Get<IAuthService>();
        public MostrarToken()
        {
            InitializeComponent();
        }

        void btnListarPosts_Clicked(System.Object sender, System.EventArgs e)
        {
            labelToken.IsVisible = true;
            labelToken.Text = _authService.BearerToken.ToString();

            labelIdUsuario.IsVisible = true;
            labelIdUsuario.Text = _authService.IdUser.ToString();
        }
    }
}

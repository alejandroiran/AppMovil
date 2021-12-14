using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pruebasdos.Services;
using Pruebasdos.Validaciones;
using Pruebasdos.Views;

namespace Pruebasdos
{
    public partial class App : Application
    {
        public NavigationPage PantallaPrincipal { get; }
        public NavigationPage LoginPage { get; }
        public App()
        {
            InitializeComponent();
            //Servicios / Validaciones
            DependencyService.Register<IAuthService, AuthService>();
            DependencyService.Register<IValidacionesService, ValidacionesService>();
            DependencyService.Register<IPostService, PostService>();
            //Shell Navigation
            MainPage = new AppShell();
            //NavigationPage
            PantallaPrincipal = new NavigationPage(new PantallaPrincipal());
            LoginPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

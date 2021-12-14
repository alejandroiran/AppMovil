using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pruebasdos.Entidades;
using Pruebasdos.Services;
using Pruebasdos.Template;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pruebasdos.Views
{
    public partial class PantallaPrincipal : ContentPage
    {
        private IAuthService _authService => DependencyService.Get<IAuthService>();
        private IPostService _postService => DependencyService.Get<IPostService>();
        public PantallaPrincipal()
        {
            if (!_authService.ValidarSesion())
            {
                Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            //listarMisPosts();
            
            InitializeComponent();
            
        }
        public async void listarMisPosts(System.Object sender, System.EventArgs e)
        {
            ListadoPosts listao = new ListadoPosts();
            List<Post> lipost = await listao.ListarPost();

            listViewPosts.ItemsSource = lipost;//await _postService.ListMisPosts();
            listViewPosts.ItemTemplate = new DataTemplate(typeof(MisPostsTemplate));
        }
        async void btnPerfil_Clicked(System.Object sender, System.EventArgs e)
        {
             await Navigation.PushAsync(new MiPerfil());
        }

        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Preferences.Clear();
            await Shell.Current.GoToAsync("//LoginPage");
        }

        async void btnGet_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Posts());
        }

        async void btnMostrart_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MostrarToken());
        }

        async void btnListarPosts_Clicked(System.Object sender, System.EventArgs e)
        {

            
            await Navigation.PushAsync(new TodosPosts());
        }

        async void listMisPosts_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {

            ListadoPosts listao = new ListadoPosts();
            Post po = (Post)e.SelectedItem;
            int iPost = po.IdPost;
            Post poste = await listao.ListPorIdPost(iPost);

            await Navigation.PushAsync(new Posts());
        }
    }
}

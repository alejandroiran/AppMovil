using System;
using System.Collections.Generic;
using Pruebasdos.Services;
using Pruebasdos.Template;
using Xamarin.Forms;

namespace Pruebasdos.Views
{
    public partial class TodosPosts : ContentPage
    {
        private IPostService _postService => DependencyService.Get<IPostService>();
        private IAuthService _authService => DependencyService.Get<IAuthService>();
        public TodosPosts()
        {
            if (!_authService.ValidarSesion())
            {
                Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            
            InitializeComponent();
            listarPosts();
        }
        public async void listarPosts()
        {
            //ListadoPosts list = new ListadoPosts();
            //List<Posts> listado = await list.ListarPost();
            
            listViewTodosPosts.ItemsSource = await _postService.ListTodosPosts();
            listViewTodosPosts.ItemTemplate = new DataTemplate(typeof(PostsTemplate));
        }
        void listViewTodosPosts_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
        }
    }
}

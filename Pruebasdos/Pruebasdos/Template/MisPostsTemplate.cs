using System;
using Xamarin.Forms;

namespace Pruebasdos.Template
{
    public class MisPostsTemplate : ViewCell
    {
        public MisPostsTemplate()
        {
            var idPost = new Label
            {
                FontSize = 15
            };

            idPost.SetBinding(Label.TextProperty, new Binding("IdPost"));

            var titulo = new Label
            {
                FontSize = 15
            };
            titulo.SetBinding(Label.TextProperty, new Binding("Titulo"));

            var descripcion = new Label
            {
                FontSize = 15
            };
            descripcion.SetBinding(Label.TextProperty, new Binding("Descripcion"));

            var fila = new StackLayout
            {
                Children = { idPost, titulo },
                Orientation = StackOrientation.Horizontal
            };

            var filaDos = new StackLayout
            {
                Children = { descripcion },
                Orientation = StackOrientation.Horizontal
            };

            View = new StackLayout
            {
                Children = { fila, filaDos },
                Orientation = StackOrientation.Vertical
            };

        }
    }
}

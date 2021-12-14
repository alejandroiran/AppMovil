using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

using System.Windows.Input;
using Pruebasdos.Services;
using System.Net.Http;
using Pruebasdos.Entidades;
using System.Linq;

namespace Pruebasdos.Views
{
    public partial class Posts : ContentPage
    {
        private Post postsss;
        private string path;
        private MediaFile _mediaFile;
        private List<Generos> listarGener;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        public Posts()
        {
            //this.postsss = listao;
            InitializeComponent();

            //if (listao != null)
            //    recuperarInfo();

            listGeneros();
        }

        private ImageSource _image;

        public ImageSource Image
        {
            get { return _image; }
            set { _image = value;  }
        }
        private void recuperarInfo()
        {
            entryTitulo.Text = postsss.IdUsuario.ToString();
        }
        private async void listGeneros()
        {
            ListadoPosts listado = new ListadoPosts();
            listarGener = await listado.listarGeneross();
            listarGener.Insert(0, new Generos { IdGenero = 0, Genero = "-Seleccionar-" });
            int nitems = listarGener.Count();

            for(int i = 0; i< nitems; i++)
            {
                pickerGeneros.Items.Add(listarGener[i].Genero);
            }
        }

        
        void btnEliminar_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        public async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }
            else
            {
                PickMediaOptions mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (_mediaFile == null) return;

                Path = _mediaFile.Path;
                Image = ImageSource.FromStream(() => _mediaFile.GetStream());
            }
        }
    }
}

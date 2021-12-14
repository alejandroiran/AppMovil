using System;
namespace Pruebasdos.Entidades
{
    public class Post
    {
        public int IdPost { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public int IdGenero { get; set; }
        public int IdUsuario { get; set; }
    }
}

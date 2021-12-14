using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pruebasdos.Entidades;

namespace Pruebasdos.Services
{
    public interface IPostService
    {
        Task<List<Post>> ListMisPosts();
        Task<List<Post>> ListTodosPosts();
        Task<List<Post>> ListPorIdPost(int Id);
    }
}

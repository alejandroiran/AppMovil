using System;
using System.Threading.Tasks;
using Pruebasdos.Entidades;

namespace Pruebasdos.Services
{
    public interface IAuthService
    {
        string BearerToken { get; }
        string IdUser { get; }
        Task<bool> AuthWithCredentialsAsync(Usuario model);
        bool ValidarSesion();
    }
}

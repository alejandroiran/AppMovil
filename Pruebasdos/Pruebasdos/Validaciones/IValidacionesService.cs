using System;
using Pruebasdos.Entidades;

namespace Pruebasdos.Validaciones
{
    public interface IValidacionesService
    {

        bool VerifyEmailID(string email);
        bool VerifyLettersNumbers(string TextMixto);
        bool SQLInyection(string validacion);
        bool HTLMInyetion(string validacion);
    }
}

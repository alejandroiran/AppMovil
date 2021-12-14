using System;
using System.Text.RegularExpressions;
using Pruebasdos.Entidades;

namespace Pruebasdos.Validaciones
{
    public class ValidacionesService : IValidacionesService 
    {
        private readonly Regex ValidationSql = new Regex(@"('(''|[^'])*')|(\b(ALTER|alter|Alter|CREATE|create|Create|DELETE|delete|Delete|DROP|drop|Drop|EXEC(UTE){0,1}|exec(ute){0,1}|Exec(ute){0,1}|INSERT( +INTO){0,1}|insert( +into){0,1}|Insert( +into){0,1}|MERGE|merge|Merge|SELECT|Select|select|UPDATE|update|Update|UNION( +ALL){0,1}|union( +all){0,1}|Union( +all){0,1})\b)");
        private readonly Regex Validationhtml = new Regex(@"<.*?>|&.*?;");

        public bool HTLMInyetion(string validacion)
        {
            bool isMatch = Validationhtml.IsMatch(validacion);
            if (isMatch)
            {
                return true;
            }
            return false;
        }

        public bool SQLInyection(string validacion)
        {
            bool isMatch = ValidationSql.IsMatch(validacion);
            if (isMatch)
            {
                return true;
            }
            return false;
        }

        public bool VerifyEmailID(string email)
        {
            string expresion;
            if(email == null || email == "")
            {
                return false;
            }
            else
            {
                expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(email, expresion))
                {
                    if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

        }

        public bool VerifyLettersNumbers(string TextMixto)
        {
            string expressionLettersNumbers;
            if(TextMixto == null || TextMixto == "")
            {
                return false;
            }
            else
            {
                expressionLettersNumbers = @"^[a-zA-Z0-9]+$";
                if (Regex.IsMatch(TextMixto, expressionLettersNumbers))
                {
                    if (Regex.Replace(TextMixto, expressionLettersNumbers, string.Empty).Length == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

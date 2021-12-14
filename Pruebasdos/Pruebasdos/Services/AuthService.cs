using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pruebasdos.Entidades;
using Xamarin.Essentials;

namespace Pruebasdos.Services
{
    public class AuthService : IAuthService
    {
        private const string AUTH_URL = "https://drawmoreart.somee.com/Publicaciones/webapi/logeo/login";
        public string BearerToken => Preferences.Get("BearerToken", string.Empty);

        public string IdUser => Preferences.Get("IdUsuario", string.Empty);

        public AuthService()
        {
        }

        

        public async Task<bool> AuthWithCredentialsAsync(Usuario model)
        {
            var httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            HttpClient client = new HttpClient(httpHandler);
            var json = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(AUTH_URL, httpContent);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var decodedToken = JWT.JsonWebToken.DecodeToObject<Dictionary<string, object>>(result, default(byte[]), false);
                //var IdUsuario = decodedToken["iss"];
               
                Preferences.Set("BearerToken", result.ToString());
                Preferences.Set("IdUsuario", decodedToken["iss"].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidarSesion()
        {
            bool token = Preferences.ContainsKey("BearerToken");
            bool idUsuario = Preferences.ContainsKey("IdUsuario");
            if(token && idUsuario)
            {
                return true;
            }
            return false;
        }
    }
}

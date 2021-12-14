using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pruebasdos.Entidades;
using Xamarin.Forms;

namespace Pruebasdos.Services
{
    public class ListadoPosts
    {
        private IAuthService _authService => DependencyService.Get<IAuthService>();
        private const string POSTURL = "https://drawmoreart.somee.com/Publicaciones/webapi/posts/";
        public ListadoPosts()
        {
        }
        
        public async Task<List<Generos>> listarGeneross()
        {
            string rpta = "";
             HttpClientHandler httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            HttpClient client = new HttpClient(httpHandler);
            var authHeader = new AuthenticationHeaderValue("bearer", _authService.BearerToken.ToString());
            client.DefaultRequestHeaders.Authorization = authHeader;
            HttpResponseMessage response = await client.GetAsync("https://drawmoreart.somee.com/Publicaciones/webapi/selects/generos");

            List<Generos> p = new List<Generos>();
            if (response != null)
            {
                rpta = await response.Content.ReadAsStringAsync();
                p = JsonConvert.DeserializeObject<List<Generos>>(rpta);
            }
            return p;
        }
         
        public async Task<List<Post>> ListarPost()
        {
            string rpta = "";
            HttpClientHandler httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            HttpClient client = new HttpClient(httpHandler);
            var authHeader = new AuthenticationHeaderValue("bearer", _authService.BearerToken.ToString());
            client.DefaultRequestHeaders.Authorization = authHeader;
            HttpResponseMessage response = await client.GetAsync(POSTURL);

            List<Post> p = new List<Post>();

            if (response != null)
            {
                rpta = await response.Content.ReadAsStringAsync();
                p = JsonConvert.DeserializeObject<List<Post>>(rpta);
            }
            return p;
        }


        public async Task<Post> ListPorIdPost(int id)
        {
            string rpta = "";
            HttpClientHandler httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            HttpClient client = new HttpClient(httpHandler);
            var authHeader = new AuthenticationHeaderValue("bearer", _authService.BearerToken.ToString());

            client.DefaultRequestHeaders.Authorization = authHeader;
            HttpResponseMessage responseMessage = await client.GetAsync(POSTURL + id);

            responseMessage.EnsureSuccessStatusCode();
            Post p = new Post();

            if (responseMessage != null)
            {
                rpta = await responseMessage.Content.ReadAsStringAsync();
                p = JsonConvert.DeserializeObject<Post>(rpta);
            }
            return p;

        }


        public async Task<int> eliminarPost(int id)
        {
            int rpta = 0;
            HttpClientHandler httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            HttpClient client = new HttpClient(httpHandler);
            var authHeader = new AuthenticationHeaderValue("bearer", _authService.BearerToken.ToString());
            client.DefaultRequestHeaders.Authorization = authHeader;
            HttpResponseMessage responseMessage = await client.GetAsync(POSTURL + id);
            Post postNuevo = new Post
            {
                IdPost = id
            };

            var jsonRequest = JsonConvert.SerializeObject(postNuevo);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");
            if (responseMessage != null)
            {
                string rptaCadena = await responseMessage   .Content.ReadAsStringAsync();
                rpta = int.Parse(rptaCadena);
            }
            return rpta;

        }
    }
}
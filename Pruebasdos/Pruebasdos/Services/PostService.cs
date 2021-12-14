using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pruebasdos.Entidades;
using Xamarin.Forms;

namespace Pruebasdos.Services
{
    public class PostService : IPostService
    {
        private IAuthService _authService => DependencyService.Get<IAuthService>();
        private const string POSTS_URL = "https://drawmoreart.somee.com/Publicaciones/webapi/selects/";
        private const string POSTURL = "https://drawmoreart.somee.com/Publicaciones/webapi/posts";
        private HttpClientHandler httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
        private HttpClient client = new HttpClient();

        public PostService()
        {
        }

        public async Task<List<Post>> ListMisPosts()
        {
            
            using ( client = new HttpClient(httpHandler, false))
            {
                //Console.WriteLine("Soy un Token: "+BearerToken);
                var authHeader = new AuthenticationHeaderValue("bearer", _authService.BearerToken.ToString());

                client.DefaultRequestHeaders.Authorization = authHeader;
                var responseMessage = await client.GetAsync(POSTS_URL + Convert.ToInt32(_authService.IdUser));

                responseMessage.EnsureSuccessStatusCode();  
                List<Post> posts = new List<Post>();

                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                 posts = JsonConvert.DeserializeObject<List<Post>>(jsonResponse);

                return posts;
            }
        }

        public async Task<List<Post>> ListTodosPosts()
        {
            //HttpClientHandler httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };

            using ( client = new HttpClient(httpHandler, false))
            {
                //Console.WriteLine("Soy un Token: "+BearerToken);
                var authHeader = new AuthenticationHeaderValue("bearer", _authService.BearerToken.ToString());

                client.DefaultRequestHeaders.Authorization = authHeader;
                var responseMessage = await client.GetAsync(POSTURL);

                responseMessage.EnsureSuccessStatusCode();
                List<Post> postsDos = new List<Post>();

                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                postsDos = JsonConvert.DeserializeObject<List<Post>>(jsonResponse);

                return postsDos;
            }
        }

        public async Task<List<Post>> ListPorIdPost(int id)
        {
            //var httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };

            using ( client = new HttpClient(httpHandler, false))
            {
                //Console.WriteLine("Soy un Token: "+BearerToken);
                var authHeader = new AuthenticationHeaderValue("bearer", _authService.BearerToken.ToString());

                client.DefaultRequestHeaders.Authorization = authHeader;
                var responseMessage = await client.GetAsync(POSTURL + id );

                responseMessage.EnsureSuccessStatusCode();
                List<Post> posts = new List<Post>();

                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                posts = JsonConvert.DeserializeObject<List<Post>>(jsonResponse);

                return posts;
            }
        }
    }
}

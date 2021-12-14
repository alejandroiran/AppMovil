using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Pruebasdos.Entidades;
using Pruebasdos.Validaciones;
using Xamarin.Forms;

namespace Pruebasdos.Views
{
    public partial class CrearCuenta : ContentPage
    {
        private string url = "https://drawmoreart.somee.com/Publicaciones/webapi/logeo/registro";


        private IValidacionesService _validacionesService => DependencyService.Get<IValidacionesService>();
        private bool vUsuario = false;
        private bool vEmail = false;
        private bool vContra = false;


        public CrearCuenta()
        {
            InitializeComponent();
        }

        public async void Button_Clicked(System.Object sender, System.EventArgs e)
        {

            //Usuario
            #region
            bool usuario = _validacionesService.VerifyLettersNumbers(entryUsuario.Text.ToString());
            if (usuario == false)
            {
                labelUsuario.IsVisible = true;
                labelUsuario.Text = "Usuario Incorrecto";
                vUsuario = false;
            }
            if (usuario == true)
            {
                bool sqlUsuario = _validacionesService.SQLInyection(entryUsuario.Text.ToString());
                if(sqlUsuario == false)
                {
                    if (entryUsuario.Text.Length < 5 || entryUsuario.Text.Length > 20)
                    {
                        labelUsuario.IsVisible = true;
                        labelUsuario.Text = "Numero de caracteres invalidos";
                        vUsuario = false;
                    }
                    else
                    {
                        labelUsuario.IsVisible = false;
                        vUsuario = true;
                    }
                }
                else
                {
                    labelUsuario.IsVisible = true;
                    labelUsuario.Text = "No Sql Inyection";
                    vUsuario = false;
                }
                
            }
            #endregion
            //Correo
            #region
            bool emailv =_validacionesService.VerifyEmailID(entryCorreo.Text.ToString());
            if (emailv == false)
            {
                labelCorreo.IsVisible = true;
                labelCorreo.Text = "Correo Incorrecto";
                vEmail = false;
            }
            if (emailv == true)
            {
                if (entryUsuario.Text.Length < 5 || entryUsuario.Text.Length > 20)
                {
                    labelCorreo.IsVisible = true;
                    labelCorreo.Text = "Numero de caracteres invalidos";
                    vEmail = false;
                }
                else
                {
                    labelCorreo.IsVisible = false;
                    vEmail = true;
                }
            }
            #endregion
            //Contrasena
            #region
            if (entryContrasena.Text != "" && entryContrasena.Text != "" && entryContrasena.Text == entryContrasena.Text)
            {
                if (entryContrasena.Text.Length > 6 && entryContrasena.Text.Length < 20)
                {
                    labelContra.IsVisible = false;
                    vContra = true;
                }
                else
                {
                    labelContra.IsVisible = true;
                    labelContra.Text = "La contraseña debe de contener minimo 7 caraceteres";
                    vContra = false;
                }
            }
            else
            {
                labelContra.IsVisible = true;
                labelContra.Text = "Contraseñas no coincidentes";
                vContra = false;
            }
            #endregion

            if (vUsuario && vEmail && vContra)
            {
                Usuario user = new Usuario
                {
                    UsuarioNombre = entryUsuario.Text,
                    CorreoElectronico = entryCorreo.Text,
                    Contrasena = entryContrasena.Text
                };
                var httpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
                HttpClient client = new HttpClient(httpHandler);

                var json = JsonConvert.SerializeObject(user);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(url, httpContent);

                var result = await response.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Mensaje", "Registro Hecho", "Ok");
                LimpiarText();
            }
        }
        private void LimpiarText()
        {
            entryUsuario.Text = "";
            entryCorreo.Text = "";
            entryContrasena.Text = "";
            entryConfirmacion.Text = "";
        }


    }
}

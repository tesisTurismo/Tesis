using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tesis.Servicios
{
    using Comun.Modelo;
    using Plugin.Connectivity;
    using System.Net.Http;
    public class ApiServicio
    {

        public async Task<Respuesta> ValidacionInternet()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Respuesta
                {
                    respExitosa = false,
                    mensaje = "Por favor conectarse a internet.",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("www.google.com");
            if (!isReachable)
            {
                return new Respuesta
                {
                    respExitosa = false,
                    mensaje = "No hay conexión a internet.",
                };
            }

            return new Respuesta
            {
                respExitosa = true,
            };
        }


        public async Task<Respuesta> mostrarLista<T>(string urlBase, string prefijo, string controlador)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefijo}{controlador}";
                var respuestaNav = await client.GetAsync(url);
                var respuestaJson = await respuestaNav.Content.ReadAsStringAsync();
                if (!respuestaNav.IsSuccessStatusCode)
                {
                    return new Respuesta
                    {
                        respExitosa = false,
                        mensaje = respuestaJson,
                    };
                }

                var lista = JsonConvert.DeserializeObject<List<T>>(respuestaJson);

                return new Respuesta
                {
                    respExitosa = true,
                    resultado = lista

                };

            }

            catch (Exception ex)
            {
                return new Respuesta
                {
                    respExitosa = false,
                    mensaje = ex.Message,
                };
            }
        }

        public async Task<Respuesta> mostrarLista<T>(string urlBase, string prefijo, string controlador,int id)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefijo}{controlador}/{id}";
                var respuestaNav = await client.GetAsync(url);
                var respuestaJson = await respuestaNav.Content.ReadAsStringAsync();
                if (!respuestaNav.IsSuccessStatusCode)
                {
                    return new Respuesta
                    {
                        respExitosa = false,
                        mensaje = respuestaJson,
                    };
                }

                var lista = JsonConvert.DeserializeObject<List<T>>(respuestaJson);

                return new Respuesta
                {
                    respExitosa = true,
                    resultado = lista

                };

            }

            catch (Exception ex)
            {
                return new Respuesta
                {
                    respExitosa = false,
                    mensaje = ex.Message,
                };
            }
        }






    }
}

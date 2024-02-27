using MauiTODO.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MauiTODO.Services
{
    public class TareaService 
    {
        private string urlApi = "http://127.0.0.1:8000";


        public async Task<bool> CheckLoginAsync(UsuarioLogin usuario)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var dataAut = Convert.ToBase64String(Encoding.ASCII.GetBytes(usuario.Username + ":" + usuario.Password));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + dataAut);
                    var response = await client.GetAsync(urlApi + "/login");
                    var responseLogin = await response.Content.ReadAsStringAsync();
                    JsonNode nodo = JsonNode.Parse(responseLogin);
                    return (bool)nodo; 
                }
            }
            catch (Exception ex)
            {
                usuario.IsVisibleUserNameError = true;
                usuario.UserNameError = $"Error en el servidor {ex}";
                return false;
            }
        }



        public async Task<int> get_usuario(UsuarioLogin usuario)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var dataAut = Convert.ToBase64String(Encoding.ASCII.GetBytes(usuario.Username + ":" + usuario.Password));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + dataAut);
                    var response = await client.GetAsync(urlApi + "/get_usuario");
                    var responseLogin = await response.Content.ReadAsStringAsync();
                    JsonNode nodo = JsonNode.Parse(responseLogin);
                    return (int)nodo;
                }
            }
            catch (Exception ex)
            {
                usuario.IsVisibleUserNameError = true;
                usuario.UserNameError = $"Error en el servidor {ex}" ;
                
                return -1;
            }
        }

        public async Task<ObservableCollection<Tarea>> ObtenerTareas(UsuarioLogin usuario)
        {
            var client= new HttpClient();
            var response = await client.GetAsync(urlApi+"/tareas");
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonNode nodos = JsonNode.Parse(responseBody);
            JsonNode result = nodos["result"];
            var tareaData = JsonSerializer.Deserialize<ObservableCollection<Tarea>>(result.ToJsonString());
            return tareaData;
        }

    }
}

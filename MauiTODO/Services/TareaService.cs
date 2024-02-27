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
        private static UsuarioLogin usuarioLogeado;


        
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
                    usuarioLogeado = usuario;
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


        public async Task<ObservableCollection<Tarea>> ObtenerTareas()
        {
            var client= new HttpClient();
            var dataAut = Convert.ToBase64String(Encoding.ASCII.GetBytes(usuarioLogeado.Username + ":" + usuarioLogeado.Password));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + dataAut);
            var response =  client.GetAsync(urlApi+"/tareas");
            var responseBody = await response.Result.Content.ReadAsStringAsync();
            JsonNode json = JsonNode.Parse(responseBody);
            var tareas = JsonSerializer.Deserialize<ObservableCollection<Tarea>>(json.ToString());
            return tareas;
        }
        public async Task<bool> crearTarea(Tarea tarea)
        {
            var client = new HttpClient();
            var dataAut = Convert.ToBase64String(Encoding.ASCII.GetBytes(usuarioLogeado.Username + ":" + usuarioLogeado.Password));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + dataAut);

            string json = JsonSerializer.Serialize(tarea);
            StringContent contentTarea = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(this.urlApi + "/add_todo", contentTarea);
            return response != null;
        }
    }
}

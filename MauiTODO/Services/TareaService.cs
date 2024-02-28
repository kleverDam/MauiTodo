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
        private static bool isEdit=true;



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
            try
            {
                using (var client = new HttpClient())
                {
                    var dataAut = Convert.ToBase64String(Encoding.ASCII.GetBytes(usuarioLogeado.Username + ":" + usuarioLogeado.Password));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + dataAut);
                    var response = client.GetAsync(urlApi + "/tareas");
                    var responseBody = await response.Result.Content.ReadAsStringAsync();
                    JsonNode json = JsonNode.Parse(responseBody);
                    var tareas = JsonSerializer.Deserialize<ObservableCollection<Tarea>>(json.ToString());
                    return tareas;
                }
            }
            catch (Exception ex)
            {
                usuarioLogeado.IsVisibleUserNameError = true;
                usuarioLogeado.UserNameError = $"Error en el servidor {ex}";
                return new ObservableCollection<Tarea>();
            }

        }
        public async Task<bool> CrearTarea(Tarea tarea)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{usuarioLogeado.Username}:{usuarioLogeado.Password}"));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials);
                    var jsonBody = JsonSerializer.Serialize(tarea);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{urlApi}/todo/create", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return bool.Parse(responseBody);  // Asegúrate de que la API devuelva un booleano
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                Console.WriteLine($"Error al crear la tarea: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> BorrarTarea(Tarea tarea)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{usuarioLogeado.Username}:{usuarioLogeado.Password}"));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials);
                    var response = await client.DeleteAsync($"{urlApi}/todo/delete/{tarea.id}");
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return bool.Parse(responseBody);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                Console.WriteLine($"Error al eliminar la tarea: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EditarTarea(Tarea tarea)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{usuarioLogeado.Username}:{usuarioLogeado.Password}"));
                    client.DefaultRequestHeaders.Add("Authorization", $"Basic {credentials}");
                    var json = JsonSerializer.Serialize(tarea);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"{urlApi}/todo/update?id={tarea.id}", content);
                    response.EnsureSuccessStatusCode(); // Lanza excepción si hay error HTTP
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }

}

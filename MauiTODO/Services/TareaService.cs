using MauiTODO.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MauiTODO.Services
{
    public class TareaService : ITareas
    {
        private string urlApi = "http://localhost:8016";

        public async Task<bool> checkLogin()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(urlApi+"/login");
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonNode nodo = JsonNode.Parse(responseBody);
            return (Boolean)nodo;
        }

        public async Task<ObservableCollection<Tarea>> ObtenerTareas()
        {
            var client= new HttpClient();
            var response = await client.GetAsync(urlApi);
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonNode nodos = JsonNode.Parse(responseBody);
            JsonNode result = nodos["result"];

            var tareaData = JsonSerializer.Deserialize<ObservableCollection<Tarea>>(result.ToJsonString());
            return tareaData;
        }

        Task<List<Tarea>> ITareas.ObtenerTareas()
        {
            throw new NotImplementedException();
        }
    }
}

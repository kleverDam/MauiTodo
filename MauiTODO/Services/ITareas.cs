using MauiTODO.Models;
using System.Collections.ObjectModel;
namespace MauiTODO.Services
{
    public interface ITareas
    {
        public Task<ObservableCollection<Tarea>> ObtenerTareas();
        public Task<Boolean> CheckLogin();
    }
}

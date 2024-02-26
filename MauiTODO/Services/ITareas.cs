using MauiTODO.Models;

namespace MauiTODO.Services
{
    public interface ITareas
    {
        public Task<List<Tarea>> ObtenerTareas();
        public Task<Boolean> checkLogin();
    }
}

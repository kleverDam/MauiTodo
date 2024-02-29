using MauiTODO.Helpers;

namespace MauiTODO.Models
{
    public class Tarea : NotifyBase
    {



        private int _id;
        private string _name;
        private string _descripcion;
        private string _estado_tarea;
        private bool _isVisibleTareaError;
        private string _errorTarea;
        private bool _isVisibleTareaExito;
        private string _exitoTarea;
       
        public int id { get => _id; set { _id = value; OnPropertyChanged(); } }
        public string name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string descripcion { get => _descripcion; set { _descripcion = value; OnPropertyChanged(); } }
        public string estado_tarea { get => _estado_tarea.Trim().Equals("1", StringComparison.OrdinalIgnoreCase) ? "Archivada" : "No Archivada"; set { _estado_tarea = value; OnPropertyChanged(); } }

        public bool IsVisibleTareaError
        {
            get => _isVisibleTareaError; set { _isVisibleTareaError = value; OnPropertyChanged(); }
        }
        public string ErrorTarea
        {
            get => _errorTarea; set { _errorTarea = value; OnPropertyChanged(); }
        }

        public bool IsVisibleTareaExito
        {
            get => _isVisibleTareaExito; set { _isVisibleTareaExito = value; OnPropertyChanged(); }
        }
        public string ExitoTarea
        {
            get => _exitoTarea; set { _exitoTarea = value; OnPropertyChanged(); }
        }

    }
}

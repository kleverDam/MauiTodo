
namespace MauiTODO.Models
{
    public class Tarea : NotifyBase
    {
            private int _id;
            private string _name;
            private string _descripcion;
            private string _estado_tarea;
            private int _usuarioLogin;

            // getter and setter
            public int TareasId
            {
                get { return _id; }
                set { _id = value; OnPropertyChanged(); }
            }
            public string Titulo
            {
                get { return _name; }
                set { _name = value; OnPropertyChanged(); }
            }

            public string Descripcion
            {
                get { return _descripcion; }
                set { _descripcion = value; OnPropertyChanged(); }
            }

            public string Estado
            {
                get { return _estado_tarea; }
                set { _estado_tarea = value; OnPropertyChanged(); }
            }

            public int UsuarioActivo
        {
            get { return _usuarioLogin; }
            set { _usuarioLogin = value; OnPropertyChanged(); }
        }

    }
}

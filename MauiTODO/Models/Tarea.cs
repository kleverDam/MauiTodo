    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace MauiTODO.Models
    {
    // Enumeration for task status
    public enum Estado
    {
        Archivada,
        NoArchivada
    }

    public class Tarea : NotifyBase
    {
        private static UsuarioLogin _usuarioLogin;
        private string _titulo;
        private string _descripcion;
        private Estado _estado;

        // getter and setter
        public UsuarioLogin UsuarioActivo
        {
            get { return _usuarioLogin; }
            set { _usuarioLogin = value; OnPropertyChanged(); }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; OnPropertyChanged(); }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; OnPropertyChanged(); }
        }

        public Estado Estado
        {
            get { return _estado; }
            set { _estado = value; OnPropertyChanged(); }
        }

    }
}

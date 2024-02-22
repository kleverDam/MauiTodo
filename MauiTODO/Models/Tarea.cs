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
        private static UsuarioLogin _usuarioActivo;
        private string _titulo;
        private string _descripcion;
        private Estado _estado;
        private string _usuario;

        // getter and setter
        public UsuarioLogin UsuarioActivo
        {
            get { return _usuarioActivo; }
            set { _usuarioActivo = value; OnPropertyChanged(); }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; OnPropertyChanged(); }
        }

    }
}

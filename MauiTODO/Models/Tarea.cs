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

    public class Tarea
    {
        private string _titulo;
        private string _descripcion;
        private Estado _estado;
        private string _usuario;

        // getter and setter
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public Estado Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
    }
}

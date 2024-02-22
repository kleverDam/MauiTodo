using Microsoft.Maui.Controls.Handlers.Compatibility;

namespace MauiTODO.Models
{
    public class UsuarioLogin
    {
        private string _usuarioid;
        private string _username;
        private string _password;


        public string UsuarioId
        {
            get { return _usuarioid; }
            set { _usuarioid = value; }
        }
        public string Username {
            get { return _username; }
            set { _username = value; }
        } 
        public string Password {
            get { return _password; }
            set { _password = value; }
        }
            
    }
}

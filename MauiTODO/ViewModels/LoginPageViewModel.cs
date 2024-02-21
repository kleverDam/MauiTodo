
using MauiTODO.Models;
using System.Collections.ObjectModel;

namespace MauiTODO.ViewModels
{
    public class LoginPageViewModel
    {
        //Borrar despues esto es temporal 
        private ObservableCollection<UsuarioLogin> _usuarios;
        public ObservableCollection<UsuarioLogin> Usuarios { get => _usuarios; set => _usuarios = value; }

        private Command _login;
        public Command LoginComan { get => _login; set => _login = value; }
        //----------------
        private string _username;
        private string _password;


        public string UserName { get { return _username; }set { _username = value; } }
        public string Password { get { return _password; }set { _password = value; } }

        public LoginPageViewModel() {
            LoginComan = new Command(this.loginAcces);
            this.Usuarios = new ObservableCollection<UsuarioLogin>();
            this.CargarUsuarios();
           

        }


        private void loginAcces()
        {

            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password)) {
                // Crear un usuario temporal con los datos proporcionados
                var usuarioTemporal = new UsuarioLogin { Username = UserName, Password = Password };
                // Verificar si el usuario temporal existe en la lista de usuarios
                var usuarioExiste = Usuarios.Any(u => u.Username == usuarioTemporal.Username && u.Password == usuarioTemporal.Password);

                if (usuarioExiste)
                {
                    // Usuario existe

                    Console.WriteLine("Acceso Correcto.");
                }
                else
                {
                    // Usuario no existe
                    Console.WriteLine("Error: Usuario o contraseña incorrecta.");
                }
            }
            else
            {
                Console.WriteLine("Error: El nombre de usuario y la contraseña no pueden estar vacíos.");
            }
        }

        private void CargarUsuarios() { 
            Usuarios.Add(new UsuarioLogin {Username = "admin", Password = "a"});
            Usuarios.Add(new UsuarioLogin {Username = "test", Password = "t"});
            Usuarios.Add(new UsuarioLogin {Username = "klever", Password = "k"});
        }



            
    }
}

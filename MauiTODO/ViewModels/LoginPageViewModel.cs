
using MauiTODO.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiTODO.ViewModels
{
    public class LoginPageViewModel


    {
        private Command _loginComan;

        private Models.UsuarioLogin _usuarioLogin;



        private ObservableCollection<UsuarioLogin> _usuarios;
        public ObservableCollection<UsuarioLogin> Usuarios { get => _usuarios; set => _usuarios = value; }
        public Command LoginComand { get => _loginComan; set => _loginComan = value; }
        public UsuarioLogin UsuarioLogin { get => _usuarioLogin; set => _usuarioLogin = value; }



        // Getter y Setter para _loginCommand





        public LoginPageViewModel()
        {
            UsuarioLogin = new UsuarioLogin();
            _loginComan = new Command(this.Login);
            Usuarios = new ObservableCollection<UsuarioLogin>();
            this.CargarUsuarios();
        }





        private async void Login()
        {
            UsuarioLogin.IsVisibleUserNameError = false;
            UsuarioLogin.UserNameError = "";

            if (!string.IsNullOrWhiteSpace(UsuarioLogin.Username) && !string.IsNullOrWhiteSpace(UsuarioLogin.Password))
            {
                var usuarioExiste = Usuarios.Any(u => u.Username == UsuarioLogin.Username && u.Password == UsuarioLogin.Password);

                if (usuarioExiste)
                {
                    await LoginAccepted();
                }
                else
                {
                    UsuarioLogin.IsVisibleUserNameError = true;
                    UsuarioLogin.UserNameError = "Usuario o contraseña incorrectos";
                    return;
                }
            }
            else
            {
                UsuarioLogin.IsVisibleUserNameError = true;
                UsuarioLogin.UserNameError = "El nombre de usuario y la contraseña no pueden estar vacíos";
                return;
            }
        }
        private async Task LoginAccepted(){ await Shell.Current.GoToAsync("HomePage"); }

        private void CargarUsuarios()
        {
            Usuarios.Add(new UsuarioLogin { Username = "admin", Password = "a" });
            Usuarios.Add(new UsuarioLogin { Username = "test", Password = "t" });
            Usuarios.Add(new UsuarioLogin { Username = "klever", Password = "k" });
        }
    }
}

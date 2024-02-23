
using MauiTODO.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiTODO.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private Command _loginComan;
        private Models.UsuarioLogin _usuarioLogin;
        private string _textUserName;
        private string _textUserPassword;
        //private Models.Tarea _tarea;

        private ObservableCollection<UsuarioLogin> _usuarios;
        public ObservableCollection<UsuarioLogin> Usuarios { get => _usuarios; set => _usuarios = value; }
        public Command LoginComand { get => _loginComan; set => _loginComan = value; }
        public UsuarioLogin UsuarioLogin { get => _usuarioLogin; set => _usuarioLogin = value; }
        public string TextUserName { get => _textUserName; set => _textUserName = value; }
        public string TextUserPassword { get => _textUserPassword; set => _textUserPassword = value; }

        //public Tarea Tarea { get => _tarea; set => _tarea = value; }
        private bool _guardarInicioSesion;


        //Borrar despues
        public bool GuardarInicioSesion
        {
            get { return _guardarInicioSesion; }
            set
            {
                if (_guardarInicioSesion != value)
                {
                    _guardarInicioSesion = value;
                    OnPropertyChanged();
                }
            }
        }
      
       
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
        private async Task LoginAccepted()
        {
            try
            {
                if (Shell.Current != null)
                {
                   

                   string usernameAndPassword = $"{UsuarioLogin.Username},{UsuarioLogin.Password}";
                   await Shell.Current.GoToAsync($"//MainPage?user={Uri.EscapeDataString(usernameAndPassword)}");
                    
                }
                else
                {
                    UsuarioLogin.UserNameError = "No se puede cargar la pagina....";
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la navegación.
                Console.WriteLine($"Error al navegar a la página MainPage: {ex.Message}");
            }
        }

        private void CargarUsuarios()
        {
            Usuarios.Add(new UsuarioLogin { Username = "admin", Password = "a" });
            Usuarios.Add(new UsuarioLogin { Username = "test", Password = "t" });
            Usuarios.Add(new UsuarioLogin { Username = "klever", Password = "k" });
        }
    }
}

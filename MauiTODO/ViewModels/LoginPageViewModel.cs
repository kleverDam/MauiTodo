using MauiTODO.Models;
using MauiTODO.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MauiTODO.ViewModels
{
    public class LoginPageViewModel : TareaService, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Command _loginCommand;
        private UsuarioLogin _usuarioLogin;
        private TareaService _loginService;
        private bool _guardarInicioSesion;

        public Command LoginCommand { get => _loginCommand; set => _loginCommand = value; }
        public UsuarioLogin UsuarioLogin { get => _usuarioLogin; set => _usuarioLogin = value; }
        public TareaService LoginService { get => _loginService; set => _loginService = value; }
        public bool GuardarInicioSesion { get => _guardarInicioSesion; set => _guardarInicioSesion = value; }

        public LoginPageViewModel()
        {
            UsuarioLogin = new UsuarioLogin();
            LoginService = new TareaService();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            LoginCommand = new Command(async () => await Login(), () => CanLogin());
        }

        private async Task Login()
        {
            UsuarioLogin.IsVisibleUserNameError = false;
            UsuarioLogin.UserNameError = "";
            bool isLoggedIn = await LoginService.checkLogin();

            if (!string.IsNullOrWhiteSpace(UsuarioLogin.Username) && !string.IsNullOrWhiteSpace(UsuarioLogin.Password))
            {
                if (isLoggedIn)
                {
                    await LoginAccepted();
                }
                else
                {
                    UsuarioLogin.IsVisibleUserNameError = true;
                    UsuarioLogin.UserNameError = "Usuario o contraseña incorrectos";
                }
            }
            else
            {
                UsuarioLogin.IsVisibleUserNameError = true;
                UsuarioLogin.UserNameError = "El nombre de usuario y la contraseña no pueden estar vacíos";
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
                    UsuarioLogin.UserNameError = "No se puede cargar la página....";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al navegar a la página MainPage: {ex.Message}");
            }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(UsuarioLogin.Username) && !string.IsNullOrWhiteSpace(UsuarioLogin.Password);
        }
    }
}
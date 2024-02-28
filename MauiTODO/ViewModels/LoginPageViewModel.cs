using MauiTODO.Helpers;
using MauiTODO.Models;
using MauiTODO.Services;
namespace MauiTODO.ViewModels
{
    public class LoginPageViewModel : NotifyBase
    {


        private Command _loginCommand;
        private UsuarioLogin _usuarioLogin;
        private TareaService _tareaService;

        public Command LoginCommand { get => _loginCommand; set {_loginCommand = value; OnPropertyChanged(); }
}
        public UsuarioLogin UsuarioLogin { get => _usuarioLogin; set {_usuarioLogin = value; OnPropertyChanged(); } }

        public LoginPageViewModel()
        {
            UsuarioLogin = new UsuarioLogin();
            _tareaService = new TareaService();
            LoginCommand = new Command(this.Login);
        }

        private async void Login()
        {
            UsuarioLogin.IsVisibleUserNameError = false;
            UsuarioLogin.UserNameError = "";

            if (!string.IsNullOrWhiteSpace(UsuarioLogin.Username) && !string.IsNullOrWhiteSpace(UsuarioLogin.Password))
            {
                bool isLoggedIn = await _tareaService.CheckLoginAsync(this.UsuarioLogin);
                if (isLoggedIn)
                {
                    LoginAccepted();
                }
                else
                {
                    if (!UsuarioLogin.IsVisibleUserNameError)
                    {
                        UsuarioLogin.IsVisibleUserNameError = true;
                        UsuarioLogin.UserNameError = "Usuario o contraseña incorrectos";
                    }
                }
            }
            else
            {
                UsuarioLogin.IsVisibleUserNameError = true;
                UsuarioLogin.UserNameError = "El nombre de usuario y la contraseña no pueden estar vacíos";
            }
        }

        private async void LoginAccepted()
        {
            try
            {
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync("//MainPage");
                    UsuarioLogin.Username = string.Empty;
                    UsuarioLogin.Password = string.Empty;
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
    }
}
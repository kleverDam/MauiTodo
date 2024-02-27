using MauiTODO.Models;
using MauiTODO.Services;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        private ICommand _loginCommand;
        private UsuarioLogin _usuarioLogin;
        private TareaService _tareaService;

        public ICommand LoginCommand { get => _loginCommand; set => _loginCommand = value; }
        public UsuarioLogin UsuarioLogin { get => _usuarioLogin; set => _usuarioLogin = value; }

        public LoginPageViewModel()
        {
            UsuarioLogin = new UsuarioLogin();
            _tareaService = new TareaService();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            LoginCommand = new Command(async () => await Login());
        }

        private async Task Login()
        {
            UsuarioLogin.IsVisibleUserNameError = false;
            UsuarioLogin.UserNameError = "";

            // Llamar al servicio para verificar el inicio de sesión
            bool isLoggedIn = await _tareaService.CheckLogin();

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
    }
}
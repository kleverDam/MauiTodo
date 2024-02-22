
using MauiTODO.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiTODO.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private ObservableCollection<UsuarioLogin> _usuarios = new ObservableCollection<UsuarioLogin>();
        public ObservableCollection<UsuarioLogin> Usuarios { get => _usuarios; set => SetProperty(ref _usuarios, value); }

        private LoginViewModel _userLogin;
        public LoginViewModel UserLogin { get => _userLogin; set => SetProperty(ref _userLogin, value); }

        private bool _isLoginAccess;
        public bool IsLoginAccess { get => _isLoginAccess; private set => SetProperty(ref _isLoginAccess, value); }

        public IList<LoginViewModel> Logins { get; } = new ObservableCollection<LoginViewModel>();

        public ICommand NewCommand { private get; set; }
        public ICommand LoginCommand { private get; set; }
        public ICommand CancelCommand { private get; set; }

        public LoginPageViewModel()
        {
            this.CargarUsuarios();

            NewCommand = new Command(
                execute: () =>
                {
                    UserLogin = new LoginViewModel();
                    IsLoginAccess = true;
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return !IsLoginAccess;
                });

            LoginCommand = new Command(
                execute: () =>
                {
                    LoginAccess();
                    UserLogin = null;
                    IsLoginAccess = false;
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return UserLogin != null &&
                           !string.IsNullOrWhiteSpace(UserLogin.UserName) &&
                           !string.IsNullOrWhiteSpace(UserLogin.Password);
                });

            CancelCommand = new Command(
               execute: () =>
               {
                   UserLogin = null;
                   IsLoginAccess = false;
                   RefreshCanExecutes();
               },
               canExecute: () =>
               {
                   return IsLoginAccess;
               });
        }

        void RefreshCanExecutes()
        {
            (NewCommand as Command)?.ChangeCanExecute();
            (LoginCommand as Command)?.ChangeCanExecute();
            (CancelCommand as Command)?.ChangeCanExecute();
        }

        private async void LoginAccess()
        {
            if (UserLogin != null && !string.IsNullOrWhiteSpace(UserLogin.UserName) && !string.IsNullOrWhiteSpace(UserLogin.Password))
            {
                var usuarioTemporal = new UsuarioLogin { Username = UserLogin.UserName, Password = UserLogin.Password };
                var usuarioExiste = Usuarios.Any(u => u.Username == usuarioTemporal.Username && u.Password == usuarioTemporal.Password);

                if (usuarioExiste)
                {
                    // Muestra un mensaje de alerta indicando un acceso correcto
                    await Application.Current.MainPage.DisplayAlert("Correcto", "Acceso correcto", "Aceptar");
                    Console.WriteLine("Acceso Correcto.");
                    //await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                }
                else
                {
                    // Muestra un mensaje de alerta indicando un error de usuario o contraseña incorrecta
                    await Application.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrecta", "Aceptar");
                    Console.WriteLine("Error: Usuario o contraseña incorrecta.");
                }
            }
            else
            {
                // Muestra un mensaje de alerta indicando que el nombre de usuario y contraseña no pueden estar vacíos
                await Application.Current.MainPage.DisplayAlert("Error", "El nombre de usuario y la contraseña no pueden estar vacíos", "Aceptar");
                Console.WriteLine("Error: El nombre de usuario y la contraseña no pueden estar vacíos.");
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

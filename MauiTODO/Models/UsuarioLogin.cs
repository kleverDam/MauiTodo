using MauiTODO.Helpers;
namespace MauiTODO.Models
{
    public class UsuarioLogin : NotifyBase
    {
        private string _username;
        private string _password;
        private bool _isVisibleUserNameError;
        private string _userNameError;


        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }

        }
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        public bool IsVisibleUserNameError
        {
            get => _isVisibleUserNameError; set { _isVisibleUserNameError = value; OnPropertyChanged(); }
        }
        public string UserNameError
        {
            get => _userNameError; set { _userNameError = value; OnPropertyChanged(); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTODO.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        private string _username;
        private string _password;
        public string UserName { get { return _username; } set {SetProperty(ref _username, value); } }
        public string Password { get { return _password; } set {SetProperty(ref _password, value); } }

        public override string ToString()
        {
            return UserName+"contraseña"+Password;
        }
    }
}

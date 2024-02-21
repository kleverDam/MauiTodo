using MauiTODO.View;
using MauiTODO.ViewModels;

namespace MauiTODO
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new View.LoginPage());
        }
    }
}
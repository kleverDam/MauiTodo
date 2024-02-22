using MauiTODO.ViewModels;

namespace MauiTODO.View;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginPageViewModel();
    }
}
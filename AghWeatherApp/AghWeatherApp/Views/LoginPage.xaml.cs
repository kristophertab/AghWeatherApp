using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AghWeatherApp.Models;
using AghWeatherApp.ViewModels;

namespace AghWeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        private LoginViewModel loginViewModel;
        private String urlTest = "http://10.10.118.25:5000";
        private String urlProd = "http://192.168.43.195:60869";

        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public LoginPage ()
		{
			InitializeComponent ();
            loginViewModel = new LoginViewModel();
            loginViewModel.SetApiUrl(urlTest);
		}

        private async Task LoginButtonClickedAsync(object sender, EventArgs e)
        {
            //ProgramState.uName = entryUser.Text;
            int id = await loginViewModel.LoginAsync(entryUser.Text, entryPassword.Text);

            if (id != 0)
            {
                await RootPage.NavigateFromMenu((int)MenuItemType.Main);
            }
            else
            {
                await DisplayAlert("Error", "no such user in database", "OK");

            }
            
        }

        private void updateUrl_Clicked(object sender, EventArgs e)
        {
            if(updateUrlEntry.Text.Length > 0)
            {
                loginViewModel.SetApiUrl(updateUrlEntry.Text);
            }
            else
            {
                DisplayAlert("Error", "url entry is empty", "OK");
            }
            
        }
    }
}
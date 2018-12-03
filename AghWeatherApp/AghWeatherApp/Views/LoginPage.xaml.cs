using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AghWeatherApp.Models;

namespace AghWeatherApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public LoginPage ()
		{
			InitializeComponent ();
		}

        private async Task LoginButtonClickedAsync(object sender, EventArgs e)
        {
            ProgramState.uName = entryUser.Text;

            await RootPage.NavigateFromMenu((int)MenuItemType.Main);
        }
    }
}
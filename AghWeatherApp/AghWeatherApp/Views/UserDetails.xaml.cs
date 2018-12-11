using AghWeatherApp.Models;
using AghWeatherApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AghWeatherApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserDetails : ContentPage
	{
        UserDetailsViewModel vm;
        int selectedUserId;
		public UserDetails ()
		{
			InitializeComponent ();

            vm = new UserDetailsViewModel();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() => refreshFuction());
                return true;
            });
        }

        protected override void OnAppearing()
        {
            vm = new UserDetailsViewModel();
        }

        /* Picker */

        private async void refreshFuction()
        {
            fillUsersPicker();
        }

        private void fillUsersPicker()
        {
            
            foreach (string name in vm.userNamesList)
            {
                if (!this.usersListPicker.Items.Contains(name))
                {
                    this.usersListPicker.Items.Add(name);
                }
               
            }
        }

        private void FillUserDataFiledBySelectedPickerItem(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                selectedUserId = vm.usersList[selectedIndex].user_Guid;

                this.usernameEntry.Text = vm.usersList[selectedIndex].username;
                this.forenameEntry.Text = vm.usersList[selectedIndex].forename;
                this.emailEntry.Text = vm.usersList[selectedIndex].email;
                this.passwordEntry.Text = vm.usersList[selectedIndex].password;
            }
        }

        private void createUserButton_Clicked(object sender, EventArgs e)
        {
            UserData newUser = new UserData();

            newUser.username = this.usernameEntry.Text;
            newUser.forename = this.forenameEntry.Text;
            newUser.email = this.emailEntry.Text;
            newUser.password = this.passwordEntry.Text;

            vm.AddUser(newUser);

        }

        private void updateUserButton_Clicked(object sender, EventArgs e)
        {
            UserData newUser = new UserData();
                    
            newUser.user_Guid = selectedUserId;
            newUser.username = this.usernameEntry.Text;
            newUser.forename = this.forenameEntry.Text;
            newUser.email = this.emailEntry.Text;
            newUser.password = this.passwordEntry.Text;

            vm.UpdateUser(newUser);
        }

        private void deleteUserButton_Clicked(object sender, EventArgs e)
        {
            vm.RemoveUserAsync(selectedUserId);
        }
    }
}
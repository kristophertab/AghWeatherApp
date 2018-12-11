using AghWeatherApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AghWeatherApp.Services;

namespace AghWeatherApp.ViewModels
{

    public class UserDetailsViewModel
    {
        public List<UserData> usersList { get; set; }
        public List<string> userNamesList { get; set; }
        public UserDetailsViewModel()
        {
            usersList = new List<UserData>();
            userNamesList = new List<string>();

            ReadAllUsers();

        }

        public async void ReadAllUsers()
        {
            usersList = await DataService.GetAllUsers();

            userNamesList.Clear();
            foreach(UserData user in usersList)
            {
                userNamesList.Add(user.username);
            }
        }

        public void AddUser(UserData user)
        {
            DataService.PostUser(user);
        }
        public void UpdateUser(UserData user)
        {
            DataService.PutUser(user);
        }

        public async void RemoveUserAsync(int id)
        {
            await DataService.DelateUser(id);
        }
    }
}

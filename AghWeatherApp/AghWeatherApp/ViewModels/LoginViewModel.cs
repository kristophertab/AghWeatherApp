using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AghWeatherApp.Models;
using AghWeatherApp.Services;

namespace AghWeatherApp.ViewModels
{
    class LoginViewModel
    {
        public void SetApiUrl(string newApiUrl)
        {
            ProgramState.apiUrl = newApiUrl;
        }

        private string newUserName = "nonane";
        private void setNewUser(User newUserData)
        {
            ProgramState.userId = newUserData.user_Guid;
            ProgramState.roleId = newUserData.role_Id;
            ProgramState.uName = newUserName;
        }

        public async Task<int> LoginAsync(string user, string password)
        {
            newUserName = user;

            await DataService.PostLoginData(user, password, setNewUser);

            return ProgramState.userId;
        }
    }
}

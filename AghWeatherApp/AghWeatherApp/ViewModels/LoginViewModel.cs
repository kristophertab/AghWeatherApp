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
        private void setNewUser(List<User> newUserData)
        {
            ProgramState.userId = newUserData[0].user_Guid;
            ProgramState.roleId = newUserData[0].role_Id;
            foreach (User u in newUserData)
            {
                if(u.role_Id == 1)
                {
                    ProgramState.roleId = 1; //is admin
                }
            }

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

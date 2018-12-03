using AghWeatherApp.Models;
using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace AghWeatherApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Detais";
        }

        public string UserName
        {
            get
            {
                return "user: " + ProgramState.uName;
            }
        }
    }
}
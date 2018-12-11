using System;
using System.Collections.Generic;
using System.Text;

namespace AghWeatherApp.Models
{
    public enum MenuItemType
    {
        Details,
        Main,
        Login,
        UserDetails,
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}

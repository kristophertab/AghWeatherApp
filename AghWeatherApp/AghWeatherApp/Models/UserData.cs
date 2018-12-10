using System;
using System.Collections.Generic;
using System.Text;

namespace AghWeatherApp.Models
{
    public class UserData
    {
        public int user_Guid { set; get; }
        public string username { set; get; }
        public string forename { set; get; }
        public string surname { set; get; }
        public string email { set; get; }        public string password { set; get; }
    }
}

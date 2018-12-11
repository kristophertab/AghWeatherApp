using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AghWeatherApp.Models;

namespace AghWeatherApp.Services
{
    class DataService
    {
        public static string serviceUrl = "http://192.168.43.159:7135/api/temperatures";
        public static string minSufix = "/min";
        public static string maxSufix = "/max";
        public static string avrSufix = "/avg";
        public static string plotSufix = "/week";
        public static string loginSufix = "/user/login";
        public static string userSufix = "/api/user";

        public static string detailedAvgSufix(int deviceNr)
        {
            return (avrSufix + "/" + deviceNr.ToString());
        }

        public static async Task<dynamic> GetDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }

        public static async Task<List<TemperatureDevice>> GetAvredgeFromServer(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            List<TemperatureDevice> data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<TemperatureDevice>>(json);
            }

            return data;
        }

        public static async Task<Weather> GetWeatherDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            //string dev_data = "{\"id\":39844,\"temperature\":100.0,\"date\":\"2017 - 01 - 01T12: 46:09\"}";

            Weather data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<Weather>(json);
            }

            return data;
        }

        public static async Task GetWeatherListFromService(string queryString, Action<List<Weather>> callback)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            //string dev_data = "[{\"id\":1,\"temperature\":74.67759,\"date\":null},{\"id\":2,\"temperature\":74.7920151,\"date\":null},{\"id\":3,\"temperature\":74.855484,\"date\":null},{\"id\":4,\"temperature\":75.76137,\"date\":null},{\"id\":5,\"temperature\":75.48369,\"date\":null},{\"id\":6,\"temperature\":75.29217,\"date\":null},{\"id\":7,\"temperature\":74.86363,\"date\":null},{\"id\":8,\"temperature\":74.88173,\"date\":null},{\"id\":9,\"temperature\":75.42991,\"date\":null},{\"id\":10,\"temperature\":75.56968,\"date\":null},{\"id\":11,\"temperature\":75.11188,\"date\":null},{\"id\":12,\"temperature\":74.23059,\"date\":null},{\"id\":13,\"temperature\":74.8671951,\"date\":null},{\"id\":14,\"temperature\":75.2824554,\"date\":null},{\"id\":15,\"temperature\":75.08726,\"date\":null},{\"id\":16,\"temperature\":74.91914,\"date\":null},{\"id\":17,\"temperature\":75.0432,\"date\":null},{\"id\":18,\"temperature\":75.618576,\"date\":null},{\"id\":19,\"temperature\":74.5906,\"date\":null},{\"id\":20,\"temperature\":75.21954,\"date\":null},{\"id\":21,\"temperature\":74.89144,\"date\":null},{\"id\":22,\"temperature\":74.96822,\"date\":null},{\"id\":23,\"temperature\":75.1072,\"date\":null},{\"id\":24,\"temperature\":75.045,\"date\":null},{\"id\":25,\"temperature\":75.38789,\"date\":null},{\"id\":26,\"temperature\":74.96557,\"date\":null},{\"id\":27,\"temperature\":74.7605,\"date\":null},{\"id\":28,\"temperature\":74.93596,\"date\":null},{\"id\":29,\"temperature\":74.507515,\"date\":null},{\"id\":30,\"temperature\":75.60325,\"date\":null},{\"id\":31,\"temperature\":75.12474,\"date\":null},{\"id\":32,\"temperature\":74.8364944,\"date\":null},{\"id\":33,\"temperature\":74.57091,\"date\":null},{\"id\":34,\"temperature\":75.20253,\"date\":null},{\"id\":35,\"temperature\":74.75013,\"date\":null},{\"id\":36,\"temperature\":75.27002,\"date\":null},{\"id\":37,\"temperature\":75.24355,\"date\":null},{\"id\":38,\"temperature\":75.05457,\"date\":null},{\"id\":39,\"temperature\":74.32671,\"date\":null},{\"id\":40,\"temperature\":75.431,\"date\":null},{\"id\":41,\"temperature\":75.01736,\"date\":null},{\"id\":42,\"temperature\":74.76663,\"date\":null},{\"id\":43,\"temperature\":75.22883,\"date\":null},{\"id\":44,\"temperature\":74.50418,\"date\":null},{\"id\":45,\"temperature\":75.10929,\"date\":null},{\"id\":46,\"temperature\":75.24053,\"date\":null},{\"id\":47,\"temperature\":75.28498,\"date\":null},{\"id\":48,\"temperature\":74.8608,\"date\":null},{\"id\":49,\"temperature\":74.84193,\"date\":null},{\"id\":50,\"temperature\":74.8532,\"date\":null},{\"id\":51,\"temperature\":75.0336456,\"date\":null},{\"id\":52,\"temperature\":75.23706,\"date\":null},{\"id\":53,\"temperature\":74.6377,\"date\":null}]";

            List<Weather> data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<Weather>>(json);
            }

            callback(data);
        }

        public class Weather
        {
            public string Id { get; set; } = " ";
            public string Temperature { get; set; } = " ";
            public string Date { get; set; } = " ";

            public string getWetherString()
            {
                string retStr = " ";
                retStr += " Id :" + this.Id;
                retStr += " Temp :" + this.Temperature;
                retStr += " Date :" + this.Date;

                return retStr;
            }
        }
        public static async Task PostLoginData(string username, string password, Action<User> callback)

        {
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format(ProgramState.apiUrl + loginSufix, string.Empty));
            string jsonData = "{\"UserName\": \"" + username + "\",\"Password\": \"" + password + "\",\"UserAgent\": \"chrome2\"}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            response = await client.PostAsync(uri, content);

            User data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<User>(json);
            }

            callback(data);

        }


        public static async Task GetAllUsers()
        {
            HttpClient client = new HttpClient();
            String queryString = ProgramState.apiUrl + userSufix;
            var response = await client.GetAsync(queryString);

            List<UserData> data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<UserData>>(json);
            }
        }

        public static async Task<UserData> GetUser(int userId)
        {
            HttpClient client = new HttpClient();
            String queryString = ProgramState.apiUrl + userSufix + "/" + userId.ToString();
            var response = await client.GetAsync(queryString);

            UserData data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<UserData>(json);
            }

            return data;
        }

        public static async Task PostUser(UserData userData)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format(ProgramState.apiUrl + userSufix, string.Empty));
            String jsonData = "{ \"user_Guid\":" + userData.user_Guid + ", \"username\":" + userData.username + ", \"forename\":" + userData.forename + ", \"surname\":" + userData.surname + ", \"email\":" + userData.email + ", \"password\":" + userData.password + " }";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            await client.PostAsync(uri, content);
        }

        public static async Task PutUser(UserData userData)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format(ProgramState.apiUrl + userSufix + "/" + userData.user_Guid.ToString() , string.Empty));
            String jsonData = "{ \"user_Guid\":" + userData.user_Guid + ", \"username\":" + userData.username + ", \"forename\":" + userData.forename + ", \"surname\":" + userData.surname + ", \"email\":" + userData.email + ", \"password\":" + userData.password + " }";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            await client.PutAsync(uri, content);
        }

        public static async Task DelateUser(int userId)
        {
            HttpClient client = new HttpClient();
            String queryString = ProgramState.apiUrl + userSufix + "/" + userId.ToString();

            await client.DeleteAsync(queryString);
        }


    }
}

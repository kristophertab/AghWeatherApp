using System;
using Microcharts;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AghWeatherApp.ViewModels;
using AghWeatherApp.Services;
using SkiaSharp;
using System.Threading.Tasks;
using System.Collections.Generic;
using AghWeatherApp.Models;

namespace AghWeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {

        ItemsViewModel viewModel;
        private List<TemperatureDevice> devices;
        private String urlTest = "http://10.10.118.25:5000";
        private String urlProd = "http://192.168.43.195:60869";

        public ItemsPage()
        {
            devices = new List<TemperatureDevice>();
            InitializeComponent();

            this.pathEntry.Text = urlTest;

            BindingContext = viewModel = new ItemsViewModel();

            TemperatureDevice mockDev1 = new TemperatureDevice
            {
                Id = 1,
                Name = "Example1",
                Temperature = 3.14f
            };
            devices.Add(mockDev1);

            TemperatureDevice mockDev2 = new TemperatureDevice()
            {
                Id = 2,
                Name = "Example2",
                Temperature = 2.71f
            };
            devices.Add(mockDev2);

            

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() => refreshFuction());
                return true;
            });
        }

        private void refreshFuction()
        {
            ShowChart();
        }

        protected override void OnAppearing()
        {
            BindingContext = viewModel = new ItemsViewModel();
        }

        private async Task Button_ClickedAsync(object sender, EventArgs e)
        {
            List<TemperatureDevice> serverDevices = null;
            if (!String.IsNullOrEmpty(pathEntry.Text))
            {
                string requestUrl = pathEntry.Text + DataService.avrSufix;

                serverDevices = await DataService.GetAvredgeFromServer(requestUrl);

                devices.Clear();
                foreach (TemperatureDevice serverDevice in serverDevices)
                {
                    devices.Add(serverDevice);
                }

                //ShowChart();

            }
        }

        private void ShowChart()
        {
            List<Microcharts.Entry> chartEntries = new List<Microcharts.Entry>();

            foreach (TemperatureDevice device in devices)
            {
                Microcharts.Entry temp = new Microcharts.Entry(device.Temperature)
                {
                    Label = device.Name,
                    ValueLabel = device.Temperature.ToString(),
                    Color = SKColor.Parse("#22BB22"),
                };

                chartEntries.Add(temp);
            }

            BarChart mockChart = new BarChart() { Entries = chartEntries };

            this.chartDevicesAvr.Chart = mockChart;
        }
    }
}
using Microcharts;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AghWeatherApp.Services;
using System.Threading.Tasks;
using System.Globalization;
using AghWeatherApp.ViewModels;

namespace AghWeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        private String urlTest = "http://10.10.118.25:5000";
        private String urlProd = "http://192.168.43.195:60869";

        private List<TemperatureDevice> devices;

        public AboutPage()
        {
            devices = new List<TemperatureDevice>();

            InitializeComponent();

            BindingContext = new AboutViewModel();

            this.pathEntry.Text = urlTest;

            showMicrochartsDevice(3);
        }

        protected override void OnAppearing()
        {
            BindingContext = new AboutViewModel();
        }

            private async Task Button_ClickedAsync(object sender, EventArgs e)
        {
            List<TemperatureDevice> serverDevices = null;
            if (!String.IsNullOrEmpty(pathEntry.Text))
            {
                string requestUrl = pathEntry.Text + DataService.avrSufix;

                serverDevices = await DataService.GetAvredgeFromServer(requestUrl);

                foreach (TemperatureDevice device in devices)
                {
                    devices.Remove(device);
                }

                foreach (TemperatureDevice serverDevice in serverDevices)
                {
                    devices.Add(serverDevice);
                }
            }

            fillPieckerExample();
        }

        private void showTemperatureOnMicrochart(List<DataService.Weather> temperatures)
        {
            List<Microcharts.Entry> chartData = new List<Microcharts.Entry>();

            if(temperatures == null)
            {
                return;
            }

            foreach (DataService.Weather tmp in temperatures)
            {
                float valueOfTemperature = float.Parse(tmp.Temperature, CultureInfo.InvariantCulture.NumberFormat); 

                chartData.Add(new Microcharts.Entry(valueOfTemperature)
                {
                    Label = tmp.Id,
                    ValueLabel = tmp.Temperature,
                });
            }

            var mockChart = new LineChart()
            {
                Entries = chartData,
                LineMode = LineMode.Straight,
                LineSize = 6,
                
            };

            this.chartView.Chart = mockChart;
        }


        private void showMicrochartsDevice(int deviceNr)
        {
            List<Microcharts.Entry> mockChartData = new List<Microcharts.Entry>();

            mockChartData.Add(new Microcharts.Entry(0));
            mockChartData.Add(new Microcharts.Entry(0));
            mockChartData.Add(new Microcharts.Entry(deviceNr));
            mockChartData.Add(new Microcharts.Entry(deviceNr));
            mockChartData.Add(new Microcharts.Entry(0));
            mockChartData.Add(new Microcharts.Entry(0));

            var mockChart = new LineChart()
            {
                Entries = mockChartData,
                LineMode = LineMode.Spline,
                LineSize = 6,
            };

            this.chartView.Chart = mockChart;
        }

        private void fillPieckerExample()
        {
            foreach(TemperatureDevice device in devices)
            {
                this.devicePicker.Items.Add(device.Name);
            }
        }

        private async Task OnPickerSelectedIndexChangedAsync(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                //showMicrochartsDevice(selectedIndex);

                string requestUrl = pathEntry.Text + DataService.detailedAvgSufix(selectedIndex+1);
                await DataService.GetWeatherListFromService(requestUrl, showTemperatureOnMicrochart);
            }
        }
    }

}
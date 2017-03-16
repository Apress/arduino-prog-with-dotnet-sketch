using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
using Windows.Web.Http;
namespace WiFiLed
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ToggleLed1(object sender, RoutedEventArgs e)
        {
            string svr = txtIP.Text;
            string url = string.Format("http://{0}/gpio1/0", svr);
            int state = 0;

            if(toggleLed1.IsOn)
            {
                url = string.Format("http://{0}/gpio1/1", svr);
                state = 1;
            }

            SendCommand(url);
            UpdateStatus(1, state);
        }

        private void ToggleLed2(object sender, RoutedEventArgs e)
        {
            string svr = txtIP.Text;
            string url = string.Format("http://{0}/gpio2/0", svr);
            int state = 0;

            if (toggleLed2.IsOn)
            {
                url = string.Format("http://{0}/gpio2/1", svr);
                state = 1;
            }

            SendCommand(url);
            UpdateStatus(2, state);
        }

        private void ToggleLed3(object sender, RoutedEventArgs e)
        {
            string svr = txtIP.Text;
            string url = string.Format("http://{0}/gpio3/0", svr);
            int state = 0;

            if (toggleLed3.IsOn)
            {
                url = string.Format("http://{0}/gpio3/1", svr);
                state = 1;
            }

            SendCommand(url);
            UpdateStatus(3, state);
        }
        private void UpdateStatus(int led, int state)
        {
            if (state == 1)
                txtStatus.Text = string.Format("LED {0} is ON", led);
            else
                txtStatus.Text = string.Format("LED {0} is OFF", led);
        }
        private async void SendCommand(string url)
        {
            HttpClient httpClient = new HttpClient();
            var headers = httpClient.DefaultRequestHeaders;

            Uri requestUri = new Uri(url);
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                //Send the GET request                
                httpResponse = await httpClient.GetAsync(requestUri,HttpCompletionOption.ResponseHeadersRead);
                httpResponse.EnsureSuccessStatusCode();                
                         
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);              
            }
            
        }
    }
}

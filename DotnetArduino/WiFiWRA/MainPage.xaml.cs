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
using Microsoft.Maker.Serial;
using Microsoft.Maker.RemoteWiring;
using Windows.Networking;
namespace WiFiWRA
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NetworkSerial connection;
        private RemoteDevice arduino;

        private const byte LED1 = 5;
        private const byte LED2 = 4;
        private const byte LED3 = 3;
        private byte currentLed;
        private int currentState;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ConnectToServer(object sender, RoutedEventArgs e)
        {
            InitWRA();
        }

        private void ToggleLed1(object sender, RoutedEventArgs e)
        {
            currentState = 0;

            if (toggleLed1.IsOn)
                currentState = 1;
            currentLed = 1;


            Execute();
            UpdateStatus(1, currentState);
        }       

        private void ToggleLed2(object sender, RoutedEventArgs e)
        {            
            currentState = 0;

            if (toggleLed2.IsOn)
                currentState = 1;
            currentLed = 2;

            Execute();
            UpdateStatus(2, currentState);
        }

        private void ToggleLed3(object sender, RoutedEventArgs e)
        {
            string svr = txtIP.Text;
            currentState = 0;

            if (toggleLed3.IsOn)
                currentState = 1;
            currentLed = 3;

            Execute();
            UpdateStatus(3, currentState);
        }
        private void UpdateStatus(int led, int state)
        {
            if (state == 1)
                txtStatus.Text = string.Format("LED {0} is ON", led);
            else
                txtStatus.Text = string.Format("LED {0} is OFF", led);
        }
        private void Execute()
        {
            
            if (currentState == 1)
                arduino.digitalWrite(currentLed, PinState.HIGH);
            else
                arduino.digitalWrite(currentLed, PinState.LOW);

            System.Diagnostics.Debug.WriteLine("Written command to Arduino");
        }

        private void InitWRA()
        {
            string svr = txtIP.Text;
            connection = new NetworkSerial(new HostName(svr), 3030);
            arduino = new RemoteDevice(connection);

            arduino.DeviceConnectionLost += Arduino_DeviceConnectionLost;

            connection.ConnectionEstablished += Connection_ConnectionEstablished;
            connection.ConnectionFailed += Connection_ConnectionFailed;

            connection.begin(115200, SerialConfig.SERIAL_8N1);
        }

        private void Arduino_DeviceConnectionLost(string message)
        {
            System.Diagnostics.Debug.WriteLine("Device is connection lost");
        }

        private void Connection_ConnectionFailed(string message)
        {
            txtServerState.Text = "Not connected";
        }

        private void Connection_ConnectionEstablished()
        {
            txtServerState.Text = "Connected";
            System.Diagnostics.Debug.WriteLine("Connected");
            
            arduino.pinMode(LED1, PinMode.OUTPUT);
            arduino.pinMode(LED2, PinMode.OUTPUT);
            arduino.pinMode(LED3, PinMode.OUTPUT);            

        }

        
    }
}

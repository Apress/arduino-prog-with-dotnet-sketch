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
namespace BlinkingLed
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private UsbSerial connection;
        private RemoteDevice arduino;
        private const byte LED1 = 12;
        private const byte LED2 = 11;
        private const byte LED3 = 10;

        public MainPage()
        {
            this.InitializeComponent();
            this.Unloaded += MainPage_Unloaded;

            InitWRA();
        }
        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            arduino.Dispose();
        }

        private void InitWRA()
        {
            connection = new UsbSerial("VID_2341", "PID_0043");
            arduino = new RemoteDevice(connection);

            connection.ConnectionEstablished += Connection_ConnectionEstablished;
            connection.begin(57600, SerialConfig.SERIAL_8N1);

        }

        private void Connection_ConnectionEstablished()
        {
            arduino.pinMode(LED1, PinMode.OUTPUT);
            arduino.pinMode(LED2, PinMode.OUTPUT);
            arduino.pinMode(LED3, PinMode.OUTPUT);

            txtStatus.Text = "Connected";
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Name == "chkLed1")
            {
                arduino.digitalWrite(LED1, PinState.HIGH);
            }
            if (cb.Name == "chkLed2")
            {
                arduino.digitalWrite(LED2, PinState.HIGH);
            }
            if (cb.Name == "chkLed3")
            {
                arduino.digitalWrite(LED3, PinState.HIGH);
            }

        }

        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Name == "chkLed1")
            {
                arduino.digitalWrite(LED1, PinState.LOW);
            }
            if (cb.Name == "chkLed2")
            {
                arduino.digitalWrite(LED2, PinState.LOW);
            }
            if (cb.Name == "chkLed3")
            {
                arduino.digitalWrite(LED3, PinState.LOW);
            }
        }

        private void TurnOffLeds(object sender, RoutedEventArgs e)
        {
            chkLed1.IsChecked = false;
            chkLed2.IsChecked = false;
            chkLed3.IsChecked = false;
        }
    }
}

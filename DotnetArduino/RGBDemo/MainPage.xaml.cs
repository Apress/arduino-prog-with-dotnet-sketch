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

using Windows.UI;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
using Microsoft.Maker.Serial;
using Microsoft.Maker.RemoteWiring;
namespace RGBDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private UsbSerial connection;
        private RemoteDevice arduino;
        private const byte RED = 9;
        private const byte GREEN = 10;
        private const byte BLUE = 11;
        private byte red;
        private byte green;
        private byte blue;

        public MainPage()
        {
            this.InitializeComponent();
            red = 0;
            green = 0;
            blue = 0;

            this.Unloaded += MainPage_Unloaded;
            InitWRA();
        }
        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {            
            arduino.Dispose();
        }

        private void ChangeGreenColor(object sender, RangeBaseValueChangedEventArgs e)
        {
            red = (byte)redSlider.Value;
            UpdatePanel();
        }

        private void ChangeRedColor(object sender, RangeBaseValueChangedEventArgs e)
        {
            green = (byte)greenSlider.Value ;
            UpdatePanel();

        }

        private void ChangeBlueColor(object sender, RangeBaseValueChangedEventArgs e)
        {
            blue = (byte)greenSlider.Value;
            UpdatePanel();
        }

        private void ChangeColor(object sender, RoutedEventArgs e)
        {
            // change color on RGB LED
            arduino.analogWrite(RED, red);
            arduino.analogWrite(GREEN, green);
            arduino.analogWrite(BLUE, blue);
        }

        private void UpdatePanel()
        {
            cvColorView.Background = new SolidColorBrush(Color.FromArgb(255, (byte)red, (byte)green, (byte)blue));
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
            System.Diagnostics.Debug.WriteLine("Connected");

            arduino.pinMode(RED, PinMode.PWM);
            arduino.pinMode(GREEN, PinMode.PWM);
            arduino.pinMode(BLUE, PinMode.PWM);
           
        }
    }
}

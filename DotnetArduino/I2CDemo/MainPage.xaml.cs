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
using System.Text;
using Windows.ApplicationModel.Core;
using Microsoft.Maker.Serial;
using Microsoft.Maker.RemoteWiring;
namespace I2CDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private UsbSerial connection;
        private RemoteDevice arduino;
        private const byte NUM_DIGITAL_PINS = 14; // Arduino Uno
        private const byte SDA = 4;
        private const byte SCL = 5;
        private const byte PCF8591 = (0x90 >> 1);  // Device address        
        private const byte PCF8591_ADC_CH0 = 0x40; // thermistor 
        private const byte PCF8591_ADC_CH1 = 0x41; // photo-voltaic cell
        private const byte PCF8591_ADC_CH2 = 0x42;
        private const byte PCF8591_ADC_CH3 = 0x43; // potentiometer
        private int index = 0;
        private Queue<int> i2cReading = new Queue<int>();
        private bool isReading;

        private DispatcherTimer timer;
        public MainPage()
        {
            this.InitializeComponent();
            this.Unloaded += MainPage_Unloaded;

            InitWRA();
        }
        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            connection.end();
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
            System.Diagnostics.Debug.WriteLine("Connected");
            arduino.pinMode(NUM_DIGITAL_PINS + SDA, PinMode.I2C);
            arduino.pinMode(NUM_DIGITAL_PINS + SCL, PinMode.I2C);

            index = 0;
                       
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(2000);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

      
        private void I2c_I2cReplyEvent(byte address_, byte reg_, Windows.Storage.Streams.DataReader response)
        {
            byte[] data = new byte[2];
            response.ReadBytes(data);           
            //byte[] data = Encoding.UTF8.GetBytes(response);
            int curr = i2cReading.Dequeue();
            UpdateData(curr, data[1]);
            System.Diagnostics.Debug.WriteLine("" + Convert.ToString(address_) + "-" + curr.ToString() + ": " + BitConverter.ToString(data));
            isReading = false;
        }

        private void Timer_Tick(object sender, object e)
        {
            if (isReading)
                return;

            isReading = true;
            switch (index)
            {
                case 0:
                    i2cReading.Enqueue(index);
                    System.Diagnostics.Debug.WriteLine("PCF8591_ADC_CH0");
                    ReadADC(PCF8591_ADC_CH0);
                    break;
                case 1:
                    i2cReading.Enqueue(index);
                    System.Diagnostics.Debug.WriteLine("PCF8591_ADC_CH1");
                    ReadADC(PCF8591_ADC_CH1);
                    break;
                case 2:
                    i2cReading.Enqueue(index);
                    System.Diagnostics.Debug.WriteLine("PCF8591_ADC_CH2");
                    ReadADC(PCF8591_ADC_CH2);
                    break;

            }
            index++;
            if (index > 2)
                index = 0;


        }

        void ReadADC(byte config)
        {
            
            arduino.I2c.enable();
            arduino.I2c.I2cReplyEvent += I2c_I2cReplyEvent;

            arduino.I2c.beginTransmission(PCF8591);
            arduino.I2c.write(config);
            arduino.I2c.endTransmission();

            arduino.I2c.requestFrom(PCF8591, 2);
            
        }

        private async void UpdateData(int index, byte value)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    () =>
                    {
                        switch (index)
                        {
                            case 0:
                                txtThermistor.Text = Convert.ToString(value);
                                break;
                            case 1:
                                txtPhoto.Text = Convert.ToString(value);
                                break;
                            case 2:
                                txtPot.Text = Convert.ToString(value);
                                break;
                        }
                    });
        }
    }
}

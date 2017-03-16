using System;
using System.Drawing;
using System.Windows.Forms;

using Solid.Arduino;
using Solid.Arduino.Firmata;
namespace RGBControlApp
{
    public partial class Form1 : Form
    {
        private int redVal;
        private int greenVal;
        private int blueVal;
        public Form1()
        {
            InitializeComponent();

            redVal = trackRed.Value;
            greenVal = trackGreen.Value;
            blueVal = trackBlue.Value;

        }

        private void trackRed_ValueChanged(object sender, EventArgs e)
        {
            redVal = trackRed.Value;
            lbRed.Text = redVal.ToString();
            UpdateColorPanel();
        }

        private void trackGreen_ValueChanged(object sender, EventArgs e)
        {
            greenVal = trackGreen.Value;
            lbGreen.Text = greenVal.ToString();
            UpdateColorPanel();
        }

        private void trackBlue_ValueChanged(object sender, EventArgs e)
        {
            blueVal = trackBlue.Value;
            lbBlue.Text = blueVal.ToString();
            UpdateColorPanel();
        }

        private void UpdateColorPanel()
        {
            panelColor.BackColor = Color.FromArgb(redVal, greenVal, blueVal);

        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                var connection = new SerialConnection("COM5", SerialBaudRate.Bps_57600);
                var session = new ArduinoSession(connection, timeOut: 250);

                IFirmataProtocol firmata = (IFirmataProtocol)session;

                int redPin = 9;
                int greenPin = 10;
                int bluePin = 11;

                firmata.SetDigitalPinMode(redPin, PinMode.PwmOutput);
                firmata.SetDigitalPinMode(greenPin, PinMode.PwmOutput);
                firmata.SetDigitalPinMode(bluePin, PinMode.PwmOutput);

                firmata.SetDigitalPin(redPin, redVal);
                firmata.SetDigitalPin(greenPin, greenVal);
                firmata.SetDigitalPin(bluePin, blueVal);

                connection.Close();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }
    }
}

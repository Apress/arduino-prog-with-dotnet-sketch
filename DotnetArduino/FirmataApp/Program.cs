using System;
using System.Threading;
using Solid.Arduino;
using Solid.Arduino.Firmata;

namespace FirmataApp
{
    class Program
    {
        static bool running = true;
        static void Main(string[] args)
        {

            try
            {
                var connection = new SerialConnection("COM5", SerialBaudRate.Bps_57600);
                var session = new ArduinoSession(connection, timeOut: 250);

                Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
                {
                    e.Cancel = true;
                    running = false;
                };

                IFirmataProtocol firmata = (IFirmataProtocol)session;

                int led1 = 12;
                int led2 = 11;
                int led3 = 10;

                firmata.SetDigitalPinMode(led1, PinMode.DigitalOutput);
                firmata.SetDigitalPinMode(led2, PinMode.DigitalOutput);
                firmata.SetDigitalPinMode(led3, PinMode.DigitalOutput);

                while (running)
                {
                    // led 1
                    Console.WriteLine("Turn on LED 1");
                    firmata.SetDigitalPin(led1, true);
                    firmata.SetDigitalPin(led2, false);
                    firmata.SetDigitalPin(led3, false);
                    Thread.Sleep(1000); // sleep

                    // led 2
                    Console.WriteLine("Turn on LED 2");
                    firmata.SetDigitalPin(led1, false);
                    firmata.SetDigitalPin(led2, true);
                    firmata.SetDigitalPin(led3, false);
                    Thread.Sleep(1000);

                    // led 3
                    Console.WriteLine("Turn on LED 3");
                    firmata.SetDigitalPin(led1, false);
                    firmata.SetDigitalPin(led2, false);
                    firmata.SetDigitalPin(led3, true);
                    Thread.Sleep(1000);
                }
                // turn off LEDs
                firmata.SetDigitalPin(led1, false);
                firmata.SetDigitalPin(led2, false);
                firmata.SetDigitalPin(led3, false);

                connection.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err);

            }
            Console.WriteLine("Program exit. Press ENTER to close.");
            Console.ReadLine();            


        }
    }
}

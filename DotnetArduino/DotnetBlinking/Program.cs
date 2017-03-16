using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace DotnetBlinking
{
    class Program
    {
        static bool running = true;
        static void Main(string[] args)
        {
            try
            {
                SerialPort serialPort = new SerialPort("COM5");
                serialPort.BaudRate = 9600;

                Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
                {
                    e.Cancel = true;
                    running = false;
                };

                serialPort.Open();
                Console.WriteLine("Press 1, 2, 3 to turn on LED 1, 2, 3 and then press ENTER");
                Console.WriteLine("Press CTRL+C to exit");
                while (running)
                {
                    Console.Write(">> ");
                    string data = Console.ReadLine();
                    if(!string.IsNullOrEmpty(data))
                    {
                        serialPort.Write(data);
                        Console.WriteLine("Sent to Arduino: {0}", data);
                    }                    
                }
                serialPort.Close();
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

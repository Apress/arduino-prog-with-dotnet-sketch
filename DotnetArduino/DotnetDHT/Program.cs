using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

using Newtonsoft.Json;
namespace DotnetDHT
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
                while (running)
                {
                    string data = serialPort.ReadLine();
                    if (!string.IsNullOrEmpty(data))
                    {
                        Console.WriteLine(data);      
                        if(data.Contains("Humidity") && data.Contains("Temperature"))
                        {
                            Sensor s = JsonConvert.DeserializeObject<Sensor>(data);

                            Console.WriteLine("Temperature: {0} ^C", s.Temperature);
                            Console.WriteLine("Humidity: {0}", s.Humidity);
                        }
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

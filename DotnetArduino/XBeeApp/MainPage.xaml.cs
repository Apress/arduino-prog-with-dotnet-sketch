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
using System.Collections.ObjectModel;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;
using System.Threading;
using System.Threading.Tasks;
namespace XBeeApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SerialDevice serialPort = null;
        private DataReader dataReaderObject = null;
        private ObservableCollection<DeviceInformation> listUART;
        private CancellationTokenSource ReadCancellationTokenSource;

        public MainPage()
        {
            this.InitializeComponent();
            listUART = new ObservableCollection<DeviceInformation>();
            GetListOfXBee();            
        }

        private async void GetListOfXBee()
        {
            try
            {
                string aqs = SerialDevice.GetDeviceSelector();
                var dis = await DeviceInformation.FindAllAsync(aqs);
                
                for (int i = 0; i < dis.Count; i++)
                {
                    listUART.Add(dis[i]);
                }
                listXBee.ItemsSource = listUART;                
                listXBee.SelectedIndex = -1;

                btnConnect.IsEnabled = true;
                btnDisconnect.IsEnabled = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


        private async void ConnectToXBee(object sender, RoutedEventArgs e)
        {
            var selection = listXBee.SelectedItems;

            if (selection.Count <= 0)
            {
                txtState.Text = "Select a Serial Port!";
                return;
            }

            DeviceInformation entry = (DeviceInformation)selection[0];

            try
            {
                serialPort = await SerialDevice.FromIdAsync(entry.Id);
                
                // Configure serial settings
                serialPort.WriteTimeout = TimeSpan.FromMilliseconds(1000);
                serialPort.ReadTimeout = TimeSpan.FromMilliseconds(1000);
                serialPort.BaudRate = 9600;
                serialPort.Parity = SerialParity.None;
                serialPort.StopBits = SerialStopBitCount.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = SerialHandshake.None;

                // Create cancellation token
                ReadCancellationTokenSource = new CancellationTokenSource();

                btnConnect.IsEnabled = false;
                btnDisconnect.IsEnabled = true;
                txtState.Text = "Connected";
                ReadDataXBee();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                btnConnect.IsEnabled = true;
            }

        }

        private void Disconnect(object sender, RoutedEventArgs e)
        {
            CloseXBee();
            txtState.Text = "Not connected";
        }
        private void CloseXBee()
        {
            try
            {
                CancelReadTask();
            }
            catch (Exception){}

            if (serialPort != null)
            {
                serialPort.Dispose();
            }
            serialPort = null;
            
            btnConnect.IsEnabled = true;
            btnDisconnect.IsEnabled = false;
        }

        private async void ReadDataXBee()
        {
            try
            {
                if (serialPort != null)
                {
                    dataReaderObject = new DataReader(serialPort.InputStream);

                    // keep reading the serial input
                    while (true)
                    {
                        await ReadXBeeAsync(ReadCancellationTokenSource.Token);
                    }
                }
            }
            catch (Exception ex)
            {
                CloseXBee();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                // Cleanup once complete
                if (dataReaderObject != null)
                {
                    dataReaderObject.DetachStream();
                    dataReaderObject = null;
                }
            }
        }
        private void CancelReadTask()
        {
            if (ReadCancellationTokenSource != null)
            {
                if (!ReadCancellationTokenSource.IsCancellationRequested)
                {
                    ReadCancellationTokenSource.Cancel();
                }
            }
        }

        private async Task ReadXBeeAsync(CancellationToken cancellationToken)
        {
            Task<UInt32> loadAsyncTask;

            uint ReadBufferLength = 1024;

            // If task cancellation was requested, comply
            cancellationToken.ThrowIfCancellationRequested();
            dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;
            loadAsyncTask = dataReaderObject.LoadAsync(ReadBufferLength).AsTask(cancellationToken);

            UInt32 bytesRead = await loadAsyncTask;
            if (bytesRead > 0)
            {
                string txt = dataReaderObject.ReadString(bytesRead);
                if (txt.IndexOf('\r') != -1)
                {
                    string val = txt.Replace('\r','\0');
                    val = txt.Replace('\n', '\0');
                    txtValue.Text = val;
                }                
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientList_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Socket Socket { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var ipAddress = IPAddress.Parse("192.168.0.106");
            var port = 22003;

            Task.Run(() =>
            {
                var endPoint = new IPEndPoint(ipAddress, port);

                try
                {
                    Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    Socket.Connect(endPoint);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string name = UsernameTextBox.Text;
            byte[] data = Encoding.ASCII.GetBytes(name);

            Socket.Send(data);

            Socket.Shutdown(SocketShutdown.Both);
            Socket.Close();
            Close();
        }
    }
}

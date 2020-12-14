using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UdpPractice
{
    class ProgramUDP
    {
        static UdpClient udpClient;
        static IPEndPoint targetEndPoint;

        public static void Main(string[] args)
        {
            udpClient = new UdpClient(new IPEndPoint(IPAddress.Loopback, 0)); // 127.0.0.1
            Console.WriteLine("Socket bound to: " + udpClient.Client.LocalEndPoint.ToString());

            Console.WriteLine("Write target address: ");
            string targetAddress = Console.ReadLine();

            CreateIPEndPoint(targetAddress);
            targetEndPoint = CreateIPEndPoint(targetAddress);

            Console.WriteLine("Endpoint set to: " + targetEndPoint.ToString());

            //Start asyncronous thread to receive data
            Task.Run(AsyncReceiveData);

            while (true)
            {
                string message = Console.ReadLine();
                var messageBinary = Encoding.ASCII.GetBytes(message);
                udpClient.Send(messageBinary, messageBinary.Length, targetEndPoint);
            }
        }

        public static IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length != 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (!IPAddress.TryParse(ep[0], out ip))
            {
                throw new FormatException("Invalid ip-adress");
            }
            int port;
            if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }
            return new IPEndPoint(ip, port);
        }

        private static async void AsyncReceiveData()
        {
            while (true)
            {
                UdpReceiveResult result = await udpClient.ReceiveAsync();
                Console.WriteLine(result.RemoteEndPoint.ToString() + ": " + Encoding.ASCII.GetString(result.Buffer));
            }
        }



    }
}

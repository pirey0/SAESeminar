using System;
using System.Collections.Generic;
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

        public static void MainUDP(string[] args)
        {
            udpClient = new UdpClient(new IPEndPoint(IPAddress.Loopback, 0));
            Console.WriteLine("Socket bound to: " + udpClient.Client.LocalEndPoint.ToString());

            Console.WriteLine("Write target local port: ");
            string targetPort = Console.ReadLine();
            
            if(int.TryParse(targetPort, out int result))
            {
                targetEndPoint = new IPEndPoint(IPAddress.Loopback, result);
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
            else
            {
                Console.WriteLine("Failed parsing port.");
                Console.ReadLine();
            }
        }

        private static async void AsyncReceiveData()
        {
            while(true)
            {
                UdpReceiveResult result = await udpClient.ReceiveAsync();
                Console.WriteLine(result.RemoteEndPoint.ToString() +": " + Encoding.ASCII.GetString(result.Buffer));
            }
        }


        
    }
}

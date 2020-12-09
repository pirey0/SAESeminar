using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpPractice
{
    class ProgramTCP
    {

        public static void MainTCP(string[] args)
        {
            Console.WriteLine("Start as TCP Server? (YES/NO)");

            string r = Console.ReadLine();

            if (r.ToLowerInvariant() == "YES".ToLowerInvariant())
            {
                Console.WriteLine("STarting as TCP Server");
                TCPServerRoutine();
            }
            else
            {
                Console.WriteLine("Starting as TCP Client");
                TCPClientRoutine();
            }
        }

        private static void TCPServerRoutine()
        {
            var server = new TcpListener(new IPEndPoint(IPAddress.Loopback, 62222));

            Console.WriteLine("Server is at " + server.LocalEndpoint.ToString());
            server.Start();

            while (true)
            {
                Console.WriteLine("Waiting for connection...");

                TcpClient newClient = server.AcceptTcpClient(); //pauses thread untill client joins
                Console.WriteLine("Connected to: " + newClient.Client.RemoteEndPoint);

                Task.Run(() => AsyncReceiveData(newClient));
                Task.Run(() => AsyncSendData(newClient));
            }
        }

        static void AsyncSendData(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            while (true)
            {
                string line = Console.ReadLine();

                if (line.Length > 0)
                {
                    byte[] messageBytes = Encoding.ASCII.GetBytes(line);
                    stream.Write(messageBytes, 0, messageBytes.Length);
                }
            }
        }

        private static async void AsyncReceiveData(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] bytes = new byte[1024];

            while (true)
            {
                int bytesRead = await stream.ReadAsync(bytes, 0, bytes.Length);

                if(bytesRead > 0)
                {
                    string message = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);

                    Console.WriteLine(client.Client.RemoteEndPoint.ToString() + ": " + message);
                }
            }
        }

        private static void TCPClientRoutine()
        {
            var tcpClient = new TcpClient(new IPEndPoint(IPAddress.Loopback, 0));
            Console.WriteLine("Socket bound to: " + tcpClient.Client.LocalEndPoint.ToString());

            Console.WriteLine("Write target local port: ");
            string targetPort = Console.ReadLine();

            if (int.TryParse(targetPort, out int result))
            {
                var targetEndPoint = new IPEndPoint(IPAddress.Loopback, result);
                Console.WriteLine("Endpoint set to: " + targetEndPoint.ToString());
                tcpClient.Connect(targetEndPoint);

                Task.Run(() => AsyncReceiveData(tcpClient));
                Task.Run(() => AsyncSendData(tcpClient));

                Thread.Sleep(100000);//Keep the thread alive
            }
            else
            {
                Console.WriteLine("Failed parsing port.");
                Console.ReadLine();
            }
        }

    }
}

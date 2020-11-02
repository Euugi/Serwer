using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Serwer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");
            TcpListener serwer = new TcpListener(address, 2048);
            serwer.Start();
            System.Console.WriteLine("Serwer zostal uruchomiony na porcie 2048");
            while (true)
            {
                System.Console.WriteLine("Oczekiwanie na polaczenie...");
                TcpClient client = serwer.AcceptTcpClient();
                System.Console.WriteLine("Polaczono");
                NetworkStream stream = client.GetStream();

                int i;
                Byte[] bytes = new Byte[256];
                String data = null;
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Odebrano: {0}", data);
                    Console.WriteLine("\n");
                    String coded_message = Biblioteka_klas.classLibrary.Encryption(data);
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(coded_message);
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Wyslano: {0}", coded_message);
                }
            }
        }
    }
}

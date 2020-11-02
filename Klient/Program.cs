using System;
using System.Net;
using System.Net.Sockets;

namespace Klient
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.Write("Podaj adres serwera: ");
            IPAddress address = IPAddress.Parse(System.Console.ReadLine());
            System.Console.Write("Podaj port serwera: ");
            Int32 port = Int32.Parse(System.Console.ReadLine());
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(address, port));
            Console.WriteLine("Polaczono z serwerem");
            String message = "";
            Console.Write("Jesli chcesz zakonczyc polaczenie wpisz 'exit' \n");
            while (message != "exit")
            {
                Console.Write("Podaj co chcesz wyslac na serwer: ");
                message = Console.ReadLine();
                if (message == "exit")
                {
                    break;
                }
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Wyslano: {0}", message);
                data = new Byte[256];
                String responseData = String.Empty;
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Odebrano zakodowaną informację: {0}", responseData);

                String decoded_message = Biblioteka_klas.classLibrary.Decryption(responseData);
                Console.WriteLine("Odkodowana informacja to: {0}", decoded_message);
            }
            Console.Write("Polaczenie zostanie zakonczone za 2 sekundy");
            System.Threading.Thread.Sleep(2000);
            client.Close();
        }
    }
}

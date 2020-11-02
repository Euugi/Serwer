using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ClassLibraryLab2
{
    public class ServerAPM : Serwer
    {
        private static byte[] buffer = new byte[256];
        public delegate void TransmissionDataDelegate(NetworkStream stream);

        public override void Start()
        {
            StartListening();
            AcceptClient();
        }

        protected override void AcceptClient()
        {
            while (true)
            {
                TcpClient tcpClient = Listener.AcceptTcpClient();

                Stream = tcpClient.GetStream();

                TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);
                transmissionDelegate.BeginInvoke(Stream, TransmissionCallback, tcpClient);

            }
        }

        public static void WriteCallback(IAsyncResult ar)
        {
            NetworkStream s = (NetworkStream)ar.AsyncState;

            s.EndWrite(ar);
            s.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallback), s);
        }
        protected override void BeginDataTransmission(NetworkStream stream)
        {
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallback), stream);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            NetworkStream s = (NetworkStream)ar.AsyncState;
            string w;
            int n = s.EndRead(ar);
            w = Encoding.ASCII.GetString(buffer, 0, n);
            w = w.ToUpper();
            byte[] x = Encoding.ASCII.GetBytes(w);
            s.BeginWrite(x, 0, n, new AsyncCallback(WriteCallback), s);

        }
    }
}
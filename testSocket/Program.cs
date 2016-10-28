using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace testSocket
{
    class Program
    {
        public static void StartClient()
        {
            byte[] bytes = new byte[1024];
            try
            {
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEp = new IPEndPoint(ipAddress, 12345);
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    sender.Connect(remoteEp);
                    byte[] msg = Encoding.ASCII.GetBytes("INIT mode=SimpleMode idP=21 ");
                    int bytesSent = sender.Send(msg);
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("receiv : " + Encoding.ASCII.GetString(bytes,0,bytesRec) +" "+bytesRec);
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        

        static void Main(string[] args)
        {
            StartClient();
            Console.ReadKey();
        }
    }
}

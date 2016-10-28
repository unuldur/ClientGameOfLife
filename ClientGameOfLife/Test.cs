using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientGameOfLife
{
    public class Test
    {
        public static void StartClient()
        {
            byte[] bytes = new byte[1024];
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEp = new IPEndPoint(ipAddress,12345);

                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    sender.Connect(remoteEp);
                    byte[] msg = Encoding.ASCII.GetBytes("INIT mode=SimpleMode idP=21");
                    Console.WriteLine(msg+" truc");
                    int bytesSent = sender.Send(msg);
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("receiv : "+Encoding.ASCII.GetString(bytes));
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                catch (Exception e)
                {
                    
                }
            }
            catch (Exception e)
            {

            }
        }

    }
}

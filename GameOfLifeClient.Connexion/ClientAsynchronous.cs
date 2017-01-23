using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameOfLifeClient.Connexion.Events;

namespace GameOfLifeClient.Connexion
{
    public class ClientAsynchronous : IClient
    {
        private const int Port = 8561;
        private const string Adress = "localhost";
        private Socket _client;
        public event EventHandler<NewMessageEvent> NewMessage;

        private static readonly ManualResetEvent ConnectDone =
            new ManualResetEvent(false);
        private static readonly ManualResetEvent SendDone =
            new ManualResetEvent(false);
        

        public ClientAsynchronous()
        {
            Connect();
        }

        public void Connect()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 12345);

            _client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            _client.BeginConnect(remoteEP,
                    ConnectCallback, _client);
            ConnectDone.WaitOne();
            Debug.WriteLine("Connecter!");
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket) ar.AsyncState;
                client.EndConnect(ar);
                ConnectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Send(string msg)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(msg);
            
            _client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), _client);
            SendDone.WaitOne();
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                SendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public string Receive()
        {
            StateObject state = new StateObject();
            state.workSocket = _client;
            _client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReceiveCallback), state);
            return "";

        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {

                StateObject state = (StateObject) ar.AsyncState;
                Socket client = state.workSocket;
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    if (state.sb.Length > 1 && bytesRead < 1023)
                    {
                        NewMessage?.Invoke(this,new NewMessageEvent{Message = state.sb.ToString()});
                        state.sb.Clear();
                    }
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

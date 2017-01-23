using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeClient.Connexion
{
    public interface IClient
    {
        void Connect();
        void Send(string msg);
        string Receive();
    }
}

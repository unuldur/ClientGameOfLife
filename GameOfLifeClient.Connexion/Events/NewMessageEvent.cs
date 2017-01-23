using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeClient.Connexion.Events
{
    public class NewMessageEvent : EventArgs
    {
        public String Message { get; set; }
    }
}

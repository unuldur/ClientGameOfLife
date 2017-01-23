using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeClient.Connexion.Events
{
    public class DeletePlayerEvent : EventArgs
    {
        public int IdPlayer { get; private set; }

        public DeletePlayerEvent(int idPlayer)
        {
            IdPlayer = idPlayer;
        }
    }
}

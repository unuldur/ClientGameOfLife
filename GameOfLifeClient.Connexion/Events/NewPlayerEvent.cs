using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Connexion.Events
{
    public class NewPlayerEvent
    {
        public Player Player { get; set; }
    }
}

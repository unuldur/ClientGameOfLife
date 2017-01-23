using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLiFeClient.Modele
{
    public class Player
    {
        public Player(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public String Name { get; private set; }
        public int Id { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLiFeClient.Modele
{
    public class CurrentGameMode
    {
        public string Name { get; private set; }
        public int NbJoueur { get; private set; }
        public int NbJoueurMax { get; private set; }
        public int Id { get; private set; }

        public CurrentGameMode(int id,string name, int nbJoueur, int nbJoueurMax)
        {
            Id = id;
            Name = name;
            NbJoueur = nbJoueur;
            NbJoueurMax = nbJoueurMax;
        }

        public CurrentGameMode(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

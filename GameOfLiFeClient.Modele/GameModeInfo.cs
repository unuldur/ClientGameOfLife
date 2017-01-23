using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeClient.Module.Menus.Models
{
    public class GameModeInfo
    {
        public string Name { get; private set; }
        public string Text { get; private set; }

        public GameModeInfo(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Module.Menus.Models;
using Prism.Events;

namespace GameOfLifeClient.Module.Menus.Events
{
    class GameModeSelectedEvent : PubSubEvent<String>
    {
    }
}

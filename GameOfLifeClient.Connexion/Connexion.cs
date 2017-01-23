using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Configuration;
using System.Threading;
using GameOfLifeClient.Connexion.Events;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Connexion
{
    public class Connexion : IConnexion
    {
        private readonly ClientAsynchronous _client;
        private bool _error;
        private int _idGameMode;
        private int _idPlayer;
        private int _idPlayerDelete;
        private bool _isReady;
        private List<CurrentGameMode> _gameModes = new List<CurrentGameMode>();
        private List<Player> _players = new List<Player>();
        private List<Cell> _cells = new List<Cell>();
        private List<Cell> _zone = new List<Cell>();
        private static readonly ManualResetEvent ReceiveDone = new ManualResetEvent(false);


        public Connexion()
        {
            _client = new ClientAsynchronous();
            _client.NewMessage += ManageMessage;
            _client.Receive();
        }
        
        private void ManageMessage(Object sender, NewMessageEvent newMessage)
        {
            if (newMessage.Message == "ERROR")
            {
                _error = true;
                ReceiveDone.Set();
            }
            string[] decompostion = newMessage.Message.Split(' ');
            string type = decompostion[0];
            switch (type)
            {
                case "GAMEMODE":
                    _idGameMode = int.Parse(decompostion[1].Split('=')[1]);
                    ReceiveDone.Set();
                    break;
                case "PLAYER":
                    _idPlayer = int.Parse(decompostion[1].Split('=')[1]);
                    ReceiveDone.Set();
                    break;
                case "GAMES":
                    _gameModes = new List<CurrentGameMode>();
                    foreach (var info in decompostion)
                    {
                        if(info == "GAMES") continue;
                        if (info == "") continue;
                        _gameModes.Add(DecryptGame(info));
                    }
                    ReceiveDone.Set();
                    break;
                case "NEWPLAYER":
                    bool ready = bool.Parse(decompostion[1].Split('=')[1]);
                    if (ready)
                    {
                        StartGame?.Invoke(this, new StartGameEvent());
                    }
                    else
                    {
                        NewPlayer?.Invoke(this, new NewPlayerEvent {Player = DecryptPlayer(decompostion[2])});
                    }
                    break;
                case "ADDPLAYER":
                    _isReady = bool.Parse(decompostion[1].Split('=')[1]);
                    ReceiveDone.Set();
                    break;
                case "PLAYERSGAME":
                    _players = new List<Player>();
                    foreach (var info in decompostion)
                    {
                        if (info == "PLAYERSGAME") continue;
                        if (info == "") continue;
                        _players.Add(DecryptPlayer(info));
                    }
                    ReceiveDone.Set();
                    break;
                case "ERRORGAME":
                    ErrorGames?.Invoke(this,new ErrorGamesEvent());
                    break;
                case "DELPLAYER":
                    DeletePLayer?.Invoke(this, new DeletePlayerEvent(int.Parse(decompostion[1].Split('=')[1])));
                    break;
                case "SWITCH":
                    SwitchCell?.Invoke(this, new SwitchCellEvent(int.Parse(decompostion[1].Split('=')[1]), int.Parse(decompostion[2].Split('=')[1]), int.Parse(decompostion[3].Split('=')[1])));
                    break;
                case "STARTGEN":
                    StartGenEv?.Invoke(this,new StartGenEvent());
                    break;
                case "DIFFGEN":
                    _cells = new List<Cell>();
                    if (decompostion[1] == "FIN")
                    {
                        _cells = null;
                        ReceiveDone.Set();
                        break;
                    }
                    foreach (var info in decompostion)
                    {
                        if (info == "DIFFGEN") continue;
                        if (info == "") continue;
                        _cells.Add(DecryptCell(info));
                    }
                    ReceiveDone.Set();
                    break;
                case "GETZONE":
                    _zone = new List<Cell>();
                    foreach (var info in decompostion)
                    {
                        if (info == "DIFFGEN") continue;
                        if (info == "") continue;
                        _zone.Add(DecryptCell(info));
                    }
                    ReceiveDone.Set();
                    break;
            }
        }

        private static Cell DecryptCell(string cell)
        {
            string[] attributsP = cell.Split(',');
            int x = 0, y=0, idP=0;
            foreach (var attribut in attributsP)
            {
                string[] nameValue = attribut.Split('=');
                switch (nameValue[0])
                {
                    case "x":
                        x = int.Parse(nameValue[1]);
                        break;
                    case "y":
                        y = int.Parse(nameValue[1]);
                        break;
                    case "idP":
                        idP = int.Parse(nameValue[1]);
                        break;
                }
            }
            return new Cell(x,y,idP);
        }

        private static Player DecryptPlayer(string player)
        {
            string nameP = "", idP = "";
            string[] attributsP = player.Split(',');
            foreach (var attribut in attributsP)
            {
                string[] nameValue = attribut.Split('=');
                switch (nameValue[0])
                {
                    case "id":
                        idP = nameValue[1];
                        break;
                    case "name":
                        nameP = nameValue[1];
                        break;
                }
            }
            return new Player(nameP, int.Parse(idP));
        }

        private static CurrentGameMode DecryptGame(string game)
        {
            string name = "", nbJoueur = "", nbJoueurMax = "", id = "";
            string[] attributs = game.Split(',');
            foreach (var attribut in attributs)
            {
                string[] nameValue = attribut.Split('=');
                switch (nameValue[0])
                {
                    case "id":
                        id = nameValue[1];
                        break;
                    case "name":
                        name = nameValue[1];
                        break;
                    case "nbJoueur":
                        nbJoueur = nameValue[1];
                        break;
                    case "nbJoueurMax":
                        nbJoueurMax = nameValue[1];
                        break;
                }
            }
            return new CurrentGameMode(int.Parse(id), name, int.Parse(nbJoueur), int.Parse(nbJoueurMax));
        }

        public int InitGameMode(string gameModeName)
        {
            _client.Send("INIT mode="+gameModeName+" \0");
            ReceiveDone.WaitOne();
            if (_error)
            {
                _error = false;
                throw new Exception("ERROR connexion");
            }
            ReceiveDone.Reset();
            return _idGameMode;
        }

        public bool AddPlayerToGame(int idGame, int idPlayer)
        {
            _client.Send("ADD idG="+idGame+" idP="+idPlayer+ " \0");
            ReceiveDone.WaitOne();
            ReceiveDone.Reset();
            if (_error)
            {
                _error = false;
                throw new Exception("ERROR connexion");
            }
            return _isReady;
        }

        public Player ConnexionPlayer(string name)
        {
            _client.Send("ADDPLAYER nameP="+name+" \0");
            ReceiveDone.WaitOne();
            ReceiveDone.Reset();
            if (_error)
            {
                _error = false;
                return null;
            }
            return new Player(name,_idPlayer);
        }

        public List<CurrentGameMode> GetGames()
        {
            _client.Send("GETGAME \0");
            ReceiveDone.WaitOne();
            ReceiveDone.Reset();
            if (_error)
            {
                _error = false;
                return new List<CurrentGameMode>();
            }
            return _gameModes;
        }

        public List<Player> GetPlayerInGame(int idGame)
        {
            _client.Send("GETPLAYERS idG="+idGame+" \0");
            ReceiveDone.WaitOne();
            ReceiveDone.Reset();
            if (_error)
            {
                _error = false;
                return new List<Player>();
            }
            return _players;
        }

        public void Switch(int x, int y, int idPlayer,int idGame)
        {
            _client.Send("SET x=" + x + " y="+y+" idP="+idPlayer+" idG="+idGame+" \0");
        }

        public void StartGen(int idGame)
        {
            _client.Send("START idG=" + idGame + " \0");
        }

        public List<Cell> GetGen(int idGame, int gen)
        {
            _client.Send("GET idG=" + idGame + " gen="+gen+" \0");
            ReceiveDone.WaitOne();
            ReceiveDone.Reset();
            if (_error)
            {
                _error = false;
                throw new Exception();
            }
            return _cells;
        }

        public List<Cell> GetZone(int idGame)
        {
            _client.Send("GETZONE idG=" + idGame +"\0");
            ReceiveDone.WaitOne();
            ReceiveDone.Reset();
            if (_error)
            {
                _error = false;
                throw new Exception();
            }
            return _zone;
        }

        public event EventHandler<NewPlayerEvent> NewPlayer;
        public event EventHandler<ErrorGamesEvent> ErrorGames;
        public event EventHandler<DeletePlayerEvent> DeletePLayer;
        public event EventHandler<StartGameEvent> StartGame;
        public event EventHandler<SwitchCellEvent> SwitchCell;
        public event EventHandler<StartGenEvent> StartGenEv;
    }
}

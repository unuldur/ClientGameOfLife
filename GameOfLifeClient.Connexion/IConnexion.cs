using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Connexion.Events;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Connexion
{
    public interface IConnexion
    {
        int InitGameMode(string gameModeName);
        bool AddPlayerToGame(int idGame, int idPlayer);
        Player ConnexionPlayer(string name);
        List<CurrentGameMode> GetGames();
        List<Player> GetPlayerInGame(int idGame);
        void Switch(int x, int y, int idPlayer, int idGame);
        void StartGen(int idGame);
        List<Cell> GetGen(int idGame, int gen);
        List<Cell> GetZone(int idGame);
        event EventHandler<NewPlayerEvent> NewPlayer;
        event EventHandler<ErrorGamesEvent> ErrorGames;
        event EventHandler<DeletePlayerEvent> DeletePLayer;
        event EventHandler<StartGameEvent> StartGame;
        event EventHandler<SwitchCellEvent> SwitchCell;
        event EventHandler<StartGenEvent> StartGenEv;
    }
    
}

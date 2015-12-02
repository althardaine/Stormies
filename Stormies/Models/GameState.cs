using System.Collections.Generic;

namespace Stormies.Models
{
    public class GameState
    {
        public Dictionary<string, Player> Players { get; private set; }
        public int BlueScore { get; private set; }
        public int RedScore { get; private set; }

        public GameState()
        {
            Players = new Dictionary<string, Player>();
            BlueScore = 0;
            RedScore = 0;
        }

        public void Join(Player player, string playerIp)
        {
            Players[playerIp] = player;
        }

        public void Leave(string playerIp)
        {
            Players.Remove(playerIp);
        }

    }
}
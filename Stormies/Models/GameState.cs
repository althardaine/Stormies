using System.Collections.Generic;

namespace Stormies.Models
{
    public class GameState
    {
        public List<Player> Players { get; private set; }
        public int BlueScore { get; private set; }
        public int RedScore { get; private set; }

        public GameState()
        {
            Players = new List<Player>();
            BlueScore = 0;
            RedScore = 0;
        }

        public void Join(Player player)
        {
            Players.Add(player);
        }

        public void Leave(Player player)
        {
            Players.Remove(player);
        }

    }
}
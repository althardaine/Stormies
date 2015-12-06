using System.Collections.Generic;

namespace Stormies.Models
{
    public class GameState
    {
        public Dictionary<string, Player> Players { get; private set; }
        public int BlueScore { get; private set; }
        public int RedScore { get; private set; }
        private readonly object _syncLock = new object();

        public GameState()
        {
            lock (_syncLock)
            {
                Players = new Dictionary<string, Player>();
                BlueScore = 0;
                RedScore = 0;
            }
        }

        public void Join(Player player, string id)
        {
            lock (_syncLock)
            {
                Players[id] = player;
            }
        }

        public void Leave(string playerId)
        {
            lock (_syncLock)
            {
                Players.Remove(playerId);
            }
        }

    }
}
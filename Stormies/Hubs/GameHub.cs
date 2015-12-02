using Microsoft.AspNet.SignalR;
using Stormies.Models;

namespace Stormies.Hubs
{
    public class ChatHub : Hub
    {

        private static readonly GameState GameState = new GameState();

        public void JoinRequest(string playerName, string playerIp)
        {
            if (GameState.Players.Find(p => p.Ip == playerIp) != null)
            {
                Clients.Caller.passErrorMessage("You are already connected!");
            }
            else if (GameState.Players.Find(p => p.Name == playerName) != null)
            {
                Clients.Caller.passErrorMessage("Player with that name already exist!");
            }
            else
            {
                var player = new Player(playerName, playerIp);
                GameState.Join(player);
                Clients.All.updateGameState(GameState);
            }
        }

        public void LeaveRequest(string playerIp)
        {
            var player = GameState.Players.Find(p => p.Ip == playerIp);
            GameState.Leave(player);
            Clients.All.updateGameState(GameState);
        }

        public void GetGameState()
        {
            Clients.Caller.updateGameState(GameState);
        }
    }
}
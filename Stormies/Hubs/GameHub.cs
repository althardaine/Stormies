using System.Linq;
using Microsoft.AspNet.SignalR;
using Stormies.Models;

namespace Stormies.Hubs
{
    public class GameHub : Hub
    {

        private static readonly GameState GameState = new GameState();

        public void JoinRequest(string playerName, string playerIp)
        {
            if (GameState.Players.ContainsKey(playerIp))
            {
                Clients.Caller.passErrorMessage("You are already connected!");
            }
            else if (GameState.Players.Values.ToList().Find(p => p.Name == playerName) != null)
            {
                Clients.Caller.passErrorMessage("Player with that name already exist!");
            }
            else
            {
                var player = new Player(playerName);
                GameState.Join(player, playerIp);
                Clients.All.playerJoined(playerIp, player);
                Clients.Caller.youJoined(GameState);
                
            }
        }

        public void LeaveRequest(string playerIp)
        {
            GameState.Leave(playerIp);
            Clients.All.playerLeft(GameState, playerIp);
        }

        public void MoveRequest(string playerIp, double x, double y, double angle)
        {
            if (!GameState.Players.ContainsKey(playerIp)) return;
            GameState.Players[playerIp].Move(x, y, angle);
            Clients.Others.playerMoved(playerIp, GameState.Players[playerIp]);
        }

        public void FirstSkillUsed(string playerId)
        {
            if (!GameState.Players.ContainsKey(playerId)) return;
            GameState.Players[playerId].TakeDamage(10);
            Clients.All.updateGameState(GameState);
        }
    }
}
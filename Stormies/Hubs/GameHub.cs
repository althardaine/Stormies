using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Stormies.Models;

namespace Stormies.Hubs
{
    public class GameHub : Hub
    {

        private static readonly GameState GameState = new GameState();
        private static readonly Dictionary<string, string> ConnectionToId = new Dictionary<string, string>();

        public void JoinRequest(string playerName)
        {
            if (GameState.Players.Values.ToList().Find(p => p.Name == playerName) != null)
            {
                Clients.Caller.passErrorMessage("Player with that name already exist!");
            } 
            if (ConnectionToId.ContainsKey(Context.ConnectionId))
            {
                Clients.Caller.passErrorMessage("You are already in the game!");
            }
            else
            {
                var playerId = Guid.NewGuid().ToString();
                var player = new Player(playerName);
                ConnectionToId[Context.ConnectionId] = playerId;
                GameState.Join(player, playerId);
                Clients.Others.playerJoined(playerId, player);
                Clients.Caller.youJoined(playerId, GameState);
                
            }
        }

        public void LeaveRequest()
        {
            var connectionId = Context.ConnectionId;
            if (!ConnectionToId.ContainsKey(connectionId)) return;

            var playerId = ConnectionToId[connectionId];
            ConnectionToId.Remove(connectionId);
            GameState.Leave(playerId);
            Clients.All.playerLeft(GameState, playerId);
        }

        public void MoveRequest(double x, double y, double angle)
        {
            var connectionId = Context.ConnectionId;
            if (!ConnectionToId.ContainsKey(Context.ConnectionId)) return;

            var playerId = ConnectionToId[connectionId];
            if (!GameState.Players.ContainsKey(playerId)) return;

            GameState.Players[playerId].Move(x, y, angle);
            Clients.Others.playerMoved(playerId, GameState.Players[playerId]);
        }

        public void UseSkillRequest(int skillId)
        {
            var connectionId = Context.ConnectionId;
            if (!ConnectionToId.ContainsKey(Context.ConnectionId)) return;

            var playerId = ConnectionToId[connectionId];
            if (!GameState.Players.ContainsKey(playerId)) return;

            if (GameState.Players[playerId].UseSkill(GameState, playerId, skillId))
            {
                Clients.All.playerUsedFirstSkill(playerId, GameState.Players[playerId]);
            }
        }
    }
}
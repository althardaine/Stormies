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
            else if (ConnectionToId.ContainsKey(Context.ConnectionId))
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

        public void MoveForewardRequest()
        {
            string playerId;
            if (!VerifyConnection(out playerId)) return;

            var player = GameState.Players[playerId];
            player.MoveForeward();
            Clients.All.playerMoved(playerId, GameState.Players[playerId]);
        }

        public void MoveBackwardRequest()
        {
            string playerId;
            if (!VerifyConnection(out playerId)) return;

            var player = GameState.Players[playerId];
            player.MoveBackward();
            Clients.All.playerMoved(playerId, GameState.Players[playerId]);
        }

        public void RotateRightRequest()
        {
            string playerId;
            if (!VerifyConnection(out playerId)) return;

            var player = GameState.Players[playerId];
            player.RotateRight();
            Clients.All.playerMoved(playerId, GameState.Players[playerId]);
        }

        public void RotateLeftRequest()
        {
            string playerId;
            if (!VerifyConnection(out playerId)) return;

            var player = GameState.Players[playerId];
            player.RotateLeft();
            Clients.All.playerMoved(playerId, GameState.Players[playerId]);
        }

        public void UseSkillRequest(int skillId)
        {
            string playerId;
            if (!VerifyConnection(out playerId)) return;

            var player = GameState.Players[playerId];
            var skill = player.GetSkill(skillId);
            if (skill == null) return;

            if (skill.Use(GameState, playerId))
            {
                Clients.All.playerUsedSkill(playerId, GameState, skill.Animation, skill.Sound);
            }
        }

        private bool VerifyConnection(out string playerId)
        {
            var connectionId = Context.ConnectionId;
            if (!ConnectionToId.ContainsKey(Context.ConnectionId))
            {
                playerId = null;
                return false;
            }
            playerId = ConnectionToId[connectionId];
            return GameState.Players.ContainsKey(playerId);
        }

    }
}
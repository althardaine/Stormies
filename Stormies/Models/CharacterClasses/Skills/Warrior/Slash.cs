using System;
using System.Linq;

namespace Stormies.Models.CharacterClasses.Skills.Warrior
{
    public class Slash : Skill
    {

        private const int Damage = 10;
        private const int Range = 80;

        public Slash()
        {
            Animation = "slashAnimation";
            Sound = "slashSound";
        }

        protected override void Execute(GameState gameState, string playerId)
        {
            var user = gameState.Players[playerId];
            foreach (var player in gameState.Players.Where(entry => entry.Key != playerId).Select(entry => entry.Value).Where(player => PlayerInRange(user, player)))
            {
                player.TakeDamage(Damage);
            }
        }

        private static bool PlayerInRange(Player user, Player player)
        {
            var radianAngle = (user.Angle + 90)*Math.PI/180;
            var bx = user.PositionX + Range * Math.Sin(radianAngle);
            var by = Math.Sqrt(Math.Pow(Range, 2) - Math.Pow(user.PositionX - bx, 2)) + user.PositionY;
            var scalar = ((bx - user.PositionX) * (player.PositionX - user.PositionX)) + ((by - user.PositionY) * (player.PositionY - user.PositionY));
            return Math.Pow((player.PositionX - user.PositionX), 2) + Math.Pow((player.PositionY - user.PositionY), 2) <= Math.Pow(Range, 2) && scalar >= 0;
        }

        public override string Name()
        {
            return "Slash";
        }

        public override string Description()
        {
            return "Slashes all enemies in front of you for moderate damage.";
        }

    }
}
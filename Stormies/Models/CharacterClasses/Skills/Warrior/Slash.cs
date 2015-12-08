using System;
using Stormies.Extensions;

namespace Stormies.Models.CharacterClasses.Skills.Warrior
{
    public class Slash : Skill
    {
        protected override void Execute(GameState gameState, string playerId)
        {
            gameState.Players[playerId].TakeDamage(10);
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
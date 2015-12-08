using System;
using Stormies.Extensions;

namespace Stormies.Models.CharacterClasses.Skills.Warrior
{
    public class Slash : Skill
    {

        public override bool Use(GameState gameState, string playerId)
        {
            var now = DateTime.Now.ToMilliseconds();
            if (LastUsed == 0)
            {
                LastUsed = now;
                return true;
            }
            if (now - LastUsed < Cooldown) return false;
            LastUsed = now;
            return true;
        }

    }
}
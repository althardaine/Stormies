using System;
using Stormies.Extensions;

namespace Stormies.Models.CharacterClasses.Skills
{
    public abstract class Skill
    {

        protected long Cooldown = 1000;
        protected long LastUsed;
        public string Animation { get; protected set; }
        public string Sound { get; protected set; }

        public bool Use(GameState gameState, string playerId)
        {
            var isOffCooldown = VerifyCooldown();
            if (isOffCooldown)
            {
                Execute(gameState, playerId);
            }
            return isOffCooldown;
        }

        private bool VerifyCooldown()
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

        protected abstract void Execute(GameState gameState, string playerId);
        public abstract string Name();
        public abstract string Description();

    }
}

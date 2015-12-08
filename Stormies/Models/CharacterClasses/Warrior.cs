using System;
using Stormies.Extensions;

namespace Stormies.Models.CharacterClasses
{
    public class Warrior : CharacterClass
    {

        public Warrior()
        {
            FirstSkillCooldown = 1000;
        }

        public override bool UseFirstSkill()
        {
            var now = DateTime.Now.ToMilliseconds();
            if (FirstSkillLastUsed == 0)
            {
                FirstSkillLastUsed = now;
                return true;
            }
            if (now - FirstSkillLastUsed < FirstSkillCooldown) return false;
            FirstSkillLastUsed = now;
            return true;
        }
    }
}
using System.Collections.Generic;
using Stormies.Models.CharacterClasses.Skills;
using Stormies.Models.CharacterClasses.Skills.Warrior;

namespace Stormies.Models.CharacterClasses
{
    public class Warrior : CharacterClass
    {

        private readonly List<Skill> _skills = new List<Skill> {new Slash()};

        public Warrior()
        {
            Health = 100;
            Speed = 2.0;
        }

        public override Skill GetSkill(int skillId)
        {
            return _skills.Count <= skillId ? null : _skills[skillId];
        }
    }
}
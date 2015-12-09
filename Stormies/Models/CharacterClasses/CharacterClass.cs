using Stormies.Models.CharacterClasses.Skills;

namespace Stormies.Models.CharacterClasses
{
    public abstract class CharacterClass
    {

        public int Health { get; protected set; }
        public double Speed { get; protected set; }

        public abstract Skill GetSkill(int skillId);

    }
}
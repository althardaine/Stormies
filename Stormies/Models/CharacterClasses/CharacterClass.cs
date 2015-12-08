namespace Stormies.Models.CharacterClasses
{
    public abstract class CharacterClass
    {

        protected long FirstSkillLastUsed = 0;
        protected long FirstSkillCooldown;

        public abstract bool UseFirstSkill();

    }
}
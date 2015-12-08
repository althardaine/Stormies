namespace Stormies.Models.CharacterClasses.Skills
{
    public abstract class Skill
    {

        protected long Cooldown = 1000;
        protected long LastUsed = 0;

        public abstract bool Use(GameState gameState, string playerId);
        public abstract string Name();
        public abstract string Description();

    }
}

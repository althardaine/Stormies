namespace Stormies.Models.CharacterClasses
{
    public abstract class CharacterClass
    {

        public int Health { get; protected set; }
        public double Speed { get; protected set; }

        public abstract bool UseSkill(GameState gameState, string playerId, int skillId);

    }
}
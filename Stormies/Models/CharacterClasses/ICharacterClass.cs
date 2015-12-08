namespace Stormies.Models.CharacterClasses
{
    public interface ICharacterClass
    {

        bool UseFirstSkill(GameState gameState, string playerId);

    }
}
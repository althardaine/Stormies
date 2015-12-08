using System;
using Stormies.Extensions;
using Stormies.Models.CharacterClasses.Skills;
using Stormies.Models.CharacterClasses.Skills.Warrior;

namespace Stormies.Models.CharacterClasses
{
    public class Warrior : ICharacterClass
    {

        private readonly Skill _firstSkill = new Slash();

        public bool UseFirstSkill(GameState gameState, string playerId)
        {
            return _firstSkill.Use(gameState, playerId);
        }

    }
}
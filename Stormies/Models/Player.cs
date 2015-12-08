using System;
using Stormies.Models.CharacterClasses;

namespace Stormies.Models
{
    public class Player
    {

        public string Name { get; private set; }
        public double PositionX { get; private set; }
        public double PositionY { get; private set; }
        public double Angle { get; private set; }
        public double Speed { get; private set; }
        public int Health { get; private set; }
        public CharacterClass CharacterClass { get; private set; }
        private const double RotationSpeed = 4;
        private readonly object _syncLock = new object();

        public Player(string name)
        {
            Name = name;
            PositionX = 20;
            PositionY = 20;
            Angle = 0;
            CharacterClass = new Warrior();
            Health = CharacterClass.Health;
            Speed = CharacterClass.Speed;
        }

        public void TakeDamage(int damage)
        {
            lock (_syncLock)
            {
                Health -= damage;
            }
        }

        public void MoveForeward()
        {
            lock (_syncLock)
            {
                var radianAngle = Angle * Math.PI / 180;
                PositionX += Math.Cos(radianAngle) * Speed;
                PositionY += Math.Sin(radianAngle) * Speed;
            }
        }

        public void MoveBackward()
        {
            lock (_syncLock)
            {
                var radianAngle = Angle * Math.PI / 180;
                PositionX -= Math.Cos(radianAngle) * Speed/2;
                PositionY -= Math.Sin(radianAngle) * Speed/2;
            }
        }

        public void RotateRight()
        {
            lock (_syncLock)
            {
                Angle += RotationSpeed;
            }
        }

        public void RotateLeft()
        {
            lock (_syncLock)
            {
                Angle -= RotationSpeed;
            }
        }

        public bool UseSkill(GameState gameState, string playerId, int skillId)
        {
            lock (_syncLock)
            {
                return CharacterClass.UseSkill(gameState, playerId, skillId);
            }
        }

    }
}
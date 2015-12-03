namespace Stormies.Models
{
    public class Player
    {

        public string Name { get; private set; }
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public int Health { get; private set; }
        private readonly object _syncLock = new object();

        public Player(string name)
        {
            Name = name;
            PositionX = 0;
            PositionY = 0;
            Health = 100;
        }

        public void TakeDamage(int damage)
        {
            lock (_syncLock)
            {
                Health -= 10;
            }
        }

        public void Move(int x, int y)
        {
            lock (_syncLock)
            {
                PositionX = x;
                PositionY = y;
            }
        }

    }
}
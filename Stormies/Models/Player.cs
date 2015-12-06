namespace Stormies.Models
{
    public class Player
    {

        public string Name { get; private set; }
        public double PositionX { get; private set; }
        public double PositionY { get; private set; }
        public double Angle { get; private set; }
        public int Health { get; private set; }
        private readonly object _syncLock = new object();

        public Player(string name)
        {
            Name = name;
            PositionX = 20;
            PositionY = 20;
            Angle = 0;
            Health = 100;
        }

        public void TakeDamage(int damage)
        {
            lock (_syncLock)
            {
                Health -= 10;
            }
        }

        public void Move(double x, double y, double angle)
        {
            lock (_syncLock)
            {
                PositionX = x;
                PositionY = y;
                Angle = angle;
            }
        }

    }
}
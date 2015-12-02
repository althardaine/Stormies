using System.Drawing;

namespace Stormies.Models
{
    public class Player
    {

        public string Name { get; private set; }
        public Point Position { get; private set; }
        public int Health { get; private set; }

        public Player(string name)
        {
            Name = name;
            Position = new Point(10, 10);
            Health = 100;
        }

        public void TakeDamage(int damage)
        {
            Health -= 10;
        }

    }
}
using System.Drawing;

namespace Stormies.Models
{
    public class Player
    {

        public string Name { get; private set; }
        public string Ip { get; private set; }
        public Point Position { get; private set; }
        public int Health { get; private set; }

        public Player(string name, string ip)
        {
            Name = name;
            Ip = ip;
            Position = new Point(10, 10);
            Health = 100;
        }

    }
}
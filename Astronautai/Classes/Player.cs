/**
 * @(#) Player.cs
 */

namespace Class_diagram
{
	public class Player : PlayerImage
    {
        const int MaxPlayerHealth = 3;
        const int MaxPlayerAmmo = 10;
        const int MaxPlayerSpeed = 48;

		public string Username { get; set; }
		public int Health { get; set; }
        public int Ammo { get; set; }
        public int Speed { get; set; }
        public int Size { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public char Rotation { get; set; }

        public Player()
        {

        }

        public Player(string username)
        {
            Username = username;
        }

        public Player(string username, int health, int ammo, int size, int speed)
        {
            Username = username;
            Health = health;
            Ammo = ammo;
            Size = size;
            Speed = speed;
        }

        public void SetCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void AddHealth(int amount)
        {
            if(Health < MaxPlayerHealth)
            {
                Health += amount;
            }
        }

        public void AddAmmo(int amount)
        {
            if (Ammo < MaxPlayerAmmo)
            {
                Ammo += amount;
            }
        }

        public void RemoveAmmo()
        {
            Ammo -= 1;
        }

        public void AddSpeed(int amount)
        {
            if (Speed < MaxPlayerSpeed)
            {
                Speed += amount;
            }
        }

        public override string ToString()
        {
            return Username;
        }

        public override string GetImage()
        {
            return @"..//..//Objects//player.png";
        }
    }
}

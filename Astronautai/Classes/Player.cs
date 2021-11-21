

using Astronautai.Classes;
using Astronautai.Classes.States;
using System;
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
        public string Effect { get; set; }
        public int TickDurration { get; set; }
        private State state;




        public State GetState()
        {
            return state;
        }

        public void SetState(State state)
        {
            this.state = state;
        }


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
            Effect = "";
            TickDurration = -1;
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
                if(Health >= MaxPlayerHealth)
                {
                    Health = MaxPlayerHealth;
                }
            }
        }

        public void AddAmmo(int amount)
        {
            if (Ammo < MaxPlayerAmmo)
            {
                Ammo += amount;
                if (Ammo >= MaxPlayerAmmo)
                {
                    Ammo = MaxPlayerAmmo;
                }
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
                if (Speed >= MaxPlayerSpeed)
                {
                    Speed = MaxPlayerSpeed;
                }
            }
        }

        public override string ToString()
        {
            return Username;
        }

        public string GetImage()
        {
            return @"..//..//Objects//player.png";
        }

        public void Affect(string effect)
        {
            SetState(GetState().ChangeSpeed(0));
            Effect = effect;
            TickDurration = 10;
        }

        public void DecreaseDurration()
        {
            if (TickDurration > 1)
            {
                TickDurration -= 1;
            }
            else
            {
                if (Effect != "")
                {
                    Console.WriteLine("effect ending");
                    TickDurration = -1;
                    Effect = "";
                    SetState(GetState().ChangeSpeed(Speed));
                }
            }
        }

    }
}

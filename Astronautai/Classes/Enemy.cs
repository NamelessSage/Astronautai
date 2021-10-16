using Astronautai.Classes.Strategy;

namespace Class_diagram
{
	public class Enemy : Coordinates, IAsteroidMoveStrategy
	{
		public int Id { get; set; }
		public int Health { get; set; }
		public int Points { get; set; }
		public int Size { get; set; }
		public int Damage { get; set; }
		public char Rotation { get; set; }
        public int Speed { get; set; }

		public Enemy()
		{

		}

        public void Move()
        {
            switch (Rotation)
            {
                case 'W':
                    Y -= Speed;
                    break;
                case 'A':
                    X -= Speed;
                    break;
                case 'S':
                    Y += Speed;
                    break;
                case 'D':
                    X += Speed;
                    break;
                case 'Q':
                    Y -= Speed;
                    X -= Speed;
                    break;
                case 'E':
                    X += Speed;
                    Y -= Speed;
                    break;
                case 'Z':
                    Y += Speed;
                    X -= Speed;
                    break;
                case 'C':
                    X -= Speed;
                    Y += Speed;
                    break;
                default:
                    break;
            }
        }

        public void MoveSlow()
        {
            Speed = 1;
            Move();
        }

        public void MoveAverage()
        {
            Speed = 2;
            Move();
        }

        public void MoveFast()
        {
            Speed = 3;
            Move();
        }
    }
}

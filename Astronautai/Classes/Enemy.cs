using Astronautai.Classes;
using Astronautai.Classes.Strategy;
using Astronautai.Classes.Visitor;

namespace Class_diagram
{
	public class Enemy : Element
	{
		public int Id { get; set; }
		public int Health { get; set; }
		public int Points { get; set; }
		public int Size { get; set; }
		public int Damage { get; set; }
		public char Rotation { get; set; }
        public int Speed { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        MoveStrategy moveStrategy;

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

        public void SetMoveStrategy(MoveStrategy strategy)
        {
            moveStrategy = strategy;
            Speed = strategy.MoveAlgorithm();
        }

        public Coordinates GetCoordinates()
        {
            return new Coordinates(X, Y);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this); ;
        }
    }
}

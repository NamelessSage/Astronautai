using Astronautai.Classes;

namespace Class_diagram
{
	public class Obstacle : Prototype
	{		
		public int Id { get; set; }
        public int Size { get; set; }

        public Coordinates coordinates { get; set; }

        public Obstacle(int id, Coordinates cord, int size)
        {
            Id = id;
            coordinates = cord;
            Size = size;
        }

        public override Prototype Clone()
        {
            return (Obstacle)this.MemberwiseClone();
        }
        public override Prototype CloneDeep()
        {
            Obstacle obs = (Obstacle)this.MemberwiseClone();
            obs.coordinates = new Coordinates(coordinates.X,coordinates.Y);
            return obs;
        }
    }
}

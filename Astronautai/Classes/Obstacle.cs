namespace Class_diagram
{
	public class Obstacle : Coordinates
	{		
		int Id { get; set; }
		int Size { get; set; }

        public Obstacle(int id, int x, int y, int size) : base(x, y)
        {
            Id = id;
            X = x;
            Y = y;
            Size = size;
        }
    }
}

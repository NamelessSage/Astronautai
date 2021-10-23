namespace Class_diagram
{
	public class Obstacle
	{		
		public int Id { get; set; }
        public int Size { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Obstacle(int id, int x, int y, int size)
        {
            Id = id;
            X = x;
            Y = y;
            Size = size;
        }
    }
}

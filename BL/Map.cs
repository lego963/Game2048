namespace BL
{
    internal class Map
    {
        public int this[int x, int y]
        {
            get
            {
                if (OnMap(x, y))
                    return _map[x, y];
                return -1;
            }
            set
            {
                if (OnMap(x, y)) _map[x, y] = value;
            }
        }

        public int Size { get; }
        private readonly int[,] _map;

        public Map(int size)
        {
            Size = size;
            _map = new int[Size, Size];
        }

        private bool OnMap(int x, int y)
        {
            return x >= 0 && x < Size && y >= 0 && y < Size;
        }
    }
}

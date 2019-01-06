using System;

namespace BL
{
    public class Model
    {
        public int Size => _map.Size;

        private readonly Map _map;
        private bool _isGameOver;

        private static readonly Random Rnd = new Random();
        private bool _moved;

        public Model(int size)
        {
            _map = new Map(size);
        }

        public void Start()
        {
            _isGameOver = false;
            for (var x = 0; x < Size; x++)
            {
                for (var y = 0; y < Size; y++)
                {
                    _map[x, y] = 0;
                }
            }
            AddRandomNumber();
            AddRandomNumber();
        }

        public bool IsGameOver()
        {
            if (_isGameOver)
                return _isGameOver;

            for (var x = 0; x < Size; x++)
                for (var y = 0; y < Size; y++)
                    if (_map[x, y] == 0)
                        return false;

            for (var x = 0; x < Size; x++)
                for (var y = 0; y < Size; y++)
                    if (_map[x, y] == _map[x + 1, y] || _map[x, y] == _map[x, y + 1])
                        return false;

            _isGameOver = true;
            return _isGameOver;
        }

        private void AddRandomNumber() //Надо сделать пизже, ибо хуерга тут, а не метод
        {
            if (_isGameOver)
                return;

            for (var i = 0; i < 100; i++)
            {
                int x = Rnd.Next(0, Size); //Возврат к 40 минуте
                int y = Rnd.Next(0, Size);
                if (_map[x, y] == 0)
                {
                    _map[x, y] = Rnd.Next(1, 3) * 2;
                    return;
                }
            }


        }

        private void Lift(int x, int y, int dx, int dy) //Сдвиг
        {
            if (_map[x, y] <= 0) return;
            while (_map[x + dx, y + dy] == 0)
            {
                _map[x + dx, y + dy] = _map[x, y];
                _map[x, y] = 0;
                x += dx;
                y += dy;
                _moved = true;
            }
        }

        private void Join(int x, int y, int dx, int dy) //Объединение
        {
            if (_map[x, y] <= 0) return;
            if (_map[x + dx, y + dy] != _map[x, y]) return;
            _map[x + dx, y + dy] = _map[x, y] * 2;
            while (_map[x - dx, y - dy] > 0)
            {
                _map[x, y] = _map[x - dx, y - dy];
                x -= dx;
                y -= dy;
            }

            _map[x, y] = 0;
            _moved = true;
        }

        public void Left()
        {
            _moved = false;
            for (var y = 0; y < Size; y++)
            {
                for (var x = 1; x < Size; x++) Lift(x, y, -1, 0);
                for (var x = 1; x < Size; x++) Join(x, y, -1, 0);
            }
            if (_moved) AddRandomNumber();


        }

        public void Right()
        {
            _moved = false;
            for (var y = 0; y < Size; y++)
            {
                for (var x = Size - 2; x >= 0; x--) Lift(x, y, +1, 0);
                for (var x = Size - 2; x >= 0; x--) Join(x, y, +1, 0);
            }
            if (_moved) AddRandomNumber();
        }

        public void Up()
        {
            _moved = false;
            for (var x = 0; x < Size; x++)
            {
                for (var y = 1; y < Size; y++) Lift(x, y, 0, -1);
                for (var y = 1; y < Size; y++) Join(x, y, 0, -1);
            }
            if (_moved) AddRandomNumber();
        }

        public void Down()
        {
            _moved = false;
            for (var x = 0; x < Size; x++)
            {
                for (var y = Size - 2; y >= 0; y--) Lift(x, y, 0, +1);
                for (var y = Size - 2; y >= 0; y--) Join(x, y, 0, +1);
            }
            if (_moved) AddRandomNumber();
        }

        public int GetMap(int x, int y) => _map[x, y];
    }
}

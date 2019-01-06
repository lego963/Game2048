using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BL
{
    public class Model
    {
        public int Size { get; private set; }
        private Map map;
        static Random rnd = new Random();
        private bool moved;
        public Model()
        {
        }

        public Model(int size)
        {
            this.Size = size;
            map = new Map(size);
        }

        private bool _isGameOver;

        public void Start()
        {
            _isGameOver = false;
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    map.Set(x, y, 0);
                }
            }

            AddRandomNumber();
            AddRandomNumber();
        }

        public bool IsGameOver()
        {
            if (_isGameOver)
            {
                return _isGameOver;
            }

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (map.Get(x, y) == 0)
                    {
                        return false;
                    }
                }
            }
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (map.Get(x, y) == map.Get(x + 1, y) || map.Get(x, y) == map.Get(x, y + 1))
                    {
                        return false;
                    }
                }
            }

            _isGameOver = true;
            return _isGameOver;
        }

        private void AddRandomNumber()
        {
            if (_isGameOver)
            {
                return;
            }

            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(0, Size); //Возврат к 40 минуте
                int y = rnd.Next(0, Size);
                if (map.Get(x, y) == 0)
                {
                    map.Set(x, y, rnd.Next(1, 3) * 2);
                    return;
                }
            }


        }

        public void Lift(int x, int y, int dx, int dy) //сдвиг
        {
            if (map.Get(x, y) > 0)
            {
                while (map.Get(x + dx, y + dy) == 0)
                {
                    map.Set(x + dx, y + dy, map.Get(x, y));
                    map.Set(x, y, 0);
                    x += dx;
                    y += dy;
                    moved = true;
                }
            }
        }

        public void Join(int x, int y, int dx, int dy) //Объединение
        {
            if (map.Get(x, y) > 0)
            {
                if (map.Get(x + dx, y + dy) == map.Get(x, y))
                {
                    map.Set(x + dx, y + dy, map.Get(x, y) * 2);
                    while (map.Get(x - dx, y - dy) > 0)
                    {
                        map.Set(x, y, map.Get(x - dx, y - dy));
                        x -= dx;
                        y -= dy;
                    }

                    map.Set(x, y, 0);
                    moved = true;
                }
            }
        }

        public void Left()
        {
            moved = false;
            for (int y = 0; y < Size; y++)
            {
                for (int x = 1; x < Size; x++)
                {
                    Lift(x, y, -1, 0);
                }
                for (int x = 1; x < Size; x++)
                {
                    Join(x, y, -1, 0);
                }
            }




        }

        public void Right()
        {
            moved = false;
            for (int y = 0; y < Size; y++)
            {
                for (int x = Size - 2; x >= 0; x--)
                {
                    Lift(x, y, +1, 0);
                }
                for (int x = Size - 2; x >= 0; x--)
                {
                    Join(x, y, +1, 0);
                }
            }
            if (moved)
            {
                AddRandomNumber();
            }
        }

        public void Up()
        {
            moved = false;
            for (int x = 0; x < Size; x++)
            {
                for (int y = 1; y < Size; y++)
                {
                    Lift(x, y, 0, -1);
                }
                for (int y = 1; y < Size; y++)
                {
                    Join(x, y, 0, -1);
                }
            }
            if (moved)
            {
                AddRandomNumber();
            }
        }

        public void Down()
        {
            moved = false;
            for (int x = 0; x < Size; x++)
            {
                for (int y = Size - 2; y >= 0; y--)
                {
                    Lift(x, y, 0, +1);
                }
                for (int y = Size - 2; y >= 0; y--)
                {
                    Join(x, y, 0, +1);
                }
            }
            if (moved)
            {
                AddRandomNumber();
            }
        }

        public int GetMap(int x, int y)
        {
            return map.Get(x, y);
        }
    }
}

using System;
using BL;

namespace ConsoleGame
{
    internal static class Game
    {
        public static void Main()
        {
            Console.Write("Type dimension: ");
            Start(int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()));
        }

        private static void Start(int size)
        {
            var model = new Model(size);
            model.Start();
            while (true)
            {
                Show(model);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        model.Left();
                        break;
                    case ConsoleKey.RightArrow:
                        model.Right();
                        break;
                    case ConsoleKey.UpArrow:
                        model.Up();
                        break;
                    case ConsoleKey.DownArrow:
                        model.Down();
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        private static void Show(Model model)
        {
            for (var y = 0; y < model.Size; y++)
            {
                for (var x = 0; x < model.Size; x++)
                {
                    Console.SetCursorPosition(x * 5 + 5, y * 2 + 2);
                    var number = model.GetMap(x, y);
                    Console.Write(number == 0 ? ". " : number + " ");
                }
            }
            Console.WriteLine();
            Console.WriteLine(model.IsGameOver() ? "Game Over" : "Still play");
        }
    }
}
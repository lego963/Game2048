using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using BL;

namespace ConsoleGame
{
    internal class Game
    {
        //private Model model;
        public static void Main(string[] args)
        {
            Game model = new Game();
            model.Start();
        }

        private void Start() //Контроллер
        {
            var model = new Model(4);
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
                        model.Start();
                        break;
                }
            }
        }

        private static void Show(Model model)
        {
            for (int y = 0; y < model.Size; y++)
            {
                for (int x = 0; x < model.Size; x++)
                {
                    Console.SetCursorPosition(x * 5 + 5, y * 2 + 2);
                    int number = model.GetMap(x, y);
                    Console.Write(number == 0 ? ". " : number.ToString() + " ");
                }
            }

            Console.WriteLine();
            Console.WriteLine(model.IsGameOver() ? "Game Over" : "Still play");
        }
    }
}

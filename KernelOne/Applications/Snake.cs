using System;
using System.Collections.Generic;

namespace KernelOne.Applications
{
    /// <summary>
    /// This is a game of snake.
    /// </summary>
    public class Snake : Application
    {
        public int[] _matrix;
        public List<int[]> _commands;
        public List<int[]> _snake;
        public List<int> _food;
        public int _randomNumber;
        Random _rnd;

        public Snake() : base("snake")
        {
            _rnd = new Random();
        }

        public string GetSnakeScore()
        {
            if (_snake.Count < 10)
            {
                return _snake.Count + "   ";
            }
            else if (_snake.Count < 100)
            {
                return _snake.Count + "  ";
            }
            else if (_snake.Count < 1000)
            {
                return _snake.Count + " ";
            }
            else
            {
                return _snake.Count + "";
            }
        }

        public void UpdatePosotion()
        {
            List<int[]> tmp = new List<int[]>();

            foreach (int[] point in _snake)
            {
                switch (point[1])
                {
                    case 1:
                        point[0] = point[0] - 1;
                        break;
                    case 2:
                        point[0] = point[0] + 80;
                        break;
                    case 3:
                        point[0] = point[0] + 1;
                        break;
                    case 4:
                        point[0] = point[0] - 80;
                        break;
                    default:
                        break;
                }
                tmp.Add(point);
            }
            _snake = tmp;
        }

        public void ChangeArray()
        {
            for (int i = 0; i < _matrix.Length; i++)
            {
                _matrix[i] = 0;
            }

            foreach (int[] point in _snake)
            {
                _matrix[point[0]] = 3;
            }

            foreach (int point in _food)
            {
                _matrix[point] = 2;
            }

            for (int i = 0; i < _matrix.Length; i++)
            {
                if (i <= 79 && i >= 0)
                {
                    _matrix[i] = 1;
                }
                else if (i <= 1760 && i >= 1679)
                {
                    _matrix[i] = 1;
                }
                else if (i % 80 == 0)
                {
                    _matrix[i] = 1;
                }

                else if (i % 80 == 79)
                {
                    _matrix[i] = 1;
                }
            }
        }

        public Boolean Gameover()
        {
            int head = _snake[0][0];
            for (int i = 1; i < _snake.Count; i++)
            {
                if (head == _snake[i][0])
                {
                    return true;
                }
            }
            if (head % 80 == 79 || head % 80 == 0 || head <= 1760 && head >= 1679 || head <= 79 && head >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PrintGame()
        {
            for (int i = 0; i < _matrix.Length; i++)
            {
                if (Gameover() && i == 585)
                {
                    Console.Write("###############################");
                    i = i + 30;
                }
                else if (Gameover() && i == 665)
                {
                    Console.Write("###############################");
                    i = i + 30;
                }
                else if (Gameover() && i == 745)
                {
                    Console.Write("####                       ####");
                    i = i + 30;
                }
                else if (Gameover() && i == 825)
                {
                    Console.Write("####       GAMEOVER        ####");
                    i = i + 30;
                }
                else if (Gameover() && i == 905)
                {
                    Console.Write("####      Score: " + GetSnakeScore() + "      ####");
                    i = i + 30;
                }
                else if (Gameover() && i == 985)
                {
                    Console.Write("####                       ####");
                    i = i + 30;
                }
                else if (Gameover() && i == 1065)
                {
                    Console.Write("###############################");
                    i = i + 30;
                }
                else if (Gameover() && i == 1145)
                {
                    Console.Write("###############################");
                    i = i + 30;
                }
                else if (_matrix[i] == 1)
                {
                    Console.Write("#");
                }
                else if (_matrix[i] == 2)
                {
                    Console.Write("$");
                }
                else if (_matrix[i] == 3)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.Write("#  Current score: " + GetSnakeScore() + "      Exit game: ESC button      Restart game: R button  #");
            Console.Write("################################################################################");
        }

        public void Delay(int time)
        {
            for (int i = 0; i < time; i++) ;
        }

        public int xy2p(int x, int y)
        {
            return y * 80 + x;
        }

        public int RandomFood()
        {
            int rand = _rnd.Next(81, 1700);
            if (rand != _randomNumber)
            {
                _randomNumber = rand;
                return rand;
            }
            else
            {
                return 1400;
            }
        }

        public void ConfigSnake()
        {
            _matrix = new int[1760];
            _commands = new List<int[]>();
            _snake = new List<int[]>();
            _food = new List<int>();
            _randomNumber = 0;
            _snake.Add(new int[2] { xy2p(10, 10), 3 });
            ChangeArray();
            _food.Add(RandomFood());
        }

        public void UpdateDirections()
        {
            List<int[]> tmp = new List<int[]>();
            foreach (int[] com in _commands)
            {
                if (com[1] < _snake.Count)
                {
                    _snake[com[1]][1] = com[0];
                    com[1] = com[1] + 1;
                    tmp.Add(com);
                }
            }
            _commands = tmp;
        }

        public void CheckIfTouchFood()
        {
            List<int> foodtmp = new List<int>();
            foreach (int pos in _food)
            {
                if (_snake[0][0] == pos)
                {
                    foodtmp.Add(RandomFood());
                    int tmp1 = _snake[_snake.Count - 1][0];
                    int tmp2 = _snake[_snake.Count - 1][1];
                    if (tmp2 == 1)
                    {
                        tmp1 = tmp1 + 1;
                    }
                    else if (tmp2 == 2)
                    {
                        tmp1 = tmp1 - 80;
                    }
                    else if (tmp2 == 3)
                    {
                        tmp1 = tmp1 - 1;
                    }
                    else if (tmp2 == 4)
                    {
                        tmp1 = tmp1 + 80;
                    }
                    _snake.Add(new int[] { tmp1, tmp2 });
                }
                else
                {
                    foodtmp.Add(pos);
                }
            }
            _food = foodtmp;
        }

        public void PrintLogo()
        {
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
            Console.WriteLine(); Console.WriteLine(); Console.WriteLine(); Console.WriteLine(); Console.WriteLine();
            Console.WriteLine("  #####                                 #######  #####  ");
            Console.WriteLine(" #     # #    #   ##   #    # ######    #     # #     # ");
            Console.WriteLine(" #       ##   #  #  #  #   #  #         #     # #       ");
            Console.WriteLine("  #####  # #  # #    # ####   #####     #     #  #####  ");
            Console.WriteLine("       # #  # # ###### #  #   #         #     #       # ");
            Console.WriteLine(" #     # #   ## #    # #   #  #         #     # #     # ");
            Console.WriteLine("  #####  #    # #    # #    # ######    #######  #####  ");
            Console.WriteLine();
        }

        public override void Run()
        {
            Console.Clear();
            PrintLogo();
            ConfigSnake();
            ConsoleKey x;
            while (true)
            {
                while (Gameover())
                {
                    PrintGame();
                    Boolean endGame = false;
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.R:
                            ConfigSnake();
                            break;
                        case ConsoleKey.Escape:
                            endGame = true;
                            break;
                    }

                    if (endGame)
                    {
                        break;
                    }
                }

                while (!Console.KeyAvailable && !Gameover())
                {
                    UpdateDirections();
                    UpdatePosotion();
                    CheckIfTouchFood();

                    Console.Clear();
                    ChangeArray();
                    PrintGame();
                    Delay(10000000);
                }

                x = Console.ReadKey(true).Key;

                if (x == ConsoleKey.LeftArrow)
                {
                    if (_snake[0][1] != 3)
                    {
                        _commands.Add(new int[2] { 1, 0 });
                    }
                }
                else if (x == ConsoleKey.UpArrow)
                {
                    if (_snake[0][1] != 2)
                    {
                        _commands.Add(new int[2] { 4, 0 });
                    }
                }
                else if (x == ConsoleKey.RightArrow)
                {
                    if (_snake[0][1] != 1)
                    {
                        _commands.Add(new int[2] { 3, 0 });
                    }
                }
                else if (x == ConsoleKey.DownArrow)
                {
                    if (_snake[0][1] != 4)
                    {
                        _commands.Add(new int[2] { 2, 0 });
                    }
                }
                else if (x == ConsoleKey.Escape)
                {
                    break;
                }
                else if (x == ConsoleKey.R)
                {
                    ConfigSnake();
                }
            }
        }
    }
}

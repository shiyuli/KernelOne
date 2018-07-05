using System;
using System.Collections.Generic;

namespace KernelOne
{
    public static class Terminal
    {
        public static Char UserChar
        {
            get
            {
                char userChar;

                if (Core.CurrentUser.IsRoot)
                {
                    userChar = CharDefine.Superuser;
                }
                else
                {
                    userChar = CharDefine.User;
                }

                return userChar;
            }
        }

        public static string CurrentDirectory { get; private set; }

        static Terminal()
        {
            CurrentDirectory = "/";
        }

        public static string ReadCommand(out string[] args)
        {
            ConsoleColor previousFontColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            string commandHead = "[" + Core.CurrentUser.Username + "@" + Core.KernelName + " " + CurrentDirectory + "]" + UserChar + " ";
            Console.Write(commandHead);
            Console.ForegroundColor = previousFontColor;

            string input = Console.ReadLine().Trim();

            if (input == string.Empty)
            {
                args = null;
                return null;
            }

            string[] inputArray = input.Split(' ');
            string command = inputArray[0];

            if (inputArray.Length == 1)
            {
                args = null;
                return command;
            }

            string[] tempArgs = input.Substring(command.Length + 1).Split();
            List<string> tempArgList = new List<string>();

            foreach (string tempArg in tempArgs)
            {
                string arg = tempArg.Trim();

                if (arg != string.Empty)
                {
                    tempArgList.Add(arg);
                }
            }

            args = tempArgList.ToArray();

            return command;
        }

        public static void WriteLine(string s, ConsoleColor color = ConsoleColor.White)
        {
            Write(s + '\n', color);
        }

        public static void Write(string s, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor previousFontColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(s);
            Console.ForegroundColor = previousFontColor;
        }

        public static void DrawLogo()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($@"
 _  __                    _  ___             
| |/ /___ _ __ _ __   ___| |/ _ \ _ __   ___ 
| ' // _ \ '__| '_ \ / _ \ | | | | '_ \ / _ \
| . \  __/ |  | | | |  __/ | |_| | | | |  __/
|_|\_\___|_|  |_| |_|\___|_|\___/|_| |_|\___|
                                                { Kernel.Version }

-------------------------------------------
|           Created by LiShiYu.           |
|     http://os.lishiyu.com/kernelone     |
-------------------------------------------");
        }

        public static void Login()
        {
            Console.ForegroundColor = ConsoleColor.White;

            string username = string.Empty;
            while (username.Trim() == string.Empty)
            {
                Console.Write(Core.KernelName + " login: ");
                username = Console.ReadLine();
            }

            string password = string.Empty;
            while (password.Trim() == string.Empty)
            {
                Console.Write("Password: ");
                password = Console.ReadLine();
            }

            User user;
            if (!User.Verify(out user, username, password))
            {
                Login();
                return;
            }

            Core.SetUser(user);

            Console.WriteLine("Nice to meet you, " + Core.CurrentUser.Username);
        }
    }
}

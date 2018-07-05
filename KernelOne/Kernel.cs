using Cosmos.System.Graphics;
using System;
using KernelOne.Commands;
using KernelOne.Applications;

namespace KernelOne
{
    public class Kernel : Cosmos.System.Kernel
    {
        public static string Version { get; set; }

        protected override void BeforeRun()
        {
            Version = "version 0.1";
            Console.Clear();
            Terminal.DrawLogo();
            Terminal.Login();
        }

        protected override void Run()
        {
            if(Core.MouseEnabled)
            {
                GUI.DrawMouse();
            }

            try
            {
                string[] args;

                string command = Terminal.ReadCommand(out args);

                if (command == null)
                {
                    return;
                }

                ParseCommand(command, args);
            }
            catch (Exception e)
            {
                Terminal.WriteLine(e.ToString(), ConsoleColor.Red);
            }
        }

        private void ParseCommand(string command, string[] args)
        {
            switch (command)
            {
                case SystemCommand.su:
                    Core.CurrentUser.Root();
                    break;
                case SystemCommand.whoami:
                    Terminal.WriteLine(Core.CurrentUser.Username);
                    break;
                case SystemCommand.ls:
                    Terminal.WriteLine("etc var root");
                    break;
                case SystemCommand.cd:
                    if (args == null)
                    {
                        Terminal.WriteLine("command args error");
                        return;
                    }

                    Terminal.WriteLine("change directory to " + args[0]);
                    break;
                case SystemCommand.startx:
                    GUI.Draw(new Point(0, 0), new Point(22, 22));
                    GUI.Test();
                    break;
                case SystemCommand.halt:
                    Core.Shutdown();
                    break;
                case SystemCommand.reboot:
                    Core.Reboot();
                    break;
                case SystemCommand.run:
                    if(args.Length > 0)
                    {
                        bool result = ApplicationManager.Instance.Run(args[0]);
                        if(!result)
                        {
                            Terminal.WriteLine("no such application", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        Terminal.WriteLine("bad application name", ConsoleColor.Red);
                    }
                    break;
                default:
                    Terminal.WriteLine("no such command", ConsoleColor.Red);
                    break;
            }
        }
    }
}

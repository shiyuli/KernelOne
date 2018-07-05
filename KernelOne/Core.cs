using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.FileSystem;

namespace KernelOne
{
    public static class Core
    {
        public static string KernelName { get; private set; }

        public static User CurrentUser { get; private set; }

        public static bool MouseEnabled { get; set; }

        static Core()
        {
            KernelName = "KernelOne";

            FileSystemFactory fileSystemFactory = new FileSystemFactory();
            //fileSystemFactory.Create(new Cosmos.HAL.BlockDevice.Partition());
        }

        public static void SetUser(User user)
        {
            CurrentUser = user;
        }

        public static void Shutdown()
        {
            Cosmos.System.Power.Shutdown();
        }

        public static void Reboot()
        {
            Cosmos.System.Power.Reboot();
        }

        public static void Desktop()
        {

        }
    }
}

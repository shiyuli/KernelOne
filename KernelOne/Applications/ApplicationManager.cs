using System;
using System.Collections.Generic;
using System.Text;

namespace KernelOne.Applications
{
    /// <summary>
    /// Application Manager (Single Instance Mode).
    /// </summary>
    public class ApplicationManager
    {
        public static readonly ApplicationManager Instance = new ApplicationManager();

        private Dictionary<string, Application> _applications;

        private ApplicationManager()
        {
            _applications = new Dictionary<string, Application>();

            Add(new Snake());
        }

        public void Add(Application app)
        {
            _applications.Add(app.Name, app);
        }

        public Application Find(string appName)
        {
            if (_applications.ContainsKey(appName))
            {
                return _applications[appName];
            }
            else
            {
                return null;
            }
        }

        public bool Run(string appName)
        {
            Application app = Find(appName);

            if (app == null)
            {
                return false;
            }

            app.Run();

            return true;
        }
    }
}

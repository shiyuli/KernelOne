using System;
using System.Collections.Generic;
using System.Text;

namespace KernelOne
{
    public class User
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsRoot { get; private set; }

        public User(string username, string password, bool isRoot)
        {
            Username = username;
            Password = password;

            IsRoot = isRoot;
        }

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            if(oldPassword == Password)
            {
                Password = newPassword;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Root(bool isRoot = true)
        {
            IsRoot = isRoot;

            return true;
        }

        public static bool Verify(out User user, string username, string password)
        {
            user = null;

            if (username == "root")
            {
                if(password == "123")
                {
                    user = new User(username, password, false);
                    return true;
                }
            }

            return false;
        }
    }
}

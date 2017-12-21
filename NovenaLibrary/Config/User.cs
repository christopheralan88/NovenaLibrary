using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Exceptions;

namespace NovenaLibrary.Config
{
    public class User
    {
        private string _username;
        private string _password;
        private bool _superuser;

        public User(string username, string password, bool superuser = false)
        {
            _username = username;
            _password = password;
            _superuser = superuser;
        }

        public string Username
        {
            get { return _username; }
        }

        public string Password
        {
            get { return _password; }
        }

        public bool SuperUser
        {
            get { return _superuser; }
        }
    }
}

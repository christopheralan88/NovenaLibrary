using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.Exceptions
{
    public class DatabaseTypeNotRecognizedException : Exception
    {
        public DatabaseTypeNotRecognizedException(string message) : base(message)
        {
        }
    }
}

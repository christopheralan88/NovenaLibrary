using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NovenaLibrary.Repositories
{
    public interface IDatabaseConnection
    {
        DataTable query(string sql);
        void userSignIn();
        DataTable getSchema(string tableName);
    }
}

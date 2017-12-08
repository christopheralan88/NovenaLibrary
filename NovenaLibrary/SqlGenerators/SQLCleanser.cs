﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NovenaLibrary.SqlGenerators
{
    public static class SQLCleanser
    {
        public static string[] WordsNeedingEscaping = new string[] { "'", "/", ";", @"""", "DROP", "CREATE", "DELETE", "INSERT", "UPDATE" };
        public static string[] WordsNeedingRemoval = new string[] { "-" };
        public static string[] SubQueryWords = new string[] { ";", "/", ";", "DROP", "CREATE", "DELETE", "INSERT", "UPDATE" };


        public static string EscapeWords(string sql)
        {
            foreach (string word in WordsNeedingEscaping)
            {
                sql = Regex.Replace(sql, word, word + word, RegexOptions.IgnoreCase);
            }

            return sql;
        }

        public static string RemoveWords(string sql)
        {
            foreach (string word in WordsNeedingRemoval)
            {
                sql = Regex.Replace(sql, word, string.Empty, RegexOptions.IgnoreCase);
            }

            return sql;
        }

        public static string EscapeAndRemoveWords(string sql)
        {
            sql = EscapeWords(sql);
            sql = RemoveWords(sql);
            return sql;
        }
    }
}

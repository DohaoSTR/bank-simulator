using System;
using System.Collections.Generic;
using System.Text;

namespace BankModel.Model
{
    class DataBase
    {
        public static string ConnectionString
        {
            get
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\netcoreapp3.1\", "");
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\TestBase.mdf" + ";Integrated Security=True";
            }
        }
    }
}

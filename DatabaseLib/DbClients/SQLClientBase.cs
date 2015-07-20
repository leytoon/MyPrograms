using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using DatabaseLib.Interfaces;

namespace DatabaseLib.DBClients
{
    public abstract class DbClientBase : IDbClient
    {
        protected string DbName;
        protected string DbAddress; //For other kind of DataBases
        protected string DbUserName;
        protected string DbPassword;
        
        public DbClientBase(string dbName, string dbUserName, string dbPassword, string dbAddress)
        {
            DbName = dbName;
            DbAddress = dbAddress;
            DbUserName = dbUserName;
            DbPassword = dbPassword;
        }
       
        public abstract DataSet GetData(string Querry);
        public abstract int SetData(string Querry);


    }
}

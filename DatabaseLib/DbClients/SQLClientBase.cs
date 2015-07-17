using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using DatabaseLib.Interfaces;

namespace DatabaseLib.DBClients
{
    public abstract class SQLClientBase : IDbClient
    {
        protected string DbName;
        protected string DbAddress; //For other kind of DataBases
        protected string DbUserName;
        protected string DbPassword;
        
        public SQLClientBase(string _dbName, string _dbUserName, string _dbPassword, string _dbAddress)
        {
            DbName = _dbName;
            DbAddress = _dbAddress;
            DbUserName = _dbUserName;
            DbPassword = _dbPassword;
        }
       
        public abstract DataSet GetData(string Querry);
        public abstract int SetData(string Querry);


    }
}

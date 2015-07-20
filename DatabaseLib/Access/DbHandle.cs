using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib.Interfaces;

namespace DatabaseLib.Access
{
    public class DbHandle : IDbHandle
    {
        protected IDbClient DbClient;
        public DbHandle(IDbClient dbClient) 
        {
            this.DbClient = dbClient;
        }

        public DataSet GetData(string querry)
        {
            return DbClient.GetData(querry);
        }
        public int InsertData(string querry)
        {
            return DbClient.SetData(querry);
        }
        public int UpdateData(string querry)
        {
            throw new NotImplementedException();
        }
        public int DeleteData(string querry)
        {
            throw new NotImplementedException();
        }
    }
}

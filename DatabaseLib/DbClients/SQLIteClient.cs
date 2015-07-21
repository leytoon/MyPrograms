using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.DBClients
{
    public class SQLIteClient : DbClientBase
    {

        private string ConnectionString = "Data Source={0}{1}{2}{3};";
        private SQLiteConnection Connection;

        public SQLIteClient(string dbName, string dbUserName, string dbPassword, string dbAddress)
            : base(dbName, dbUserName, dbPassword, dbAddress)
        {
            this.ConnectionString = String.Format(ConnectionString, dbAddress, "", "", "", "");
        }

        public override DataSet GetData(string querry)
        {
            this.Connection = new SQLiteConnection(this.ConnectionString);

            var adapter = new SQLiteDataAdapter(new SQLiteCommand(querry, this.Connection));
            var result = new DataSet();

            this.Connection.Open();
            adapter.Fill(result);
            this.Connection.Dispose();
            return result;
        }

        public override int SetData(string querry)
        {
            this.Connection = new SQLiteConnection(this.ConnectionString);

            var sqliteCommand = new SQLiteCommand(querry, this.Connection);

            this.Connection.Open();
            
            return sqliteCommand.ExecuteNonQuery();
        }
    }
}

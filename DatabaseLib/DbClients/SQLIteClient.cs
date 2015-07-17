using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.DBClients
{
    public class SQLIteClient : SQLClientBase
    {

        private string ConnectionString = "Data Source={0}{1}{2}{3};";
        private SQLiteConnection Connection;
        
        public SQLIteClient(string name, string userName, string dbaddress, string dbpassword)
            : base(name, userName, dbaddress, dbpassword)
        {
            this.ConnectionString = String.Format(ConnectionString, dbaddress, "", "", "", "");
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

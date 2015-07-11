using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Interfaces
{
    public interface IDbClient
    {
        DataSet GetData(string query);
        int SetData(string query);
        int UpdateData(string query);
        int DeleteData(string query);

    }
}

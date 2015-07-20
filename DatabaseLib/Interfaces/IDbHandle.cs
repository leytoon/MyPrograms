using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Interfaces
{
    public interface IDbHandle
    {
        DataSet GetData(string querry);
        int InsertData(string querry);
        int UpdateData(string querry);
        int DeleteData(string querry);
    }
}

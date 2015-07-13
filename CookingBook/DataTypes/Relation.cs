using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.DataTypes
{
    public class Relation
    {
       public int Id;
       public int IdComp;
       public int IdRec;
       public String Amount;
        public Relation(DataRow row)
        {
            Id = Convert.ToInt32(row.ItemArray[0]);
            IdComp = Convert.ToInt32(row.ItemArray[1]);
            IdRec = Convert.ToInt32(row.ItemArray[2]);
            Amount = row.ItemArray[2].ToString();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.DataTypes
{
    public class Component : CookBookDataTypeBase
    {

        public string Value { get; set; }

        public string Amount { get; set; }
        public Component(int _id, string _name, string _value="0", string _amount = "0")
        {
            this.Id = _id;
            this.Name = _name;
            this.Value = _value;
            this.Amount = _amount;
            
        }
        public Component(string _name, string _value, string _amount = "0")
        {
            this.Name = _name;
            this.Value = _value;
            this.Amount = _amount;
        }
        public Component(DataRow row, string _amount = "0")
        {
            this.Id = Convert.ToInt32(row.ItemArray[0]);
            this.Name = row.ItemArray[1].ToString();
            this.Value = row.ItemArray[2].ToString();
            this.Amount = _amount;
        }

    }
}

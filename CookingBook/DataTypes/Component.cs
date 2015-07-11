using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.DataTypes
{
    class Component
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public Single Value { get; set; }
       

        public Component(int _id, string _name, Single _value)
        {
            this.Id = _id;
            this.Name = _name;
            this.Value = _value;
            
        }
        public Component(string _name, Single _value)
        {
            this.Name = _name;
            this.Value = _value;
        }
    }
}

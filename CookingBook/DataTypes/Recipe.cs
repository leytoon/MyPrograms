using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace CookingBook.DataTypes
{   
    public class Recipe
    {  
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string RecipeTxt { get; set; }
        public int Persons { get; set; }
        public string TypeOfDish { get; set; }
        public Recipe() { }
        public Recipe(int _id, string _name, string _recipe, int _persons = 1)
        {
            this.Id = _id;
            this.Name = _name;
            this.RecipeTxt = _recipe;
            this.Persons = _persons;
        }
        public Recipe(string _name, string _recipe, int _persons = 1)
        {

            this.Name = _name;
            this.RecipeTxt = _recipe;
            this.Persons = _persons;
        }
        public Recipe(DataRow row)
        {
            this.Id = Convert.ToInt32(row.ItemArray[0]);
            this.Name = row.ItemArray[1].ToString();
            this.RecipeTxt = row.ItemArray[2].ToString();
            this.Persons = Convert.ToInt32(row.ItemArray[3]);
            this.TypeOfDish = row.ItemArray[4].ToString();
        }
    }
}

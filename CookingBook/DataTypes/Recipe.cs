using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.DataTypes
{
    class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RecipeTxt { get; set; }
        public int Persons { get; set; }

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
    }
}

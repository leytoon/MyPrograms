using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
namespace CookingBook.DataTypes
{
    
    public class RecipiesTable
    { 
       
        public string dName { get; set; }
        public string RecipeTxt { get; set; }
        public int Persons { get; set; }
        public RecipiesTable() { }
    }
}

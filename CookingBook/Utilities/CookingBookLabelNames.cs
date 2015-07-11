using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.Utilities
{
    public class CookingBookLabelNames
    {
        public string Action { get; set; }
        public string Menu { get; set; }
        public string Main { get; set; }
        public string Window { get; set; }
        public string Add { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
        public string Recipe { get; set; }
        public string Component { get; set; }
        public string Price { get; set; }
        public string Name { get; set; }

        public string AddRecipieWindow 
        {
            get
            {
                return this.Add + this.Recipe + this.Window;
            }
        }

        public string AddComponentWindow 
        { 
            get {
                return this.Add + this.Component + this.Window;
            }
        }
        public string AddComponent
        {
            get
            {
                return this.Add + " "+this.Component;
            }
        }

        public string RecipeName
        {
            get
            {
                return this.Recipe + " " + this.Name;
            }
        }


        }
    
}

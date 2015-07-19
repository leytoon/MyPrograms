using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.Utilities
{
    public class CookingBookLabelNames
    {
        public string Chosen { get; set; }
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
        public string Amount { get; set; }
        public string Search { get; set; }
        public string Persons { get; set; }
        public string Refresh { get; set; }
        public string All { get; set; }
        public string Base { get; set; }
        public string Server { get; set; }
        public string Migration{ get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string FromBaseToBase
        {
            get
            {
                return this.Base + "-" + this.Base;
            }
        }

        public string FromBaseToServer
        {
            get
            {
                return this.Base + "-" + this.Server;
            }
        }

        public string FromServerToBase
        {
            get
            {
                return this.Server + "-" + this.Base;
            }
        }
            
        public string RefreshAll
        {
            get
            {
                return this.Refresh + " " + this.All;
            }
        }
        public string DeleteComponent
        {
            get
            {
                return this.Delete + " " + this.Component;
            }
        }
        public string SearchRecipie
        {
            get
            {
                return this.Search + " " + this.Recipe;
            }
        }
        public string SearchComponent
        {
            get
            {
                return this.Search + " " + this.Component;
            }
        }
        public string AddRecipie 
        {
            get
            {
                return this.Add + " "+this.Recipe;
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
        public string ChosenReciepe
        {
            get
            {
                return this.Chosen + " " + this.Recipe;
            }
        }

        }
    
}

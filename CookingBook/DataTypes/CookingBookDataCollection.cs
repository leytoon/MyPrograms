using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib.DBClients;

namespace CookingBook.DataTypes
{
    public class CookingBookDataCollection  //Get and stores Collection of the data from database
    {
        SQLIteClient SQLCli;

        List<Recipe> ListOfRecipes = new List<Recipe>();
        List<Component> ListOfComponents = new List<Component>();
        List<Component> ChoosenComponents = new List<Component>();
        public CookingBookDataCollection(SQLIteClient dbCli)
        {
            SQLCli = dbCli;
        }

        public List<Recipe> GetFullRecipeList()
        {
            ListOfRecipes = new List<Recipe>();

            var data = SQLCli.GetData("select * from RecipiesTable");

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var Items = data.Tables[0].Rows[i].ItemArray;
                ListOfRecipes.Add(new Recipe(Convert.ToInt32(Items[0]), Items[1].ToString(),
                                    Items[2].ToString(), Convert.ToInt32(Items[3])));
               // ListOfRecipes.Add(new Recipe(data.Tables[0].Rows[i].ItemArray));//pomśleć o konstruktorze
            }
            return ListOfRecipes;
        }

        public List<Component> GetComponentList(Recipe selectedRecipe)//Making list of Components included in given Recipe
        {
            //String SQLquerry = "SELECT ResourcesTable.Idres, ResourcesTable.Resource, ResourcesTable.Value FROM ResourcesTable NATURAL JOIN RelationsTable WHERE relationsTable.RecipeId='" + selectedRecipe.Id + "'AND RelationsTable.ComponentId=ResourcesTable.Idres";
            String SQLquerry = "SELECT ResourcesTable.Idres, ResourcesTable.Resource, ResourcesTable.Value FROM ResourcesTable" +
           " NATURAL JOIN RelationsTable WHERE relationsTable.RecipeId='" + selectedRecipe.Id +
           "'AND RelationsTable.ComponentId=ResourcesTable.Idres ";

            var data = SQLCli.GetData(SQLquerry);

            SQLquerry = "SELECT RelationsTable.Amount FROM RelationsTable WHERE RelationsTable.RecipeId='" + selectedRecipe.Id + "'";// + "'AND RelationsTable.ComponentId=ResourcesTable.Idres ";
            var amountT = SQLCli.GetData(SQLquerry);
            List<Component> ListOfAllComponents = new List<Component>();


            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                object[] amount = amountT.Tables[0].Rows[i].ItemArray;
                object[] items = data.Tables[0].Rows[i].ItemArray;
                ListOfAllComponents.Add(new Component(Convert.ToInt32(items[0]), items[1].ToString(), items[2].ToString(), amount[0].ToString()));
            }

            return ListOfAllComponents;
        }

        public List<Component> GetFullComponentList()// Making list of all components
        {
            var data = SQLCli.GetData("select * from ResourcesTable");

            //ListOfComponents.Clear();

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var Items = data.Tables[0].Rows[i].ItemArray;
                ListOfComponents.Add(new Component(Convert.ToInt32(Items[0]), Items[1].ToString(), Items[2].ToString()));
            }
            return ListOfComponents;
        }

        /*public List<Recipe> GetFullRecipeList(Recipe Lista)
        {
            ListOfRecipes = new List<Recipe>();

            var data = SQLCli.GetData("select * from RecipiesTable");

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var Items = data.Tables[0].Rows[i].ItemArray;
                ListOfRecipes.Add(new Recipe(Convert.ToInt32(Items[0]), Items[1].ToString(),
                                    Items[2].ToString(), Convert.ToInt32(Items[3])));
                // ListOfRecipes.Add(new Recipe(data.Tables[0].Rows[i].ItemArray));//pomśleć o konstruktorze
            }
            return ListOfRecipes;
        }


        public List<Component> GetComponentList(Recipe selectedRecipe)//Making list of Components included in given Recipe
        {
            //String SQLquerry = "SELECT ResourcesTable.Idres, ResourcesTable.Resource, ResourcesTable.Value FROM ResourcesTable NATURAL JOIN RelationsTable WHERE relationsTable.RecipeId='" + selectedRecipe.Id + "'AND RelationsTable.ComponentId=ResourcesTable.Idres";
            String SQLquerry = "SELECT ResourcesTable.Idres, ResourcesTable.Resource, ResourcesTable.Value FROM ResourcesTable" +
           " NATURAL JOIN RelationsTable WHERE relationsTable.RecipeId='" + selectedRecipe.Id +
           "'AND RelationsTable.ComponentId=ResourcesTable.Idres ";

            var data = SQLCli.GetData(SQLquerry);

            SQLquerry = "SELECT RelationsTable.Amount FROM RelationsTable WHERE RelationsTable.RecipeId='" + selectedRecipe.Id + "'";// + "'AND RelationsTable.ComponentId=ResourcesTable.Idres ";
            var amountT = SQLCli.GetData(SQLquerry);
            List<Component> ListOfAllComponents = new List<Component>();


            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                object[] amount = amountT.Tables[0].Rows[i].ItemArray;
                object[] items = data.Tables[0].Rows[i].ItemArray;
                ListOfAllComponents.Add(new Component(Convert.ToInt32(items[0]), items[1].ToString(), items[2].ToString(), amount[0].ToString()));
            }

            return ListOfAllComponents;
        }

        public List<Component> GetFullComponentList()// Making list of all components
        {
            var data = SQLCli.GetData("select * from ResourcesTable");

            ListOfComponents.Clear();

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var Items = data.Tables[0].Rows[i].ItemArray;
                ListOfComponents.Add(new Component(Convert.ToInt32(Items[0]), Items[1].ToString(), Items[2].ToString()));
            }
            return ListOfComponents;
        }*/
        
    }
}

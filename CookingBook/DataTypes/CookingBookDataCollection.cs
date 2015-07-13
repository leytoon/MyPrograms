using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib.DBClients;

namespace CookingBook.DataTypes
{
    public class CookingBookDataCollection  //Get and stores Collection of the data from database
    {
        SQLIteClient SQLCli;

        public List<Recipe> ListOfRecipes = new List<Recipe>();
        public List<Component> ListOfComponents = new List<Component>();
        public List<Component> ChoosenComponents = new List<Component>();
        public List<Recipe> FilteredListOfRecipes = new List<Recipe>();
        public List<Relation> ListOfRelations;
        public CookingBookDataCollection(SQLIteClient dbCli)
        {
            SQLCli = dbCli;
            ListOfRelations = GetRelationList();
            
        }
        private List<Relation> GetRelationList()
        {
            ListOfRelations = new List<Relation>();

            DataSet relationsTable = new DataSet();

            relationsTable = SQLCli.GetData("SELECT * FROM RelationsTable");

            for (int i = 0; i < relationsTable.Tables[0].Rows.Count; i++)
                ListOfRelations.Add(new Relation(relationsTable.Tables[0].Rows[i]));
            
            return ListOfRelations;
        }
        public List<Recipe> GetFullRecipeList()
        {
            ListOfRecipes = new List<Recipe>();

            var data = SQLCli.GetData("select * from RecipiesTable");

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                ListOfRecipes.Add(new Recipe(data.Tables[0].Rows[i]));
            
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
                ListOfAllComponents.Add(new Component(data.Tables[0].Rows[i], amount[0].ToString()));
            }

            return ListOfAllComponents;
        }

        public List<Component> GetFullComponentList()// Making list of all components
        {
            var data = SQLCli.GetData("select * from ResourcesTable");

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                ListOfComponents.Add(new Component(data.Tables[0].Rows[i]));

            return ListOfComponents;
        }

        public List<Component> DeleteComponentFromList(Component selected)// Making list of all components
        {
            if (this.ListOfComponents.Count != 0)
                this.ListOfComponents.Remove(selected);
            
            return ListOfComponents;
        }
        public List<Component> DeleteComponentFromChosenList(Component selected)// Making list of all components
        {
            if (this.ChoosenComponents.Count != 0)
                this.ChoosenComponents.Remove(selected);

            return ChoosenComponents;
        }
        public List<Component> AddComponentToList(Component selected)// Making list of all components
        {
            this.ChoosenComponents.Add(selected);

            return ChoosenComponents;
        }
        //FilterRecipeList Pomyśl jak to usprawnić,=> MOŻE SŁOWNIKIEM TO ZROBIĘ!!! WTEDY WYSZUKIWANIA ZNIKNĄ!!!
        //Kluczami będą indexy z klas 
        public List<Recipe> FilterRecipeList(int rConstrain, int pConstrain, int tConstrain)// Making list of all components
        {
            int[,] conection = new int[2, ListOfRecipes.Count];

           /* for (int i = 0; i < ListOfRecipes.Count; i++)//Creating table
            {
                conection[1,i] = 0;
                conection[0, i] = ListOfRecipes[i].Id;
            }*/
            
            
            for(int i=0; i<ListOfRelations.Count; i++)//Searching maches in Reciepes
            {
                for (int j = 0; j < ChoosenComponents.Count; j++)
                {
                    if (ListOfRelations[i].IdComp == ChoosenComponents[j].Id)
                        for (int k = 0; k < ListOfRecipes.Count; k++)
                        {
                            conection[1,k] = 0;//Preparing table
                            conection[0, k] = ListOfRecipes[k].Id;//Preparing table

                            if (ListOfRecipes[k].Id == ListOfRelations[i].IdRec)
                                conection[1,k]++;
                        } 

                }
            }

            for (int k = 0; k < ListOfRecipes.Count; k++)//Add mached recipes to list
                if()
                FilteredListOfRecipes.Add(ListOfRecipes[conection[0,k]]);

            return FilteredListOfRecipes;
        }

    }
}

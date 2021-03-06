﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib.DBClients;
using DatabaseLib.Interfaces;

namespace CookingBook.DataTypes
{
    public class CookingBookDataCollection  //Get and stores Collection of the data from database
    {
        IDbHandle DbCli;

        public List<Recipe> ListOfRecipes = new List<Recipe>();
        public List<Component> ListOfComponents = new List<Component>(); 
        public List<Component> ChoosenComponents = new List<Component>();
        public List<Recipe> FilteredListOfRecipes = new List<Recipe>();
        public List<Relation> ListOfRelations;
        public CookingBookDataCollection(IDbHandle dbCli)
        {
            DbCli = dbCli;
            ListOfRelations = GetRelationList();
            
        }
        public void GetAll() 
        {
            GetRelationList();
            GetFullRecipeList();
            GetFullComponentList();
        }
        public List<Relation> GetRelationList()
        {
            ListOfRelations = new List<Relation>();

            DataSet relationsTable = new DataSet();

            relationsTable = DbCli.GetData("SELECT * FROM RelationsTable");

            for (int i = 0; i < relationsTable.Tables[0].Rows.Count; i++)
                ListOfRelations.Add(new Relation(relationsTable.Tables[0].Rows[i]));
            
            return ListOfRelations;
        }
        public List<Recipe> GetFullRecipeList()
        {
            ListOfRecipes = new List<Recipe>();

            var data = DbCli.GetData("SELECT * FROM RecipiesTable");

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                ListOfRecipes.Add(new Recipe(data.Tables[0].Rows[i]));
            
            return ListOfRecipes;
        }

        public List<Component> GetComponentList(Recipe selectedRecipe)//Making list of Components included in given Recipe
        {
            var data = DbCli.GetData(string.Format("SELECT ResourcesTable.Idres, ResourcesTable.Resource, ResourcesTable.Value " +
                        "FROM ResourcesTable" +
                        " NATURAL JOIN RelationsTable WHERE relationsTable.RecipeId='{0}'"+
                        "AND RelationsTable.ComponentId=ResourcesTable.Idres", selectedRecipe.Id));
            // Select those components what are included in recipe

            var amountT = DbCli.GetData("SELECT RelationsTable.Amount FROM RelationsTable WHERE RelationsTable.RecipeId='" + selectedRecipe.Id + "'");
            
            List<Component> ListOfIncludedComponents = new List<Component>();

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                object[] amount = amountT.Tables[0].Rows[i].ItemArray;
                ListOfIncludedComponents.Add(new Component(data.Tables[0].Rows[i], amount[0].ToString()));
            }

            return ListOfIncludedComponents;
        }

        public List<Component> GetFullComponentList()// Making list of all components
        {
            ListOfComponents.Clear();

            var data = DbCli.GetData("select * from ResourcesTable");

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                ListOfComponents.Add(new Component(data.Tables[0].Rows[i]));

            return ListOfComponents;
        }

        public List<Component> DeleteComponentFromList(Component selected)
        {
            if (this.ListOfComponents.Count != 0)
                this.ListOfComponents.Remove(selected);
            
            return ListOfComponents;
        }
        public List<Component> DeleteComponentFromChosenList(Component selected)
        {
            if (this.ChoosenComponents.Count != 0)
                this.ChoosenComponents.Remove(selected);

            return ChoosenComponents;
        }
        public List<Component> AddComponentToList(Component selected)
        {
            this.ChoosenComponents.Add(selected);

            return ChoosenComponents;
        }

        //FilterRecipeList Pomyśl jak zmniejszyć ilość kodu, ta metoda działa popawnie
        public List<Recipe> FilterRecipeList(int cConstrain, int pConstrain, int tConstrain)// Making list of all components
        {
            int[,] conection = new int[2, ListOfRecipes.Count];
            FilteredListOfRecipes.Clear();

            for(int i=0; i<ListOfRelations.Count; i++)//Searching maches in Reciepes
            {
                for (int j = 0; j < ChoosenComponents.Count; j++)
                {
                    if (ChoosenComponents[j].Id!=null && ListOfRelations[i].IdComp == ChoosenComponents[j].Id)
                        for (int k = 0; k < ListOfRecipes.Count; k++)
                        {
                            conection[0, k] = ListOfRecipes[k].Id;//Preparing table

                            if (ListOfRecipes[k].Id == ListOfRelations[i].IdRec)
                                conection[1,k]++;
                        } 
                }
            }

            for (int j = 0; j < ListOfRecipes.Count; j++)//Add mached recipes to list
            {
                for (int k = 0; k < ListOfRecipes.Count; k++)
                {
                    if (conection[0, j] == ListOfRecipes[k].Id && conection[1, j] >= cConstrain 
                        && ListOfRecipes[k].Persons >= pConstrain
                        && ListOfRecipes[k].Id >= 0)//<-here goes time constrain|ListOfRecipes[k].PreparingTime <= tConstrain|
                    {
                        FilteredListOfRecipes.Add(ListOfRecipes[k]);
                    }
                }
            }

            return FilteredListOfRecipes;
        }

     

    }
}

// //String SQLquerry = "SELECT ResourcesTable.Idres, ResourcesTable.Resource, ResourcesTable.Value FROM ResourcesTable NATURAL JOIN RelationsTable WHERE relationsTable.RecipeId='" + selectedRecipe.Id + "'AND RelationsTable.ComponentId=ResourcesTable.Idres";
// String SQLquerry = "SELECT ResourcesTable.Idres, ResourcesTable.Resource, ResourcesTable.Value FROM ResourcesTable" +
//" NATURAL JOIN RelationsTable WHERE relationsTable.RecipeId='" + selectedRecipe.Id +
//"'AND RelationsTable.ComponentId=ResourcesTable.Idres ";

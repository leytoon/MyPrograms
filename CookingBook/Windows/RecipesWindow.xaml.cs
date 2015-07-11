using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DatabaseLib.DBClients;
using CookingBook.DataTypes;
using CookingBook.Utilities;

namespace CookingBook.Windows
{

    public partial class RecipesWindow : Window
    {
        List<Recipe> ListOfReciepes = new List<Recipe>();
        List<Component> ListOfComponents = new List<Component>();

        SQLIteClient SQLCli = null;
        Recipe SelectedRecipe;
        bool FillNamesList()
        {
            // RecipeList = new List<Recipe>();
            var data = SQLCli.GetData("select * from RecipiesTable");

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var Items = data.Tables[0].Rows[i].ItemArray;
                ListOfReciepes.Add(new Recipe(Convert.ToInt32(Items[0]), Items[1].ToString(), 
                                    Items[2].ToString(), Convert.ToInt32(Items[3])));
            }
            
            RecipeListViev.ItemsSource = ListOfReciepes;
            
            return true;
        }

        List<Component> FillComponentsList(Recipe selectedRecipe)//Making list of Components included in Recipe
        {
            String SQLquerry = "SELECT ResourcesTable.Idres, ResourcesTable.Resource, ResourcesTable. Value FROM ResourcesTable NATURAL JOIN RelationsTable WHERE relationsTable.RecipeId='" + selectedRecipe.Id + "'AND RelationsTable.ComponentId=ResourcesTable.Idres";
            var data = SQLCli.GetData(SQLquerry);
            ListOfComponents.Clear();

            
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var Items = data.Tables[0].Rows[i].ItemArray;
                ListOfComponents.Add(new Component(Convert.ToInt32(Items[0]), Items[1].ToString(), Convert.ToSingle(Items[2])));
            }

            return ListOfComponents;
        }

        List<Component> FillComponentsList()//Przerobić logikę tego czegoś po tym, jak zrobisz metodę wyżej. Pomyśl o zrobieniu jednej metody od
        {
            // RecipeList = new List<Recipe>();
            var data = SQLCli.GetData("select * from RecipiesTable");
            
            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var Items = data.Tables[0].Rows[i].ItemArray;
                ListOfReciepes.Add(new Recipe(Convert.ToInt32(Items[0]), Items[1].ToString(), Items[2].ToString(), Convert.ToInt32(Items[3])));

            }

            RecipeListViev.ItemsSource = ListOfReciepes;

            return ListOfComponents;

        }

        public RecipesWindow()
        {
            InitializeComponent();

            SQLCli = new SQLIteClient("", "", MainWindow.DbPath, "");

            RecipeListViev.DataContext = CookinBookDictionary.Instance.GetNames(MainWindow.SelectedLanguage);
            MainGrid.DataContext = CookinBookDictionary.Instance.GetNames(MainWindow.SelectedLanguage);
            ComponentsInViev.DataContext = CookinBookDictionary.Instance.GetNames(MainWindow.SelectedLanguage);

            FillNamesList();
        }
        
        private void getSelectedItem(object sender, MouseButtonEventArgs e)//Get selected Reciepe from list
        {
            var item = sender as ListView;

            SelectedRecipe = (Recipe)item.SelectedItems[0];

            RecipeRichTextBox.SelectAll();
            RecipeRichTextBox.Selection.Text = SelectedRecipe.RecipeTxt;

            ComponentsInViev.ItemsSource = null;
            
            ComponentsInViev.ItemsSource = FillComponentsList(SelectedRecipe); //Filling List of Components 
                                                                               //included in Reciepe
        }

        private void RecipeListViev_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

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
using System.Globalization;

namespace CookingBook.Windows
{

    public partial class RecipesWindow : Window
    {
       
        CookingBookDataCollection DataCollection;

        SQLIteClient SQLCli = null;
        
        Recipe SelectedRecipe;
       /* List<Recipe> FillNamesList()
        {
            ListOfReciepes = new List<Recipe>();

            var data = SQLCli.GetData("select * from RecipiesTable");

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var Items = data.Tables[0].Rows[i].ItemArray;
                ListOfReciepes.Add(new Recipe(Convert.ToInt32(Items[0]), Items[1].ToString(), 
                                    Items[2].ToString(), Convert.ToInt32(Items[3])));
            }
            return ListOfReciepes;
        }

        
        List<Component> FillComponentsList(Recipe selectedRecipe)//Making list of Components included in Recipe
        {
            //String SQLquerry = "SELECT ResourcesTable.Idres, ResourcesTable.Resource, ResourcesTable.Value FROM ResourcesTable NATURAL JOIN RelationsTable WHERE relationsTable.RecipeId='" + selectedRecipe.Id + "'AND RelationsTable.ComponentId=ResourcesTable.Idres";
            String SQLquerry = "SELECT ResourcesTable.Idres, ResourcesTable.Resource, ResourcesTable.Value FROM ResourcesTable" +
           " NATURAL JOIN RelationsTable WHERE relationsTable.RecipeId='" + selectedRecipe.Id +
           "'AND RelationsTable.ComponentId=ResourcesTable.Idres ";
           
            var data = SQLCli.GetData(SQLquerry);

            SQLquerry = "SELECT RelationsTable.Amount FROM RelationsTable WHERE RelationsTable.RecipeId='" + selectedRecipe.Id + "'";// + "'AND RelationsTable.ComponentId=ResourcesTable.Idres ";
            var amountT = SQLCli.GetData(SQLquerry);
            List<Component> ListOfAllComponents=new List<Component>();
            

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                object[] amount = amountT.Tables[0].Rows[i].ItemArray;
                object[] items = data.Tables[0].Rows[i].ItemArray;
                ListOfAllComponents.Add(new Component(Convert.ToInt32(items[0]), items[1].ToString(), items[2].ToString(), amount[0].ToString()));
            }

            return ListOfAllComponents;
        }

        List<Component> FillComponentsList()//Przerobić logikę tego czegoś po tym, jak zrobisz metodę wyżej. Pomyśl o zrobieniu jednej metody od
        {
            // RecipeList = new List<Recipe>();
            var data = SQLCli.GetData("select * from ResourcesTable");

            ListOfComponents.Clear();


            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var Items = data.Tables[0].Rows[i].ItemArray;
                ListOfComponents.Add(new Component(Convert.ToInt32(Items[0]), Items[1].ToString(), Items[2].ToString()));
            }

            return ListOfComponents;

        }*/

        public RecipesWindow()
        {
            InitializeComponent();

            SQLCli = new SQLIteClient("", "", MainWindow.DbPath, "");

            DataCollection = new CookingBookDataCollection(SQLCli);

            MainDockPanel.DataContext = CookinBookDictionary.Instance.GetNames(MainWindow.SelectedLanguage);
           
            RecipeListViev.ItemsSource = DataCollection.GetFullRecipeList();
            AllComponentsViev.ItemsSource = DataCollection.GetFullComponentList();

            CollectionView RecipeViev = (CollectionView)CollectionViewSource.GetDefaultView(RecipeListViev.ItemsSource);
            RecipeViev.Filter = (item => (String.IsNullOrEmpty(RecipeFilterText.Text) ? true : ((item as Recipe).Name.IndexOf(RecipeFilterText.Text, StringComparison.OrdinalIgnoreCase) >= 0)));
            CollectionView ComponentViev = (CollectionView)CollectionViewSource.GetDefaultView(AllComponentsViev.ItemsSource);
            ComponentViev.Filter = (item => (String.IsNullOrEmpty(ComponentsFilterText.Text) ? true : ((item as Component).Name.IndexOf(ComponentsFilterText.Text, StringComparison.OrdinalIgnoreCase) >= 0)));

        }
       
        private void getSelectedRecipe(object sender, MouseButtonEventArgs e)//Get selected Reciepe from list
        {
            var item = sender as ListView;

            SelectedRecipe = (Recipe)item.SelectedItems[0];
            
            
            ChosenRecipeRichTextBox.Selection.Text = SelectedRecipe.RecipeTxt;
            ChosenNameTextBlock.Text = SelectedRecipe.Name;
            ComponentsInViev.ItemsSource = null;
            
            ComponentsInViev.ItemsSource = DataCollection.GetComponentList(SelectedRecipe); //Filling List of Components 
                                                                               //included in Reciepe 
        }
        private void AddComponentToReciepeList(object sender, RoutedEventArgs e)
        {
            if (SelectedRecipe != null && TxtValidator.IsAmountValid(AmountOfComponentTextBox.Text.Replace(".", ",")))
            {
                var SR = (Recipe)RecipeListViev.SelectedItems[0];//SelectedRecipe
                var SC = (Component)AllComponentsViev.SelectedItem;//SelectedComponent
               
                string querry = "INSERT INTO RelationsTable (ComponentId,RecipeId,Amount)VALUES('" + SC.Id + "','" + SR.Id + "','" +AmountOfComponentTextBox.Text.Replace(".",",") + "')";
                SQLCli.SetData(querry);

                ComponentsInViev.ItemsSource = null;

                ComponentsInViev.ItemsSource = DataCollection.GetComponentList(SelectedRecipe); //Filling List of Components 
                                                                                   //included in Reciepe 
            }
            else
            {
                MessageBox.Show("Wybierz Przepis");
            }
        }

        private void DeleteComponentFromReciepeList(object sender, RoutedEventArgs e) 
        {

            if (ComponentsInViev.Items.Count != 0)
            {
                var SR = (Recipe)RecipeListViev.SelectedItems[0];//SelectedRecipe
                var SC = (Component)ComponentsInViev.SelectedItem;//SelectedComponent

                string querry = "DELETE FROM RelationsTable WHERE ComponentId='" + SC.Id + "'";
                SQLCli.SetData(querry);

                ComponentsInViev.ItemsSource = null;

                ComponentsInViev.ItemsSource = DataCollection.GetComponentList(SelectedRecipe); //Filling List of Components 
                                                                                   //included in Reciepe 
            }
            else
            {
                MessageBox.Show("Wybierz Skladnik");
            }
        }

        private void AddNewRecipe (object sender, RoutedEventArgs e) 
        {
            ChosenRecipeRichTextBox.SelectAll();
           
            string querry = "INSERT INTO RecipiesTable (Name,Recipe,Persons)VALUES('" + RecipeNameTextBox.Text + "','" + ChosenRecipeRichTextBox.Selection.Text + "','" + NumberOfPeopleTextBox.Text + "')";
            SQLCli.SetData(querry);
            RecipeListViev.ItemsSource = null;
            RecipeListViev.ItemsSource = DataCollection.GetFullRecipeList();

        }

        private void UpdateRecipe(object sender, RoutedEventArgs e) 
        { 


        }
        private void DeleteRecipe(object sender, RoutedEventArgs e)
        {
            SQLCli.DeleteData("DELETE FROM RecipiesTable WHERE Id='" + SelectedRecipe.Id + "'");
            SQLCli.DeleteData("DELETE FROM RelationsTable WHERE RecipeId='" + SelectedRecipe.Id + "'");
            RecipeListViev.ItemsSource = null;
            RecipeListViev.ItemsSource = DataCollection.GetFullRecipeList();
        }
        private void ComponentsFilterText_Changed(object sender, TextChangedEventArgs e)
        { 
            CollectionViewSource.GetDefaultView(AllComponentsViev.ItemsSource).Refresh(); 
        }

        private void RecipeFilterText_Changed(object sender, TextChangedEventArgs e)
        { 
            CollectionViewSource.GetDefaultView(RecipeListViev.ItemsSource).Refresh(); 
        }
    }
}

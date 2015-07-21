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
using DatabaseLib.Interfaces;

namespace CookingBook.Windows
{

    public partial class RecipesWindow : Window
    {
       
        CookingBookDataCollection DataCollection;

        IDbHandle DbCli = null;
        
        Recipe SelectedRecipe;

        public RecipesWindow(IDbHandle dbCli)
        {
            InitializeComponent();

            DbCli = dbCli;

            DataCollection = new CookingBookDataCollection(DbCli);

            MainGrid.DataContext = CookinBookDictionary.Instance.GetNames(MainWindow.SelectedLanguage);
           
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

            if (item.SelectedItems.Count != 0)
            {
                SelectedRecipe = (Recipe)item.SelectedItems[0];

                ChosenRecipeRichTextBox.Selection.Text = SelectedRecipe.RecipeTxt;
                ChosenNameTextBlock.Text = SelectedRecipe.Name;
                ComponentsInViev.ItemsSource = null;

                ComponentsInViev.ItemsSource = DataCollection.GetComponentList(SelectedRecipe); //Filling List of Components 
            }                                                                  //included in Reciepe 
        }
        private void AddComponentToReciepeList(object sender, RoutedEventArgs e)
        {
            //var SR = (Recipe)RecipeListViev.SelectedItems[0];//SelectedRecipe
            var SC = (Component)AllComponentsViev.SelectedItem;//SelectedComponent

            if (SelectedRecipe != null && SC!= null && TxtValidator.IsAmountValid(AmountOfComponentTextBox.Text.Replace(".", ",")))
            {
                DbCli.InsertData(string.Format("INSERT INTO RelationsTable (ComponentId,RecipeId,Amount)VALUES('{0}','{1}','{2}')", 
                    SC.Id, 
                    SelectedRecipe.Id, 
                    AmountOfComponentTextBox.Text.Replace(".", ",")));

                ComponentsInViev.ItemsSource = null;

                ComponentsInViev.ItemsSource = DataCollection.GetComponentList(SelectedRecipe); //Filling List of Components 
                                                                                   //included in Reciepe 
            }
            else
            {
                MessageBox.Show("Ilość się liczy");
            }
        }

        private void DeleteComponentFromReciepeList(object sender, RoutedEventArgs e) 
        {
            if (ComponentsInViev.SelectedItem != null && RecipeListViev.SelectedItem != null)
            {
                var SR = (Recipe)RecipeListViev.SelectedItems[0];//SelectedRecipe
                var SC = (Component)ComponentsInViev.SelectedItem;//SelectedComponent
      
                DbCli.InsertData(string.Format("DELETE FROM RelationsTable WHERE ComponentId='{0}'", SC.Id));

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
            if (RecipeNameTextBox.Text != "" && SQLInjectionParser.Parse(ChosenRecipeRichTextBox.Selection.Text + NumberOfPeopleTextBox.Text + RecipeNameTextBox.Text))
            {
                ChosenRecipeRichTextBox.SelectAll();

                DbCli.InsertData(string.Format("INSERT INTO RecipiesTable (Name,Recipe,Persons)VALUES('{0}','{1}','{2}')", 
                    RecipeNameTextBox.Text,
                    ChosenRecipeRichTextBox.Selection.Text,
                    NumberOfPeopleTextBox.Text));
                
                RecipeListViev.ItemsSource = null;
                RecipeListViev.ItemsSource = DataCollection.GetFullRecipeList();

                CollectionView RecipeViev = (CollectionView)CollectionViewSource.GetDefaultView(RecipeListViev.ItemsSource);
                RecipeViev.Filter = (item => (String.IsNullOrEmpty(RecipeFilterText.Text) ? true : ((item as Recipe).Name.IndexOf(RecipeFilterText.Text, StringComparison.OrdinalIgnoreCase) >= 0)));
            }
            else { MessageBox.Show("Niepoprawna nazwa"); }
        }

        private void UpdateRecipe(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Jeszcze nie zrobione.");
            CollectionView RecipeViev = (CollectionView)CollectionViewSource.GetDefaultView(RecipeListViev.ItemsSource);
            RecipeViev.Filter = (item => (String.IsNullOrEmpty(RecipeFilterText.Text) ? true : ((item as Recipe).Name.IndexOf(RecipeFilterText.Text, StringComparison.OrdinalIgnoreCase) >= 0)));

        }
        private void DeleteRecipe(object sender, RoutedEventArgs e)
        {
            if (SelectedRecipe != null)
            {
                DbCli.InsertData(string.Format("DELETE FROM RecipiesTable WHERE Id='{0}'", SelectedRecipe.Id));
                DbCli.InsertData(string.Format("DELETE FROM RelationsTable WHERE RecipeId='{0}'", SelectedRecipe.Id));
                
                RecipeListViev.ItemsSource = null;
                RecipeListViev.ItemsSource = DataCollection.GetFullRecipeList();

                CollectionView RecipeViev = (CollectionView)CollectionViewSource.GetDefaultView(RecipeListViev.ItemsSource);
                RecipeViev.Filter = (item => (String.IsNullOrEmpty(RecipeFilterText.Text) ? true : ((item as Recipe).Name.IndexOf(RecipeFilterText.Text, StringComparison.OrdinalIgnoreCase) >= 0)));
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CookingBook.DataTypes;
using CookingBook.Utilities;
using DatabaseLib.DBClients;

namespace CookingBook.Windows
{
    /// <summary>
    /// Interaction logic for RecipeSearchWindow.xaml
    /// </summary>
    public partial class SearchRecipeWindow : Window
    {   
        
        SQLIteClient SQLCli = null;
        
        Recipe SelectedRecipe;

        CookingBookDataCollection DataCollection;
        public SearchRecipeWindow()
        {
            InitializeComponent();

            SQLCli = new SQLIteClient("", "", MainWindow.DbPath, "");

            MainDockPanel.DataContext = CookinBookDictionary.Instance.GetNames(MainWindow.SelectedLanguage);

            DataCollection = new CookingBookDataCollection(SQLCli);

            RecipeListViev.ItemsSource = DataCollection.GetFullRecipeList();
            AllComponentsViev.ItemsSource = DataCollection.GetFullComponentList();

            CollectionView RecipeViev = (CollectionView)CollectionViewSource.GetDefaultView(RecipeListViev.ItemsSource);
            RecipeViev.Filter = (item => (String.IsNullOrEmpty(RecipeFilterText.Text) ? true : ((item as Recipe).Name.IndexOf(RecipeFilterText.Text, StringComparison.OrdinalIgnoreCase) >= 0)));

            CollectionView ComponentViev = (CollectionView)CollectionViewSource.GetDefaultView(AllComponentsViev.ItemsSource);
            ComponentViev.Filter = (item => (String.IsNullOrEmpty(ComponentsFilterText.Text) ? true : ((item as Component).Name.IndexOf(ComponentsFilterText.Text, StringComparison.OrdinalIgnoreCase) >= 0)));
        }
        private void getSelectedComponent(object sender, MouseButtonEventArgs e) 
        {
            RecipeListViev.SelectedItem = null;
            ComponentsInView.ItemsSource = DataCollection.ChoosenComponents;

        }
        private void getSelectedRecipe(object sender, MouseButtonEventArgs e)//Get selected Reciepe from list
        {
            var item = sender as ListView;
            if (item.SelectedItems[0] != null)
            {
                SelectedRecipe = (Recipe)item.SelectedItems[0];

                ChosenRecipeRichTextBox.Selection.Text = SelectedRecipe.RecipeTxt;


                AllComponentsViev.SelectedItem = null;
                ComponentsInView.ItemsSource = null;
                ComponentsInView.ItemsSource = DataCollection.GetComponentList(SelectedRecipe); //Filling List of Components 
            }                                                             //included in Reciepe 3
        }

        private void AddComponentToList(object sender, RoutedEventArgs e)
        {
            var SC = (Component)AllComponentsViev.SelectedItem;//SelectedComponent
               
            ComponentsInView.ItemsSource = null;
           
            ComponentsInView.ItemsSource = DataCollection.AddComponentToList(SC); //Filling List of Components 
             
        }

        private void DeleteComponentFromList(object sender, RoutedEventArgs e) 
        {
            if (ComponentsInView.Items.Count != 0)
            {
                var SC = (Component)ComponentsInView.SelectedItem;//SelectedComponent
                ComponentsInView.ItemsSource = null;
                ComponentsInView.ItemsSource = DataCollection.DeleteComponentFromChosenList(SC); //Filling List of Components 
                                                                                   //included in Reciepe
            }
            else
            {
                MessageBox.Show("Wybierz Skladnik");
            }
        }
        private void GetAllRecipes(object sender, RoutedEventArgs e)
        {
            RecipeListViev.ItemsSource = null;
            RecipeListViev.ItemsSource = DataCollection.ListOfRecipes;
            RecipeFilterText.Text = "";
        }
        private void ComponentsFilterText_Changed(object sender, TextChangedEventArgs e)
        { 
            CollectionViewSource.GetDefaultView(AllComponentsViev.ItemsSource).Refresh(); 
        }

        private void RecipeFilterText_Changed(object sender, TextChangedEventArgs e)
        { 
            CollectionViewSource.GetDefaultView(RecipeListViev.ItemsSource).Refresh(); 
        }
        private void FilteringReciepe(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tConstrain.Text))
                tConstrain.Text = "0";
            if (string.IsNullOrEmpty(pConstrain.Text))
                pConstrain.Text = "1";

            if (TxtValidator.IsPersonsValid(cConstrain.Text) && TxtValidator.IsIntegerValid(tConstrain.Text) &&
                TxtValidator.IsIntegerValid(pConstrain.Text))
            {
                RecipeListViev.ItemsSource = null;
                RecipeListViev.ItemsSource = DataCollection.FilterRecipeList(Convert.ToInt32(cConstrain.Text), 
                                                                            Convert.ToInt32(pConstrain.Text), 
                                                                            Convert.ToInt32(tConstrain.Text));
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
        }
    }
}

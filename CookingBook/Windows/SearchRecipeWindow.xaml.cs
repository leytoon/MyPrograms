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

        private void getSelectedRecipe(object sender, MouseButtonEventArgs e)//Get selected Reciepe from list
        {
            var item = sender as ListView;

            SelectedRecipe = (Recipe)item.SelectedItems[0];
            
            
            ChosenRecipeRichTextBox.Selection.Text = SelectedRecipe.RecipeTxt;
            ChosenNameTextBlock.Text = SelectedRecipe.Name;
            
            ComponentsInViev.ItemsSource = null;
            ComponentsInViev.ItemsSource = DataCollection.GetComponentList(SelectedRecipe); //Filling List of Components 
                                                                               //included in Reciepe 3
        }

        private void AddComponentToReciepeList(object sender, RoutedEventArgs e)
        {
            if (SelectedRecipe != null)
            {
                var SR = (Recipe)RecipeListViev.SelectedItems[0];//SelectedRecipe
                var SC = (Component)AllComponentsViev.SelectedItem;//SelectedComponent
               
                ComponentsInViev.ItemsSource = null;

                ComponentsInViev.ItemsSource = DataCollection.GetComponentList(SelectedRecipe); //Filling List of Components 
                                                                                   //included in Reciepe 4
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

                ComponentsInViev.ItemsSource = null;

                ComponentsInViev.ItemsSource = DataCollection.GetComponentList(SelectedRecipe); //Filling List of Components 
                                                                                   //included in Reciepe 5
            }
            else
            {
                MessageBox.Show("Wybierz Skladnik");
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

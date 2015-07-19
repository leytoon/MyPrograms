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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CookingBook.Windows;
using CookingBook.Utilities;
using CookingBook.DataTypes;

namespace CookingBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public static string SelectedLanguage;
        public static string DbPath = @".\SqlDB.db";// @"D:\SqlDB.db"; //@".\SqlDB.db";
        public MainWindow()
        {
            InitializeComponent();
            
            SelectedLanguage = "PL";
            CookingBookLanguageSelect.ChangeLanuage(SelectedLanguage, this);
            
            
           /* RecipiesTable Expample = new RecipiesTable() {dName = "ddEntity Test", Persons = 2, RecipeTxt = "Test Entity Frameworka" };

            using (var context = new CookingBookDbContext())
            {
                context.ListOfRecipes.Add(Expample);
                context.SaveChanges();
            }*/
            
        }
        void ChangeToENG(object sender, RoutedEventArgs e)
        {
            SelectedLanguage = "ENG";
            CookingBookLanguageSelect.ChangeLanuage(SelectedLanguage, this);
        }
        void ChangeToPL(object sender, RoutedEventArgs e)
        {
            SelectedLanguage = "PL";
            CookingBookLanguageSelect.ChangeLanuage(SelectedLanguage, this);
        }
        void ShowAddComponentWindow(object sender, RoutedEventArgs e)
        {
            ComponentsWindow Window = new ComponentsWindow();
            Window.Title = CookinBookDictionary.Instance.GetNames(SelectedLanguage).AddComponent;
            Window.Show();
           
        }
        void ShowAddRecepieWindow(object sender, RoutedEventArgs e)
        {
            RecipesWindow Window = new RecipesWindow();
            Window.Title = CookinBookDictionary.Instance.GetNames(SelectedLanguage).AddRecipie;
            Window.Show();
        }
        void ShowSearchRecipeWindow(object sender, RoutedEventArgs e)
        {
            SearchRecipeWindow Window = new SearchRecipeWindow();
            Window.Title = CookinBookDictionary.Instance.GetNames(SelectedLanguage).SearchRecipie;
            Window.Show();
        }
       
        
    }
}

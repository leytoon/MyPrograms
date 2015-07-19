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
using DatabaseLib.DBClients;
using CookingBook.Utilities;
using CookingBook.DataTypes;
using DatabaseLib.Interfaces;
using Autofac;
using Autofac.Core;

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

            var builder = new ContainerBuilder();

            builder.RegisterType<SQLIteClient>().As<IDbClient>().WithParameters( new Parameter[] { 
                                  new NamedParameter("name", ""), 
                                  new NamedParameter("username", ""), 
                                  new NamedParameter("DbPath",DbPath), 
                                  new NamedParameter("dbaddress", ""),
                                  new NamedParameter("dbpassword", "")});

            var container = builder.Build();

            var service = container.Resolve<SQLIteClient>();
            
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

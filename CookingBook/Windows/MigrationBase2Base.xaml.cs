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
using DatabaseLib.Interfaces;
using DatabaseLib.DBClients;
using DatabaseLib.Access;
using CookingBook.DataTypes;
using CookingBook.Windows;

namespace CookingBook.Windows
{
    /// <summary>
    /// Interaction logic for MigrationBase2Base.xaml
    /// </summary>
    public partial class MigrationBase2Base : Window
    {
        DbHandle ForeignDB;
        IDbHandle MainDB;
        CookingBookDataCollection ForeignColection;
        CookingBookDataCollection MainColection;

       
        public MigrationBase2Base(IDbHandle dbCli)
        {
            InitializeComponent();

            MainDB = dbCli;

            MainColection = new CookingBookDataCollection(dbCli);
            
            MainColection.GetAll();

            MainDbListViev.ItemsSource = MainColection.ListOfRecipes;

            CollectionView MainRecipeViev = (CollectionView)CollectionViewSource.GetDefaultView(MainDbListViev.ItemsSource);
            MainRecipeViev.Filter = (item => (String.IsNullOrEmpty(MainRecipeFilterText.Text) ? true : ((item as Recipe).Name.IndexOf(MainRecipeFilterText.Text, StringComparison.OrdinalIgnoreCase) >= 0)));

        }

        private void OpenForeignDB(object sender, RoutedEventArgs e)
        {
            var DbFile = new Microsoft.Win32.OpenFileDialog() { Filter = "DataBase Files (*.db)|*.db" };
            var result = DbFile.ShowDialog();

            if ((bool)result)
            {
                var file = DbFile.FileName;

                ForeignDB = new DbHandle(new SQLIteClient("", "", "", @file));

                ForeignColection = new CookingBookDataCollection(ForeignDB);

                ForeignColection.GetAll();

                ForeignDbListViev.ItemsSource = ForeignColection.ListOfRecipes;

                CollectionView ForeignRecipeViev = (CollectionView)CollectionViewSource.GetDefaultView(ForeignDbListViev.ItemsSource);
                ForeignRecipeViev.Filter = (item => (String.IsNullOrEmpty(ForeignRecipeFilterText.Text) ? true : ((item as Recipe).Name.IndexOf(ForeignRecipeFilterText.Text, StringComparison.OrdinalIgnoreCase) >= 0)));
                OpenedName.Text = @DbFile.SafeFileName;
            }
            else
            {
                MessageBox.Show("dsfgds");
            }
        }

        private void MigrateRecipe(object sender, RoutedEventArgs e)
        {   
            // zrobić sprawdzenie czy przepis o podanej nazwie już ustnieje w bazie danych + nazwy składników!!!
            if (ForeignDbListViev.SelectedItems.Count == 1 &&
                ContainsRecipe(MainColection.ListOfRecipes, (Recipe)ForeignDbListViev.SelectedItems[0]))//!MainColection.ListOfRecipes.Contains((Recipe)ForeignDbListViev.SelectedItems[0]))//tutaj mam źle
            {
                Recipe tempRecipe = new Recipe();
                tempRecipe = (Recipe)ForeignDbListViev.SelectedItem;
                MainColection.ListOfRecipes.Add(tempRecipe);

                List<Component> componentsMigrating = new List<Component>();

                componentsMigrating.AddRange(ForeignColection.GetComponentList(tempRecipe));//get all components of chosen foreing recipe;

                MainDB.InsertData(string.Format("INSERT INTO RecipiesTable (Name,Recipe,Persons,Type)VALUES('{0}','{1}','{2}','{3}')",
                    tempRecipe.Name,
                    tempRecipe.RecipeTxt,
                    tempRecipe.Persons,
                    tempRecipe.TypeOfDish));//Insert chosen recipe into MainDB

                Recipe containerRecipe = new Recipe(MainDB.GetData(
                    string.Format("SELECT Id,Name,Recipe,Persons,Type FROM RecipiesTable WHERE Name='{0}'",
                    tempRecipe.Name)).Tables[0].Rows[0]);// Get inserted new id(for set relations in different table) and other fields of Recipe


                for (int i = 0; i < componentsMigrating.Count; ++i)
                {
                    if (ContainsComponent(MainColection.ListOfComponents, componentsMigrating[i]))    //if (!MainColection.ListOfComponents.Contains(componentsMigrating[i]))//Tutaj mam źle
                    {
                        MainDB.InsertData(string.Format("INSERT INTO ResourcesTable (Resource,Value)VALUES ('{0}','{1}')",
                           componentsMigrating[i].Name,
                           componentsMigrating[i].Value.Replace(".", ",")));// Insert Components into mainDB
                    }                                                       // if doesn't exit in mainDB
                }

                List<int> compIds = new List<int>();

                for (int i = 0; i < componentsMigrating.Count; ++i)
                {
                    compIds.Add(Convert.ToInt32(MainDB.GetData(string.Format("SELECT Idres FROM ResourcesTable WHERE Resource='{0}'",
                         componentsMigrating[i].Name)).Tables[0].Rows[0].ItemArray[0]));// get ids of components from mainDb
                }

                for (int i = 0; i < componentsMigrating.Count; ++i)
                {
                    MainDB.InsertData(string.Format("INSERT INTO RelationsTable (ComponentId,RecipeId,Amount)VALUES('{0}','{1}','{2}')",
                    compIds[i],
                    containerRecipe.Id,
                    "1"));// Set relations in mainDb
                }

                MainDbListViev.ItemsSource = null;
                MainDbListViev.ItemsSource = MainColection.ListOfRecipes;
                //pomyśleć jak to skrócić | zredukować operacje na bazie. Ale bałagan 
            }
            else
            {
                MessageBox.Show("przepis już istnieje");
            }
        }

        bool ContainsComponent(List<Component> components, Component chosen) 
        {
            for (int i = 0; i < components.Count; ++i)
            {
                if (components[i].Name.Contains(chosen.Name))
                {
                    return false;
                }
            }

            return true;
        }
        bool ContainsRecipe(List<Recipe> recipes, Recipe chosen)
        {
            for (int i = 0; i < recipes.Count; ++i)
            {
                if (recipes[i].Name.Contains(chosen.Name))
                {
                    return false;
                }
            }

            return true;
        }
        private void MainRecipeFilterText_Changed(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(MainDbListViev.ItemsSource).Refresh();
        }

        private void ForeignRecipeFilterText_Changed(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ForeignDbListViev.ItemsSource).Refresh();
        }
       
    }
}

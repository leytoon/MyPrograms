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
using DatabaseLib.Interfaces;
using CookingBook.DataTypes;
using CookingBook.Utilities;

namespace CookingBook.Windows
{
    public partial class ComponentsWindow : Window
    {

        IDbHandle DbCli = null;
        CookingBookDataCollection Components = null;
        Component AddResObj;
        Component UpdateResObj = new Component(1, "0", "0");
        

        private bool ComponentFilter(object item)
        {
            if (String.IsNullOrEmpty(ComponentsFilterTextt.Text))
                return true;
            else
                return ((item as Component).Name.IndexOf(ComponentsFilterTextt.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void getSelectedItem(object sender, RoutedEventArgs e)
        {
            var item = sender as ListView;

            if (item.SelectedItems.Count != 0)
            {
                UpdateResObj = (Component)item.SelectedItems[0];

                UpResourceTextBlock.Text = UpdateResObj.Name;
                UpValueTextBlock.Text = UpdateResObj.Value.ToString();
            }
        }
        private void Add(object sender, RoutedEventArgs e)
        {

            if (TxtValidator.IsPriceValid(UpValueTextBox.Text.ToString().Replace(".", ",")) 
                && UpResourceTextBox.Text!=""
                && SQLInjectionParser.Parse(UpResourceTextBox.Text + UpValueTextBox.Text))
            {
                AddResObj = new Component(UpResourceTextBox.Text, UpValueTextBox.Text);

                DbCli.InsertData(string.Format("INSERT INTO ResourcesTable (Resource,Value)VALUES ('{0}','{1}')",
                   AddResObj.Name, 
                   AddResObj.Value.Replace(".", ",")));

                UpResourceTextBox.Text = "";
                UpValueTextBox.Text = "";

                ComponentsListViev.ItemsSource = null;
                ComponentsListViev.ItemsSource = Components.GetFullComponentList();

                CollectionView ComponentViev = (CollectionView)CollectionViewSource.GetDefaultView(ComponentsListViev.ItemsSource);
                ComponentViev.Filter = ComponentFilter;//To allow search after Addition
              
            }
            else
            {
                MessageBox.Show("Format:[].[][]");
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UpValueTextBox.Text))
            {
                UpValueTextBox.Text = UpdateResObj.Value;
            }
            else if (TxtValidator.IsPriceValid(UpValueTextBox.Text.ToString().Replace(".", ",")) 
                && SQLInjectionParser.Parse(UpValueTextBox.Text + UpResourceTextBox.Text))
            {
                if (string.IsNullOrEmpty(UpResourceTextBox.Text))
                    UpResourceTextBox.Text = UpdateResObj.Name;

                DbCli.InsertData(string.Format("UPDATE ResourcesTable SET Resource= '{0}',Value='{1}' WHERE Idres='{2}'", 
                    UpResourceTextBox.Text, 
                    UpValueTextBox.Text.Replace(".", ","), 
                    UpdateResObj.Id));

                UpResourceTextBox.Text = "";
                UpValueTextBox.Text = "";

                ComponentsListViev.ItemsSource = null;
                ComponentsListViev.ItemsSource = Components.GetFullComponentList();

                //pomyśleć jakby to można skrócić
                CollectionView ComponentViev = (CollectionView)CollectionViewSource.GetDefaultView(ComponentsListViev.ItemsSource);
                ComponentViev.Filter = ComponentFilter;//To allow search
            }
            else
            {
                MessageBox.Show("Format:[],[][]");
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (ComponentsListViev.SelectedItems.Count != 0)
            {
                DbCli.InsertData(string.Format("DELETE FROM ResourcesTable WHERE Idres='{0}'", UpdateResObj.Id));
                DbCli.InsertData(string.Format("DELETE FROM RelationsTable WHERE ComponentId='{0}'", UpdateResObj.Id));
                
                ComponentsListViev.ItemsSource = Components.GetFullComponentList();

                CollectionView ComponentViev = (CollectionView)CollectionViewSource.GetDefaultView(ComponentsListViev.ItemsSource);
                ComponentViev.Filter = ComponentFilter;
            }
        }

        private void ComponentTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ComponentsListViev.ItemsSource).Refresh();
        }
        public ComponentsWindow(IDbHandle dbCli)
        {
            InitializeComponent();
           
            CookingBookLanguageSelect.ChangeLanuage(MainWindow.SelectedLanguage, this);

            DbCli = dbCli;
           
            Components = new CookingBookDataCollection(DbCli);

            ComponentsListViev.ItemsSource = Components.GetFullComponentList();

            CollectionView ComponentViev = (CollectionView)CollectionViewSource.GetDefaultView(ComponentsListViev.ItemsSource);
            ComponentViev.Filter = ComponentFilter;

        }

    }
}

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
    public partial class ComponentsWindow : Window
    {
       
        SQLIteClient SQLCli = null;
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

            if (TxtValidator.IsPriceValid(UpValueTextBox.Text.ToString().Replace(".", ",")) && UpResourceTextBox.Text!="")
            {
                AddResObj = new Component(UpResourceTextBox.Text.ToString(), UpValueTextBox.Text.ToString());

                SQLCli.SetData("INSERT INTO ResourcesTable (Resource,Value)VALUES ('" +
                        AddResObj.Name + "','" +
                        AddResObj.Value.Replace(".", ",") + "')");

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
            else if (TxtValidator.IsPriceValid(UpValueTextBox.Text.ToString().Replace(".", ",")))
            {
                if (string.IsNullOrEmpty(UpResourceTextBox.Text))
                    UpResourceTextBox.Text = UpdateResObj.Name;

                SQLCli.SetData("UPDATE ResourcesTable SET Resource= '" + UpResourceTextBox.Text +
                    "',Value='" + UpValueTextBox.Text.Replace(".", ",") +
                    "' WHERE Idres='" + UpdateResObj.Id + "'");

                UpResourceTextBox.Text = "";
                UpValueTextBox.Text = "";

                ComponentsListViev.ItemsSource = null;
                ComponentsListViev.ItemsSource = Components.GetFullComponentList();

                //pomyśleć jakby to można skrócić
                CollectionView ComponentViev = (CollectionView)CollectionViewSource.GetDefaultView(ComponentsListViev.ItemsSource);
                ComponentViev.Filter = ComponentFilter;//To allow search after Updation
            }
            else
            {
                MessageBox.Show("Format:[],[][]");
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {

            SQLCli.SetData("DELETE FROM ResourcesTable WHERE Idres='" + UpdateResObj.Id + "'");
            SQLCli.SetData("DELETE FROM RelationsTable WHERE ComponentId='" + UpdateResObj.Id + "'");
            ComponentsListViev.ItemsSource = Components.GetFullComponentList();

            CollectionView ComponentViev = (CollectionView)CollectionViewSource.GetDefaultView(ComponentsListViev.ItemsSource);
            ComponentViev.Filter = ComponentFilter;//To allow search after Updation
        }

        private void ComponentTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ComponentsListViev.ItemsSource).Refresh();
        }
        public ComponentsWindow()
        {
            InitializeComponent();
           
            CookingBookLanguageSelect.ChangeLanuage(MainWindow.SelectedLanguage, this);

            SQLCli = new SQLIteClient("", "", MainWindow.DbPath, "");
           
            Components = new CookingBookDataCollection(SQLCli);

            ComponentsListViev.ItemsSource = Components.GetFullComponentList();

            CollectionView ComponentViev = (CollectionView)CollectionViewSource.GetDefaultView(ComponentsListViev.ItemsSource);
            ComponentViev.Filter = ComponentFilter;//To allow search

        }

    }
}

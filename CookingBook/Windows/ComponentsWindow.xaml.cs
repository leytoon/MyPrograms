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
        List<Component> ResourceList = null;
        SQLIteClient SQLCli = null;

        Component AddResObj;
        Component UpdateResObj = new Component(1, "0", "0");
        
        private bool MakeList(SQLIteClient SQLTest)
        {
            var data = SQLTest.GetData("select * from ResourcesTable");

            ResourceList = new List<Component>();

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var Items = data.Tables[0].Rows[i].ItemArray;
                ResourceList.Add(new Component(Convert.ToInt32(Items[0]), Items[1].ToString(), Items[2].ToString()));

            }

            ComponentsListViev.ItemsSource = ResourceList;
            
            return true;
        }
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
            UpdateResObj = (Component)item.SelectedItems[0];

            UpResourceTextBlock.Text = UpdateResObj.Name;
            UpValueTextBlock.Text = UpdateResObj.Value.ToString();
        }
        private void Add(object sender, RoutedEventArgs e)
        {

            if (TxtValidator.IsPriceValid(ValueTextBox.Text.ToString().Replace(".",",")))
            {
                AddResObj = new Component(ResourceTextBox.Text.ToString(), ValueTextBox.Text.ToString());

                SQLCli.SetData("INSERT INTO ResourcesTable (Resource,Value)VALUES ('" +
                        AddResObj.Name + "','" +
                        AddResObj.Value.Replace(".", ",") + "')");

                ResourceTextBox.Text = "";
                ValueTextBox.Text = "";
                MakeList(SQLCli);

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

                MakeList(SQLCli);

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
            MakeList(SQLCli);
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
            MakeList(SQLCli);

            CollectionView ComponentViev = (CollectionView)CollectionViewSource.GetDefaultView(ComponentsListViev.ItemsSource);
            ComponentViev.Filter = ComponentFilter;//To allow search

        }
    }
}

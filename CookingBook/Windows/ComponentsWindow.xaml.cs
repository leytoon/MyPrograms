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
        Component UpdateResObj = new Component(1, "0", 1);
        
        private bool MakeList(SQLIteClient SQLTest)
        {
            var data = SQLTest.GetData("select * from ResourcesTable");

            ResourceList = new List<Component>();

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                var Items = data.Tables[0].Rows[i].ItemArray;
                ResourceList.Add(new Component(Convert.ToInt32(Items[0]), Items[1].ToString(), Convert.ToSingle(Items[2])));

            }

            ResourcesList.ItemsSource = ResourceList;
            
            return true;
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

            if (TxtValidator.IsPriceValid(Convert.ToString(ValueTextBox.Text)))
            {
                AddResObj = new Component(ResourceTextBox.Text.ToString(), Convert.ToSingle(ValueTextBox.Text));

                SQLCli.SetData("INSERT INTO ResourcesTable (Resource,Value)VALUES ('" +
                        AddResObj.Name + "','" +
                        AddResObj.Value + "')"); //make something with casts to this 

                ResourceTextBox.Text = "";
                ValueTextBox.Text = "";
                MakeList(SQLCli);
              
            }
            else
            {
                MessageBox.Show("Format:[].[][]");
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
           
            if (TxtValidator.IsPriceValid(Convert.ToString(ValueTextBox.Text)))
            {
                SQLCli.UpdateData("UPDATE ResourcesTable SET Resource= '" +
                    UpResourceTextBox.Text + "',Value='" + UpValueTextBox.Text + "' WHERE Idres='" + UpdateResObj.Id + "'");
                    
                UpResourceTextBox.Text = "";
                UpValueTextBox.Text = "";
                
                MakeList(SQLCli);
            }
            else
            {
                MessageBox.Show("Format:[],[][]");
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            SQLCli.DeleteData("DELETE FROM ResourcesTable WHERE Idres='" + UpdateResObj.Id + "'");
            SQLCli.DeleteData("DELETE FROM RelationsTable WHERE ComponentId='" + UpdateResObj.Id + "'");
            MakeList(SQLCli);
        }
        public ComponentsWindow()
        {
            InitializeComponent();
           
            CookingBookLanguageSelect.ChangeLanuage(MainWindow.SelectedLanguage, this);

            SQLCli = new SQLIteClient("", "", MainWindow.DbPath, "");
            MakeList(SQLCli);

        }
    }
}

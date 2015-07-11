﻿using System;
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
        void ShowAddResourceWindow(object sender, RoutedEventArgs e)
        {
            ComponentsWindow Window = new ComponentsWindow();
            Window.Title = CookinBookDictionary.Instance.GetNames(SelectedLanguage).AddComponentWindow;
            Window.Show();
           
        }
        void ShowAddRecepieWindow(object sender, RoutedEventArgs e)
        {
            RecipesWindow Window = new RecipesWindow();
            Window.Title = CookinBookDictionary.Instance.GetNames(SelectedLanguage).AddRecipieWindow;
            Window.Show();
        }
       
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Icarus.Data;

namespace DeskApp
{
    /// <summary>
    /// Interaction logic for ItemDisplay.xaml
    /// </summary>
    public partial class ItemDisplay : UserControl
    {
        private Item thisItem = new Item();

        public ItemDisplay()
        {
            InitializeComponent();
            this.DataContext = new List<Item>();
        }

        private void SkuBox_KeyChange(object sender, TextChangedEventArgs e)
        {
            string s = ((TextBox)e.Source).Text;
            if(s != "")
            {
                s = Regex.Replace(s, "[^0-9]", "");
                ((TextBox)e.Source).Text = s;
            }
        
        }

        private void PopulateFields()
        {
            SkuBox.Text = thisItem.Sku.ToString();
        }

        private void PopulateItem()
        {
            thisItem.Sku = Int32.Parse(SkuBox.Text);
        }

        private void NewItemButton_Click(object sender, RoutedEventArgs e)
        {
            thisItem = new Item();
            PopulateFields();
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

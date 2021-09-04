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
        private Catalog thisCatalog = new Catalog();

        public ItemDisplay()
        {
            InitializeComponent();
            PopulateList();
        }

        private void PopulateList()
        {
            this.DataContext = thisCatalog;
        }

        private void SkuBox_KeyChange(object sender, TextChangedEventArgs e)
        {
            string s = ((TextBox)e.Source).Text;
            if(s != "")
            {
                s = Regex.Replace(s, "[^0-9]", "");
                ((TextBox)e.Source).Text = s;
            }

            if(SkuBox.Text.Length != 7 && NameBox.Text.Length < 5)
                AddItemButton.IsEnabled = false;
            else
                AddItemButton.IsEnabled = true;

        }

        private void PopulateFields(Item thisItem)
        {
            SkuBox.Text = thisItem.Sku.ToString();
            NameBox.Text = thisItem.Name;
        }

        private Item PopulateItem()
        {
            Item thisItem = new Item();

            thisItem.Sku = Int32.Parse(SkuBox.Text);
            thisItem.Name = NameBox.Text;

            return thisItem;
        }

        private void NewItemButton_Click(object sender, RoutedEventArgs e)
        {
            Item thisItem = new Item();
            PopulateFields(thisItem);
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {

            Item thisItem = PopulateItem();
            thisCatalog.Add(thisItem);
            this.DataContext = thisCatalog;
        }

        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

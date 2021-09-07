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
using System.IO;
using Icarus.Data;
using Newtonsoft.Json;

namespace DeskApp
{
    /*
     * Author: Jordan Stafford
     * Class: ItemDisplay.xaml.cs
     * Purpose: Drives ItemDisplay.xaml
     */
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
            string json = File.ReadAllText(@"C:\Users\Admin\source\repos\Icarus\Documents\Items.json");
            
            if(json != "")
                thisCatalog.SetList(JsonConvert.DeserializeObject<List<Item>>(json));

            this.DataContext = thisCatalog;
        }

        private void AddButtonCheck()
        {
            if(SkuBox.Text.Length != 7 || NameBox.Text.Length < 5 || DeptNameBox.Text.Length < 5 || DeptIDBox.Text.Length != 3 || CataBox.Text.Length < 5 || InventByBox.Text.Length < 4)
                AddItemButton.IsEnabled = false;
            else
                AddItemButton.IsEnabled = true;
        }

        private void NumOnlyBox_KeyChange(object sender, TextChangedEventArgs e)
        {
            string s = ((TextBox)e.Source).Text;
            if(s != "")
            {
                string t = s;
                int cursorPos = ((TextBox)e.Source).SelectionStart;

                s = Regex.Replace(s, @"[^0-9]", "");

                ((TextBox)e.Source).Text = s;
                ((TextBox)e.Source).SelectionStart = cursorPos - (t.Length - s.Length);

            }

            AddButtonCheck();
        }

        private void SFBox_textChange(object sender, TextChangedEventArgs e)
        {
            string s = ((TextBox)e.Source).Text;
            int cursorPos = ((TextBox)e.Source).SelectionStart;
            TextChange[] o = new TextChange[1];
            e.Changes.CopyTo(o, 0);


            if(o[0].AddedLength > 1)
            {
                string p = "";

                if(s[0] == '-')
                    p = "-";
                
                p += Regex.Replace(s, @"[^0-9]", "");

                ((TextBox)e.Source).Text = p;
                cursorPos = cursorPos - (s.Length - p.Length);

                ((TextBox)e.Source).SelectionStart = cursorPos;
            }

            else if (o[0].AddedLength == 1)
            {
                string t = s.TrimStart('-');
                string u = "";

                if(s[0] == '-')
                    u = "-";

                char d = s[o[0].Offset];

                if((o[0].Offset == 0 && d != '-') || (d < 48 || d > 57))
                {
                    foreach(char c in t)
                    {
                        if(c != '-' && ((int)c >= 48 && (int)c <= 57))
                            u += c.ToString();
                        else
                            cursorPos = u.Length;
                    }

                    if(s[0] == '-')
                        cursorPos++;

                    ((TextBox)e.Source).Text = u;
                    ((TextBox)e.Source).SelectionStart = cursorPos;
                }
            }

            AddButtonCheck();
        }

        private void SFBox_KeyChange(object sender, KeyEventArgs e)
        {
            int i = (int)e.Key;
            e.Handled = !((i > 33 && i < 43) || (i > 73 && i < 83) || i == 143 || i == 87);
        }

        private void TextOnlyBox_KeyPress(object sender, KeyEventArgs e)
        {
            string s = e.Key.ToString();
            int i = e.Key.ToString().Length;
            e.Handled = !(e.Key.ToString().Length == 1);

            AddButtonCheck();
        }

        private void TextOnlyBox_KeyChange(object sender, TextChangedEventArgs e)
        {
            string s = ((TextBox)e.Source).Text;
            if(s != "")
            {
                int cursorPos = ((TextBox)e.Source).SelectionStart;
                string t = s;

                s = Regex.Replace(s, @"[^a-zA-Z\s]", "");

                ((TextBox)e.Source).Text = s;
                ((TextBox)e.Source).SelectionStart = cursorPos - (t.Length - s.Length);
            }

            AddButtonCheck();
        }

        private void NumTextBox_KeyChange(object sender, TextChangedEventArgs e)
        {
            string s = ((TextBox)e.Source).Text;
            if(s != "")
            {
                s = Regex.Replace(s, @"[^0-9]+[^a-zA-Z]+", "");
                ((TextBox)e.Source).Text = s;
            }

            AddButtonCheck();
        }

        private void VendorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(((TextBox)e.Source).Text != "")
            {
                using(StringReader reader = new StringReader(((TextBox)e.Source).Text))
                {
                    string line = "";

                    do
                    {
                        line = reader.ReadLine();

                        if(line != null)
                        {
                            if(line.Length > 19)
                            {
                                e.Handled = true;
                            }
                        }

                    } while(line != null);
                }
            }
        }

        private void VendorTextBox_KeyChange(object sender, TextChangedEventArgs e)
        {
            TextChange[] o = new TextChange[1];
            e.Changes.CopyTo(o, 0);
            int cursorPos = ((TextBox)e.Source).SelectionStart;
            string s = ((TextBox)e.Source).Text;

            if(((TextBox)e.Source).Text != "")
            {
                using(StringReader reader = new StringReader(((TextBox)e.Source).Text))
                {
                    string line = "";

                    do
                    {
                        line = reader.ReadLine();

                        if(line != null)
                        {
                            if(line.Length > 19)
                            {
                                StringBuilder sb = new StringBuilder(((TextBox)e.Source).Text);
                                sb.Remove(o[0].Offset, o[0].AddedLength);
                                ((TextBox)e.Source).Text = sb.ToString();
                                ((TextBox)e.Source).SelectionStart = cursorPos - (s.Length - sb.ToString().Length);
                            }
                        }

                    } while(line != null);
                }
            }
        }

        private void PopulateFields(Item thisItem)
        {
            SkuBox.Text = thisItem.Sku.ToString();
            NameBox.Text = thisItem.Name;
            DeptNameBox.Text = thisItem.DepartmentName;
            DeptIDBox.Text = thisItem.DepartmentID.ToString();
            CataBox.Text = thisItem.Catagory;
            PopulateVendorLocBox(thisItem.Vendors);
            SFBox.Text = thisItem.SalesFloorQuantity.ToString();
            MaxBox.Text = thisItem.MaxSalesFloor.ToString();
            OSBox.Text = thisItem.OverstockQuantity.ToString();
            PrePaidBox.Text = thisItem.PrepaidQuantity.ToString();

            InventByBox.Text = thisItem.InventoriedTeamMember;
        }

        private Item PopulateItem()
        {
            Item thisItem = new Item();

            thisItem.Sku = Int32.Parse(SkuBox.Text);
            thisItem.Name = NameBox.Text;
            thisItem.DepartmentName = DeptNameBox.Text;
            thisItem.DepartmentID = Int32.Parse(DeptIDBox.Text);
            thisItem.Catagory = CataBox.Text;
            thisItem.SalesFloorQuantity = Int32.Parse(SFBox.Text);
            thisItem.MaxSalesFloor = Int32.Parse(MaxBox.Text);
            thisItem.OverstockQuantity = Int32.Parse(OSBox.Text);
            thisItem.PrepaidQuantity = Int32.Parse(PrePaidBox.Text);
            thisItem.LocationStocked = PopulateVendorLocItem();
            thisItem.DateRecieved = DateTime.Now;
            thisItem.DateInventoried = DateTime.Now;
            thisItem.InventoriedTeamMember = InventByBox.Text;
            thisItem.Vendors = PopulateVendorLocItem();

            return thisItem;
        }

        private List<string> PopulateVendorLocItem()
        {
            List<string> thisList = new List<string>();

            if(VendorBox.Text != "")
            {
                using(StringReader reader = new StringReader(VendorBox.Text))
                {
                    string line = "";

                    do
                    {
                        line = reader.ReadLine();

                        if(line != null)
                        {
                            thisList.Add(line);
                        }

                    } while(line != null);
                }
            }

            return thisList;
        }

        private void PopulateVendorLocBox(List<string> thisList)
        {
            if(thisList != null)
            {
                int i = thisList.Count;
                int j = 0;

                foreach(string line in thisList)
                {
                    j++;

                    if(i != j)
                        VendorBox.Text += line + Environment.NewLine;
                    else
                        VendorBox.Text += line;
                }
            }

            else
                VendorBox.Text = "";
        }

        private void ListItem_Select(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count != 0 && e.AddedItems[0] is Item item)
                PopulateFields(item);
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
            if(ItemListView.SelectedItem != null)
            {
                Item item = (Item)ItemListView.SelectedItem;
                thisCatalog.Remove(item);
                this.DataContext = thisCatalog;
            }
        }

        private void SubmitListButton_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(thisCatalog.GetList(), Formatting.Indented);

            File.WriteAllText(@"C:\Users\Admin\source\repos\Icarus\Documents\Items.json", json);
        }
    }
}

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

        /// <summary>
        /// Populates the Listview in ItemDisplay.xaml
        /// </summary>
        private void PopulateList()
        {
            string json = "";

            try
            {
                json = File.ReadAllText(@"C:\Users\Admin\source\repos\Icarus\Documents\Items.json");
            }

            catch(Exception)
            {
                MessageBox.Show("Failed to load file Items.json. File directory does not exist.");
            }

            finally
            {
                if(json != "")
                    thisCatalog.SetList(JsonConvert.DeserializeObject<List<Item>>(json));

                this.DataContext = thisCatalog;
            }
        }

        /// <summary>
        /// Checks to see if fields have the minimal values to add to list
        /// </summary>
        private void AddButtonCheck()
        {
            if(SkuBox.Text.Length != 7 || NameBox.Text.Length < 5 || DeptNameBox.Text.Length < 5 || DeptIDBox.Text.Length != 3 || CataBox.Text.Length < 5 || InventByBox.Text.Length < 4)
                AddItemButton.IsEnabled = false;
            else
                AddItemButton.IsEnabled = true;
        }

        /// <summary>
        /// Prevents any character that isn't a number to be entered in a textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Prevents any character that isn't a number or '-' to be entered into SFBox. For copy and paste entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Prevents any character that isn't a number or '-' to be entered into SFBox. For single key enteries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SFBox_KeyChange(object sender, KeyEventArgs e)
        {
            int i = (int)e.Key;
            e.Handled = !((i > 33 && i < 43) || (i > 73 && i < 83) || i == 143 || i == 87);
            AddButtonCheck();
        }

        /// <summary>
        /// Prevents any character that isn't a letter to be entered into a textbox. For single key entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextOnlyBox_KeyPress(object sender, KeyEventArgs e)
        {
            string s = e.Key.ToString();
            int i = e.Key.ToString().Length;
            e.Handled = !(e.Key.ToString().Length == 1);

            AddButtonCheck();
        }

        /// <summary>
        /// Prevents any character that isn't a letter to be entered into a textbox. For copy and paste entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                if(sender is TextBox box)
                {
                    if(box.Name == "InventByBox")
                        InventoriedBox.Text = DateTime.Now.ToString();
                }
            }

            AddButtonCheck();
        }

        /// <summary>
        /// Prevents any character that isn't alphanumeric to be entered into a textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Prevents line length in VendorBox from being longer then 20 characters. Single key entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Prevents line length in VendorBox from being longer then 20 characters. Copy Paste Entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void LocationTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(((TextBox)e.Source).Text != "")
            {

                int i = (int)e.Key;
                e.Handled = !((i >= ((int)Key.D0) && i <= ((int)Key.Z)) || (i >= (int)Key.NumPad0 && i <= ((int)Key.NumPad9)));

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

        private void LocationTextBox_KeyChange(object sender, TextChangedEventArgs e)
        {
            TextChange[] o = new TextChange[1];
            e.Changes.CopyTo(o, 0);
            int cursorPos = ((TextBox)e.Source).SelectionStart;
            string s = ((TextBox)e.Source).Text;
            List<string> thisList = new List<string>();

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
                            if(line.Length > 4)
                            {
                                StringBuilder sb = new StringBuilder(((TextBox)e.Source).Text);
                                sb.Remove(o[0].Offset, o[0].AddedLength);
                                ((TextBox)e.Source).Text = sb.ToString();
                                ((TextBox)e.Source).SelectionStart = cursorPos - (s.Length - sb.ToString().Length);
                            }

                            else if(line != "")
                            {

                                string t = "";

                                foreach(char c in line)
                                {
                                    if(((int)c >= 48 && (int)c <= 57) || ((int)c >= 65 && (int)c <= 90))
                                    {
                                        t += c;
                                    }
                                }

                                if(t != "")
                                    thisList.Add(t);
                            }
                        }

                    } while(line != null);
                }


            }
        }

        /// <summary>
        /// Populates the textboxes in ItemDisplay.xaml
        /// </summary>
        /// <param name="thisItem"></param>
        private void PopulateFields(Item thisItem)
        {
            SkuBox.Text = thisItem.Sku.ToString();
            NameBox.Text = thisItem.Name;
            DeptNameBox.Text = thisItem.DepartmentName;
            DeptIDBox.Text = thisItem.DepartmentID.ToString();
            CataBox.Text = thisItem.Catagory;
            PopulateVendorBox(thisItem.Vendors);
            SFBox.Text = thisItem.SalesFloorQuantity.ToString();
            MaxBox.Text = thisItem.MaxSalesFloor.ToString();
            OSBox.Text = thisItem.OverstockQuantity.ToString();
            PrePaidBox.Text = thisItem.PrepaidQuantity.ToString();
            RecievedBox.Text = thisItem.DateRecieved.ToString("MM/dd/yyyy HH:mm");
            InventoriedBox.Text = thisItem.DateInventoried.ToString("MM/dd/yyyy HH:mm");
            InventByBox.Text = thisItem.InventoriedTeamMember;

            AddButtonCheck();
        }

        /// <summary>
        /// Saves the info in ItemDisplay's textboxes into an Item object
        /// </summary>
        /// <returns></returns>
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
            thisItem.LocationStocked = PopulateVendorItem();
            thisItem.DateRecieved = DateTime.Now;
            thisItem.DateInventoried = Convert.ToDateTime(InventoriedBox.Text);
            thisItem.InventoriedTeamMember = InventByBox.Text;
            thisItem.Vendors = PopulateVendorItem();

            return thisItem;
        }

        /// <summary>
        /// Populates the vendor and location Item value
        /// </summary>
        /// <returns></returns>
        private List<string> PopulateVendorItem()
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

        /// <summary>
        /// Populates the vendor and location textboxes
        /// </summary>
        /// <param name="thisList"></param>
        private void PopulateVendorBox(List<string> thisList)
        {

            VendorBox.Text = "";

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
        }

        /// <summary>
        /// Handler for listview item selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListItem_Select(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count != 0 && e.AddedItems[0] is Item item)
                PopulateFields(item);

            EditItemButton.IsEnabled = true;
        }

        /// <summary>
        /// Handler for new item button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewItemButton_Click(object sender, RoutedEventArgs e)
        {
            Item thisItem = new Item();
            PopulateFields(thisItem);
        }

        /// <summary>
        /// Handler for add item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {

            Item thisItem = PopulateItem();
            thisCatalog.Add(thisItem);
            this.DataContext = thisCatalog;
            SubmitListButton.IsEnabled = true;
        }

        /// <summary>
        /// Handler for edit item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditItemButton_Click(object sender, RoutedEventArgs e)
        {
            if(ItemListView.SelectedItem != null)
            {
                Item thisItem = PopulateItem();
                thisCatalog.Edit((Item)ItemListView.SelectedItem, thisItem);
                this.DataContext = thisCatalog;
                SubmitListButton.IsEnabled = true;
                EditItemButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Handler for delete item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            if(ItemListView.SelectedItem != null)
            {
                Item item = (Item)ItemListView.SelectedItem;
                thisCatalog.Remove(item);
                this.DataContext = thisCatalog;
                SubmitListButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// Handler for submit list button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitListButton_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(thisCatalog.GetList(), Formatting.Indented);

            try
            {
                File.WriteAllText(@"C:\Users\Admin\source\repos\Icarus\Documents\Items.json", json);
            }

            catch(Exception)
            {
                MessageBox.Show("Unable to save list. File directory may not exist.");
            }

            finally
            {
                SubmitListButton.IsEnabled = false;

                MessageBox.Show("List Saved Successful.");
            }
        }
    }
}

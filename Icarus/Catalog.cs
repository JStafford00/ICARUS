using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Icarus.Data
{

    /*
     *  Author: Jordan Stafford
     *  Class: Catalog
     *  Purpose: Holds all items in the catalog.
     */
    public class Catalog : INotifyPropertyChanged
    {
        private List<Item> items = new List<Item>();
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Turns list of items into array
        /// </summary>
        public IEnumerable<Item> Items => items.ToArray();

        /// <summary>
        /// Adds item into list of items and notifies property changed
        /// </summary>
        /// <param name="item">Item being added</param>
        public void Add(Item item)
        {
            if(item is INotifyPropertyChanged notifier)
                notifier.PropertyChanged += OnItemPropertyChange;

            items.Add(item);
            items.Sort((x, y) => x.Sku.CompareTo(y.Sku));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Items"));
        }

        /// <summary>
        /// Removes item from item list
        /// </summary>
        /// <param name="item"></param>
        public  void Remove(Item item)
        {
            if(item is INotifyPropertyChanged notifier)
                notifier.PropertyChanged += OnItemPropertyChange;

            items.Remove(item);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Items"));
        }

        /// <summary>
        /// Returns the list of items
        /// </summary>
        /// <returns></returns>
        public List<Item> GetList()
        {
            return items;
        }

        /// <summary>
        /// Sets the list
        /// </summary>
        /// <param name="list"></param>
        public void SetList(List<Item> list)
        {
            items = list;
        }

        /// <summary>
        /// Notifies of property change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Items"));
        }
    }
}

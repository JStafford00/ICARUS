using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Icarus.Data;
using Xunit;

namespace DataTests.UnitTests
{
    public class CatalogTest
    {
        /// <summary>
        /// Adding someing to catalog should have it appear in catalog items list
        /// </summary>
        [Fact]
        public void AddItemsAppearInItemsProperty()
        {
            var catalog = new Catalog();
            var item = new Item();

            catalog.Add(item);

            Assert.Contains(item, catalog.Items);
        }

        /// <summary>
        /// Removing something from catalog should have it not appear in catalog items list
        /// </summary>
        [Fact]
        public void RemovedItemDoesNotAppearInItems()
        {
            var catalog = new Catalog();
            var item = new Item();

            catalog.Add(item);
            catalog.Remove(item);

            Assert.DoesNotContain(item, catalog.Items);
        }

        /// <summary>
        /// Editing an item in catalog will actually edit the item.
        /// </summary>
        [Fact]
        public void EditedItemReplacesOldItemWithNew()
        {
            var catalog = new Catalog();
            var oldItem = new Item();
            var newItem = new Item();

            oldItem.Sku = 1000000;
            newItem.Sku = 2000000;

            catalog.Add(oldItem);

            catalog.Edit(oldItem, newItem);

            Assert.Contains(newItem, catalog.Items);
            Assert.DoesNotContain(oldItem, catalog.Items);
        }

        /// <summary>
        /// Setting new list to Catalog actually sets a new list.
        /// </summary>
        [Fact]
        public void SetListSetsCatalogList()
        {
            var catalog = new Catalog();
            List<Item> items = new List<Item>();
            var item = new Item();

            items.Add(item);

            catalog.SetList(items);

            Assert.Contains(item, catalog.Items);
        }

        /// <summary>
        /// Getting catalog list actualy return the correct list
        /// </summary>
        [Fact]
        public void GetListReturnsActualList()
        {
            var catalog = new Catalog();
            var item = new Item();
            List<Item> items = new List<Item>();

            catalog.Add(item);
            items.Add(item);

            Assert.Equal(catalog.GetList(), items);
        }
    }
}

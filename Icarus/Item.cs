using System;
using System.Collections.Generic;
using System.Text;

namespace Icarus.Data
{
    /*
     * Author: Jordan Stafford
     * Class: Item.cs
     * Purpose: To hold informaiton on general items.
     */
    public class Item
    {
        private int sku;
        private string name;
        private int overallQuantity;
        private int salesFloorQuantity;
        private int overstockQuantity;
        private int prepaidQuantity;
        private int maxSalesFloor;
        private DateTime dateRecieved;
        private DateTime dateInventoried;
        private string inventoriedTeamMember;
        private List<string> locationStocked;
        private int departmentID;
        private string departmentName;
        private string catagory;
        private List<string> vendors;

        public Item()
        {
            dateRecieved = new DateTime();
            dateInventoried = new DateTime();
            locationStocked = new List<string>();
            vendors = new List<string>();
        }

        /// <summary>
        /// Sku number for an item.
        /// </summary>
        public int Sku
        {
            get
            {
                return sku;
            }

            set
            {
                sku = value;
            }
        }

        /// <summary>
        /// Name of the item.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Overall quantity in sales floor and overstock.
        /// </summary>
        public int OverallQuantity
        {
            get
            {
                return overallQuantity;
            }

            set
            {
                overallQuantity = value;
            }
        }

        /// <summary>
        /// Quantity on the Sales Floor.
        /// </summary>
        public int SalesFloorQuantity
        {
            get
            {
                return salesFloorQuantity;
            }

            set
            {
                salesFloorQuantity = value;
            }
        }

        /// <summary>
        /// Quantity in Overstock.
        /// </summary>
        public int OverstockQuantity
        {
            get
            {
                return overstockQuantity;
            }

            set
            {
                overstockQuantity = value;
            }
        }

        /// <summary>
        /// Quantity on prepaid.
        /// </summary>
        public int PrepaidQuantity
        {
            get
            {
                return prepaidQuantity;
            }

            set
            {
                prepaidQuantity = value;
            }
        }

        /// <summary>
        /// Max Quantity that can Go on the Sales Floor.
        /// </summary>
        public int MaxSalesFloor
        {
            get
            {
                return maxSalesFloor;
            }

            set
            {
                maxSalesFloor = value;
            }
        }

        /// <summary>
        /// Date last recieved.
        /// </summary>
        public DateTime DateRecieved
        {
            get
            {
                return dateRecieved;
            }

            set
            {
                dateRecieved = value;
            }
        }

        /// <summary>
        /// Date last inventoried.
        /// </summary>
        public DateTime DateInventoried
        {
            get
            {
                return dateInventoried;
            }

            set
            {
                dateInventoried = value;
            }
        }

        /// <summary>
        /// Team member that inventoried item last.
        /// </summary>
        public string InventoriedTeamMember
        {
            get
            {
                return inventoriedTeamMember;
            }

            set
            {
                inventoriedTeamMember = value;
            }
        }

        /// <summary>
        /// Location of Stocked Item
        /// </summary>
        public List<string> LocationStocked
        {
            get
            {
                return locationStocked;
            }

            set
            {
                locationStocked = value;
            }
        }

        /// <summary>
        /// Department ID number.
        /// </summary>
        public int DepartmentID
        {
            get
            {
                return departmentID;
            }

            set
            {
                departmentID = value;
            }
        }

        /// <summary>
        /// Department name.
        /// </summary>
        public string DepartmentName
        {
            get
            {
                return departmentName;
            }

            set
            {
                departmentName = value;
            }
        }

        /// <summary>
        /// Department Caragory
        /// </summary>
        public string Catagory
        {
            get
            {
                return catagory;
            }

            set
            {
                catagory = value;
            }
        }

        /// <summary>
        /// List of vendors
        /// </summary>
        public List<string> Vendors
        {
            get
            {
                return vendors;
            }

            set
            {
                vendors = value;
            }
        }
    }
}
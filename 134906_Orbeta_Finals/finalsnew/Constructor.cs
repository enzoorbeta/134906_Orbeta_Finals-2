using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace finalsnew
{
    public class Constructor
    {
        private string name;
        private double price;
        private double sales;
        private int inventoryCount;
        private string code;

        

        public Constructor(string code, string name, double price, int stock)
        {
            this.code = code;
            this.name = name;
            this.price = price;
            this.inventoryCount = stock;
            this.sales = 0;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public int InventoryCount
        {
            get { return inventoryCount; }
            set { inventoryCount = value; } 
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public void Sellquantity(int num)
        {
            inventoryCount = inventoryCount - num;
        }

        /*public bool Sell(int num)
        {
            if (num <= inventoryCount)
            {
                inventoryCount = inventoryCount - num;
                sales = sales + price * num;
                return true;
            }
            else
            {
                return false;
            }
        }*/

        public double GetSales()
        {
            return this.sales;
        }
        public bool Restock(int num)
        {
            if (num > 0)
            {
                inventoryCount = inventoryCount + num;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

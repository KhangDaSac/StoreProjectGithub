using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.DTO
{
    public class Product
    {
        private int productID;
        private String productName;
        private String unit;
        private double price;
        private double importPrice;
        private int quantityOnHand;
        public Product(int productID, String productName, String unit, double price, double importPrice, int quantityOnHand)
        {
            this.ProductID = productID;
            this.ProductName = productName;
            this.Unit = unit;
            this.Price = price;
            this.ImportPrice = importPrice;
            this.QuantityOnHand = quantityOnHand;
        }

        public Product(String productName, String unit, double price, double importPrice, int quantityOnHand)
        {
            this.ProductName = productName;
            this.Unit = unit;
            this.Price = price;
            this.ImportPrice = importPrice;
            this.QuantityOnHand = quantityOnHand;
        }

        public Product(DataRow row)
        {
            this.ProductID = (int)row["ProductID"];
            this.ProductName = row["ProductName"].ToString();
            this.Unit = row["Unit"].ToString();
            this.Price = (double)row["Price"];
            this.ImportPrice = (double)row["ImportPrice"];
            this.QuantityOnHand = (int)row["QuantityOnHand"];
        }


        public int ProductID { get => productID; set => productID = value; }
        public string ProductName { get => productName; set => productName = value; }
        public string Unit { get => unit; set => unit = value; }
        public double Price { get => price; set => price = value; }
        public double ImportPrice { get => importPrice; set => importPrice = value; }
        public int QuantityOnHand { get => quantityOnHand; set => quantityOnHand = value; }
        
    }
}

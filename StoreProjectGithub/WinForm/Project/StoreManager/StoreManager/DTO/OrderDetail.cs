using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.DTO
{
    public class OrderDetail
    {
        private int orderDetailID;
        private int orderID;
        private int productID;
        private String productName;
        private String unit;
        private double price;
        private int quantity;
        private double charge;

        public int OrderDetailID { get => orderDetailID; set => orderDetailID = value; }
        public int OrderID { get => orderID; set => orderID = value; }
        public int ProductID { get => productID; set => productID = value; }
        public String ProductName { get => productName; set => productName = value; }
        public string Unit { get => unit; set => unit = value; }
        public double Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Charge { get => charge; set => charge = value; }
        

        public OrderDetail() { }

        public OrderDetail(int orderDetailID, int orderID, int productID, string productName, String unit, double price, int quantity)
        {
            this.OrderDetailID = orderDetailID;
            this.OrderID = orderID;
            this.ProductID = productID;
            this.ProductName = productName;
            this.Unit = unit;
            this.Price = price;
            this.Quantity = quantity;
            this.Charge = this.Price * this.Quantity;
        }

        public OrderDetail(int productID, string productName, double price, int quantity)
        {
            this.ProductID = productID;
            this.ProductName = productName;
            this.Price = price;
            this.Quantity = quantity;
            this.Charge = this.Price * this.Quantity;
        }

        public OrderDetail(DataRow row)
        {
            this.OrderDetailID = (int)row["OrderDetailID"];
            this.OrderID = (int)row["OrderID"];
            this.ProductID = (int)row["ProductID"];
            this.ProductName = row["ProductName"].ToString();
            this.Unit = row["Unit"].ToString();
            this.Price = (double)row["Price"];
            this.Quantity = (int)row["Quantity"];
            this.Charge = this.Price * this.Quantity;
        }
    }
}

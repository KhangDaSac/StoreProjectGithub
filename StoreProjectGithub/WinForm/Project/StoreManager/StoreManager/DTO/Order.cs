using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.DTO
{
    public class Order
    {
        private int orderID;
        private int customerID;
        private string customerName;
        private int employeeID;
        private string employeeName;
        private DateTime orderDate;
        private double amount;
        private String paymentMethod;

        public int OrderID { get => orderID; set => orderID = value; }
        public int CustomerID { get => customerID; set => customerID = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public string EmployeeName { get => employeeName; set => employeeName = value; }
        public DateTime OrderDate { get => orderDate; set => orderDate = value; }
        public double Amount { get => amount; set => amount = value; }
        public string PaymentMethod { get => paymentMethod; set => paymentMethod = value; }

        public Order(int orderID, int customerID, string customerName, int employeeID, string employeeName, DateTime orderDate, double amount, String paymentMethod)
        {
            this.OrderID = orderID;
            this.CustomerID = customerID;
            this.CustomerName = customerName;
            this.EmployeeID = employeeID;
            this.EmployeeName = employeeName;
            this.OrderDate = orderDate;
            this.Amount = amount;
            this.PaymentMethod = paymentMethod;
        }

        public Order(int customerID, int employeeID, DateTime orderDate, double amount, String paymentMethod)
        {
            this.CustomerID = customerID;
            this.EmployeeID = employeeID;
            this.OrderDate = orderDate;
            this.Amount = amount;
            this.PaymentMethod = paymentMethod;
        }

        public Order() { }

        public Order(DataRow row)
        {
            this.OrderID = (int)row["OrderID"];
            this.CustomerID = (int)row["CustomerID"];
            this.customerName = row["customerName"].ToString();
            this.EmployeeID = (int)row["EmployeeID"];
            this.employeeName = row["employeeName"].ToString();
            this.OrderDate = (DateTime)row["OrderDate"];
            this.Amount = (double)row["Amount"];
            this.PaymentMethod = row["PaymentMethod"].ToString();
        }
    }
}

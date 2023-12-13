using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.DTO
{
    public class Customer
    {
        private int customerID;
        private string customerName;
        private string phone;
        private string typeCustomer;
        private double debt;

        public int CustomerID { get => customerID; set => customerID = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string Phone { get => phone; set => phone = value; }
        public string TypeCustomer { get => typeCustomer; set => typeCustomer = value; }
        public double Debt { get => debt; set => debt = value; }

        public Customer() { }
        public Customer(int customerID, string customerName, string phone, string typeCustomer, double debt)
        {
            this.CustomerID = customerID;
            this.CustomerName = customerName;
            this.Phone = phone;
            this.TypeCustomer = typeCustomer;
            this.Debt = debt;
        }

        public Customer(DataRow row)
        {
            this.CustomerID = (int)row["CustomerID"];
            this.customerName = row["customerName"].ToString();
            this.Phone = row["Phone"].ToString();
            this.TypeCustomer = row["TypeCustomer"].ToString();
            this.Debt = (double)row["Debt"];
        }
    }
}

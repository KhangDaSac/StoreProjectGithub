using StoreManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.DAO
{
    public class OrderDAO
    {
        private static OrderDAO instance;

        public static OrderDAO Instance { 
            get
            {
                if (instance == null)
                {
                    instance = new OrderDAO();
                }
                return instance;
            }
            private set
            {
                OrderDAO.Instance = value;
            }
        }

        public List<Order> loadListOrder(DataTable table = null)
        {
            List<Order> list = new List<Order>();
            DataTable data = null;
            if(table == null)
            {
                String query = "select o.OrderID , o.CustomerID, c.CustomerName, o.EmployeeID, e.EmployeeName, o.OrderDate, o.Amount, o.PaymentMethod from Orders o join Employees e on o.EmployeeID = e.EmployeeID join Customers c on o.CustomerID = c.CustomerID";
                data = DataProvider.Instance.ExecuteQuery(query);
            }
            else
            {
                data = table;
            }

            foreach (DataRow row in data.Rows)
            {
                Order orderDisplay = new Order(row); 
                list.Add(orderDisplay);
            }
            
            return list;
        }

        public void addOrder(Order order)
        {
            String query = "exec USP_InsertValueIntoOrders @CustomerID , @EmployeeID , @OrderDate , @Amount , @PaymentMethod";

            DataProvider.Instance.ExecuteQuery(query, new Object[]
            {
                order.CustomerID,
                order.EmployeeID,
                order.OrderDate,
                order.Amount,
                order.PaymentMethod
            });
        }
    }
}

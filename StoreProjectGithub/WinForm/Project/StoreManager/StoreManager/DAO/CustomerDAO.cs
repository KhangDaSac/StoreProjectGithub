using StoreManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO instance;

        public static CustomerDAO Instance 
        { 
            get
            {
                if (instance == null)
                    instance = new CustomerDAO();
                return instance;
            }
            private set { instance = value; }
        }
        public List<Customer> loadListCustomer(DataTable table)
        {
            DataTable data = null;
            List<Customer> customerList = new List<Customer>();

            if (table == null)
            {
                String query = "exec USP_GetCustomerList";
                data = DataProvider.Instance.ExecuteQuery(query);
            }
            else
            {
                data = table;
            }

            foreach (DataRow row in data.Rows)
            {
                Customer p = new Customer(row);
                customerList.Add(p);
            }
            return customerList;
        }

        public Customer getCustomerByID(List<Customer> list, int id)
        {
            foreach(Customer customer in list)
            {
                if(customer.CustomerID == id)
                    return customer;
            }
            return null;
        }

        public void addCustomer(Customer customer)
        {
            String query = "exec USP_InsertValueIntoCustomers @CustomerName , @Phone , @TypeCustomer , @Debt";

            DataProvider.Instance.ExecuteQuery(query, new Object[] {
                    customer.CustomerName,
                    customer.Phone,
                    customer.TypeCustomer,
                    customer.Debt});
        }

        public void deleteCustomerByID(int customerID) 
        {
            String query = "exec USP_DeleteCustomerById @CustomerID";

            DataProvider.Instance.ExecuteQuery(query, new Object[] { customerID });
        }

        public void updateCustomer(Customer customer)
        {
            String query = "exec USP_UpdateValueFormCustomer @CustomerID , @CustomerName , @Phone , @TypeCustomer , @Debt ";
            DataProvider.Instance.ExecuteQuery(query, new Object[] {
                        customer.CustomerID,
                        customer.CustomerName,
                        customer.Phone,
                        customer.TypeCustomer,
                        customer.Debt});
        }
    }
}

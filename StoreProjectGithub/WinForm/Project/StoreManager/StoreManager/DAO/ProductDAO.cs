using StoreManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance;

        public static ProductDAO Instance 
        { 
            get
            {
                if (instance == null) instance = new ProductDAO();
                return instance;
            }
            private set
            {
                ProductDAO.instance = value;
            }
        }

        public List<Product> loadListProduct(DataTable table = null)
        {
            DataTable data = null;
            List <Product> productList = new List<Product>();
            
            if (table == null)
            {
                String query = "exec USP_GetProductsList";
                data = DataProvider.Instance.ExecuteQuery(query);
            }
            else
            {
                data = table;
            }

            foreach (DataRow row in data.Rows)
            {
                Product p = new Product(row);
                productList.Add(p);
            }
            return productList;
        }
    }
}

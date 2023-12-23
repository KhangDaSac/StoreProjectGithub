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

        public static Product getProductById(List<Product> list, int id)
        {
            foreach (Product p in list)
            {
                if (p.ProductID == id) return p;
            }
            return null;
        }

        public void addProduct(Product product)
        {
            String query = "exec USP_InsertValueIntoProducts @ProductName , @Unit , @Price , @ImportPrice , @QuantityOnHand";

            DataProvider.Instance.ExecuteQuery(query, new Object[] {
                    product.ProductName,
                    product.Unit,
                    product.Price,
                    product.ImportPrice,
                    product.QuantityOnHand});
        }

        public void deleteProductByID(int productID)
        {
            String query = "exec USP_DeleteProductById @ProductID";

            DataProvider.Instance.ExecuteQuery(query, new Object[] { productID });
        }

        public void updateProduct(Product product)
        {
            String query = "exec USP_UpdateValueFormProduct @ProductID , @ProductName , @Unit , @Price , @ImportPrice , @QuantityOnHand";
            DataProvider.Instance.ExecuteQuery(query, new Object[] {
                    product.ProductID,
                    product.ProductName,
                    product.Unit,
                    product.Price,
                    product.ImportPrice,
                    product.QuantityOnHand});
        }


    }
}

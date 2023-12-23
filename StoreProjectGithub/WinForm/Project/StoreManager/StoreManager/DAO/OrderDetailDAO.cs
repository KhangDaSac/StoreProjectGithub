using StoreManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.DAO
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance;

        public static OrderDetailDAO Instance 
        { 
            get
            {
                if (instance == null)
                {
                    instance = new OrderDetailDAO();
                }
                return instance;
            }
            private set
            {
                OrderDetailDAO.instance = value;
            }
        }

        public List<OrderDetail> getListOrderDetialByOrderID(int orderID)
        {
            List<OrderDetail> orderDetailList = new List<OrderDetail>();

            String query = "exec USP_GetListOrderDetialByOrderID @OrderID";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new Object[] { orderID });
            

            foreach (DataRow row in result.Rows)
            {
                OrderDetail od = new OrderDetail(row);
                orderDetailList.Add(od);
            }
            return orderDetailList;
        }
    }
}

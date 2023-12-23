using StoreManager.DAO;
using StoreManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;

namespace StoreManager
{
    public partial class FormOrderDetail : Form
    {

        public FormOrderDetail(int orderID)
        {
            this.OrderID = orderID;
            InitializeComponent();
            loadListOrderDetail();
        }

        private int orderID;
        public int OrderID { get => orderID; set => orderID = value; }

        private static List<OrderDetail> orderDetailList = new List<OrderDetail>();

        private void loadListOrderDetail()
        {
            orderDetailList = OrderDetailDAO.Instance.getListOrderDetialByOrderID(orderID);
            lsvListOrderDetailDisplay.Items.Clear();

            foreach (OrderDetail orderDetail in orderDetailList)
            {
                ListViewItem lsvItem = new ListViewItem(orderDetail.OrderDetailID.ToString());

                lsvItem.SubItems.Add(orderDetail.OrderID.ToString());
                lsvItem.SubItems.Add(orderDetail.ProductID.ToString());
                lsvItem.SubItems.Add(orderDetail.ProductName);
                lsvItem.SubItems.Add(orderDetail.Unit);
                lsvItem.SubItems.Add(FormatMoney.Instance.transformFormat(orderDetail.Price));
                lsvItem.SubItems.Add(orderDetail.Quantity.ToString());
                lsvItem.SubItems.Add(FormatMoney.Instance.transformFormat(orderDetail.Charge));

                lsvListOrderDetailDisplay.Items.Add(lsvItem);

            }
        }
    }
}

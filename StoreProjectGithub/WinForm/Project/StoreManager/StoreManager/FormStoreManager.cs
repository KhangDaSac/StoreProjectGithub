using Microsoft.SqlServer.Server;
using StoreManager.DAO;
using StoreManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StoreManager
{
    public partial class FormStoreManager : Form
    {
        #region Properties
        private static List<Product> productList = new List<Product>();
        private static List<Customer> customerList = new List<Customer>();
        #endregion
        public FormStoreManager()
        {
            InitializeComponent();
            loadListProduct();
            loadListCustomer();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdmin f = new FormAdmin();
            f.ShowDialog();
        }

        #region Method
        #region Method of list Product
        private void loadListProduct(DataTable data = null)
        {
            productList = ProductDAO.Instance.loadListProduct(data);
            lsvListProduct.Items.Clear();
            foreach (Product product in productList)
            {
                ListViewItem lsvItem = new ListViewItem(product.ProductID.ToString());

                lsvItem.SubItems.Add(product.ProductName.ToString());
                lsvItem.SubItems.Add(product.Unit.ToString());
                lsvItem.SubItems.Add(FormatMoney.Instance.transformFormat(product.Price));
                lsvItem.SubItems.Add(FormatMoney.Instance.transformFormat(product.ImportPrice));
                lsvItem.SubItems.Add(product.QuantityOnHand.ToString());

                lsvListProduct.Items.Add(lsvItem);
            }
        }

        private void buttonDeleteProduct_Click(object sender, EventArgs e)
        {
            if (lsvListProduct.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Bạn có thực sự muốn xóa sán phẩm này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    String idProductDeleteStr = lsvListProduct.SelectedItems[0].SubItems[0].Text;
                    int idProductDelete = int.Parse(idProductDeleteStr);
                    String query = "exec USP_DeleteProductById @ProductID";

                    DataProvider.Instance.ExecuteQuery(query, new Object[] { idProductDelete });
                    loadListProduct();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 sản phẩm", "Thông báo");
            }
        }

        private void lsvListProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvListProduct.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvListProduct.SelectedItems[0];
                Product product = ProductDAO.getProductById(productList, int.Parse(item.SubItems[0].Text));
                textBoxProductID.Text = product.ProductID.ToString();
                cbProductName.Text = product.ProductName;
                cbUnit.Text = product.Unit;
                numPrice.Value = (int)product.Price;
                numImportPrice.Value = (int)product.ImportPrice;
                numQuatity.Value = product.QuantityOnHand;
            }
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thêm sán phẩm này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                String query = "exec USP_InsertValueIntoProducts @ProductName , @Unit , @Price , @ImportPrice , @QuantityOnHand";

                DataProvider.Instance.ExecuteQuery(query, new Object[] {
                    cbProductName.Text,
                    cbUnit.Text,
                    numPrice.Value,
                    numImportPrice.Value,
                    numQuatity.Value });
                loadListProduct();
            }
        }

        private void buttonUpdateProduct_Click(object sender, EventArgs e)
        {
            if (lsvListProduct.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Bạn có thực sự muốn chỉnh sửa sán phẩm này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    String query = "exec USP_UpdateValueFormProduct @ProductID , @ProductName , @Unit , @Price , @ImportPrice , @QuantityOnHand";
                    DataProvider.Instance.ExecuteQuery(query, new Object[] {
                        textBoxProductID.Text,
                        cbProductName.Text,
                        cbUnit.Text,
                        numPrice.Value,
                        numImportPrice.Value,
                        numQuatity.Value });
                    loadListProduct();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 sản phẩm", "Thông báo");
            }
        }

        private void lsvListProduct_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column >= 0)
            {
                ColumnHeader col = lsvListProduct.Columns[e.Column];
                String columText = col.Text;
                String query = $"exec USP_SortTableProductBy{columText}";
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                loadListProduct(data);
            }
        }
        #endregion

        #region Method of list Customer
        private void loadListCustomer(DataTable data = null)
        {
            customerList = CustomerDAO.Instance.loadListCustomer(data);
            lsvListCustomer.Items.Clear();
            foreach (Customer customer in customerList)
            {
                ListViewItem lsvItem = new ListViewItem(customer.CustomerID.ToString());

                lsvItem.SubItems.Add(customer.CustomerName.ToString());
                lsvItem.SubItems.Add(customer.Phone.ToString());
                lsvItem.SubItems.Add(customer.TypeCustomer.ToString());
                lsvItem.SubItems.Add(FormatMoney.Instance.transformFormat(customer.Debt));

                lsvListCustomer.Items.Add(lsvItem);
            }
        }

        private void lsvListCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvListCustomer.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvListCustomer.SelectedItems[0];
                Customer customer = CustomerDAO.Instance.getCustomerByID(customerList, int.Parse(item.SubItems[0].Text));
                textBoxCustomerIDOnList.Text = customer.CustomerID.ToString();
                comboBoxCustomerNameOnList.Text = customer.CustomerName;
                comboBoxCustomerPhoneOnList.Text = customer.Phone;
                textBoxTypeCustomerOnList.Text = customer.TypeCustomer;
                numDebtOnList.Value = (int)customer.Debt;
            }
        }

        private void buttonAddCustomer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thêm khách hàng này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                String query = "exec USP_InsertValueIntoCustomers @CustomerName , @Phone , @TypeCustomer , @Debt";

                DataProvider.Instance.ExecuteQuery(query, new Object[] {
                    comboBoxCustomerNameOnList.Text,
                    comboBoxCustomerPhoneOnList.Text,
                    textBoxTypeCustomerOnList.Text,
                    numDebtOnList.Value});
                loadListCustomer();
            }
        }


        #endregion
        #endregion



    }
}

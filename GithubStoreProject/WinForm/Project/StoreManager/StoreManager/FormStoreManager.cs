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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StoreManager
{
    public partial class FormStoreManager : Form
    {
        public FormStoreManager()
        {
            InitializeComponent();
            loadListProduct();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdmin f = new FormAdmin();
            f.ShowDialog();
        }

        #region Method
        private void loadListProduct()
        {
            List<Product> productList = ProductDAO.Instance.loadListProduct();
            lsvListProduct.Items.Clear();
            foreach (Product product in productList)
            {
                ListViewItem lsvItem = new ListViewItem(product.ProductID.ToString());
                lsvItem.SubItems.Add(product.ProductName.ToString());
                lsvItem.SubItems.Add(product.Unit.ToString());
                lsvItem.SubItems.Add(product.Price.ToString());
                lsvItem.SubItems.Add(product.ImportPrice.ToString());
                lsvItem.SubItems.Add(product.QuantityOnHand.ToString());

                lsvListProduct.Items.Add(lsvItem);
            }
        }

        private void loadListProductByDataTable(DataTable data)
        {
            List<Product> productList = ProductDAO.Instance.loadListProduct(data);
            lsvListProduct.Items.Clear();
            foreach (Product product in productList)
            {
                ListViewItem lsvItem = new ListViewItem(product.ProductID.ToString());
                lsvItem.SubItems.Add(product.ProductName.ToString());
                lsvItem.SubItems.Add(product.Unit.ToString());
                lsvItem.SubItems.Add(product.Price.ToString());
                lsvItem.SubItems.Add(product.ImportPrice.ToString());
                lsvItem.SubItems.Add(product.QuantityOnHand.ToString());

                lsvListProduct.Items.Add(lsvItem);
            }
        }
        #endregion


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
                textBoxProductID.Text = item.SubItems[0].Text;
                cbProductName.Text = item.SubItems[1].Text;
                cbUnit.Text = item.SubItems[2].Text;
                numPrice.Value = int.Parse(item.SubItems[3].Text);
                numImportPrice.Value = int.Parse(item.SubItems[4].Text);
                numQuatity.Value = int.Parse(item.SubItems[5].Text);
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
                loadListProductByDataTable(data);
            }
        }
    }
}

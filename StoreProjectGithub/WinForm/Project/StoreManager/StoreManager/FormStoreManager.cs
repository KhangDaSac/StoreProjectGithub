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


namespace StoreManager
{
    public partial class FormStoreManager : Form
    {
        #region Properties
        private static List<Product> productList = new List<Product>();
        private static List<Customer> customerList = new List<Customer>();
        private static List<Employee> employeeList = new List<Employee>();

        #endregion
        public FormStoreManager()
        {
            InitializeComponent();
            loadListProduct();
            loadListCustomer();
            loadListEmployee();
            loadToolBoxListCustomer();
            loadToolBoxListEmployee();
            loadToolBoxListProduct();
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

                loadToolBoxListProduct();
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
                comboBoxProductIDOnList.Text = product.ProductID.ToString();
                comboBoxProductNameOnList.Text = product.ProductName;
                comboBoxUnitOnList.Text = product.Unit;
                numericUpDownPriceOnList.Value = (int)product.Price;
                numericUpDownImportPriceOnList.Value = (int)product.ImportPrice;
                numericUpDownQuatityOnList.Value = product.QuantityOnHand;
            }
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thêm sán phẩm này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                String query = "exec USP_InsertValueIntoProducts @ProductName , @Unit , @Price , @ImportPrice , @QuantityOnHand";

                DataProvider.Instance.ExecuteQuery(query, new Object[] {
                    comboBoxProductNameOnList.Text,
                    comboBoxUnitOnList.Text,
                    numericUpDownPriceOnList.Value,
                    numericUpDownImportPriceOnList.Value,
                    numericUpDownQuatityOnList.Value });
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
                        comboBoxProductIDOnList.Text,
                        comboBoxProductNameOnList.Text,
                        comboBoxUnitOnList.Text,
                        numericUpDownPriceOnList.Value,
                        numericUpDownImportPriceOnList.Value,
                        numericUpDownQuatityOnList.Value });
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
                loadToolBoxListCustomer();
            }
        }

        private void lsvListCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvListCustomer.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvListCustomer.SelectedItems[0];
                Customer customer = CustomerDAO.Instance.getCustomerByID(customerList, int.Parse(item.SubItems[0].Text));
                comboBoxCustomerIDOnList.Text = customer.CustomerID.ToString();
                comboBoxCustomerNameOnList.Text = customer.CustomerName;
                comboBoxCustomerPhoneOnList.Text = customer.Phone;
                comboBoxTypeCustomerOnList.Text = customer.TypeCustomer;
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
                    comboBoxTypeCustomerOnList.Text,
                    numDebtOnList.Value});
                loadListCustomer();
            }
        }

        private void buttonDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (lsvListCustomer.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Bạn có thực sự muốn xóa khách hàng này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    String idCustomerDeleteStr = lsvListCustomer.SelectedItems[0].SubItems[0].Text;
                    int idCustomerDelete = int.Parse(idCustomerDeleteStr);
                    String query = "exec USP_DeleteCustomerById @CustomerID";

                    DataProvider.Instance.ExecuteQuery(query, new Object[] { idCustomerDelete });
                    loadListCustomer();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 khách hàng", "Thông báo");
            }
        }

        private void buttonUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (lsvListCustomer.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Bạn có thực sự muốn chỉnh sửa thông tin khách hàng này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    String query = "exec USP_UpdateValueFormCustomer @CustomerID , @CustomerName , @Phone , @TypeCustomer , @Debt ";
                    DataProvider.Instance.ExecuteQuery(query, new Object[] {
                        comboBoxCustomerIDOnList.Text,
                        comboBoxCustomerNameOnList.Text,
                        comboBoxCustomerPhoneOnList.Text,
                        comboBoxTypeCustomerOnList.Text,
                        numDebtOnList.Value,});
                    loadListCustomer();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 khách hàng", "Thông báo");
            }
        }
        #endregion

        #region Method of list Employee
        private void loadListEmployee(DataTable data = null)
        {
            employeeList = EmployeeDAO.Instance.loadListEmployee(data);
            lsvListEmployee.Items.Clear();
            foreach (Employee employee in employeeList)
            {
                ListViewItem lsvItem = new ListViewItem(employee.EmployeeID.ToString());

                lsvItem.SubItems.Add(employee.EmployeeName.ToString());
                lsvItem.SubItems.Add(employee.Phone.ToString());
                lsvItem.SubItems.Add(employee.TypeEmployee.ToString());

                lsvListEmployee.Items.Add(lsvItem);
                loadToolBoxListEmployee();
            }
        }

        private void lsvListEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvListEmployee.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvListEmployee.SelectedItems[0];
                Employee employee = EmployeeDAO.Instance.getEmployeeByID(employeeList, int.Parse(item.SubItems[0].Text));
                comboBoxEmployeeIDOnList.Text = employee.EmployeeID.ToString();
                comboBoxEmployeeNameOnList.Text = employee.EmployeeName.ToString();
                comboBoxEmployeePhoneOnList.Text = employee.Phone.ToString();
                comboBoxTypeEmployeeOnList.Text = employee.TypeEmployee.ToString();
            }
        }

        private void buttonAddEmployee_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thêm nhân viên này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                String query = "exec USP_InsertValueIntoEmployees @EmployeeName , @Phone , @TypeEmployee";

                DataProvider.Instance.ExecuteQuery(query, new Object[] {
                    comboBoxEmployeeNameOnList.Text,
                    comboBoxEmployeePhoneOnList.Text,
                    comboBoxTypeEmployeeOnList.Text});
                loadListEmployee();
            }
        }

        private void buttonDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (lsvListEmployee.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Bạn có thực sự muốn xóa nhân viên này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    String idEmployeeDeleteStr = lsvListEmployee.SelectedItems[0].SubItems[0].Text;
                    int idEmployeeDelete = int.Parse(idEmployeeDeleteStr);
                    String query = "exec USP_DeleteEmployeeById @EmployeeID";

                    DataProvider.Instance.ExecuteQuery(query, new Object[] { idEmployeeDelete });
                    loadListEmployee();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 nhân viên", "Thông báo");
            }
        }

        private void buttonUpdateEmployee_Click(object sender, EventArgs e)
        {
            if (lsvListEmployee.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Bạn có thực sự muốn chỉnh sửa thông tin nhân viên này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    String query = "exec USP_UpdateValueFormEmployee @EmployeeID , @EmployeeName , @Phone , @TypeEmployee";
                    DataProvider.Instance.ExecuteQuery(query, new Object[] {
                        comboBoxEmployeeIDOnList.Text,
                        comboBoxEmployeeNameOnList.Text,
                        comboBoxEmployeePhoneOnList.Text,
                        comboBoxTypeEmployeeOnList.Text});
                    loadListEmployee();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 nhân viên này", "Thông báo");
            }
        }

        #endregion

        #region Method new buy
        private void loadToolBoxListCustomer()
        {
            //On buy
            comboBoxCustomerID.DataSource = customerList;
            comboBoxCustomerID.DisplayMember = "CustomerID";
            comboBoxCustomerID.SelectedIndex = -1;

            comboBoxCustomerName.DataSource = customerList;
            comboBoxCustomerName.DisplayMember = "CustomerName";
            comboBoxCustomerName.SelectedIndex = -1;

            //On list Customer
            comboBoxCustomerIDOnList.DataSource = customerList;
            comboBoxCustomerIDOnList.DisplayMember = "CustomerID";
            comboBoxCustomerIDOnList.SelectedIndex = -1;

            comboBoxCustomerNameOnList.DataSource = customerList;
            comboBoxCustomerNameOnList.DisplayMember = "CustomerName";
            comboBoxCustomerNameOnList.SelectedIndex = -1;

            comboBoxCustomerPhoneOnList.DataSource = customerList;
            comboBoxCustomerPhoneOnList.DisplayMember = "Phone";
            comboBoxCustomerPhoneOnList.SelectedIndex = -1;

            List<String> list = new List<String>() { "VIP", "VL" };
            comboBoxTypeCustomerOnList.DataSource = list;
            comboBoxTypeCustomerOnList.SelectedIndex = -1;
        }

        private void loadToolBoxListEmployee()
        {
            //On buy
            comboBoxEmployeeID.DataSource = employeeList;
            comboBoxEmployeeID.DisplayMember = "EmployeeID";
            comboBoxEmployeeID.SelectedIndex = -1;

            comboBoxEmployeeName.DataSource = employeeList;
            comboBoxEmployeeName.DisplayMember = "EmployeeName";
            comboBoxEmployeeName.SelectedIndex = -1;

            //On list Employee
            comboBoxEmployeeIDOnList.DataSource = employeeList;
            comboBoxEmployeeIDOnList.DisplayMember = "EmployeeID";
            comboBoxEmployeeIDOnList.SelectedIndex = -1;

            comboBoxEmployeeNameOnList.DataSource = employeeList;
            comboBoxEmployeeNameOnList.DisplayMember = "EmployeeName";
            comboBoxEmployeeNameOnList.SelectedIndex = -1;

            comboBoxEmployeePhoneOnList.DataSource = employeeList;
            comboBoxEmployeePhoneOnList.DisplayMember = "Phone";
            comboBoxEmployeePhoneOnList.SelectedIndex = -1;

            List<String> list = new List<String>() { "QL", "BH" };
            comboBoxTypeEmployeeOnList.DataSource = list;
            comboBoxTypeEmployeeOnList.SelectedIndex = -1;
        }

        private void loadToolBoxListProduct()
        {
            //On buy
            comboBoxProductID.DataSource = productList;
            comboBoxProductID.DisplayMember = "ProductID";
            comboBoxProductID.SelectedIndex = -1;

            comboBoxProductName.DataSource = productList;
            comboBoxProductName.DisplayMember = "ProductName";
            comboBoxProductName.SelectedIndex = -1;

            numericUpDownPrice.Value = 0;

            //On list Product
            comboBoxProductIDOnList.DataSource = productList;
            comboBoxProductIDOnList.DisplayMember = "ProductID";
            comboBoxProductIDOnList.SelectedIndex = -1;

            comboBoxProductNameOnList.DataSource = productList;
            comboBoxProductNameOnList.DisplayMember = "ProductName";
            comboBoxProductNameOnList.SelectedIndex = -1;


        }

        private void comboBoxProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product product = comboBoxProductID.SelectedItem as Product;
            if (product != null)
            {
                numericUpDownPrice.Value = (int)product.Price;
            }
        }

        #endregion
        #endregion















    }
}

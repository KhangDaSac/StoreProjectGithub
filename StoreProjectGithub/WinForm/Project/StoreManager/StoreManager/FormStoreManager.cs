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
        private static List<OrderDetail> newOrderDetailList = new List<OrderDetail>();
        private static List<Order> orderList = new List<Order>();

        #endregion
        public FormStoreManager()
        {
            InitializeComponent();
            loadListProduct();
            loadListCustomer();
            loadListEmployee();
            loadListOrder();
            loadToolBoxListCustomer();
            loadToolBoxListEmployee();
            loadToolBoxListProduct();
            loadOtherToolBox();
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

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thêm sán phẩm này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                Product product = new Product(
                    comboBoxProductNameOnList.Text,
                    comboBoxUnitOnList.Text,
                    (double)numericUpDownPriceOnList.Value,
                    (double)numericUpDownImportPriceOnList.Value,
                    (int)numericUpDownQuatityOnList.Value);
                ProductDAO.Instance.addProduct(product);

                loadListProduct();
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
                    ProductDAO.Instance.deleteProductByID(idProductDelete);
                    loadListProduct();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 sản phẩm", "Thông báo");
            }
        }

        private void buttonUpdateProduct_Click(object sender, EventArgs e)
        {
            if (lsvListProduct.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Bạn có thực sự muốn chỉnh sửa sán phẩm này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    Product product = new Product(
                        int.Parse(comboBoxProductIDOnList.Text),
                        comboBoxProductNameOnList.Text,
                        comboBoxUnitOnList.Text,
                        (double)numericUpDownPriceOnList.Value,
                        (double)numericUpDownImportPriceOnList.Value,
                        (int)numericUpDownQuatityOnList.Value);
                    ProductDAO.Instance.updateProduct(product);
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

                Customer customer = new Customer(
                    comboBoxCustomerNameOnList.Text,
                    comboBoxCustomerPhoneOnList.Text,
                    comboBoxTypeCustomerOnList.Text,
                    (double)numDebtOnList.Value);
                CustomerDAO.Instance.addCustomer(customer);
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

                    CustomerDAO.Instance.deleteCustomerByID(idCustomerDelete);

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

                    Customer customer = new Customer(
                        int.Parse(comboBoxCustomerIDOnList.Text),
                        comboBoxCustomerNameOnList.Text,
                        comboBoxCustomerPhoneOnList.Text,
                        comboBoxTypeCustomerOnList.Text,
                        (double)numDebtOnList.Value);

                    CustomerDAO.Instance.updateCustomer(customer);

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
                comboBoxEmployeeNameOnList.Text = employee.EmployeeName;
                comboBoxEmployeePhoneOnList.Text = employee.Phone;
                comboBoxTypeEmployeeOnList.Text = employee.TypeEmployee;
            }
        }

        private void buttonAddEmployee_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thêm nhân viên này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {


                Employee employee = new Employee(
                    comboBoxEmployeeNameOnList.Text,
                    comboBoxEmployeePhoneOnList.Text,
                    comboBoxTypeEmployeeOnList.Text);

                EmployeeDAO.Instance.addEmployee(employee);

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

                    EmployeeDAO.Instance.deteleEmployeeByID(idEmployeeDelete);

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
                    Employee employee = new Employee(
                        int.Parse(comboBoxEmployeeIDOnList.Text),
                        comboBoxEmployeeNameOnList.Text,
                        comboBoxEmployeePhoneOnList.Text,
                        comboBoxTypeEmployeeOnList.Text);
                    EmployeeDAO.Instance.updateEmployee(employee);

                    loadListEmployee();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 nhân viên này", "Thông báo");
            }
        }

        #endregion

        #region Method of list OrderDislay

        private void loadListOrder(DataTable data = null)
        {
            orderList = OrderDAO.Instance.loadListOrder(data);
            lsvListOrder.Items.Clear();
            foreach (Order orderDisplay in orderList)
            {
                ListViewItem lsvItem = new ListViewItem(orderDisplay.OrderID.ToString());
                lsvItem.SubItems.Add(orderDisplay.CustomerID.ToString());
                lsvItem.SubItems.Add(orderDisplay.CustomerName);
                lsvItem.SubItems.Add(orderDisplay.EmployeeID.ToString());
                lsvItem.SubItems.Add(orderDisplay.EmployeeName);
                lsvItem.SubItems.Add(orderDisplay.OrderDate.ToString());
                lsvItem.SubItems.Add(FormatMoney.Instance.transformFormat(orderDisplay.Amount));
                lsvItem.SubItems.Add(orderDisplay.PaymentMethod);

                lsvListOrder.Items.Add(lsvItem);
            }

        }


        private void buttonSeeOrderDetail_Click(object sender, EventArgs e)
        {
            if (lsvListOrder.SelectedItems.Count > 0)
            {
                String idOrderStr = lsvListOrder.SelectedItems[0].SubItems[0].Text;
                int idOrder = int.Parse(idOrderStr);

                FormOrderDetail orderDetail = new FormOrderDetail(idOrder);
                orderDetail.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 sản phẩm", "Thông báo");
            }
        }

        #endregion

        #region Method new buy
        private void loadToolBoxListCustomer()
        {
            //On buy
            List<Customer> customerListCopy = new List<Customer>();
            foreach (Customer customer in customerList)
            {
                customerListCopy.Add(customer);
            }

            comboBoxCustomerID.DataSource = customerListCopy;
            comboBoxCustomerID.DisplayMember = "CustomerID";
            comboBoxCustomerID.SelectedIndex = -1;

            comboBoxCustomerName.DataSource = customerListCopy;
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
            List<Employee> employeeListCopy = new List<Employee>();
            foreach (Employee employee in employeeList)
            {
                employeeListCopy.Add(employee);
            }

            comboBoxEmployeeID.DataSource = employeeListCopy;
            comboBoxEmployeeID.DisplayMember = "EmployeeID";
            comboBoxEmployeeID.SelectedIndex = -1;

            comboBoxEmployeeName.DataSource = employeeListCopy;
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
            List<Product> productListCopy = new List<Product>();
            foreach (Product product in productList)
            {
                productListCopy.Add(product);
            }

            comboBoxProductID.DataSource = productListCopy;
            comboBoxProductID.DisplayMember = "ProductID";
            comboBoxProductID.SelectedIndex = -1;

            comboBoxProductName.DataSource = productListCopy;
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

        private void loadOtherToolBox()
        {
            List<String> list = new List<String>() { "Tiền mặt", "Momo", "Ngân hàng", "Ghi nợ" };
            comboBoxPaymentMethod.DataSource = list;
            comboBoxPaymentMethod.SelectedIndex = -1;
        }

        private void comboBoxProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product product = comboBoxProductID.SelectedItem as Product;
            if (product != null)
            {
                numericUpDownPrice.Value = (int)product.Price;
            }
        }

        private void loadOrderDetail()
        {
            lsvNewListOrderDetail.Items.Clear();
            foreach (OrderDetail orderDetail in newOrderDetailList)
            {

                ListViewItem lsvItem = new ListViewItem(orderDetail.ProductName);

                lsvItem.SubItems.Add(FormatMoney.Instance.transformFormat(orderDetail.Price));
                lsvItem.SubItems.Add(orderDetail.Quantity.ToString());
                lsvItem.SubItems.Add(FormatMoney.Instance.transformFormat(orderDetail.Charge));
                lsvNewListOrderDetail.Items.Add(lsvItem);
                loadTotalAmount();
            }
        }

        private void buttonAddProductIntoOrder_Click(object sender, EventArgs e)
        {
            OrderDetail orderDetail = new OrderDetail(
                int.Parse(comboBoxProductID.Text),
                comboBoxProductName.Text,
                (double)numericUpDownPrice.Value,
                (int)numericUpDownQuatity.Value);
            newOrderDetailList.Add(orderDetail);

            loadOrderDetail();
        }

        private void buttonDeleteProductIntoOrder_Click(object sender, EventArgs e)
        {
            if (lsvNewListOrderDetail.SelectedItems.Count > 0)
            {
                int index = lsvNewListOrderDetail.SelectedIndices[0];
                newOrderDetailList.RemoveAt(index);
                loadOrderDetail();
            }
        }

        private void loadTotalAmount()
        {
            double totalAmount = 0;
            foreach (OrderDetail orderDetail in newOrderDetailList)
            {
                totalAmount += orderDetail.Charge;
            }
            numericUpDownTotalAmount.Value = (int)totalAmount;
        }

        private void buttonBuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn mua đơn hàng này?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                Order order = new Order(
                int.Parse(comboBoxCustomerID.Text),
                int.Parse(comboBoxEmployeeID.Text),
                dateTimePickerOrderDate.Value,
                (double)numericUpDownTotalAmount.Value,
                comboBoxPaymentMethod.Text);

                OrderDAO.Instance.addOrder(order);
                loadListOrder();
            }

                
        }
        #endregion
        #endregion





















    }
}

using StoreManager.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreManager
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
            LoadAccountList();
        }

        private void LoadAccountList()
        {
            String query = "select AccountName, LoginName, TypeAcc from Accounts";

            dgvAccountList.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
    }
}

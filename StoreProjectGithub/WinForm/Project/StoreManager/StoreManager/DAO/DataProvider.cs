using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.DAO
{
    public class DataProvider
    {
        //Kiến trúc Singleton
        private static DataProvider instance;
        public static DataProvider Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }
                return DataProvider.instance;
            }

            private set
            {
                DataProvider.instance = value;
            }
        }

        String connectionStr = "Data Source=.\\KHANG;Initial Catalog=StoreManager;Integrated Security=True";

        public DataTable ExecuteQuery(String query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    String[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (String item in listPara) 
                    { 
                        if (item.Contains("@"))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(data);

                connection.Close();

                
            }
            return data;
        }

        public int ExecuteNonQuery(String query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    String[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (String item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                data = cmd.ExecuteNonQuery();

            }
            return data;
        }

        public Object ExecuteScalar(String query, object[] parameter = null)
        {
            Object data = new Object();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    String[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (String item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                data = cmd.ExecuteScalar();

            }
            return data;
        }

        public bool login(String loginName, String passwordAcc)
        {
            String query = "exec USP_Login @LoginName , @PasswordAcc";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {loginName, passwordAcc});

            return result.Rows.Count > 0;
        }
    }
}

using StoreManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.DAO
{
    public class EmployeeDAO
    {
        private static EmployeeDAO instance;

        public static EmployeeDAO Instance { 
            get
            {
                if (instance == null)
                {
                    instance = new EmployeeDAO();
                }
                return instance;
            }
            set 
            { 
                instance = value;
            }
        }

        public List<Employee> loadListEmployee(DataTable table = null)
        {
            DataTable data = null;
            List<Employee> employeeList = new List<Employee>();

            if (table == null)
            {
                String query = "exec USP_GetEmployeeList";
                data = DataProvider.Instance.ExecuteQuery(query);
            }
            else
            {
                data = table;
            }

            foreach (DataRow row in data.Rows)
            {
                Employee p = new Employee(row);
                employeeList.Add(p);
            }
            return employeeList;
        }

        public Employee getEmployeeByID(List<Employee> list, int id)
        {
            foreach (Employee e in list)
            {
                if(e.EmployeeID == id) 
                    return e;
            }
            return null;
        }

        public void addEmployee(Employee employee)
        {
            String query = "exec USP_InsertValueIntoEmployees @EmployeeName , @Phone , @TypeEmployee";

            DataProvider.Instance.ExecuteQuery(query, new Object[] {
                    employee.EmployeeName,
                    employee.Phone,
                    employee.TypeEmployee});
        }

        public void deteleEmployeeByID(int employeeID)
        {
            String query = "exec USP_DeleteEmployeeById @EmployeeID";

            DataProvider.Instance.ExecuteQuery(query, new Object[] { employeeID });
        }

        public void updateEmployee(Employee employee)
        {
            String query = "exec USP_UpdateValueFormEmployee @EmployeeID , @EmployeeName , @Phone , @TypeEmployee";
            DataProvider.Instance.ExecuteQuery(query, new Object[] {
                employee.EmployeeID,
                employee.EmployeeName,
                employee.EmployeeName,
                employee.TypeEmployee});
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.DTO
{
    public class Employee
    {
        private int employeeID;
        private String employeeName;
        private String phone;
        private String typeEmployee;

        public int EmployeeID { get => employeeID; set => employeeID = value; }
        public string EmployeeName { get => employeeName; set => employeeName = value; }
        public string Phone { get => phone; set => phone = value; }
        public string TypeEmployee { get => typeEmployee; set => typeEmployee = value; }

        public Employee() { }

        public Employee(int employeeID, String employeeName, String phone, String typeEmployee)
        {
            this.employeeID = employeeID;
            this.employeeName = employeeName;
            this.phone = phone;
            this.typeEmployee = typeEmployee;
        }

        public Employee(String employeeName, String phone, String typeEmployee)
        {
            this.employeeName = employeeName;
            this.phone = phone;
            this.typeEmployee = typeEmployee;
        }

        public Employee(DataRow row)
        {
            this.EmployeeID = (int)row["EmployeeID"];
            this.EmployeeName = row["EmployeeName"].ToString();
            this.Phone = row["Phone"].ToString();
            this.TypeEmployee = row["TypeEmployee"].ToString();
        }
    }
}

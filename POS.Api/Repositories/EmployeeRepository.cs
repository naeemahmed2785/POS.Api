using POS.Api.Models;
using POS.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace POS.Api.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public bool Delete(int id)
        {
            var query  = $"delete from employee where employeeid = {id}";
            using (SqlConnection conn = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    return true;
                }
            }
        }

        private const string constring = @"SERVER=NAEEM-PC\SQLEXPRESS; initial catalog=POS; Trusted_Connection=true;";

        public ICollection<Employee> GetAll()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from employee", constring);
            var dt = new DataTable();
            da.Fill(dt);

            List<Employee> lst = new List<Employee>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new Employee()
                {
                    EmployeeId = Convert.ToInt16(row["EmployeeId"]),
                    FirstName = row["FirstName"].ToString(),
                    SurName = row["SurName"].ToString(),
                    Phone = row["phone"].ToString(),
                    Email = row["email"].ToString(),
                    Address = row["Address"].ToString(),
                    Town = row["town"].ToString(),
                    Postcode = row["Postcode"].ToString(),
                    Dob = Convert.ToDateTime(row["Dob"])
                });
            }

            return lst;
        }

        public Employee GetById(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter($"select * from Employee where Employeeid = {id}", constring);
            var dt = new DataTable();
            da.Fill(dt);

            Employee Employee = new Employee();
            foreach (DataRow row in dt.Rows)
            {
                Employee.EmployeeId = Convert.ToInt16(row["EmployeeId"]);
                Employee.FirstName = row["FirstName"].ToString();
                Employee.SurName = row["SurName"].ToString();
                Employee.Phone = row["phone"].ToString();
                Employee.Email = row["email"].ToString();
                Employee.Address = row["Address"].ToString();
                Employee.Town = row["town"].ToString();
                Employee.Postcode = row["Postcode"].ToString();
                Employee.Dob = Convert.ToDateTime(row["Dob"]);
            }
            return Employee;
        }

        public Employee insert(Employee t)
        {
            string query = $"insert into Employee(FirstName,SurName,email,phone, Address,Town,Postcode, Dob)";
            query += $"values('{t.FirstName}', '{t.SurName}', '{t.Email}', '{t.Phone}', '{t.Address}', '{t.Town}', '{t.Postcode}', '{t.Dob.ToString("yyyy-MM-dd")}');";
            query += "SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var id = executeQuery(query);
            return GetById(Convert.ToInt32(id));
        }

        public Employee update(Employee t)
        {
            string query = $"update Employee set ";
            query += $"FirstName = '{t.FirstName}', Dob = '{t.Dob.ToString("yyyy-MM-dd")}', surName='{t.SurName}', phone='{t.Phone}', email='{t.Email}', Address='{t.Address}', town='{t.Town}', Postcode='{t.Postcode}' where Employeeid = {t.EmployeeId};";
            executeQuery(query);
            return GetById(Convert.ToInt32(t.EmployeeId));
        }

        private object executeQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    cmd.CommandText = query;
                    var identity = cmd.ExecuteScalar();
                    cmd.Connection.Close();
                    return identity;
                }
            }
        }
    }
}

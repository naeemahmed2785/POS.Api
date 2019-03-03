using POS.Api.Models;
using POS.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace POS.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private const string constring = @"SERVER=NAEEM-PC\SQLEXPRESS; initial catalog=POS; Trusted_Connection=true;";

        public bool Delete(int id)
        {
            var query = $"delete from Customers where customerid = {id}";
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

        public ICollection<Customer> GetAll()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from customers", constring);
            var dt = new DataTable();
            da.Fill(dt);

            List<Customer> lst = new List<Customer>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new Customer()
                {
                    Id = Convert.ToInt16(row["customerId"]),
                    FirstName = row["FirstName"].ToString(),
                    SurName = row["SurName"].ToString(),
                    Phone = row["phone"].ToString(),
                    Email = row["email"].ToString(),
                    Address = row["Address"].ToString(),
                    Town = row["town"].ToString(),
                    Postcode = row["Postcode"].ToString(),
                });
            }

            return lst;
        }

        public Customer GetById(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter($"select * from customers where customerid = {id}", constring);
            var dt = new DataTable();
            da.Fill(dt);

            Customer customer = new Customer();
            foreach (DataRow row in dt.Rows)
            {
                customer.Id = Convert.ToInt16(row["customerId"]);
                customer.FirstName = row["FirstName"].ToString();
                customer.SurName = row["SurName"].ToString();
                customer.Phone = row["phone"].ToString();
                customer.Email = row["email"].ToString();
                customer.Address = row["Address"].ToString();
                customer.Town = row["town"].ToString();
                customer.Postcode = row["Postcode"].ToString();
            }
            return customer;
        }

        public Customer insert(Customer t)
        {
            string query = $"insert into Customers(FirstName,SurName,email,phone, Address,Town,Postcode)";
            query += $"values('{t.FirstName}', '{t.SurName}', '{t.Email}', '{t.Phone}', '{t.Address}', '{t.Town}', '{t.Postcode}');";
            query += "SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var id = executeQuery(query);
            return GetById(Convert.ToInt32(id));
        }

        public Customer update(Customer t)
        {
            string query = $"update Customers set ";
            query += $"FirstName = '{t.FirstName}', surName='{t.SurName}', phone='{t.Phone}', email='{t.Email}', Address='{t.Address}', town='{t.Town}', Postcode='{t.Postcode}' where customerid = {t.Id};";
            executeQuery(query);
            return GetById(Convert.ToInt32(t.Id));
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

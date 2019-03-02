using POS.Api.Models;
using POS.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Api.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {

        public bool Delete(int id)
        {
            var query = $"Delete from category wherecategoryid = {id}";
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

        public ICollection<Category> Getall()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from category", constring);
            var dt = new DataTable();
            da.Fill(dt);

            List<Category> lst = new List<Category>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new Category()
                {
                    CategoryId = Convert.ToInt16(row["CategoryId"]),
                    CategoryName = row["CategoryName"].ToString(),
                    Description = row["Description"].ToString(),
                });
            }
            return lst;
        }


        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Category insert(Category t)
        {
            throw new NotImplementedException();
        }

        public Category update(Category t)
        {
            throw new NotImplementedException();
        }
    }
}

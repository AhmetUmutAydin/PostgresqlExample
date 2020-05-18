using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using PostgresqlExample.Controllers;
using PostgresqlExample.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgresqlExample.Managers
{
    public class PostgresqlManager: IPostgresqlManager
    {
        private readonly string connectionString;

        public PostgresqlManager(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionString:PostgreSQLConnection"];
        }

        public List<Categories> GetCategories() 
        {
            List<Categories> categories = new List<Categories>();
            using(var con=new NpgsqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM  categories ";
                var cmd = new NpgsqlCommand(sql, con);
                NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Categories category = new Categories();
                    category.Id = Convert.ToInt32(rdr["Id"]);
                    category.Name = rdr["Name"].ToString();
                    category.Picture = rdr.IsDBNull(2) ? (byte[])rdr["Picture"] : null;
                    categories.Add(category);
                }
                return categories;
            }
        }

        public Categories GetCategory(int id)
        {
            Categories category = new Categories();
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM  categories where id =" + id;
                var cmd = new NpgsqlCommand(sql, con);
                NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    category.Id = Convert.ToInt32(rdr["Id"]);
                    category.Name = rdr["Name"].ToString();
                    category.Picture = rdr.IsDBNull(2) ? (byte[])rdr["Picture"] :null ;
                }
                return category;
            }
        }

        public List<Posts> GetPosts()
        {
            List<Posts> posts = new List<Posts>();
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM  posts ";
                var cmd = new NpgsqlCommand(sql, con);
                NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Posts post = new Posts();
                    post.Id = Convert.ToInt32(rdr["Id"]);
                    post.Message = rdr["Message"].ToString();
                    post.OperationDate =Convert.ToDateTime(rdr["OperationDate"]);
                    posts.Add(post);
                }
                return posts;
            }
        }

        public Posts GetPost(int id)
        {
            Posts post = new Posts();
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM  posts where id =" + id;
                var cmd = new NpgsqlCommand(sql, con);
                NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    post.Id = Convert.ToInt32(rdr["Id"]);
                    post.Message = rdr["Message"].ToString();
                    post.OperationDate = Convert.ToDateTime(rdr["OperationDate"]);
                }

                return post;
            }
        }

        public Posts SavePost(Posts post)
        {
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                using (NpgsqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO  Posts (Message, OperationDate) VALUES (@message,@operationDate)";
                    cmd.Parameters.AddWithValue("@message", post.Message);
                    cmd.Parameters.AddWithValue("@operationDate", post.OperationDate);
                    int result = cmd.ExecuteNonQuery();
                    return post;
                }
            }
        }

        public void UpdatePost(int id,Posts post)
        {
            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                using (NpgsqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE posts  SET message = @message" + ", operationDate= @opDate " + " where id =" + id;
                    cmd.Parameters.AddWithValue("@opDate", post.OperationDate);
                    cmd.Parameters.AddWithValue("@message", post.Message);
                    int result = cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletePost(int Id)
        {
            using (var con = new NpgsqlConnection(connectionString))
            {              
                con.Open();
                using (NpgsqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM posts WHERE id =" + Id;

                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {

                    }
                }             
            }
        }

        public List<Posts> GetCategoryPosts(int categoryId)
        {
            List<Posts> posts = new List<Posts>();

            using (var con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT * FROM Posts INNER JOIN PostCategories ON Posts.Id = PostCategories.PostId where PostCategories.CategoryId=" +categoryId ;
                var cmd = new NpgsqlCommand(sql, con);
                NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Posts post = new Posts();
                    post.Id = Convert.ToInt32(rdr["Id"]);
                    post.Message = rdr["Message"].ToString();
                    post.OperationDate = Convert.ToDateTime(rdr["OperationDate"]);
                    posts.Add(post);
                }

                return posts;
            }
        }
  
    }


}

using Microsoft.Data.SqlClient;
using AdoDotNetMvc.Models;

namespace AdoDotNetMvc.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public List<Department> GetAll()
        {
            var departments = new List<Department>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id, Name, Description FROM Departments";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2)
                        });
                    }
                }
            }

            return departments;
        }


        public Department GetById(Guid id)
        {
            Department dept = null!;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id, Name, Description FROM Departments WHERE Id = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dept = new Department
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2)
                        };
                    }
                }
            }

            return dept;
        }


        public void Create(Department department)
        {
           
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Departments (Id, Name, Description) VALUES (@Id, @Name, @Description)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                command.Parameters.AddWithValue("@Name", department.Name);
                command.Parameters.AddWithValue("@Description", (object?)department.Description ?? DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Edit(Guid id, Department department)
        {
             using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Departments SET Name = @Name, Description = @Description WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", department.Name);
                command.Parameters.AddWithValue("@Description", (object?)department.Description ?? DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Departments WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }

        }
    }
}

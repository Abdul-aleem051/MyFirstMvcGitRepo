using AdoDotNetMvc.Models;
using AdoDotNetMvc.ViewModel;
using Microsoft.Data.SqlClient;

namespace AdoDotNetMvc.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task Create(CreateEmployeeViewModel request)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Employees (Id, FirstName, LastName, Email, Department, HireDate, Salary) " +
                                "VALUES (@Id,@FirstName, @LastName, @Email, @Department, @HireDate, @Salary)";


                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                command.Parameters.AddWithValue("@FirstName", request.FirstName);
                command.Parameters.AddWithValue("@LastName", request.LastName);
                command.Parameters.AddWithValue("@Email", request.Email);
                command.Parameters.AddWithValue("@Department", request.Department);
                command.Parameters.AddWithValue("@HireDate", request.HireDate);
                command.Parameters.AddWithValue("@Salary", request.Salary);

                try
                {
                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Employee added successfully!");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public async Task<List<EmployeeViewModel>?> GetAllEmployeesAsync()
        {
            var employees = new List<EmployeeViewModel>();

            using (SqlConnection connection = new(_connectionString))
            {
                string query = "SELECT * FROM Employees";
                SqlCommand command = new(query, connection);

                try
                {
                    await connection.OpenAsync();
                    using SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var employee = new EmployeeViewModel
                        {
                            Id = reader.GetGuid(0),
                            FirstName = reader.GetString(1),
                            LastName = (string)reader["LastName"],
                            Email = (string)reader["Email"],
                            Department = (string)reader["Department"],
                            HireDate = ((DateTime)reader["HireDate"]).ToShortDateString(),
                            Salary = $"${Convert.ToDecimal(reader["Salary"]):F2}"
                        };

                        employees.Add(employee);
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return employees;
        }

        public EmployeeViewModel? SearchById(Guid id)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                string query = "SELECT * FROM Employees WHERE Id = @Id";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    using SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new EmployeeViewModel
                        {
                            Id = reader.GetGuid(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            Department =  reader.GetString(4),
                            HireDate = ((DateTime)reader["HireDate"]).ToString("yyyy-MM-dd HH:mm"),
                            Salary = $"${Convert.ToDecimal(reader["Salary"]):F2}"
                        };
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return null;
        }

        public EmployeeViewModel? SearchByName(string name)
        {


            using (SqlConnection connection = new(_connectionString))
            {


                string query = "SELECT * FROM Employees WHERE FirstName = @FirstName";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@FirstName", name);








                try
                {
                    connection.Open();
                    using SqlDataReader reader = command.ExecuteReader();



                    if (reader.Read())
                    {
                        return new EmployeeViewModel
                        {
                            Id = reader.GetGuid(0),
                            FirstName = reader.GetString(1),
                            LastName = (string)reader["LastName"],
                            Email = (string)reader["Email"],
                            Department = (string)reader["Department"],
                            HireDate = ((DateTime)reader["HireDate"]).ToString("yyyy-MM-dd HH:mm"),
                            Salary = $"${Convert.ToDecimal(reader["Salary"]):F2}"
                        };
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return null;
        }

        public void Edit(UpdateEmployeeViewModel employeeModel)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                string query = @"UPDATE Employees 
                               SET FirstName = @FirstName, 
                                   LastName = @LastName, 
                                   Email = @Email, 
                                   Department = @Department, 
                                   Salary = @Salary, 
                                   HireDate = @HireDate 
                               WHERE Id = @Id";

                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", employeeModel.Id);
                command.Parameters.AddWithValue("@FirstName", employeeModel.FirstName);
                command.Parameters.AddWithValue("@LastName", employeeModel.LastName);
                command.Parameters.AddWithValue("@Email", employeeModel.Email);
                command.Parameters.AddWithValue("@Department", employeeModel.Department);
                command.Parameters.AddWithValue("@Salary", decimal.Parse(employeeModel.Salary.TrimStart('$')));
                command.Parameters.AddWithValue("@HireDate", employeeModel.HireDate);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public UpdateEmployeeViewModel? GetEmployeeForUpdate(Guid id)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                string query = "SELECT * FROM Employees WHERE Id = @Id";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    using SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new UpdateEmployeeViewModel
                        {
                            Id = reader.GetGuid(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email =  reader.GetString(3),
                            Department =  reader.GetString(4),
                            HireDate = (DateTime)reader["HireDate"],
                            Salary = $"${Convert.ToDecimal(reader["Salary"]):F2}"
                        };
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return null;
        }

        public void Delete(Guid id)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                string query = "DELETE FROM Employees WHERE Id = @Id";
                SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
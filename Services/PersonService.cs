using System.Data;
using k8sFormResultViewApp.Models;
using Npgsql;

namespace k8sFormResultViewApp.Services
{
    public class PersonService : IPersonService
    {
        private readonly string _connectionString;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IConfiguration configuration, ILogger<PersonService> logger)
        {
            _connectionString = configuration.GetConnectionString("PostgreSQL")
                ?? throw new ArgumentNullException("PostgreSQL connection string is required");
            _logger = logger;
        }

        public async Task<List<PersonViewModel>> GetAllPersonsAsync()
        {
            var persons = new List<PersonViewModel>();

            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var query = @"
                    SELECT id, first_name, last_name, address, created_at 
                    FROM persons 
                    ORDER BY created_at DESC";

                using var command = new NpgsqlCommand(query, connection);
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    persons.Add(new PersonViewModel
                    {
                        Id = reader.GetInt32("id"),
                        FirstName = reader.GetString("first_name"),
                        LastName = reader.GetString("last_name"),
                        Address = reader.GetString("address"),
                        CreatedAt = reader.GetDateTime("created_at")
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving persons from database");
                throw;
            }

            return persons;
        }

        public async Task<int> GetTotalCountAsync()
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var query = "SELECT COUNT(*) FROM persons";
                using var command = new NpgsqlCommand(query, connection);

                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting total count from database");
                return 0;
            }
        }
    }
}

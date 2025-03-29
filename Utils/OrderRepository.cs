// Repository for accessing order-related data from the SauceDemoDB database.
// Provides methods to verify order presence and retrieve the latest order for a user.

using MySql.Data.MySqlClient;
using SauceDemoTests.Utils;

namespace SauceDemoTests.Database
{
    public static class OrderRepository
    {
        private const string ConnectionString = "server=localhost;user=root;password=ADMIN123;database=SauceDemoDB";

        // Checks if any orders exist for the specified customer username.
        public static bool IsOrderPresentForCustomer(string firstName)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var query = "SELECT COUNT(*) FROM Orders WHERE Username = @FirstName";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@FirstName", firstName);

            var count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        // Retrieves the most recent order placed by the specified user.
        public static OrderRecord? GetLatestOrderForUser(string username)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var query = @"
                SELECT Username, ProductName, TotalPrice, OrderDate 
                FROM Orders 
                WHERE Username = @Username 
                ORDER BY OrderDate DESC 
                LIMIT 1";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Username", username);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new OrderRecord
                {
                    Username = reader.GetString("Username"),
                    ProductName = reader.GetString("ProductName"),
                    TotalPrice = reader.GetDecimal("TotalPrice"),
                    OrderDate = reader.GetDateTime("OrderDate")
                };
            }

            return null;
        }
    }
}

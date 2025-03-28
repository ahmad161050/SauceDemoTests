using MySql.Data.MySqlClient;

namespace SauceDemoTests.Utils
{
    public static class OrderDatabaseHelper
    {
        private const string ConnectionString = "server=localhost;user=root;password=ADMIN123;database=SauceDemoDB";

        public static void InsertOrder(string username, string productName, decimal totalPrice)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var query = @"INSERT INTO Orders (Username, ProductName, TotalPrice)
                  VALUES (@Username, @ProductName, @TotalPrice)";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@ProductName", productName);
            cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);

            cmd.ExecuteNonQuery();
        }

        public static void DeleteOrdersForCustomer(string username)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var deleteQuery = @"DELETE FROM Orders WHERE Username = @Username";

            using var cmd = new MySqlCommand(deleteQuery, connection);
            cmd.Parameters.AddWithValue("@Username", username);

            cmd.ExecuteNonQuery();
        }
        

    }

}

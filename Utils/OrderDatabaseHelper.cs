using MySql.Data.MySqlClient;

namespace SauceDemoTests.Utils
{
    public static class OrderDatabaseHelper
    {
        private const string ConnectionString = "server=localhost;user=root;password=ADMIN123;database=SauceDemoDB";

        public static void InsertOrder(string productName, string firstName, string lastName, string postalCode)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var query = @"INSERT INTO Orders (ProductName, FirstName, LastName, PostalCode)
                          VALUES (@ProductName, @FirstName, @LastName, @PostalCode)";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ProductName", productName);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@PostalCode", postalCode);

            cmd.ExecuteNonQuery();
        }
        public static void DeleteOrdersForCustomer(string firstName)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var deleteQuery = @"DELETE FROM Orders WHERE FirstName = @FirstName";

            using var cmd = new MySqlCommand(deleteQuery, connection);
            cmd.Parameters.AddWithValue("@FirstName", firstName);

            cmd.ExecuteNonQuery();
        }

    }

}

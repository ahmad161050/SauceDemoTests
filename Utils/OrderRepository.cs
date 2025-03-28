using MySql.Data.MySqlClient;

namespace SauceDemoTests.Database
{
    public static class OrderRepository
    {
        private const string ConnectionString = "server=localhost;user=root;password=ADMIN123;database=SauceDemoDB";

        public static bool IsOrderPresentForCustomer(string firstName)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var query = "SELECT COUNT(*) FROM Orders WHERE FirstName = @FirstName";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@FirstName", firstName);

            var count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
    }
}

// Represents a single order record for use in data transfer or assertions.
// Maps to the structure of the Orders table in the database.

public class OrderRecord
{
    // Username of the customer who placed the order.
    public required string Username { get; set; }

    // Name of the product ordered.
    public required string ProductName { get; set; }

    // Total price of the order.
    public decimal TotalPrice { get; set; }

    // Date and time when the order was placed.
    public DateTime OrderDate { get; set; }
}

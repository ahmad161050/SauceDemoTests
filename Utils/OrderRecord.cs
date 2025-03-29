public class OrderRecord
{
    public required string Username { get; set; }
    public required string ProductName { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}

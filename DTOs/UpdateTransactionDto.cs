namespace API.DTOs;

public class UpdateTransactionDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
}
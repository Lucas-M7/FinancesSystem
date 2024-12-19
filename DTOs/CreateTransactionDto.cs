namespace API.DTOs;

public class CreateTransactionDto
{
    public string UserId { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
}
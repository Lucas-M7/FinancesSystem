using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class User : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public ICollection<Transaction> Transactions { get; set; } = default!;
}
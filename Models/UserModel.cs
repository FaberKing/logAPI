namespace LogApi.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
}
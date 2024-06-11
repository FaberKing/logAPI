namespace LogApi.Models.DTOs.User;

public record GetUserResponse(Guid userId, string email, string password, DateTimeOffset createdAt);
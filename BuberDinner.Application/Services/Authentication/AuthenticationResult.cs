namespace BuberDinner.Application.Services.Authentication;

public record AuthenticationResult(
    Guid Id,
    string firstName,
    string lastName,
    string Email,
    string Token
);
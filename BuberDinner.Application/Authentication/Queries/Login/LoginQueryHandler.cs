using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            throw new Exception("User with the given email does not exist");
        }

        if (user.Password != query.Password)
        {
            throw new Exception("Invalid password");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token
        );
    }
}
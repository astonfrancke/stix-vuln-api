using Microsoft.AspNetCore.Mvc;

using StixVuln.Api.DTO.Authentication;
using StixVuln.Core.Authentication.Interfaces;

namespace StixVuln.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationController(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequestDTO request)
    {
        var user = await _userRepository.GetUserByUsername(request.Username);

        if (user == null)
        {
            return BadRequest();
        }

        if (user.Password != request.Password)
        {
            return BadRequest();
        }

        return Ok(new LoginResponseDTO(_jwtTokenGenerator.GenerateToken(user)));
    }
}
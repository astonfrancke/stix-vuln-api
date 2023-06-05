namespace StixVuln.Api.DTO.Authentication;

public record LoginRequestDTO(
    string Username,
    string Password);

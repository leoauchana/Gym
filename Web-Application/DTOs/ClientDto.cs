namespace Web_Application.DTOs;

public class ClientDto
{
    public record ClientRequest(string? name, string? lastName, int dni, string? domicile, int age);
    public record ClientResponse(string? name, string? lastName, int dni, string? domicile, int age);
}

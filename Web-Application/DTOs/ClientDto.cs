namespace Web_Application.DTOs;

public class ClientDto
{
    public record Request(string? name, string? lastName, int dni, string? domicile, int age);
    public record Response(string? name, string? lastName, int dni, string? domicile, int age);
}

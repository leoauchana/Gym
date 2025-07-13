namespace Web_Application.DTOs;

public class PayDto
{
    public record Request(Guid idClient);
    public record Response(DateTime? payDate, double? value, bool isSuccess);
}

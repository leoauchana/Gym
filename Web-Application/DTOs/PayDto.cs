namespace Web_Application.DTOs;

public class PayDto
{
    public record PayRequest(Guid idClient);
    public record PayResponse(DateTime? payDate, double? value, bool isSuccess);
}

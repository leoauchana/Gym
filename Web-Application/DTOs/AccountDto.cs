namespace Web_Application.DTOs;

public class AccountDto
{
    public record PayRequest(string? description);
    public record PayResponse(DateTime? payDate, DateTime? endDate, double? value, bool isSuccess);
    public record CancelResponse(string name, string lastName, int feeNumber, bool isSuccess);
}

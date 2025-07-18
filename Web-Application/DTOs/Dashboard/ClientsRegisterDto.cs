namespace Web_Application.DTOs.Dashboard;

public class ClientsRegisterDto
{
    public record ClientsRegisterResponse(ClientDto.ClientResponse client, EmployeeDto.EmployeeResponse employee, DateTime registerDate);
}

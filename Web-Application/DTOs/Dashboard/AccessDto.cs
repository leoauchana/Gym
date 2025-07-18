namespace Web_Application.DTOs.Dashboard;

public class AccessDto
{
    public record AccessResponse(bool isSuccess, DateTime? accesDate, UserDto.UserResponseLog user);
}

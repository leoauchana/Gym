using Web_Application.DTOs.Dashboard;

namespace Web_Application.Interfaces;

public interface IDashboarService
{
    Task<List<AccessDto>?> GetAccess();
}

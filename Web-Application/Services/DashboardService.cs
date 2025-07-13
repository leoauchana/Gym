using Web_Application.DTOs.Dashboard;
using Web_Application.Interfaces;
using Web_Domain.Entities;
using Web_Domain.Logs;
using Web_Domain.Repository;

namespace Web_Application.Services;

public class DashboardService : IDashboarService
{
    private readonly IRepository _repository;
    public DashboardService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<AccessDto>?> GetAccess()
    {
        var access = await _repository.ListarTodos<LogAccess>(nameof(User), nameof(User.Employee));
        if (access == null) return null;
        var accessDto = access.Select(a => new AccessDto
        {
            isSuccess = a.isSuccess,
            AccessDate = a.AccessDate,
            Name = a.User!.Employee!.Name,
            LastName = a.User.Employee.LastName,
            TypeEmployee = a.User.Employee.TypeEmployee.ToString(),
            File = a.User.Employee.File
        }).ToList();
        return accessDto;
    }
}

using System.Linq.Expressions;
using Web_Domain.Common;

namespace Web_Domain.Repository;

public interface IRepository
{
    Task<List<TEntity>> ListarTodos<TEntity>(params string[] incluidos) where TEntity : EntityBase;
    Task<List<TEntity>> ListarTodosCon<TEntity>(params Expression<Func<TEntity, object>>[] includes) where TEntity : EntityBase;
    Task<List<TEntity>> Listar<TEntity>(Expression<Func<TEntity, bool>> predicado, params string[] incluidos) where TEntity : EntityBase;
    Task<TEntity?> ObtenerElPrimero<TEntity>(Expression<Func<TEntity, bool>> predicado, params string[] incluidos) where TEntity : EntityBase;
    Task Agregar<TEntity>(TEntity entidad) where TEntity : EntityBase;
    Task Actualizar<TEntity>(TEntity entidad) where TEntity : EntityBase;
    Task Eliminar<TEntity>(TEntity entidad) where TEntity : EntityBase;
    Task<TEntity?> ObtenerPorId<TEntity>(Guid id, params string[] incluidos) where TEntity : EntityBase;
    Task<TEntity> ObtenerPorIdCon<TEntity>(Guid id, params Expression<Func<TEntity, object>>[] includes) where TEntity : EntityBase;
    Task<List<TEntity>> ObtenerTodos<TEntity>() where TEntity : EntityBase;
}

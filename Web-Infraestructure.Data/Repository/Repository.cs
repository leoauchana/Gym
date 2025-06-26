using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Web_Domain.Common;
using Web_Domain.Repository;

namespace Web_Infraestructure.Data.Repository;

public class Repository : IRepository
{
    private readonly GymContext _context;
    public Repository(GymContext context)
    {
        _context = context;
    }
    public async Task Actualizar<TEntity>(TEntity entidad) where TEntity : EntityBase
    {
        _context.Update(entidad);
        await _context.SaveChangesAsync();
    }

    public async Task Agregar<TEntity>(TEntity entidad) where TEntity : EntityBase
    {
        await _context.Set<TEntity>().AddAsync(entidad);
        await _context.SaveChangesAsync();
    }

    public async Task Eliminar<TEntity>(TEntity entidad) where TEntity : EntityBase
    {
        _context.Set<TEntity>().Remove(entidad);
        await _context.SaveChangesAsync();
    }

    private IQueryable<TEntity> Incluir<TEntity>(IQueryable<TEntity> consulta, string[] incluidos) where TEntity : EntityBase
    {
        var incluidosConsulta = consulta;

        foreach (var incluido in incluidos)
        {
            incluidosConsulta = incluidosConsulta.Include(incluido);
        }

        return incluidosConsulta;
    }

    public async Task<List<TEntity>> Listar<TEntity>(Expression<Func<TEntity, bool>> predicado, params string[] incluidos) where TEntity : EntityBase
    {
        return await Incluir(_context.Set<TEntity>(), incluidos).Where(predicado).ToListAsync();
    }

    public async Task<List<TEntity>> ListarTodos<TEntity>(params string[] incluidos) where TEntity : EntityBase
    {
        return await Incluir(_context.Set<TEntity>(), incluidos).ToListAsync();
    }

    public async Task<TEntity?> ObtenerElPrimero<TEntity>(Expression<Func<TEntity, bool>> predicado, params string[] incluidos) where TEntity : EntityBase
    {
        return await Incluir(_context.Set<TEntity>(), incluidos).FirstOrDefaultAsync(predicado);
    }

    public async Task<TEntity?> ObtenerPorId<TEntity>(Guid id, params string[] incluidos) where TEntity : EntityBase
    {
        return await Incluir(_context.Set<TEntity>(), incluidos).SingleOrDefaultAsync(e => e.Id == id);
    }
    public async Task<List<TEntity>> ObtenerTodos<TEntity>() where TEntity : EntityBase
    {
        return await _context.Set<TEntity>().ToListAsync();
    }
}

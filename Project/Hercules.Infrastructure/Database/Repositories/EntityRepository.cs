using Microsoft.EntityFrameworkCore;

internal class EntityRepository<T> : IEntityRepository<T> where T : class, IEntityBase
{
    protected readonly HerculesContext _db;
    protected readonly QueryBuilder<T> _builder;
    public EntityRepository(HerculesContext context, QueryBuilder<T> builder)
        => (_db, _builder) = (context, builder);

    public async Task<T?> Get(int id, bool isTracking = false)
    {
        var query = _builder.Build(isTracking);
        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }
    public async Task<T[]> Get(int[] ids, bool isTracking = false)
    {
        var query = _builder.Build(isTracking);
        return await query.Where(e => ids.Contains(e.Id)).ToArrayAsync();
    }
    public async Task<T[]> GetAll(int amount, int page = 0, bool isTracking = false)
    {
        var query = _builder.Build(isTracking);
        return await query.Skip(amount * page).Take(amount).ToArrayAsync();
    }
    public async Task Post(T entity)
    {
        _db.Set<T>().Add(entity);
        await _db.SaveChangesAsync();
    }
    public async Task Update(T entity)
    {
        var query = _builder.Build();
        var dbEntity = await query.FirstOrDefaultAsync(e => e.Id == entity.Id);

        if (dbEntity == null)
            throw new NotFoundException("Updating an unexistent entity");

        _db.Set<T>().Update(entity);
        await _db.SaveChangesAsync();
    }
    public async Task<bool> Delete(int id)
    {
        var query = _builder.Build();
        var dbEntity = await query.FirstOrDefaultAsync(e => e.Id == id);

        if (dbEntity == null)
            return false;

        await _db.Set<T>()
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync();

        return true;
    }
    public async Task Delete(int[] ids)
    {
        await _db.Set<T>()
            .Where(e => ids.Contains(e.Id))
            .ExecuteDeleteAsync();
    }
}
using Microsoft.EntityFrameworkCore;

internal abstract class QueryBuilder<T> where T : class, IEntityBase
{
    protected readonly HerculesContext _db;
    protected QueryBuilder(HerculesContext context) => _db = context; 
    internal abstract IQueryable<T> Build(bool isTracking = false);
}
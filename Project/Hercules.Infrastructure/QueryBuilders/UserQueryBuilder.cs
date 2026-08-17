using Microsoft.EntityFrameworkCore;

internal class UserQueryBuilder : QueryBuilder<UserEntity>
{
    public UserQueryBuilder(HerculesContext context) : base(context) {}
    internal override IQueryable<UserEntity> Build(bool isTracking = false)
    {
        var query = _db.Set<UserEntity>().AsQueryable();

        if (!isTracking)
            query = query.AsNoTracking();

        return query;
    }
}
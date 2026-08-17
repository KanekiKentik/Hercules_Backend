using Microsoft.EntityFrameworkCore;

internal class MuscleGroupQueryBuilder : QueryBuilder<MuscleGroupEntity>
{
    public MuscleGroupQueryBuilder(HerculesContext context) : base(context) {}
    internal override IQueryable<MuscleGroupEntity> Build(bool isTracking = false)
    {
        var query = _db.Set<MuscleGroupEntity>().AsQueryable();

        if (!isTracking)
            query = query.AsNoTracking();

        query = query.Include(m => m.Exercises);

        return query;
    }
}
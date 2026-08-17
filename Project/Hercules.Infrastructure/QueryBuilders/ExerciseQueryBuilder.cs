using Microsoft.EntityFrameworkCore;

internal class ExerciseQueryBuilder : QueryBuilder<ExerciseEntity>
{
    public ExerciseQueryBuilder(HerculesContext context) : base(context) {}
    internal override IQueryable<ExerciseEntity> Build(bool isTracking = false)
    {
        var query = _db.Set<ExerciseEntity>().AsQueryable();

        if (!isTracking)
            query = query.AsNoTracking();

        query = query.Include(e => e.Muscles);

        return query;
    }
}
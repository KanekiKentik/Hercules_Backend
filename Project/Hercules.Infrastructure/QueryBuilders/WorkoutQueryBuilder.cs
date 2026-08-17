using Microsoft.EntityFrameworkCore;

internal class WorkoutQueryBuilder : QueryBuilder<WorkoutEntity>
{
    public WorkoutQueryBuilder(HerculesContext context) : base(context) {}
    internal override IQueryable<WorkoutEntity> Build(bool isTracking = false)
    {
        var query = _db.Set<WorkoutEntity>().AsQueryable();

        if (!isTracking)
            query = query.AsNoTracking();

        query = query.Include(w => w.SessionExercises)
            .ThenInclude(s => s.Sets);

        return query;
    }
}
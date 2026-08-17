using Microsoft.EntityFrameworkCore;

internal class WorkoutsRepository : EntityRepository<WorkoutEntity>, IWorkoutsRepository
{
    public WorkoutsRepository(HerculesContext context, QueryBuilder<WorkoutEntity> builder) : base(context, builder) {}
    public async Task<WorkoutEntity[]> GetAllFiltered(int userId, DateTime? dateFrom = null, DateTime? dateTo = null, bool isTracking = false)
    {
        var query = _builder.Build(isTracking);
        query = query.Where(w => w.UserId == userId);

        if (dateFrom.HasValue) 
            query = query.Where(w => w.StartTime >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(w => w.StartTime <= dateTo.Value);

        return await query.ToArrayAsync();
    }
    public async Task<WorkoutEntity[]> GetAllFiltered(int userId, WorkoutSearchFilter filter, bool isTracking = false)
    {
        var query = _builder.Build(isTracking);
        query = query.Where(w => w.UserId == userId);

        var dateFrom = filter.DateFrom;
        var dateTo = filter.DateTo;

        if (dateFrom.HasValue) 
            query = query.Where(w => w.StartTime >= dateFrom.Value.DateTime);

        if (dateTo.HasValue)
            query = query.Where(w => w.StartTime <= dateTo.Value.DateTime);

        return await query.ToArrayAsync();
    }
    public async Task<WorkoutEntity[]> GetAll(int userId, int amount, int page = 0, bool isTracking = false)
    {
        var query = _builder.Build(isTracking);

        return await query
            .Where(w => w.UserId == userId)
            .Skip(amount * page)
            .Take(amount)
            .ToArrayAsync();
    }
}
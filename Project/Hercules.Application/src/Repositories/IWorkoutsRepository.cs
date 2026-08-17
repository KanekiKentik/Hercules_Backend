public interface IWorkoutsRepository : IEntityRepository<WorkoutEntity>
{
    public Task<WorkoutEntity[]> GetAll(int userId, int ammount, int page = 0, bool isTracking = false);
    public Task<WorkoutEntity[]> GetAllFiltered(int userId, DateTime? dateFrom, DateTime? dateTo, bool isTracking = false);
    public Task<WorkoutEntity[]> GetAllFiltered(int userId, WorkoutSearchFilter filter, bool isTracking = false);
}
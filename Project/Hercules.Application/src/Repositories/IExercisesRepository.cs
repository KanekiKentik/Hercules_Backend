public interface IExercisesRepository : IEntityRepository<ExerciseEntity>
{
    public Task<ExerciseEntity[]> GetFiltered(string? name = null, string[]? muscleGroups = null, bool isTracking = false);
}
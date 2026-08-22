using Microsoft.EntityFrameworkCore;

internal class ExercisesRepository : EntityRepository<ExerciseEntity>, IExercisesRepository
{
    public ExercisesRepository(HerculesContext context, QueryBuilder<ExerciseEntity> builder) : base(context, builder) {}
    public async Task<ExerciseEntity[]> GetFiltered(string? name = null, string[]? muscleGroups = null, bool isTracking = false)
    {
        var querry = _builder.Build(isTracking); 

        if(!string.IsNullOrEmpty(name)) 
            querry = querry.Where(e => e.Name.ToLower().Contains(name.ToLower()));

        if(muscleGroups is { Length: > 0 }) 
            querry = querry.Where(e => muscleGroups.All(m => e.Muscles.Select(m => m.Name.ToLower()).Contains(m.ToLower())));

        return await querry.ToArrayAsync();
    }
}
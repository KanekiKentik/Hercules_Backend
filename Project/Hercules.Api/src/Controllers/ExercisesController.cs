using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("/exercises")]
public class ExercisesController : ControllerBase
{
    private readonly IExercisesRepository _eRepo;
    private readonly IEntityRepository<MuscleGroupEntity> _mRepo;
    public ExercisesController(IExercisesRepository eRepo, IEntityRepository<MuscleGroupEntity> mRepo)
        => (_eRepo, _mRepo) = (eRepo, mRepo);

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var exercises = await _eRepo.GetAll(100);
        if (exercises is not { Length: > 0 }) return NotFound();

        return Ok(exercises.Select(e => e.ToResponse()));
    }

    [HttpGet("get-muscle-groups")]
    public async Task<IActionResult> GetAllMuscleGroups()
    {
        var muscles = await _mRepo.GetAll(100);
        if (muscles is not { Length: > 0 }) return NotFound();

        return Ok(muscles.Select(m => m.ToResponse()));
    }

    [HttpGet("get-filtered")]
    public async Task<IActionResult> GetFiltered([FromQuery] ExerciseSearchFilter filter)
    {
        var exercises = await _eRepo.GetFiltered(filter.Name, filter.MuscleGroups);
        if (exercises is not { Length: > 0 }) return NotFound();

        return Ok(exercises.Select(e => e.ToResponse()));
    }

    [HttpGet("get-by-id")]
    public async Task<IActionResult> Get([FromQuery] int exerciseId)
    {
        var exercise = await _eRepo.Get(exerciseId);
        if (exercise == null) return NotFound();

        return Ok(exercise.ToResponse());
    }
}
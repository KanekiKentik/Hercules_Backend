public sealed class TemplateService : ServiceBase
{
    private readonly ITemplatesRepository _tRepo;
    private readonly IExercisesRepository _eRepo;
    public TemplateService(ITemplatesRepository tRepo, IExercisesRepository eRepo, ICurrentUser user) : base(user)
        => (_tRepo, _eRepo) = (tRepo, eRepo);

    public async Task<TemplateResponse[]> GetUsersTemplates()
    {
        int userId = _user.UserId;
        var templates = await _tRepo.GetAll(userId);

        return templates.Select(t => t.ToResponse()).ToArray();
    }
    public async Task<Result> Post(TemplateRequest request)
    {
        var exercises = await _eRepo.Get(request.ExerciseIds, true);
        if (exercises.Length != request.ExerciseIds.Length)
            return MissingIds(request.ExerciseIds, exercises.Select(e => e.Id));

        int userId = _user.UserId;
        TemplateEntity template = new (userId, request.Name, exercises);

        await _tRepo.Post(template);
        return Result.Success();
    }
    public async Task<Result> Delete(int templateId)
    {
        int userId = _user.UserId;

        var result = await CheckAccessAndGet(templateId, userId);
        if (result.IsFailure)
            return Result.Failure(result.ErrorType);

        await _tRepo.Delete(templateId);
        return Result.Success();
    }
    public async Task<Result> Update(int templateId, TemplateRequest request)
    {
        int userId = _user.UserId;

        var result = await CheckAccessAndGet(templateId, userId, true);
        if (result.IsFailure)
            return Result.Failure(result.ErrorType);
        var template = result.Value;

        var exercises = await _eRepo.Get(request.ExerciseIds, true);
        if (exercises.Length != request.ExerciseIds.Length)
            return MissingIds(request.ExerciseIds, exercises.Select(e => e.Id));

        template.SetName(request.Name);
        template.SetExercises(exercises);
        await _tRepo.Update(template);
        return Result.Success();
    }
    private Result MissingIds(IEnumerable<int> input, IEnumerable<int> real)
    {
        int[] missing = input.Where(id => !real.Contains(id)).ToArray();

        return Result.Failure(ErrorType.NotFound, 
            $"Cannot find exercises with exercise ids: {string.Join(", ", missing)}");
    }
    private async Task<Result<TemplateEntity>> CheckAccessAndGet(int templateId, int userId, bool isTracking = false)
    {
        var template = await _tRepo.Get(templateId, isTracking);
        if (template == null)
            return Result<TemplateEntity>.Failure(ErrorType.NotFound);
        if (template.UserId != userId)
            return Result<TemplateEntity>.Failure(ErrorType.Forbidden);

        return Result<TemplateEntity>.Success(template);
    }
}
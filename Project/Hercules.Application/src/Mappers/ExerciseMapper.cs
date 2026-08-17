public static class ExerciseMapper
{
    public static ExerciseResponse ToResponse(this ExerciseEntity exercise)
    {
        var muscles = exercise.Muscles;
        if (muscles is not { Count: > 0 })
            throw new ArgumentException($"Exercise id: {exercise.Id} does not contain muscle groups");

        var response = new ExerciseResponse(exercise.Id, exercise.Name, exercise.Muscles.Select(m => m.ToResponse()));
        return response;
    }
}
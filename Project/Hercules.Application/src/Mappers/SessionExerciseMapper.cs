public static class SessionExerciseMapper
{
    public static SessionExerciseResponse ToResponse(this SessionExerciseEntity entity)
    {
        var response = new SessionExerciseResponse(entity.Id, entity.ExerciseId, entity.Order, entity.Sets.Select(s => s.ToResponse()));
        return response;
    }
}
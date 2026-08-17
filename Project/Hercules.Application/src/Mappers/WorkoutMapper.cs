public static class WorkoutMapper
{
    public static WorkoutSummaryResponse ToSummaryDTO(this WorkoutEntity workout)
    {
        return new (workout.Id, workout.StartTime, workout.EndTime);
    }

    public static WorkoutDetailedResponse ToDetailedDTO(this WorkoutEntity workout)
    {
        var response = new WorkoutDetailedResponse(workout.Id, workout.StartTime, workout.EndTime, workout.SessionExercises.Select(s => s.ToResponse()).ToArray());
        return response;
    }
}
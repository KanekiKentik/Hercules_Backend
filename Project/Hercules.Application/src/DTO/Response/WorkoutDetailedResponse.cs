public record WorkoutDetailedResponse
{
    public int WorkoutId { get; init; }
    public DateTime StartDateTime { get; init; } = default;
    public DateTime? EndDateTime { get; init; } = default;
    public SessionExerciseResponse[] SessionExercises { get; init; } = [];

    public WorkoutDetailedResponse(int id, DateTime start, DateTime? end, IEnumerable<SessionExerciseResponse> sessionExercises)
        => (WorkoutId, StartDateTime, EndDateTime, SessionExercises) = (id, start, end, sessionExercises.ToArray());
}
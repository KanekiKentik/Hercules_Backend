public record WorkoutSummaryResponse
{
    public int WorkoutId { get; init; }
    public DateTime StartDateTime { get; init; } = default;
    public DateTime? EndDateTime { get; init; } = default;

    public WorkoutSummaryResponse(int workoutId, DateTime startDateTime, DateTime? endDateTime)
        => (WorkoutId, StartDateTime, EndDateTime) = (workoutId, startDateTime, endDateTime);
}
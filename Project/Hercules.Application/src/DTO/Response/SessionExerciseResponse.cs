public record SessionExerciseResponse
{
    public int SessionExerciseId { get; init; }
    public int ExerciseId { get; init; }
    public int Order { get; init; }
    public SetResponse[] Sets { get; init; } = [];

    public SessionExerciseResponse(int id, int exerciseId, int order, IEnumerable<SetResponse> sets)
        => (SessionExerciseId, ExerciseId, Order, Sets) = (id, exerciseId, order, sets.ToArray());
}
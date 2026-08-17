public record ExerciseResponse
{
    public int ExerciseId { get; init; }
    public string Name { get; init; } = string.Empty;
    public MuscleGroupResponse[] MuscleGroups { get; init; } = [];
    public ExerciseResponse(int id, string name, IEnumerable<MuscleGroupResponse> muscles)
        => (ExerciseId, Name, MuscleGroups) = (id, name, muscles.ToArray());
}
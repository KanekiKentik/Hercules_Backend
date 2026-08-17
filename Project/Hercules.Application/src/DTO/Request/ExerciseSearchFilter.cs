public record ExerciseSearchFilter
{
    public string Name { get; init; } = string.Empty;
    public string[] MuscleGroups { get; init; } = [];
}
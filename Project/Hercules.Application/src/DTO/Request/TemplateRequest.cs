public record TemplateRequest
{
    public string Name { get; init; } = string.Empty;
    public int[] ExerciseIds { get; init; } = [];
}
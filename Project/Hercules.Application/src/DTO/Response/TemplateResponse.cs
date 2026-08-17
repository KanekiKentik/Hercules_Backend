public record TemplateResponse
{
    public int TemplateId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int[] ExerciseIds { get; init; } = [];

    public TemplateResponse(int templateId, string name, int[] ids)
        => (TemplateId, Name, ExerciseIds) = (templateId, name, ids.ToArray());
}
public record MuscleGroupResponse
{
    public int MuscleGroupId { get; init; }
    public string Name {get; init; } = string.Empty;

    public MuscleGroupResponse(int id, string name)
        => (MuscleGroupId, Name) = (id, name);
}
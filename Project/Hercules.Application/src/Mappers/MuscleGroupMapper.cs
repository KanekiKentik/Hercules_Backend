public static class MuscleGroupMapper
{
    public static MuscleGroupResponse ToResponse(this MuscleGroupEntity muscle)
    {
        return new (muscle.Id, muscle.Name);
    } 
}
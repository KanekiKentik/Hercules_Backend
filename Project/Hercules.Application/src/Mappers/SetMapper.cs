public static class SetMapper
{
    public static SetResponse ToResponse(this SetEntity set)
    {
        return new (set.Id, set.Weight, set.Reps, set.Order);
    }
}
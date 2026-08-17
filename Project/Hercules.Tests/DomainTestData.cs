internal static partial class TestData
{
    public static MuscleGroupEntity[] GetMuscles(int count = 3)
    {
        var muscles = new MuscleGroupEntity[count];

        for (int i = 0; i < count; i++)
            muscles[i] = new (new ('a', MuscleGroupEntity.MinNameLength));
        return muscles;
    }
    public static ExerciseEntity GetExercise()
    {
        return new (new ('a', ExerciseEntity.MinNameLength), GetMuscles());
    }
}
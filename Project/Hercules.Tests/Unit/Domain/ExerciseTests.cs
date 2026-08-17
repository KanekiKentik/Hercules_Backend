using Xunit;
using Xunit.Abstractions;

namespace Domain;

public class ExerciseEntityTests
{
    private readonly ITestOutputHelper _output;
    public ExerciseEntityTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void SetName_ShortName_ThrowsException()
    {
        string name = new ('a', ExerciseEntity.MinNameLength - 1);

        Assert.Throws<DomainException>(() => new ExerciseEntity(name, TestData.GetMuscles()));
    }

    [Fact]
    public void SetName_LongName_ThrowsException()
    {
        string name = new ('a', ExerciseEntity.MaxNameLength + 1);

        Assert.Throws<DomainException>(() => new ExerciseEntity(name, TestData.GetMuscles()));
    }

    [Fact]
    public void SetName_EmptyName_ThrowsException()
    {
        string name = string.Empty;

        Assert.Throws<DomainException>(() => new ExerciseEntity(name, TestData.GetMuscles()));
    }

    [Fact]
    public void SetName_NullName_ThrowsException()
    {
        string name = null!;

        Assert.Throws<DomainException>(() => new ExerciseEntity(name, TestData.GetMuscles()));
    }

    [Fact]
    public void AddMuscleGroup_AddSame_ReturnsFalse()
    {
        var muscle = TestData.GetMuscles()[0];
        var exercise = TestData.GetExercise();
        int lenBefore = exercise.Muscles.Count();

        bool result = exercise.AddMuscleGroup(muscle);
        int lenAfter = exercise.Muscles.Count();

        Assert.False(result);
        Assert.Equal(lenBefore, lenAfter);
    }

    [Fact]
    public void RemoveMuscleGroup_RemoveUnexistent_ReturnsFalse()
    {
        var muscle = new MuscleGroupEntity(new string('j', MuscleGroupEntity.MaxNameLength));
        var exercise = TestData.GetExercise();
        int lenBefore = exercise.Muscles.Count();

        bool result = exercise.RemoveMuscleGroup(muscle);
        int lenAfter = exercise.Muscles.Count();

        Assert.False(result);
        Assert.Equal(lenBefore, lenAfter);
    }

    [Fact]
    public void SetMuscles_WhenEmptyThrowsException()
    {
        var exercise = TestData.GetExercise();
        var muscles = new MuscleGroupEntity[0];

        Assert.Throws<DomainException>(() => exercise.SetMuscleGroups(muscles));
    }

    [Fact]
    public void SetMuscles_WhenNullThrowsException()
    {
        var exercise = TestData.GetExercise();

        Assert.Throws<DomainException>(() => exercise.SetMuscleGroups(null!));
    }

    [Fact]
    public void SetMuscles_SetDistincts()
    {
        var exercise = TestData.GetExercise();
        var muscles = new MuscleGroupEntity[] { TestData.GetMuscles()[0], TestData.GetMuscles()[0] };
        
        exercise.SetMuscleGroups(muscles);

        Assert.Single(exercise.Muscles);
    }
}

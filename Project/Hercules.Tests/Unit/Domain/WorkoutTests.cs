using FluentAssertions;
using Xunit;

namespace Domain;

public class WorkoutTests
{
    [Theory]
    [MemberData(nameof(GetUpdateTestMethods))]
    public void GenericUpdate_AlreadyCompleted_ResultInvalidOperation(Func<WorkoutEntity, Result> action)
    {
        var workout = TestData.GetFilledWorkout();

        workout.Complete(DateTimeOffset.UtcNow);
        var result = action(workout);

        result.IsFailure.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.InvalidOperation);
    }

    [Fact]
    public void Complete_SessionExercisesEmpty_ResultFailure()
    {
        var workout = TestData.GetEmptyWorkout(1);

        var result = workout.Complete(DateTimeOffset.Now);

        result.IsFailure.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.InvalidOperation);
    }

    [Fact]
    public void Complete_AnySessionExerciseEmpty_ResultFailure()
    {
        var workout = TestData.GetEmptyWorkout(1);
        
        workout.AddSessionExercise(1);
        var result = workout.Complete(DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.InvalidOperation);
    }

    [Fact]
    public void Complete_Valid_SetsEndTime()
    {
        var workout = TestData.GetFilledWorkout(1);
        var time = DateTimeOffset.UtcNow;

        var result = workout.Complete(time);

        result.IsSuccess.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.None);
        workout.EndTime.Should().Be(time.DateTime);
    }

    [Fact]
    public void Complete_LessThanStartTime_ThrowsException()
    {
        var workout = TestData.GetFilledWorkout();
        DateTimeOffset time = DateTimeOffset.UtcNow.AddDays(-1);

        Assert.Throws<DomainException>(() => workout.Complete(time));
    }

    [Fact]
    public void Complete_FutureTime_ThrowsException()
    {
        var workout = TestData.GetFilledWorkout();
        DateTimeOffset time = DateTimeOffset.UtcNow.AddDays(1);

        Assert.Throws<DomainException>(() => workout.Complete(time));
    }

    [Fact]
    public void AddSessionExercise_Valid_Appends()
    {
        var workout = TestData.GetEmptyWorkout();
        int lenBefore = workout.SessionExercises.Count();

        var result = workout.AddSessionExercise(1);

        result.IsSuccess.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.None);
        lenBefore.Should().BeLessThan(workout.SessionExercises.Count());
    }

    [Fact]
    public void AddSessionExercise_Valid_MaxOrderIncreased()
    {
        var workout = TestData.GetFilledWorkout();
        int maxBefore = GetSessionsMaxOrder(workout);

        var result = workout.AddSessionExercise(1);

        result.IsSuccess.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.None);
        maxBefore.Should().BeLessThan(GetSessionsMaxOrder(workout));
    }

    [Fact]
    public void AddSet_Valid_MaxOrderIncreased()
    {
        var workout = TestData.GetFilledWorkout();
        int maxBefore = GetSetMaxOrder(workout, 1);

        var result = workout.AddSet(1, 15, 15);

        result.IsSuccess.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.None);
        maxBefore.Should().BeLessThan(GetSetMaxOrder(workout, 1));
    }

    [Fact]
    public void Start_AncientTime_ThrowsDomainException()
    {
        var workout = TestData.GetFilledWorkout();
        DateTimeOffset time = DateTimeOffset.Parse(ValidationConstants.MinimalTime);

        Assert.Throws<DomainException>(() => workout.Complete(time.AddDays(-1)));
    }

    [Fact]
    public void Start_FutureTime_ThrowsDomainException()
    {
        var workout = TestData.GetFilledWorkout();
        DateTimeOffset time = DateTimeOffset.UtcNow.AddDays(1);

        Assert.Throws<DomainException>(() => workout.Complete(time));
    }

    public static IEnumerable<object[]> GetUpdateTestMethods()
    {
        yield return new object[] { (Func<WorkoutEntity, Result>)(w => w.AddSessionExercise(1)) };
        yield return new object[] { (Func<WorkoutEntity, Result>)(w => w.RemoveSessionExercise(1)) };
        yield return new object[] { (Func<WorkoutEntity, Result>)(w => w.AddSet(1, 15, 15)) };
        yield return new object[] { (Func<WorkoutEntity, Result>)(w => w.UpdateSet(1, 15, 15)) };
        yield return new object[] { (Func<WorkoutEntity, Result>)(w => w.RemoveSet(1)) };
    }
    private int GetSessionsMaxOrder(WorkoutEntity workout)
    {
        if (workout.SessionExercises.Count() == 0)
            return 0;

        return workout.SessionExercises.Max(s => s.Order);
    }
    private int GetSetMaxOrder(WorkoutEntity workout, int sessionId)
    {
        var session = workout.SessionExercises.FirstOrDefault(s => s.Id == sessionId);
        if (session == null)
            return 0;

        return session.Sets.Max(s => s.Order);
    }
}
using Xunit;

namespace Domain;


public class TemlateEntityTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void SetName_NullOrEmpty_ThrowsExeption(string? name)
    {
        var template = TestData.GetTemplate();

        Assert.Throws<DomainException>(() => template.SetName(name!));
    }

    [Fact]
    public void SetName_LengthOutOfRange_ThrowsException()
    {
        string longName = new ('a', TemplateEntity.MaxNameLength + 1); 
        string shortName = new ('a', TemplateEntity.MinNameLength - 1);
        var template = TestData.GetTemplate();

        Assert.Throws<DomainException>(() => template.SetName(shortName));
        Assert.Throws<DomainException>(() => template.SetName(longName));
    }

    [Fact]
    public void SetExercises_Empty_ThrowsExcption()
    {
        var template = TestData.GetTemplate();
        var exercises = new ExerciseEntity[0];

        Assert.Throws<DomainException>(() => template.SetExercises(exercises));
    }
}
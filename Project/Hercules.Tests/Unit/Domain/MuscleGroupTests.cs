using Xunit;

namespace Domain;

public class MuscleGroupEntityTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void SetName_NullOrEmpty_ThrowsDomainException(string? name)
    {
        var muscle = new MuscleGroupEntity(new ('a', MuscleGroupEntity.MinNameLength));

        Assert.Throws<DomainException>(() => muscle.SetName(name!));
    }
}
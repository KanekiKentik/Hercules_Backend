using Xunit;

namespace Domain;

public class UserEntityTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void SetUsername_NullOrEmpty_ThrowsException(string? name)
    {
        var user = TestData.GetUser();

        Assert.Throws<DomainException>(() => user.SetUsername(name!));
    }

    [Fact]
    public void SetUsername_TooShort_ThrowsException()
    {
        var user = TestData.GetUser();
        string name = new ('a', UserEntity.MinUsernameLength - 1);

        Assert.Throws<DomainException>(() => user.SetUsername(name));
    }

    [Fact]
    public void SetUsername_TooLong_ThrowsException()
    {
        var user = TestData.GetUser();
        string name = new ('a', UserEntity.MaxUsernameLength + 1);

        Assert.Throws<DomainException>(() => user.SetUsername(name));
    }

    [Fact]
    public void Create_AncientTime_ThrowsException()
    {
        var time = DateTimeOffset.UtcNow.AddHours(-1);
        var cred = TestData.GetCredentials();

        Assert.Throws<DomainException>(() => new UserEntity(
                cred.Username,
                cred.Password,
                time
        ));
    }

    [Fact]
    public void Create_FutureTime_ThrowsException()
    {
        var time = DateTimeOffset.UtcNow.AddHours(1);
        var cred = TestData.GetCredentials();

        Assert.Throws<DomainException>(() => new UserEntity(
                cred.Username,
                cred.Password,
                time
        ));
    }
}
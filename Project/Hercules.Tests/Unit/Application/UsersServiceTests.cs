using FluentAssertions;
using Moq;
using Xunit;

namespace Application;

public class UsersServiceTests
{
    private readonly Mock<IUsersRepository> _repo;
    private readonly Mock<IPasswordHasher> _hasher;
    private readonly Mock<IJwtProvider> _tokenProvider;
    private readonly Mock<ICurrentUser> _user;
    private readonly UsersService _service;
    public UsersServiceTests()
    {
        _repo = new(); _hasher = new(); _tokenProvider = new(); _user = new();
        _service = new(_hasher.Object, _repo.Object, _tokenProvider.Object, _user.Object);
    }

    [Fact]
    public async Task Register_SameUsername_ResultConflict()
    {
        var cred = TestData.GetCredentials();
        _repo.Setup(r => r.Get(cred.Username))
            .ReturnsAsync(TestData.GetUser());

        var result = await _service.Register(cred);

        result.IsFailure.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.Conflict);
        _repo.Verify(r => r.Post(It.IsAny<UserEntity>()), Times.Never);
    }

    [Fact]
    public async Task Register_ValidUser_IsPosted()
    {
        var cred = TestData.GetCredentials();
        
        var result = await _service.Register(cred);

        result.IsSuccess.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.None);
        _repo.Verify(r => r.Get(cred.Username), Times.Once);
        _repo.Verify(r => r.Post(It.IsAny<UserEntity>()), Times.Once);
        _hasher.Verify(h => h.Generate(cred.Password), Times.Once);
    }

    [Fact]
    public async Task Login_UnexistentUser_NotFound()
    {
        var cred = TestData.GetCredentials();

        var result = await _service.Login(cred);

        result.IsFailure.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.NotFound);
        _tokenProvider.Verify(p => p.GenerateToken(It.IsAny<UserEntity>()), Times.Never);
    }

    [Fact]
    public async Task Login_WrongPassword_ReturnsUnauthorized()
    {
        var cred = TestData.GetCredentials();
        var user = TestData.GetUser();
        _repo.Setup(r => r.Get(cred.Username))
            .ReturnsAsync(user);
        _hasher.Setup(h => h.Verify(user.PasswordHash, cred.Password));

        var result = await _service.Login(cred);

        result.IsFailure.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.Unauthorized);
        _tokenProvider.Verify(p => p.GenerateToken(It.IsAny<UserEntity>()), Times.Never);
    }

    [Fact]
    public async Task Login_CorrectPassword_Successful()
    {
        var cred = TestData.GetCredentials();
        var user = TestData.GetUser();
        _repo.Setup(r => r.Get(cred.Username))
            .ReturnsAsync(user);
        _hasher.Setup(h => h.Verify(user.PasswordHash, cred.Password))
            .Returns(true);
        _tokenProvider.Setup(p => p.GenerateToken(user))
            .Returns("token");

        var result = await _service.Login(cred);
        
        result.IsSuccess.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.None);
        result.Value.Should().Be("token");
        _tokenProvider.Verify(p => p.GenerateToken(It.IsAny<UserEntity>()), Times.Once);
    }

    [Fact]
    public async Task ChangePassword_ExistingUser_SetsHash()
    {
        var user = TestData.GetUser();
        var request = TestData.GetPasswordRequest(user.PasswordHash);
        _repo.Setup(r => r.Get(1))
            .ReturnsAsync(user);
        _hasher.Setup(h => h.Generate(request.Password))
            .Returns($"{request.Password}hash");

        var result = await _service.ChangePassword(request, 1);

        result.IsSuccess.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.None);
        user.PasswordHash.Should().StartWith(request.Password)
            .And.EndWith("hash");
        _repo.Verify(r => r.Update(It.IsAny<UserEntity>()), Times.Once);
        _hasher.Verify(h => h.Generate(request.Password), Times.Once);
    }

    [Fact]
    public async Task ChangePassword_NullUser_ReturnsNotFound()
    {
        var request = TestData.GetPasswordRequest(TestData.GetCredentials().Password);

        var result = await _service.ChangePassword(request, 1);

        result.IsFailure.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public async Task ChangeUsername_Valid_ChangesUsername()
    {
        var user = TestData.GetUser();
        string old = user.Username;
        var request = TestData.GetUsernameRequest($"{old}new");
        _repo.Setup(r => r.Get(1))
            .ReturnsAsync(user);

        var result = await _service.ChangeUsername(request, 1);

        result.IsSuccess.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.None);
        user.Username.Should().StartWith(old)
            .And.EndWith("new");
        _repo.Verify(r => r.Update(It.IsAny<UserEntity>()), Times.Once);
    }

    [Fact]
    public async Task ChangeUsername_NullUser_ReturnsNotFound()
    {
        var request = TestData.GetUsernameRequest();

        var result = await _service.ChangeUsername(request, 1);

        result.IsFailure.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public async Task ChangePrivilege_Valid_ChangesPrivilege()
    {
        var user = TestData.GetUser();
        _repo.Setup(r => r.Get(1))
            .ReturnsAsync(user);
        
        var result = await _service.ChangePrivilege(1, Privilege.Admin);

        result.IsSuccess.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.None);
        user.Privilege.Should().Be(Privilege.Admin);
        _repo.Verify(r => r.Update(It.IsAny<UserEntity>()), Times.Once);
    }

    [Fact]
    public async Task ChangePrivilege_NullUser_ReturnsNotFound()
    {
        var result = await _service.ChangePrivilege(1, Privilege.Admin);

        result.IsFailure.Should().BeTrue();
        result.ErrorType.Should().Be(ErrorType.NotFound);
    }
}
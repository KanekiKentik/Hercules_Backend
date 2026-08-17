public interface IUsersRepository : IEntityRepository<UserEntity>
{
    public Task<UserEntity?> Get(string username, bool isTracking = false);
}
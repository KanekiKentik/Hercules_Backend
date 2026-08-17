using Microsoft.EntityFrameworkCore;

internal class UsersRepository : EntityRepository<UserEntity>, IUsersRepository
{
    public UsersRepository(HerculesContext context, QueryBuilder<UserEntity> builder) : base(context, builder) {}
    public async Task<UserEntity?> Get(string username, bool isTracking = false)
    {
        var query = _builder.Build(isTracking);

        return await query
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync();
    }
}
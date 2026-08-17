using Microsoft.EntityFrameworkCore;

internal class TemplatesRepository : EntityRepository<TemplateEntity>, ITemplatesRepository
{
    public TemplatesRepository(HerculesContext context, QueryBuilder<TemplateEntity> builder) : base(context, builder) {}
    public async Task<TemplateEntity[]> GetAll(int userId, bool isTracking = false)
    {
        var query = _builder.Build(isTracking);

        return await query
            .Where(t => t.UserId == userId)
            .ToArrayAsync();
    }
}
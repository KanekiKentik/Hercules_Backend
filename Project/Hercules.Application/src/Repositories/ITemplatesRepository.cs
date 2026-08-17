public interface ITemplatesRepository : IEntityRepository<TemplateEntity>
{
    public Task<TemplateEntity[]> GetAll(int userId, bool isTrackiking = false);
}
using Microsoft.EntityFrameworkCore;

internal class TemplateQueryBuilder : QueryBuilder<TemplateEntity>
{
    public TemplateQueryBuilder(HerculesContext context) : base(context) {}
    internal override IQueryable<TemplateEntity> Build(bool isTracking = false)
    {
        var query = _db.Set<TemplateEntity>().AsQueryable();

        if (!isTracking)
            query = query.AsNoTracking();

        query = query.Include(t => t.Exercises);

        return query;
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class TemplateConfig : IEntityTypeConfiguration<TemplateEntity>
{
    public void Configure(EntityTypeBuilder<TemplateEntity> builder)
    {
        builder.ToTable("templates");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

        builder.Property(t => t.Name)
                .IsRequired(true)
                .HasMaxLength(TemplateEntity.MaxNameLength);

        builder.HasOne(t => t.User)
                .WithMany(u => u.Templates)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(t => t.UserId)
                .IsRequired(true);
    }
}
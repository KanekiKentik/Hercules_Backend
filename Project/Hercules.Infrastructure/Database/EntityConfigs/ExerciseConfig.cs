using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class ExerciseConfig : IEntityTypeConfiguration<ExerciseEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseEntity> builder)
    {
        builder.ToTable("exercises");

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Name)
            .IsUnique(true);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .IsUnicode(true)
            .IsRequired(true)
            .HasMaxLength(ExerciseEntity.MaxNameLength);

        builder.HasMany(ExerciseEntity.NameofTemplates)
            .WithMany(TemplateEntity.NameofExercises)
            .UsingEntity("exercises_templates");
    }
}
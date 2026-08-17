using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class MuscleGroupConfig : IEntityTypeConfiguration<MuscleGroupEntity>
{
    public void Configure(EntityTypeBuilder<MuscleGroupEntity> builder)
    {
        builder.ToTable("musclegroups");

        builder.HasKey(m => m.Id);

        builder.HasIndex(m => m.Name)
                .IsUnique(true);

        builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

        builder.Property(m => m.Name)
                .IsUnicode(true)
                .IsRequired(true)
                .HasMaxLength(MuscleGroupEntity.MaxNameLength);

        builder.HasMany(m => m.Exercises)
                .WithMany(e => e.Muscles)
                .UsingEntity(j => j.ToTable("exercises_muscles"));
    }
}
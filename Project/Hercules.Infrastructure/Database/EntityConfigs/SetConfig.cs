using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class SetConfig : IEntityTypeConfiguration<SetEntity>
{
    public void Configure(EntityTypeBuilder<SetEntity> builder)
    {
        builder.ToTable("sets");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
                .ValueGeneratedOnAdd();

        builder.HasOne(s => s.SessionExercise)
                .WithMany(se => se.Sets)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(s => s.SessionExerciseId)
                .IsRequired(true);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class SessionExerciseConfig : IEntityTypeConfiguration<SessionExerciseEntity>
{
    public void Configure(EntityTypeBuilder<SessionExerciseEntity> builder)
    {
        builder.ToTable("sessionexercises");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(s => s.Workout)
            .WithMany(w => w.SessionExercises)
            .HasForeignKey(s => s.WorkoutId)
            .IsRequired(true);

        builder.HasOne(s => s.Exercise)
            .WithOne(ExerciseEntity.NameofSessionExercises)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}
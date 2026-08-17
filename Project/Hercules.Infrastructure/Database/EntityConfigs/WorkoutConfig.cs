using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

internal class WorkoutConfig : IEntityTypeConfiguration<WorkoutEntity>
{
    public void Configure(EntityTypeBuilder<WorkoutEntity> builder)
    {
        builder.ToTable("workouts");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
                .ValueGeneratedOnAdd();

        builder.Property(w => w.StartTime)
                .HasColumnType("timestamp without time zone")
                .IsRequired(true);

        builder.Property(w => w.EndTime)
                .HasColumnType("timestamp without time zone")
                .IsRequired(false);

        builder.Ignore(w => w.IsCompleted);

        builder.HasOne(w => w.User)
                .WithMany(u => u.Workouts)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(w => w.UserId)
                .IsRequired(true);
    }
}
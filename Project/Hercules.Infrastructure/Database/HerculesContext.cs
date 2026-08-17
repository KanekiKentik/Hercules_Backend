using Microsoft.EntityFrameworkCore;

internal class HerculesContext : DbContext
{
    internal DbSet<UserEntity> Users { get; private set; }
    internal DbSet<WorkoutEntity> Workouts { get; private set; }
    internal DbSet<ExerciseEntity> Exercises { get; private set; }
    internal DbSet<SessionExerciseEntity> SessionExercises { get; private set; }
    internal DbSet<MuscleGroupEntity> MuscleGroups { get; private set; }
    internal DbSet<SetEntity> Sets { get; private set; }
    internal DbSet<TemplateEntity> Templates { get; private set; }

    public HerculesContext(DbContextOptions<HerculesContext> options) : base(options) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ExerciseConfig());
        modelBuilder.ApplyConfiguration(new MuscleGroupConfig());
        modelBuilder.ApplyConfiguration(new SessionExerciseConfig());
        modelBuilder.ApplyConfiguration(new SetConfig());
        modelBuilder.ApplyConfiguration(new TemplateConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new WorkoutConfig());

        base.OnModelCreating(modelBuilder);
    }
}
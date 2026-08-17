public record SeedingData(string[] MuscleGroups, ExerciseSeed[] Exercises);

public record ExerciseSeed(string Name, string[] MuscleGroups);
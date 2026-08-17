using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class UserConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Username)
                .IsUnique(true);

        builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();

        builder.Property(u => u.Username)
                .HasMaxLength(UserEntity.MaxUsernameLength)
                .IsUnicode(true)
                .IsRequired(true);

        builder.Property(u => u.PasswordHash)
                .IsUnicode(true)
                .IsRequired(true);

        builder.Property(u => u.RegistrationDate)
                .HasColumnType("timestamp without time zone")
                .IsRequired(true);

        builder.Property(u => u.Privilege)
                .HasConversion<string>();
    }
}
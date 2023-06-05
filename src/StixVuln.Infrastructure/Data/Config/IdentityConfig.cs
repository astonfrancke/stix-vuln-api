using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StixVuln.Core.Identity;

namespace StixVuln.Infrastructure.Data.Config;
public class IdentityConfig : IEntityTypeConfiguration<Identity>
{
    public void Configure(EntityTypeBuilder<Identity> builder)
    {
        ConfigureIdentitiesTable(builder);
        ConfigureIdentityClassesTable(builder);
        ConfigureRolesTable(builder);
        ConfigureSectorsTable(builder);
    }

    private void ConfigureIdentitiesTable(EntityTypeBuilder<Identity> builder)
    {
        builder.ToTable("Identitites");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id)
            .ValueGeneratedNever();

        builder.Property(i => i.Name)
            .IsRequired();
    }

    private void ConfigureIdentityClassesTable(EntityTypeBuilder<Identity> builder)
    {
        builder.OwnsOne(i => i.IdentityClass, icb =>
        {
            icb.ToTable("IdentityClasses");
            icb.WithOwner().HasForeignKey("Id");
            icb.HasKey(nameof(IdentityClass.IdentityClassId));
        });
    }

    private void ConfigureRolesTable(EntityTypeBuilder<Identity> builder)
    {
        builder.OwnsMany(i => i.Roles, rb =>
        {
            rb.ToTable("Roles");
            rb.WithOwner().HasForeignKey("Id");
            rb.HasKey(nameof(Role.RoleId));
        });
    }

    private void ConfigureSectorsTable(EntityTypeBuilder<Identity> builder)
    {
        builder.OwnsMany(i => i.Sectors, sb =>
        {
            sb.ToTable("Sectors");
            sb.WithOwner().HasForeignKey("Id");
            sb.HasKey(nameof(Sector.SectorId));
        });
    }
}

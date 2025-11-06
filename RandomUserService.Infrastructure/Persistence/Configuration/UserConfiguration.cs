using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RandomUserService.Domain.Entities;

namespace RandomUserService.Infrastructure.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).ValueGeneratedNever();

            entity.Property(u => u.Gender).HasMaxLength(20);
            entity.Property(u => u.Email).HasMaxLength(255);
            entity.Property(u => u.Phone).HasMaxLength(50);
            entity.Property(u => u.Cell).HasMaxLength(50);
            entity.Property(u => u.Nat).HasMaxLength(10);
            entity.Property(u => u.CreatedAt);

            // --- Name ---
            entity.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.Title).HasColumnName("Name_Title").HasMaxLength(20);
                name.Property(n => n.First).HasColumnName("Name_First").HasMaxLength(100);
                name.Property(n => n.Last).HasColumnName("Name_Last").HasMaxLength(100);
            });

            // --- Location ---
            entity.OwnsOne(u => u.Location, location =>
            {
                location.Property(l => l.City).HasColumnName("Location_City").HasMaxLength(100);
                location.Property(l => l.State).HasColumnName("Location_State").HasMaxLength(100);
                location.Property(l => l.Country).HasColumnName("Location_Country").HasMaxLength(100);
                location.Property(l => l.PostCode).HasColumnName("Location_PostCode");

                // Street (owned by Location)
                location.OwnsOne(l => l.Street, street =>
                {
                    street.Property(s => s.Number).HasColumnName("Location_Street_Number");
                    street.Property(s => s.Name).HasColumnName("Location_Street_Name").HasMaxLength(200);
                });

                // Coordinates (owned by Location)
                location.OwnsOne(l => l.Coordinates, coords =>
                {
                    coords.Property(c => c.Latitude).HasColumnName("Location_Latitude");
                    coords.Property(c => c.Longitude).HasColumnName("Location_Longitude");
                });

                // TimeZone (owned by Location)
                location.OwnsOne(l => l.TimeZone, tz =>
                {
                    tz.Property(t => t.Offset).HasColumnName("Location_TimeZone_Offset").HasMaxLength(10);
                    tz.Property(t => t.Description).HasColumnName("Location_TimeZone_Description").HasMaxLength(200);
                });
            });

            // --- DateOfBirth ---
            entity.OwnsOne(u => u.DateOfBirth, dob =>
            {
                dob.Property(d => d.Date).HasColumnName("DateOfBirth_Date");
                dob.Property(d => d.Age).HasColumnName("DateOfBirth_Age");
            });

            // --- Registered ---
            entity.OwnsOne(u => u.Registered, reg =>
            {
                reg.Property(r => r.Date).HasColumnName("Registered_Date");
                reg.Property(r => r.Age).HasColumnName("Registered_Age");
            });

            // --- External ID ---
            entity.OwnsOne(u => u.ExternalId, ext =>
            {
                ext.Property(e => e.Name).HasColumnName("ExternalId_Name").HasMaxLength(50).IsRequired(false);
                ext.Property(e => e.Value).HasColumnName("ExternalId_Value").HasMaxLength(100).IsRequired(false);
            });

            // --- Picture ---
            entity.OwnsOne(u => u.Picture, pic =>
            {
                pic.Property(p => p.Large).HasColumnName("Picture_Large").HasMaxLength(500);
                pic.Property(p => p.Medium).HasColumnName("Picture_Medium").HasMaxLength(500);
                pic.Property(p => p.Thumbnail).HasColumnName("Picture_Thumbnail").HasMaxLength(500);
            });
        }
    }
}


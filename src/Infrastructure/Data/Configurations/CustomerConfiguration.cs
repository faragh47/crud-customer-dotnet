using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.OwnsOne(w => w.Email, wt =>
        {
            wt.Property(wt => wt.Value).HasColumnName("Email");
        });
        builder.OwnsOne(w => w.PhoneNumber, wt =>
        {
            wt.Property(wt => wt.Value).HasColumnName("PhoneNumber");
        });
        builder.OwnsOne(w => w.BankAccountNumber, wt =>
        {
            wt.Property(wt => wt.Value).HasColumnName("BankAccountNumber");
        });
        builder
            .HasIndex(x => new { x.FirstName, x.LastName, x.DateOfBirth })
            .IsUnique();
    }
}

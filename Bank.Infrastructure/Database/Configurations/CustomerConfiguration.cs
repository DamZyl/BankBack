using Bank.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Infrastructure.Database.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.OwnsOne(
                    x => x.Address,
                    address =>
                    {
                        address.Property(x => x.Street).HasColumnName("Street");
                        address.Property(x => x.Number).HasColumnName("Number");
                        address.Property(x => x.PostCode).HasColumnName("PostCode");
                        address.Property(x => x.City).HasColumnName("City");
                        address.Property(x => x.Country).HasColumnName("Country");
                    }
                );

            builder.Property(x => x.RoleInSystem).HasConversion<string>();
        }
    }
}
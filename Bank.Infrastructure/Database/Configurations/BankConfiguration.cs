using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankEntity = Bank.Domain.Models.Bank;

namespace Bank.Infrastructure.Database.Configurations
{
    public class BankConfiguration : IEntityTypeConfiguration<BankEntity>
    {
        public void Configure(EntityTypeBuilder<BankEntity> builder)
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
        }
    }
}
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Config
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> b)
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.Name).HasMaxLength(200).IsRequired(true);
            b.Property(e => e.ZipCode).HasMaxLength(8).IsRequired(false);
            b.Property(e => e.City).HasMaxLength(150).IsRequired(false);
            b.Property(e => e.State).HasMaxLength(2).IsRequired(false);

            b.HasOne(p => p.Client)
            .WithMany(p => p.AddressList)
            .HasForeignKey(x => x.ClientId);
        }
    }
}
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Config
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> b)
        {
            b.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

            b.Property(x => x.PhoneNumber)
            .HasMaxLength(11)
            .IsRequired(false);

            b.Property(x => x.Logo)
            .HasMaxLength(100)
            .IsRequired(false);            


            b.Property(x => x.UpdateAt)
            .IsRequired(false);

            b.Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()").ValueGeneratedOnAdd()
            .IsRequired();
        }
    }
}
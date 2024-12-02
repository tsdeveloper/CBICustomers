using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Config
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> b)
        {
            b.Property(x => x.Phone)
            .IsRequired(false);

            b.Property(x => x.UpdateAt)
            .IsRequired(false);

            b.Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()").ValueGeneratedOnAdd()
            .IsRequired();
        }
    }
}
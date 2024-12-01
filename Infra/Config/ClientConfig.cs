using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Config
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> b)
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Name).HasMaxLength(200).IsRequired(true);

            b.Property(x => x.UpdateAt)
            .IsRequired(false);

            b.Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()").ValueGeneratedOnAdd()
            .IsRequired();
        }
    }
}
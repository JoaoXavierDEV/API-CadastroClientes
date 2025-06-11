using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XPTO.Domain.Entities;

namespace XPTO.Infrastructure.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //builder.ToTable("Cliente");

            builder.HasKey(a => a.Id);
            //builder.HasKey(a => new { a.Id, a.Email });

            builder
                .Property(a => a.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Telefone)
                .HasMaxLength(15);

            builder.HasOne(a => a.Endereco);
        }
    }


}

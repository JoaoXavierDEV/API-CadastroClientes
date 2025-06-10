using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XPTO.Domain.Entities;

namespace XPTO.Infrastructure.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Rua)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Numero)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(p => p.Cidade)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Estado)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Cep)
                .IsRequired()
                .HasMaxLength(8);

        }
    }

}

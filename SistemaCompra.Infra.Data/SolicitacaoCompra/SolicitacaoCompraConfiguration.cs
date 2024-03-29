﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoAgg.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoAgg.SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompra");
            builder
                .OwnsOne(c => c.TotalGeral, b => b.Property("Value")
                    .HasColumnName("TotalGeral"));

            builder.OwnsOne(x => x.CondicaoPagamento);
            builder.OwnsOne(x => x.NomeFornecedor);
            builder.OwnsOne(x => x.UsuarioSolicitante);
            builder.OwnsOne(x => x.TotalGeral);
        }
    }
}

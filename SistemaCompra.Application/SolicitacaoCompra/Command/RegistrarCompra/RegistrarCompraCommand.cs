using System.Collections.Generic;
using MediatR;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        public IEnumerable<SolicitacaoAgg.Item> Items { get; set; }
        public string UsuarioSolicitante { get; set; }
        public string NomeFornecedor { get; set; }
        public int CondicaoDePamento { get; set; }
    }
}

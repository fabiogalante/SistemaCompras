using MediatR;
using System;
using System.Collections.Generic;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommand : IRequest<bool>
    {
        
        public DateTime Data { get; set; }
        public string UsuarioSolicitante { get; set; }
        public string NomeFornecedor { get; set; }
        public int Condicao { get; set; }
        public IList<Item> Itens { get; set; }
    }
}

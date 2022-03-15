using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }
        public CondicaoPagamento CondicaoPagamento { get; private set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor, CondicaoPagamento condicaoPagamento)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
            CondicaoPagamento = condicaoPagamento;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            
            var list = itens.ToList();

            if (!list.Any())
                throw new BusinessRuleException("A solicitação de compra deve possuir itens!");
          

            var totalGeral = list.Sum(_ => _.Produto.Preco.Value) * list.Single().Qtde;

            var money = new Money(totalGeral);

            if (money.Value > 50000)
            {
                CondicaoPagamento.Valor = 30;
            }

        }
    }
}

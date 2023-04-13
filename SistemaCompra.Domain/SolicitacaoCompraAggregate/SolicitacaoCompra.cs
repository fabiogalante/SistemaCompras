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
        public IList<Item> Itens { get; private set; } = new List<Item>();
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }

        public CondicaoPagamento CondicaoPagamento { get;  set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(
            string usuarioSolicitante, 
            string nomeFornecedor, 
            CondicaoPagamento condicaoPagamento)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
            CondicaoPagamento = condicaoPagamento;
        }

        public SolicitacaoCompra(Money totalGeral)
        {
            TotalGeral = totalGeral;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }


        public void AdicionarTotalGeral(Money totalGeral)
        {
            TotalGeral = totalGeral;
        }


        public void RegistrarCompra(IEnumerable<Item> itens)
        {

            if (!itens.Any())
                throw new BusinessRuleException("A solicitação de compra deve possuir itens!");


            const decimal TotalGeralCondicaoPagamento = 50000;

            decimal totalGeralDaCompra = itens.Sum(iten => iten.Qtde * iten.Produto.Preco.Value);

            

            if (totalGeralDaCompra > TotalGeralCondicaoPagamento)
            {
                CondicaoPagamento.Valor = 30;
            }

        }
    }
}

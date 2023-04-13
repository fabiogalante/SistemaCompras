using MediatR;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly IProdutoRepository _produtoRepository;
        public RegistrarCompraCommandHandler(IUnitOfWork uow, 
            IMediator mediator, 
            ISolicitacaoCompraRepository solicitacaoCompraRepository, 
            IProdutoRepository produtoRepository) : base(uow, mediator)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
            _produtoRepository = produtoRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {

            var solicitacaoCompra = new SolicitacaoAgg
                .SolicitacaoCompra(
                    request.UsuarioSolicitante, 
                    request.NomeFornecedor, 
                    new CondicaoPagamento(request.Condicao));

            var listaProdutos = new List<Item>();
            
            decimal totalGeral = 0;

            foreach (var item in request.Itens)
            {

                var produto = _produtoRepository.Obter(item.Id);
                if (produto == null)
                    throw new BusinessRuleException("Produto não castrado");
                solicitacaoCompra
                    .AdicionarItem(produto, item.Qtde);
                listaProdutos.Add(new Item(
                    new ProdutoAgg.Produto(
                        produto.Nome,
                        produto.Descricao,
                        produto.Categoria.ToString(),
                        produto.Preco.Value),
                    item.Qtde));

                totalGeral += produto.Preco.Value * item.Qtde;
            }

            
            solicitacaoCompra.AdicionarTotalGeral(new Money(totalGeral));



            solicitacaoCompra
                .RegistrarCompra(listaProdutos);

           
            
            _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);

            Commit();
            PublishEvents(solicitacaoCompra.Events);

            return Task.FromResult(true);
        }
    }
}

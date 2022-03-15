using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SistemaCompra.Infra.Data.UoW;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly SolicitacaoAgg.ISolicitacaoCompraRepository _solicitacaoCompraRepository;

        public RegistrarCompraCommandHandler(IUnitOfWork uow, IMediator mediator, SolicitacaoAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository) : base(uow, mediator)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var itens = request.Items;

            var solicitacao = new SolicitacaoAgg.SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor,
                new SolicitacaoAgg.CondicaoPagamento(request.CondicaoDePamento));

            solicitacao.RegistrarCompra(itens);

            Commit();
            PublishEvents(solicitacao.Events);

            return Task.FromResult(true);
        }
    }
}

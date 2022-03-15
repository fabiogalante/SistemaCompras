using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SistemaCompra.API.Produto
{
    public class SolicitacaoCompraControlller : ControllerBase
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraControlller(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}

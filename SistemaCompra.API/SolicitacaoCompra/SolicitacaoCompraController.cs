﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;

namespace SistemaCompra.API.SolicitacaoCompra
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoCompraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost, Route("solicitacaocompra/registrar")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult RegistrarCompra([FromBody]  RegistrarCompraCommand registrarCompraCommand)
        {
            _mediator.Send(registrarCompraCommand);
            return StatusCode(201);
        }
    }
}

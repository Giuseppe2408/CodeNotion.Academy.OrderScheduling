﻿using CodeNotion.Academy.OrderScheduling.Cqrs.Commands;
using CodeNotion.Academy.OrderScheduling.Cqrs.Queries;
using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeNotion.Academy.OrderScheduling.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Order order)
    {
        // assicurarsi che il model sia valid
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var model = new CreateOrderCommand(order);
        var result = await _mediator.Send(model);
        return Ok(result);
    }

    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(typeof(List<Order>), 200)]
    public async Task<IActionResult> List([FromQuery] string? customer, [FromQuery] string? orderNumber)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var query = new GetAllOrderQuery(customer, orderNumber);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut]
    [Route("[action]/{id:int}")]
    public async Task<IActionResult> Update([FromBody] Order data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var model = new UpdateOrderCommand(data);
        var result = await _mediator.Send(model);
        return Ok(result);
    }

    [HttpDelete]
    [Route("[action]/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var model = new DeleteOrderCommand(id);
        await _mediator.Send(model);
        return Ok("removed file");
    }
}
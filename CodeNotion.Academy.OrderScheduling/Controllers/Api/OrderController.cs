using CodeNotion.Academy.OrderScheduling.Commands;
using CodeNotion.Academy.OrderScheduling.Models;
using CodeNotion.Academy.OrderScheduling.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeNotion.Academy.OrderScheduling.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediator _mediator;
    public OrderController(IOrderRepository orderRepository, IMediator mediator)
    {
        _orderRepository = orderRepository;
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
    public async Task<IActionResult> List()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var query = new GetAllOrderQuery();
        var result =  await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut]
    [Route("[action]/{id:int}")]
    public IActionResult Update(int id, Order data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var order = _orderRepository.GetById(id);
        _orderRepository.Update(order ?? throw new InvalidOperationException(), data);
        return Ok(order);
    }

    [HttpDelete]
    [Route("[action]/{id:int}")]
    public IActionResult Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var order = _orderRepository.GetById(id);
        _orderRepository.Delete(order ?? throw new InvalidOperationException());
        return Ok();
    }
}
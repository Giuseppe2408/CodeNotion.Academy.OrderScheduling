using CodeNotion.Academy.OrderScheduling.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeNotion.Academy.OrderScheduling.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpPost]
    public IActionResult Create(Order order)
    {
        // assicurarsi che il model sia valid
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _orderRepository.Create(order);
        return Ok(order);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult List()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var orders = _orderRepository.All();
        return Ok(orders);
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
using System.Diagnostics;
using CodeNotion.Academy.OrderScheduling.Models;
using Microsoft.AspNetCore.Mvc;


namespace CodeNotion.Academy.OrderScheduling.Controllers.Api;

    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase 
    {
        IOrderRepository orderRepository;

        public OrderController(IOrderRepository  _orderRepository)
        {
            orderRepository = _orderRepository;
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            //assicurarsi che il model sia valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            orderRepository.Create(order);
             
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
            
            List<Order> orders = orderRepository.All();
            
            return Ok(orders);
            
             
            
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public IActionResult Update(int id, Order data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Order order = orderRepository.GetById(id);
            
            orderRepository.Update(order, data);
              
            
            return Ok(order);
        }
        
        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Order order = orderRepository.GetById(id);
            orderRepository.Delete(order);
            
            return Ok();
            
        }
        
        
    }

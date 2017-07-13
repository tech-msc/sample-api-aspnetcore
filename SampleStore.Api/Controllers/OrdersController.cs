using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using SampleStore.Api.Data;
using SampleStore.Api.Models;

namespace SampleStore.Api.Controllers
{

    [Produces("application/json")]
    [Route("v1/orders")]
    public class OrderController : Controller
    {
        #region Props
        private readonly DataContext _context;
        #endregion

        #region Ctor
        public OrderController(DataContext context)
        {
            _context = context;
        }
        #endregion


        #region Methods

          // GET: api/orders
          [HttpGet]
          public IEnumerable<Order> GetAll()
          {
              return _context.Orders;
          }

          // GET: api/orders/10
          [HttpGet("{id}")]
          public async Task<IActionResult> GetByID([FromRoute] Guid id)
          {

              if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }

              var order = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);

              if (order == null)
              {
                  return NotFound();
              }

              return Ok(order);

          }

          // PUT: api/orders/10
          [HttpPut("{id}")]
          public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] Order order)
          {
              if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }

              if (id != order.Id)
              {
                  return BadRequest();
              }

              _context.Entry(order).State = EntityState.Modified;

              try
              {
                  await _context.SaveChangesAsync();
              }
              catch (DbUpdateConcurrencyException)
              {
                  // se pedido.ID é diferente id retorna = Não encontrado
                  if (!OrderExists(id))
                  {
                      return NotFound();
                  }
                  else
                  {
                      throw;
                  }
              }

              return NoContent();
          }

          // POST: api/orders
          [HttpPost]
          public async Task<IActionResult> Create([FromBody] Order order)
          {
              if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }

              _context.Orders.Add(order);
              await _context.SaveChangesAsync();

              return CreatedAtAction("Create", new { id = order.Id }, order);
          }

          // DELETE: api/orders/id
          [HttpDelete("{id}")]
          public async Task<IActionResult> Delete([FromRoute] Guid id)
          {
              if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }

              var order = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);

              if (order == null)
              {
                  return NotFound();
              }

              _context.Orders.Remove(order);
              await _context.SaveChangesAsync();

              return Ok(order);

          }



          private bool OrderExists(Guid id)
          {
            return _context.Orders.Any(e => e.Id == id);
          }

        #endregion



    }
}
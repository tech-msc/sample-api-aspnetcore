using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleStore.Api.Data;
using SampleStore.Api.Models;

namespace SampleStore.Api
{
    [Produces("application/json")]
    [Route("v1/OrderItems")]
    public class OrdersItemController : Controller
    {
        #region Prop
            private readonly DataContext _context;
        #endregion

        #region Ctor
            public OrdersItemController(DataContext context){
                _context = context;
            }
        #endregion

        #region Methods
            
            [HttpGet]
            public IEnumerable<OrderItem> GetAll(){
                return _context.OrderItems;
            }
            
            [HttpGet("{id}")]
            public async Task<IActionResult> GetByID([FromRoute] Guid id){
                
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

                var orderItem = await _context.OrderItems.SingleOrDefaultAsync(e=>e.Id == id);

                if( orderItem == null ){
                    return NotFound();
                }

                return Ok(orderItem);
            }


            [HttpPost("{id}")]
            public async Task<IActionResult> Create([FromBody] OrderItem orderItem){

                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

                _context.OrderItems.Add(orderItem);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Create", new {id=orderItem.Id}, orderItem);
            }


            [HttpPut("{id}")]
            public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] OrderItem orderItem){

                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

                if(id!=orderItem.Id){
                    return BadRequest();
                }

                _context.Entry(orderItem).State = EntityState.Modified;

                try{
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException){
                    if(!OrdemItemExists(id)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                
                return NoContent();
            }


            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete([FromRoute] Guid id){

                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

                var orderItem = await _context.OrderItems.SingleOrDefaultAsync( e => e.Id == id );

                if(orderItem == null){
                    return NotFound();
                }

                _context.OrderItems.Remove(orderItem);
                
                await _context.SaveChangesAsync();

                return Ok(orderItem);
            }

            public bool OrdemItemExists(Guid id){
                return _context.OrderItems.Any(e => e.Id == id);
            }
        #endregion
        
    }
}
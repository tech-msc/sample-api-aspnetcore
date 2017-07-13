using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SampleStore.Api.Data;
using SampleStore.Api.Models;


namespace SampleStore.Api.Controllers
{
    
    [Produces("application/json")]
    [Route("v1/customers")]
    public class CustomersController :Controller
    {
        
        #region Prop
            private readonly DataContext _context;
        #endregion

        #region Ctor
            public CustomersController(DataContext context){
                _context = context;
            }
        #endregion

        #region Methods
            [HttpGet]
            public IEnumerable<Customer> GetAll(){
                // TODO - Remove TOLIST()
                return _context.Customers.OrderBy(e=>e.FirstName).ToList();
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetByID([FromRoute] Guid id){
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

                var customer = await _context.Customers.SingleOrDefaultAsync(e=> e.Id == id);
                
                if(customer == null){
                    return NotFound();
                }

                return Ok(customer);                
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] Customer customer){
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Create", new { id = customer.Id }, customer);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Customer customer){
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

                if(id != customer.Id ){
                    return BadRequest();
                }

                _context.Entry(customer).State = EntityState.Modified;

                try{
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException){
                    if(!CustomerExists(id)){
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

                var customer = await _context.Customers.SingleOrDefaultAsync(e=> e.Id == id);

                if (customer == null ){
                    return NotFound();
                }

                _context.Customers.Remove(customer);

                await _context.SaveChangesAsync();

                return Ok(customer);
            }

            public bool CustomerExists(Guid id){
                return _context.Customers.Any(e=>e.Id == id);
            }
        #endregion

    }
}
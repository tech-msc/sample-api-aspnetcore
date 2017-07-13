using Microsoft.AspNetCore.Mvc;
using SampleStore.Api.Data;
using SampleStore.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleStore.Api.Controllers
{
    // TODO - Update: Methods
    public class ProductsController : Controller
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }


        // CREATE A PRODUCT
        [HttpPost]
        [Route("v1/products")]
        public IActionResult Post([FromBody]Product p)
        {
            _context.Products.Add(p);
            _context.SaveChanges();
            return Ok();
        }

        // #region Post Collection
        //     [HttpPost]
        //     [Produces("application/json")]
        //     [Route("v1/products/collection")]
        //     public async Task<IActionResult> PostCollection([FromBody] IEnumerable<Product> list_p)
        //     {

        //         IEnumerable<Product> Prds = list_p;
        //         Console.WriteLine("############\n#\n#\n#  ENTREI AQUI\n#\n#############");

        //         if (Prds != null)
        //         {
        //             _context.Products.AddRange(Prds);
        //             await _context.SaveChangesAsync();
        //             return Ok("All saved");
        //         }
        //         return Forbid("Error");
        //     }         
        // #endregion

        // LIST ALL PRODUCTS
        [HttpGet]
        [Route("v1/products")]
        public IActionResult Get()
        {
            return Ok(_context.Products.OrderBy(x => x.Title).ToList());
        }

        // UPDATE PRODUCT
        [HttpPut]
        [Route("v1/products/{id}")]
        public IActionResult Put(Guid id, [FromBody]Product p)
        {
            var product = _context.Products.Find(id);
            product.Title = p.Title.ToLower();
            product.Price = p.Price;
            product.Image = p.Image;

            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok();

        }

        // DELETE PRODUCT
        [HttpDelete]
        [Route("v1/products/{id}")]
        public ActionResult Delete(Guid id)
        {
            _context.Products.Remove(_context.Products.Find(id));
            _context.SaveChanges();
            return Ok("Product removed");
        }

        // LIST ONE PRODUCT
        [HttpGet]
        [Route("v1/products/{id}")]
        public ActionResult GetByID(Guid id)
        {
            var prod = _context.Products.Find(id);

            if (prod != null)
            {
                return Ok(prod);
            }
            return NotFound("Item not found");

        }

    }
}

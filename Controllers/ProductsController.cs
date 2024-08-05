using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecureApiWithAuthentication.Autentication;
using SecureApiWithAuthentication.Auth;
using SecureApiWithAuthentication.Authorization;
using SecureApiWithAuthentication.Data;
using SecureApiWithAuthentication.filters;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SecureApiWithAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [FilterAttribute]
    public class ProductsController(AppDbcontext context) : ControllerBase
    {
        [HttpGet]
        [authenticationbasedPermissions(PermissionEnum.ReadAll)]
        public async Task<ActionResult<IEnumerable<Product>>> GetALL() 
        {
            var Products = await context.Product.ToListAsync();
            if(Products.IsNullOrEmpty())
            {
                return Products = [];
            }
            return Ok(Products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> Get(int id) 
        {
            var product = await context.Product.FindAsync(id);
            if(product== null)
            {
                return NotFound(" no item matches ");
            }

            return Ok(product);
        }

        [HttpPost]
        [authenticationbasedPermissions(PermissionEnum.Create)]
        public async Task<ActionResult> Add(Product newProduct) 
        {
            Product product = new Product
            {   Name = newProduct.Name,
                SKu = newProduct.SKu, };
              context.Add(product);
              await context.SaveChangesAsync();
            return StatusCode(201,product);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateItem(Product Product, int id) 
        {
            var Result = context.Product.FindAsync(id);
            var UpdatedProduct = new Product
            {
                Name = Product.Name,
                SKu = Product.SKu,
            };
            context.Update(UpdatedProduct);
            await context.SaveChangesAsync();
            return Ok(Result);
        }
        [HttpDelete]
        public void Delete(int id)
        {
            var result = context.Product.FindAsync(id);
            context.Remove(result);

        }
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Product>>> search(string name)
        {
            var results = await context.Product.Where(x => x.Name == name).ToListAsync();
            return Ok(results);

        }

    }
}

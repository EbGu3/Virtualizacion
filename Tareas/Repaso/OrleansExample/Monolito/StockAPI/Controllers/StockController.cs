using Microsoft.AspNetCore.Mvc;

namespace StockAPI.Controllers
{
    public class StockController : Controller
    {
        [HttpGet("find-product/{productName}")]
        public ActionResult<string> FindProduct(string productName)
        {
            try
            {
                string descripcion = $"{productName}, color azul";
                return Ok(descripcion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all-products")]
        public ActionResult<List<string>> AllProducts ()
        {
            try
            {
                List<string> products = new ()
                {
                    "IPAD Mini", 
                    "Razer Blade",
                    "Sony Headphone",
                    "Galaxy Tab",
                    "IPhone 15 Pro",
                    "Samsung 22",
                };
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove-product/{productName}")]
        public ActionResult RemoveProduct (string productName) 
        { 
            try
            {
                return Ok("Producto Eliminado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("aviable-amount-product/{productName}")]
        public ActionResult<string> AviableAmoutProduct (string productName)
        {
            try
            {
                Random random = new Random();
                int amount = random.Next(0, 20);
                string describeAmount = $"{productName} | Disponible = {amount}";
                return Ok(describeAmount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

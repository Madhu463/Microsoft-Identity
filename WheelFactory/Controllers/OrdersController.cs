using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using WheelFactory.Models;
using WheelFactory.Services;

namespace WheelFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class OrdersController : ControllerBase
    {
        private readonly string _basePath = @"C:\Users\ksathvikreddy\Desktop\Projects\Microsoft-Identity\WheelFactory\wwwroot\images\";
        private readonly WheelContext _wheelContext;
        private readonly IOrdersService _ordersService;
        public OrdersController(WheelContext wc, IOrdersService orderService)
        {
            _ordersService = orderService;
            _wheelContext = wc;

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _ordersService.GetOrders(id: id).FirstOrDefault<Orders>();
                if (order == null)
                {
                    return NotFound();
                }
               
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet()]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = _ordersService.GetOrders().ToList<Orders>();
        
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("current")]
        public IActionResult GetCurrentOrders()
        {
            try
            {
                var orders = _ordersService.GetOrders().Where<Orders>(o => (o.Status != "completed" && o.Status != "Scrap")).ToList();
               
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("completed")]
        public IActionResult GetCompletedOrders()
        {
            try
            {
                var orders = _ordersService.GetOrders(status: "completed").ToList<Orders>();
               
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("scraped")]
        public IActionResult GetScrapedOrders()
        {
            try
            {
                var orders = _ordersService.GetOrders(status: "Scrap").ToList<Orders>();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] OrderDTO value)
        {
            try
            {
                //if (value.ImageUrl == null || value.ImageUrl.Length == 0)
                //    return BadRequest("No file uploaded.");

                //var originalFileName = Path.GetFileName(value.ImageUrl.FileName);
                //var filePath = Path.Combine(_basePath, originalFileName);

                //if (!Directory.Exists(_basePath))
                //{
                //    Directory.CreateDirectory(_basePath);
                //}

                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await value.ImageUrl.CopyToAsync(stream);
                //}

                //var order = new Orders
                //{
                //    ClientName = value.ClientName,
                //    Year = (int)value.Year,
                //    Make = value.Make,
                //    Model = value.Model,
                //    Notes = value.Notes,
                //    Status = "neworder",
                //    DamageType = value.DamageType,
                //    ImageUrl = "http://localhost:5041/images/" + originalFileName
                //};

                var newOrder = _ordersService.AddOrder(value);

                if (newOrder != null)
                {
                    return Ok(newOrder);
                }

                return BadRequest("Failed to add the order.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromForm] OrderDTO value)
        //{
        //    try
        //    {
        //        if (value == null || string.IsNullOrEmpty(value.Status))
        //        {
        //            return BadRequest("Invalid order data provided.");
        //        }

        //        var updatedOrder = _ordersService.UpdateOrder(id, value);
        //        if (updatedOrder != null)
        //        {
        //            return Ok(updatedOrder);
        //        }

        //        return BadRequest($"Order with ID {id} not found or update failed.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        [HttpPut("scrap/{id}")]
        public IActionResult PutScrapOrder(int id)
        {
            try
            {
                var updatedOrder = _ordersService.UpdateOrder(id, status: "Scrap");
                if (updatedOrder != null)
                {
                    return Ok($"Scraped order {id}");
                }
                return BadRequest("Failed to scrap the order.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Inventory")]
        public IActionResult GetOrdersInventory()
        {
            try
            {
                var orders = _ordersService.GetOrders(status: "neworder");
               
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Inventory/{id}")]
        public IActionResult PutOrdersInventory(int id)
        {
            try
            {
                var updatedOrder = _ordersService.UpdateOrder(id, status: "Soldering");
                if (updatedOrder != null)
                {
                    return Ok("Status changed to Soldering");
                }
                return BadRequest("Failed to update inventory order.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}


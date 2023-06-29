using Emedicine.BAL.CartBased;
using Emedicine.BAL.OrderBased;
using Emedicine.DAL.DataManupulation;
using Emedicine.DAL.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Emedicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //add IOrder interface to use its methods..
        private readonly IOrderMain ic;
        public OrderController(IOrderMain _ic)
        {
            ic = _ic;
        }
        //Add order to database
        [HttpPost("AddOrder")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        public async Task<IActionResult> AddOrder(OrderVm order)
        {
            try
            {
               if(await ic.AddOrder(order))
                {

                   return StatusCode(
                   StatusCodes.Status200OK,
                   "order added successfully");
                    
                }
                else
               return  StatusCode(
                       StatusCodes.Status403Forbidden,
                       " Something went wrong");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //DeleteOrder from database
        [HttpDelete("DeleteOrder")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        public async Task<IActionResult> deleteItems(int orderId)
        {
            try
            {

                 if(await ic.RemoveOrderItem(orderId))
                {
                    Order order=await ic.GetOrderById(orderId);
                    if(await ic.RemoveOrder(order))
                    {
                        return StatusCode(
                       StatusCodes.Status200OK,
                       "order deleted successfully");
                    }
                    else
                    {
                        return StatusCode(
                       StatusCodes.Status403Forbidden,
                       " Format is wrong");
                    }
                }
                else
                {
                    return StatusCode(
                       StatusCodes.Status403Forbidden,
                       " Format is wrong");
                }

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Get full order with medicine and user parameter.
        [HttpGet("wholeorder/{orderId}")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        public async Task<Order> GetOrderById(int orderId)
        {
            return await ic.kireOrderDibi(orderId);
        }

        [HttpGetAttribute("GetCartById/{userId}")]
        [EnableCors("AllowOrigin")]

        public  IActionResult getCartByUserId(int userId)
        {
            try
            {
                var carts= ic.getcartById(userId);
                return Ok(carts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGetAttribute("AllOrder")]
        [EnableCors("AllowOrigin")]
        [Authorize]

        public async Task<IActionResult> getAllOtrder()
        {
            try
            {
                var orders = await ic.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

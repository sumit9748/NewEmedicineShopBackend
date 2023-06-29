using Emedicine.BAL.CartBased;
using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.DataManupulation;
using Emedicine.DAL.model;

namespace Emedicine.BAL.OrderBased
{
    public class OrderMain : IOrderMain
    {
        private readonly IDataAccess _da;
        public OrderMain(IDataAccess da) 
        {
            _da = da;
        }
        //Add a order into database.
        public async Task<bool> AddOrder(OrderVm order)
        {
            if (order == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                //Its a tricky one..
                //Here i try to add the order from cart
                //First i calculate the orderTotal parameter 
                //Then i create a Order object and add it to database
                double orderTotal = 0, discount = 0;
                foreach(var item in order.carts)
                {
                    orderTotal += item.Price;
                    discount += item.Discount;
                }
                //After adding and discount
                orderTotal = (orderTotal * (100 - discount)) / 100;

                Order newOrder = new Order
                {
                    UserId = order.UserId,
                    OrderTotal = (decimal)orderTotal,
                    OrderStatus = "pending",
                    OrderDate = DateTime.Now,

                };
                _da.order.AddAsync(newOrder);
                _da.save();
                //This one for adding the orderItems
                //Means which items are ordered for a particular orderId
                foreach (var item in order.carts)
                {
                    var orderItem = new OrderItem()
                    {

                        MedicineId = item.MedicineId,
                        OrderId = newOrder.Id,
                    };
                    _da.orderItem.AddAsync(orderItem);
                    _da.save();
                };
                
                return await Task.FromResult(true);
            }
        }

        
        //Get orderBy id
        public async Task<Order> GetOrderById(int orderId)
        {
            return await _da.order.GetFirstOrDefaultAsync(c=>c.Id==orderId);
            
        }
        //Remove Order from database by orderId
        public async Task<bool> RemoveOrder(Order order)
        {
            if(order==null)
            {
                return await Task.FromResult(false);
            }
           _da.order.Remove(order);
           _da.save();
            return await Task.FromResult(true);

        }
        //Remove orderItem from database
        //After removing the order it is mandetory to remove orderItems also from the table.
        public async Task<bool> RemoveOrderItem(int orderId)
        {
            IEnumerable<OrderItem> orderItems= await _da.orderItem.GetAllListAsync(o=>o.OrderId == orderId);
            if(orderItems.Count()==0) return await Task.FromResult(false);
            foreach(var item in orderItems)
            {
                _da.orderItem.Remove(item);
            }
            return await Task.FromResult(true);
        }
        //DelteorderItems
        public async Task<IEnumerable<OrderItem>> DeleteOrderItems(int orderId)
        {
            return await _da.orderItem.GetAllListAsync(c=>c.OrderId==orderId);
        }
        public async Task<Order> kireOrderDibi(int id)
        {
            return await _da.order.GetorderById(id);
        }
        public IEnumerable<Cart> getcartById(int userId)
        {
                var carts= _da.cart.getCartsOfUser(userId);
                return carts;
        }
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders =  _da.order.getAllOrder();
            return orders;
        }
        
    }
}

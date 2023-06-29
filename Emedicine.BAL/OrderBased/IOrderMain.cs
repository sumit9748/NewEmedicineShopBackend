using Emedicine.DAL.DataManupulation;
using Emedicine.DAL.model;

namespace Emedicine.BAL.OrderBased
{
    public interface IOrderMain
    {
        public Task<bool> AddOrder(OrderVm order);
        public Task<bool> RemoveOrder(Order order);
        public Task<bool> RemoveOrderItem(int orderId);
        public Task<IEnumerable<OrderItem>> DeleteOrderItems(int orderId);
        public Task<Order> GetOrderById(int orderId);
        public Task<Order> kireOrderDibi(int id);
        public IEnumerable<Cart> getcartById(int userId);
        public Task<IEnumerable<Order>> GetAllOrders();

    }
}

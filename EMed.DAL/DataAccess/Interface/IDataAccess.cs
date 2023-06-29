using Emedicine.DAL.model;


namespace Emedicine.DAL.DataAccess.Interface
{
    public interface IDataAccess
    {
        public IUser user { get; }
        public IMedicine medicine { get; }
        public IMedicalShop medicalShop { get; }
        public IOrder order { get; }
        public IOrderItem orderItem { get; }
        public IMedicalShopItem medicalShopItem { get; }
        public ICart cart { get; }
        public void save();


    }
    public interface IUser : IRepo<User>
    {
    }
    public interface IMedicine:IRepo<Medicine>
    {
        public Task<IEnumerable<Medicine>> GetMedicalShopItems(int id);
    }
    public interface IMedicalShop:IRepo<Medicalshop> { }
    public interface IOrder : IRepo<Order>
    {
        public Task<Order> GetorderById(int id);
        public IEnumerable<Order> getAllOrder();
    }
    public interface IOrderItem : IRepo<OrderItem> { }
    public interface IMedicalShopItem : IRepo<MedicalShopItem> { }
    public interface ICart : IRepo<Cart> 
    {
        Task<IEnumerable<Medicine>> GetMedicinesByUserfromcart(int userId);
        public Cart GetCartById(int id);
        public IEnumerable<Cart> getCartsOfUser(int userId);
    }




}

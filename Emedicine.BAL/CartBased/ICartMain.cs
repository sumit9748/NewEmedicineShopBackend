using Emedicine.DAL.DataManupulation;
using Emedicine.DAL.model;



namespace Emedicine.BAL.CartBased
{
    public interface ICartMain
    {
        public Task<IEnumerable<Cart>> GGetCartByUserId(int userId);
        public Task<bool> AddCart(CartVm cart);
        public void UpdateCart(Cart cart);
        public void DeleteCart(Cart cart);
        public Task<Cart> GetCartById(int id);
        public Task<IEnumerable<Medicine>> getMedicinefromCart(int userId);

    }
}

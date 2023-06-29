using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.DataManupulation;
using Emedicine.DAL.model;


namespace Emedicine.BAL.CartBased
{
    public class CartMain : ICartMain
    {   //this is called dependency injection It helps to couple the system loosely by which dependecy of each system gets reduce.
        private readonly IDataAccess _da;
        public CartMain(IDataAccess da) 
        {
            _da = da;

        }
        //add cart methos to add product in cart..
        //CartVm is a object similar to cart to add items in cart.
        public async Task<bool> AddCart(CartVm cart)
        {
            if (cart == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                //Get cart by userId
                IEnumerable<Cart> carts = await _da.cart.GetAllListAsync(c=>c.UserId==cart.UserId);
                
                foreach (var item in cart.Medicines)
                {
                    //get medicine by its id parameter.
                    var medicine = await _da.medicine.GetFirstOrDefaultAsync(c => c.Id == item.Item1);
                    //check if any cart item with same userId and MedicineId is exists or not.
                    if (carts.Any(c => c.MedicineId == item.Item1)) continue;
                    
                    //create new cart object
                    Cart newCart = new Cart
                    {

                        UserId = cart.UserId,
                        Price=medicine.UnitPrice* item.Item2,
                        Discount=medicine.Discount,
                        Quantity=item.Item2,
                        MedicineId=item.Item1,
                        MedicalShopId=cart.MedicalShopId,

                    };
                    //add the cart
                    _da.cart.AddAsync(newCart);
                    _da.save();
                }
                return await Task.FromResult(true);
            }
        }
        //Deletemethod of cart
        public void DeleteCart(Cart cart)
        {
            _da.cart.Remove(cart);
            _da.save();
        }

        //Get cart by userId
        public async Task<IEnumerable<Cart>> GGetCartByUserId(int userId)
        {
            return await _da.cart.GetAllListAsync(c=>c.UserId==userId);
        }
        //Update a cart
        public void UpdateCart(Cart cart)
        {
            _da.cart.UpdateExisting(cart);
            _da.save();
        }
        //Task is use in case of async statements
        public async Task<Cart> GetCartById(int id)
        {
            return await _da.cart.GetFirstOrDefaultAsync(c=>c.Id==id);
        }
        //Get medicine list from cart by userId
        public async Task<IEnumerable<Medicine>> getMedicinefromCart(int userId)
        {
            return await _da.cart.GetMedicinesByUserfromcart(userId);
        }

        
    }
}

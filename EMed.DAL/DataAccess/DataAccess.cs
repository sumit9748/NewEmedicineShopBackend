using Emedicine.DAL.Data;
using Emedicine.DAL.DataAccess.Interface;
using Emedicine.DAL.DataManupulation;
using Emedicine.DAL.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emedicine.DAL.DataAccess
{
    //It implements the IDataaccess interface
    public class DataAccess : IDataAccess
    {
        public readonly MedicineDbContext md;
        public DataAccess(MedicineDbContext _md)
        {
            md = _md;
            user=new UserRepo(md);
            medicine = new MedicineRepo(md);
            medicalShop = new MedicalShopRepo(md);
            medicalShopItem=new MedicalShopItemRepo(md);
            order =new OrderRepo(md);
            orderItem = new OrderItemRepo(md);
            cart=new CartRepo(md);
        }
        //From here we use all the models.
        //so here it is use beacause:- not only with those 6 methods we also need some extra features for each class seperately
        //Which are mentioned in each repo
        public IUser user { get;private set; }
        public IMedicine medicine { get; private set; }
        public IMedicalShop medicalShop { get; private set; }
        public IOrder order { get; private set; }   
        public IMedicalShopItem medicalShopItem { get; private set; }
        public IOrderItem orderItem { get; private set; }
        public ICart cart { get; private set;}
        public void save()
        {
             md.SaveChangesAsync();
        }

    }
    //UserRepo class implements Repo<User> and Iuser Interface
    //So it can access the methods of repo and also the IUser interface methods.
    public class UserRepo : Repo<User>, IUser
    {
        public readonly MedicineDbContext md;
        public DbSet<User> UserDbSet { get; set; }
        public UserRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
            UserDbSet = md.Set<User>();
        }
        
    }
    public class MedicineRepo : Repo<Medicine>, IMedicine
    {
        public readonly MedicineDbContext md;
        public DbSet<Medicine> MedicineDbSet { get; set; }
        public DbSet<MedicalShopItem> MedicalShopItemDbSet { get; set; }


        public MedicineRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
            MedicineDbSet = md.Set<Medicine>();
            MedicalShopItemDbSet=md.Set<MedicalShopItem>();
        }
        public async Task<IEnumerable<Medicine>> GetMedicalShopItems(int id)
        {
            var ans = from ms in MedicalShopItemDbSet
                      where ms.MedicalShopId == id
                      select ms.Medicine;

            return await ans.ToListAsync();
        }



    }
    public class MedicalShopRepo : Repo<Medicalshop>, IMedicalShop
    {
        private readonly MedicineDbContext md;
        public DbSet<Medicalshop> MedicalShopDbSet { get; set; }

        public MedicalShopRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
            MedicalShopDbSet = md.Set<Medicalshop>();
        }

    }
    public class OrderRepo : Repo<Order>, IOrder
    {
        public readonly MedicineDbContext md;
        public OrderRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
        }
        public async Task<Order> GetorderById(int id)
        {
            return await md.Orders.Include(o=>o.user).FirstOrDefaultAsync(c=>c.Id==id);
        }
        /*
        public async Task<OrderByMonth> GetOrderByMonth()
        {
            var orders =  md.Orders.ToList();
            var orderedStatistics = orders
            .GroupBy(s => s.Date.Month)
            .OrderBy(g => g.Key)
            .Select(g => new
            {
                Month = g.Key,
                MonthlyStatistics = g.OrderBy(s => s.Date)
            });
        }
        */
        public IEnumerable<Order> getAllOrder()
        {
            var orders=md.Orders.Include(o=>o.user);
            return orders;
        }
    }
    public class OrderItemRepo : Repo<OrderItem>, IOrderItem
    {
        public readonly MedicineDbContext md;
        public OrderItemRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
        }
    }
    public class MedicalShopItemRepo : Repo<MedicalShopItem>, IMedicalShopItem
    {
        public readonly MedicineDbContext md;
        public MedicalShopItemRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
        }
    }
    public class CartRepo : Repo<Cart>, ICart
    {
        private readonly MedicineDbContext md;
        public DbSet<Cart> cartDbSet { get; set; }


        public CartRepo(MedicineDbContext _md) : base(_md)
        {
            md = _md;
            cartDbSet = md.Set<Cart>();


        }
        public async Task<IEnumerable<Medicine>> GetMedicinesByUserfromcart(int userId)
        {
            var medicines = from c in cartDbSet
                            where c.UserId == userId
                            select c.Medicine;
            return await medicines.ToListAsync();
        }
        public Cart GetCartById(int id)
        {
            var cart =  cartDbSet.Include(c=>c.Medicine).
                Include(c=>c.Medicine).
                Include(c=>c.Medicicalshop).
                FirstOrDefault(c => c.Id == id);
            return cart; 
        }
        public IEnumerable<Cart> getCartsOfUser(int userId)
        {
            var cart = md.Carts.Include(c => c.Medicine).
                Where(c => c.UserId == userId);

            return cart;
        }
    }
}

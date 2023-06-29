using Emedicine.DAL.model;
using Microsoft.EntityFrameworkCore;


namespace Emedicine.DAL.Data
{
    public class MedicineDbContext:DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedicalShopItem>()
                 .HasOne(b => b.Medicine)
                 .WithMany(ba => ba.MedicalShopItems)
                 .HasForeignKey(bi => bi.MedicineId);

            modelBuilder.Entity<MedicalShopItem>()
                 .HasOne(b => b.MedicalShop)
                 .WithMany(ba => ba.MedicalShopItems)
                 .HasForeignKey(bi => bi.MedicalShopId);

            base.OnModelCreating(modelBuilder);
            
        }
        public MedicineDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Medicalshop> Medicalshops { get; set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<MedicalShopItem> MedicalShopItems { get; set; }
        public DbSet<OrderItem> OrdersItems { get; set;}

   

    }
    



}

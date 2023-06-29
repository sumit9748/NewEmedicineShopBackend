using Emedicine.DAL.Data;
using Emedicine.DAL.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Emedicine.DAL.DataAccess
{
    //It is a generic type class 
    //here we can see several methods which are common for all the models like add,update,delete,getrange,getbyid
    //T represent a specific class which is called during object creation
    //Like from userMain when we try to call any method of repo class the T replaces with User model
    //By which the add ,update, delete and the other methods are work as useradd,userupdate,userdelete
    public class Repo<T> : IRepo<T> where T : class
    {
        //without using the dbcontext directly we use as a parameterize dependency injection
        private readonly MedicineDbContext md;
        //Dbset for generic type sets
        public DbSet<T> DbSet { get; set; }
        public Repo(MedicineDbContext _md) 
        {
            md = _md;
            DbSet=_md.Set<T>();
        }
        //Add items 
        public async void AddAsync(T entity)
        {
          await DbSet.AddAsync(entity);
            md.SaveChanges();
    
        }
        //Get all items
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = DbSet;
            return await query.ToListAsync();
        }
        //get all list async
        //Here it is accepting a linq expression
        public async Task<IEnumerable<T>> GetAllListAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DbSet;
            return await query.Where(filter).ToListAsync();
        }
        //Get firstordefault 
        //Here firstordefault use to handle the null exception
        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }
        //Remove a item from database
        public void Remove(T entity)
        {
             DbSet.Remove(entity);
            md.SaveChanges();

        }
        //removerange from database
        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
        //update database
        public void UpdateExisting(T entity)
        {
            DbSet.Update(entity);
        }
    }
}

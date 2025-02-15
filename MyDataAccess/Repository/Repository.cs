using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyDataAccess.Data;
using MyDataAccess.Data.Repository.IRepository;

namespace MyAspNetCoreApp.MyDataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CrudeContext _context;
        internal DbSet<T> dbSet;
        public Repository(CrudeContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
            _context.Products.Include(u=>u.Item).Include(u=>u.ItemId);
            
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter,string? includeProperties=null , bool tracked=false)
        {
             IQueryable<T> query = dbSet;

            if(tracked){
            query = dbSet;
            
             }
            else
            {
             query = dbSet.AsNoTracking();
          
            }
              query=query.Where(filter);
             if(!string.IsNullOrEmpty(includeProperties))
           {
            foreach(var includeprop in includeProperties.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeprop);
            }
          
           }
            return query.FirstOrDefault();
            
        }
        
            

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter,string? includeProperties=null)
        {
           IQueryable<T> query = dbSet;
           if(filter!=null){
             query = query.Where(filter);
           }
           if(!string.IsNullOrEmpty(includeProperties))
           {
            foreach(var includeprop in includeProperties.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeprop);
            }
          
           }
            return query.ToList();
        }

        public void Remove(T entity)
        {
dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
dbSet.RemoveRange(entity);
        }
    }
}

   
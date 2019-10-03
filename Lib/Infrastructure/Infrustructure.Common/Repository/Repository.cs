using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Infrastructure.Common
{

    public class Repository<T>: IRepository<T>
            where T:class,new ()
    {
        private readonly DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
        }   
        public void Create(T entity)
        {
            await _context.Set<T>.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        T GetEntity(Guid id){;}
        IEnumerable<T> GetEntities(){}
        void Update(T entity){}
        void Delete(T entity){}

        T GetBy(Expression<Func<T,R>> predicator) where R:class{}
        IEnumerable<T> GetBy(Expression<Func<T,R>> predicator)where R:class{}

    }
}
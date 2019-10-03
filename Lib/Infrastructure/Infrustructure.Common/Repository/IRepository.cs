using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Infrastructure.Common
{

    public interface IRepository<T>
            where T:class,new ()
    {
        
        void Create(T entity);
        T GetEntity(Guid id);
        IEnumerable<T> GetEntities();
        void Update(T entity);
        void Delete(T entity);

        T GetBy(Expression<Func<T,R>> predicator) where R:class;
        IEnumerable<T> GetBy(Expression<Func<T,R>> predicator)where R:class;

    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Infrastructure.Common
{

    public interface UnitOfWork<T>
            where T:class,new ()
    {
        
        void SaveChanges();
        void Commit();

    }
}
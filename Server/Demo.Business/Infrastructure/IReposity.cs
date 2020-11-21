using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Demo.Model;
using System.Linq;

namespace Demo.Business.InfraStructure
{
    public interface IRepository<T> where T: ModelBase
    {
        void Add(T Entity);
        void AddAsync(T entity);
        void Update(T Entity);
        void Delete(T Entity);
         void Delete(Expression<Func<T, bool>> where);
        T SelectById(long id);
        T SelectOne(Expression<Func<T, bool>> where);
        IQueryable<T> SelectAll(Expression<Func<T, bool>> where);
        IQueryable<T> SelectAllWithRelatedEntities(Expression<Func<T, bool>> where);
        IQueryable<T> SelectAll();
        IQueryable<T> SelectAllWithRelatedEntities();
        bool Exists(Expression<Func<T, bool>> where);
    }
}
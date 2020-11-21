using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Demo.Common;
using Demo.DAL;
using Demo.Model;

namespace Demo.Business.InfraStructure {
    public class RepositoryBase<T> : IRepository<T> where T : ModelBase {
        private readonly DbSet<T> _dbSet;
        private DemoDbContext _dataContext;
        protected IDatabaseFactory DataBaseFactory { get; private set; }
        protected DemoDbContext DataContext {
            get { return _dataContext ?? (_dataContext = DataBaseFactory.GetContext ()); }
        }

        public RepositoryBase (IDatabaseFactory databaseFactory) {
            DataBaseFactory = databaseFactory;
            _dbSet = DataContext.Set<T> ();
        }

        public void Add (T entity) {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = UserStateManagement.UserId;
            _dbSet.Add (entity);
        }
        public void AddAsync (T entity) {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = UserStateManagement.UserId;
            _dbSet.AddAsync (entity);
        }
        public void Delete (T entity) {
            _dbSet.Remove (entity);
        }

        public void Delete (IEnumerable<T> entity) {
            foreach (T obj in entity)
                Delete (obj);
        }
        public void Update (T Entity) {
            Entity.UpdatedDate = DateTime.Now;
            Entity.UpdatedBy = UserStateManagement.UserId;
            _dbSet.Attach (Entity);
            _dataContext.Entry (Entity).State = EntityState.Modified;
        }

        public T SelectById (long id) {
            return _dbSet.Find(id);
        }
        public Task<T> SelectByIdAsync (long id) {
            return _dbSet.FindAsync (id);
        }
        public T SelectOne (Expression<Func<T, bool>> where) {
            return _dbSet.Where (where).Include(_dataContext.GetIncludePaths(typeof(T))).FirstOrDefault();
        }

        public IQueryable<T> SelectAll (Expression<Func<T, bool>> where) {
            return _dbSet.Where (where);
        }
         public IQueryable<T> SelectAllWithRelatedEntities (Expression<Func<T, bool>> where) {
            return _dbSet.Where (where).Include(_dataContext.GetIncludePaths(typeof(T)));
        }
        public IQueryable<T> SelectAll  () {
            return _dbSet;
        }
        public IQueryable<T> SelectAllWithRelatedEntities  () {
            return _dbSet.Include(_dataContext.GetIncludePaths(typeof(T)));
        }

        public bool Exists (Expression<Func<T, bool>> where) {
            var firstItem =_dbSet.Where (where).Include(_dataContext.GetIncludePaths(typeof(T))).FirstOrDefault();
            if(firstItem == null)
                return false;
            else
                return true;
           // return _dbSet.Any (where);
        }
        public void Delete (Expression<Func<T, bool>> where) {
            var objects = _dbSet.Where(where).AsEnumerable();
            foreach (T obj in objects)
                Delete (obj);
        }
    }
}
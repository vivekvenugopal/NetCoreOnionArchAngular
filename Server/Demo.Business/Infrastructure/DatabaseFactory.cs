using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Demo.DAL;

namespace Demo.Business.InfraStructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private  DemoDbContext _dataContext;
        public DatabaseFactory()
        {
        }
        public DemoDbContext  GetContext()
        {
            return  _dataContext ?? (_dataContext = new DemoDbContext());
        }
        protected override void DisposeCore()
        {
            if(_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
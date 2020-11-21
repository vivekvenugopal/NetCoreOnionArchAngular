using Demo.DAL;
namespace Demo.Business.InfraStructure {
    public class UnitOfWork : IUnitOfWork {
        protected IDatabaseFactory _databaseFactory { get; set; }
        private DemoDbContext _dataContext;
        public UnitOfWork (IDatabaseFactory databaseFactory) {
            _databaseFactory = databaseFactory;
        }
        protected DemoDbContext DataContext { 
            get{ return _dataContext ?? (_dataContext = _databaseFactory.GetContext());}
         }
        public void Commit () {
            DataContext.Commit();
        }
    }
}
using Demo.DAL;

namespace Demo.Business.InfraStructure{
    public interface IDatabaseFactory
    {
         DemoDbContext GetContext();
    }
}
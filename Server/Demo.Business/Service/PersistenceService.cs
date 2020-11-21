using System;

namespace Demo.Business.Service
{
    public class PersistenceService
    {
        protected void SaveChanges(IUnitOfWork unitOfWork)
        {
            unitOfWork.Commit();
        }
    }
}

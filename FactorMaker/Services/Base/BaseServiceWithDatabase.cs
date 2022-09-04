namespace FactorMaker.Services.Base
{
    public class BaseServiceWithDatabase
    {
        public BaseServiceWithDatabase(Data.IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        protected Data.IUnitOfWork UnitOfWork { get; }
    }
}

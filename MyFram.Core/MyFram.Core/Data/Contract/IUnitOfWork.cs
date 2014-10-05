namespace MyFram.Core.Data.Contract
{
    using Domain;

    public interface IUnitOfWork<T> where T : BaseEntity
    {
        void Commit();

        void Rollback();
    }
}
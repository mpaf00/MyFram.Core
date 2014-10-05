namespace MyFram.Core.Data.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Domain;

    public interface IRepository<T> where T : BaseEntity
    {
        T Add(T entity);

        void Add(IEnumerable<T> entities);

        IQueryable<T> All();

        void Delete(T entity);

        void Delete(IEnumerable<T> entities);

        IQueryable<T> FilterBy(Expression<Func<T, bool>> expression);

        IQueryable<T> FilterBy(string where);

        T FindBy(Expression<Func<T, bool>> expression);

        T FindBy(string handleS);

        T Update(T entity);
    }
}
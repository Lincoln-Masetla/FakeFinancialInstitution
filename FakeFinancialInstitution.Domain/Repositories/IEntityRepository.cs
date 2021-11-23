using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace FakeFinancialInstitution.Domain.Repositories
{
    public interface IEntityRepository
    {


        void EnlistTransaction(Transaction transaction);


        IQueryable<T> AllIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class;

        IQueryable<T> All<T>() where T : class;

        int Count<T>() where T : class;

        void Detach<T>(T entity) where T : class;

        T GetSingle<T>(Expression<Func<T, bool>> predicate) where T : class;

        T GetSingle<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class;

        IQueryable<T> FindBy<T>(Expression<Func<T, bool>> predicate) where T : class;

        void Add<T>(T entity) where T : class;

        void AddRange<T>(IEnumerable<T> entities) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        void DeleteWhere<T>(Expression<Func<T, bool>> predicate) where T : class;

        void Commit();
    }
}

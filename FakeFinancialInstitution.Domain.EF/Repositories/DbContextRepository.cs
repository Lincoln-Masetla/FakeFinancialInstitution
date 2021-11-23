using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using FakeFinancialInstitution.Domain.EF.Contexts;
using FakeFinancialInstitution.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FakeFinancialInstitution.Domain.EF.Repositories
{
    public class DbContextRepository : IEntityRepository
    {
        private readonly FakeFinancialInstitutionDbContext _context;

        public void EnlistTransaction(Transaction transaction)
        {

            _context.Database.OpenConnection();

            _context.Database.EnlistTransaction(transaction);
        }



        public DbContextRepository(FakeFinancialInstitutionDbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> All<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void Detach<T>(T entity) where T : class
        {
            var result = _context.Set<T>().FirstOrDefault(x => x == entity);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        public virtual int Count<T>() where T : class
        {
            return _context.Set<T>().Count();
        }

        public virtual IQueryable<T> AllIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public T GetSingle<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T GetSingle<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query.Where(predicate).FirstOrDefault();
        }

        public virtual IQueryable<T> FindBy<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void AddRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().AddRange(entities);
        }

        public virtual void Update<T>(T entity) where T : class
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public virtual void Delete<T>(T entity) where T : class
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public virtual void DeleteWhere<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            IEnumerable<T> entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                var entry = _context.Entry(entity);
                entry.State = EntityState.Deleted;
            }
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }
    }
}

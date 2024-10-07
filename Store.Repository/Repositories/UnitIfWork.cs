using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositories.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<int> Complete()
        => await _context.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var repositoryKey = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(repositoryKey))
            {
                var repositoryType = typeof(GenercRepository<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)), _context);
                _repositories.Add(repositoryKey, repositoryInstance);
            }
            return (IGenericRepository<TEntity, TKey>)_repositories[repositoryKey];
        }
    }
}

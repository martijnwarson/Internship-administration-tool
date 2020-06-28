using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.UnitTests.UnitTests.InMemoryRepositories
{
    /// <inheritdoc cref="ICrudRepository{TModel}"/>
    public abstract class InMemoryRepository<TModel>
        : ICrudRepository<TModel>,
            IDisposable
    where TModel : BaseModel
    {
        private static long _number = 1000 ;

        protected InMemoryRepository(IList<TModel> models)
        {
            Models = models;
            Factory = new TaskFactory();
        }


        protected IList<TModel> Models;
        protected TaskFactory Factory;

        public async Task<IList<TModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Factory.StartNew(() =>  Models, cancellationToken);
        }

        public async Task<TModel> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await Factory.StartNew(() => Models.SingleOrDefault(m => m.Id == id), cancellationToken);
        }

        public async Task AddAsync(TModel entity, CancellationToken cancellationToken)
        {
            await Factory.StartNew(() =>
            {
                IdGenerator(entity);
                Models.Add(entity);
            },
                cancellationToken);
        }

        public async Task UpdateAsync(TModel entity, CancellationToken cancellationToken)
        {
            await RemoveAsync(entity, cancellationToken);
            await AddAsync(entity, cancellationToken);
        }

        public async Task RemoveAsync(long id, CancellationToken cancellationToken)
        {
            TModel model = await GetByIdAsync(id, cancellationToken);
            await RemoveAsync(model, cancellationToken);
        }

        public async Task RemoveAsync(TModel entity, CancellationToken cancellationToken)
        {
            await Factory.StartNew( () =>
                {
                    Models.Remove(entity);
                },
                cancellationToken);
        }

        private void IdGenerator(TModel model)
        {
            if (model.Id == 0)
            {
                model.Id = _number;
                _number++;
            }
        }

        public static void SetNumber(long number)
        {
            _number = number;
        }

        public void Dispose()
        {
            Models.Clear();
        }
    }
}
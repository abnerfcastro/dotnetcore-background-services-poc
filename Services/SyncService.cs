using Future.Api.Models;
using Future.Api.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Future.Api.Services
{
    public class SyncService : ISyncService
    {
        private readonly IRepository _repository;

        public SyncService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Add(int id, string data)
        {
            await _repository.AddAsync(id, data);
        }

        public async Task<Sync> Fetch(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<IEnumerable<Sync>> FetchAll()
        {
            return await _repository.GetAll();
        }
    }
}
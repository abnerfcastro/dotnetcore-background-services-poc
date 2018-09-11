using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Future.Api.Models;

namespace Future.Api.Repository
{
    public class InMemoryRepository : IRepository
    {
        private readonly IList<Sync> _list = new List<Sync>();

        public async Task AddAsync(int id, string data)
        {
            if (_list.Any(s => s.Id == id))
                throw new Exception($"Id = {id} already exists.");

            _list.Add(new Sync
            {
                Id = id,
                Data = data,
                Status = Status.Pending
            });

            await Task.CompletedTask;
        }

        public Task<Sync> GetAsync(int id)
        {
            var result = _list.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(result);
        }

        public Task<IList<Sync>> GetAll()
        {
            return Task.FromResult(_list);
        }

        public async Task SetStatusAsync(int id, Status status)
        {
            var result = await GetAsync(id);

            if (result == null)
                throw new Exception($"{id} does not exist.");

            result.Status = status;
        }
    }
}
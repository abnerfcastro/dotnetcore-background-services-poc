using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Future.Api.Models;

namespace Future.Api.Repository
{
    public interface IRepository
    {
        Task AddAsync(int id, string data);

        Task<Sync> GetAsync(int id);

        Task<IList<Sync>> GetAll();

        Task SetStatusAsync(int id, Status status);
    }
}
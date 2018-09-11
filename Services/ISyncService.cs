using Future.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Future.Api.Services
{
    public interface ISyncService
    {
        Task Add(int id, string data);

        Task<Sync> Fetch(int id);

        Task<IEnumerable<Sync>> FetchAll();
    }
}
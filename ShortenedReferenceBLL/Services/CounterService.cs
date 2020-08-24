using ShortenedReferenceBLL.Interfaces;
using ShortenedReferenceCommon.Model;
using ShortenedReferenceDAL.Interfaces;
using System.Threading.Tasks;

namespace ShortenedReferenceBLL.Services
{
    public class CounterService : ICounterService<Counter>
    {
        private readonly ICounterRepository<Counter> _counterRepository;

        public CounterService(ICounterRepository<Counter> counterRepository)
        {
            _counterRepository = counterRepository;
        }

        public async Task<Counter> Create(Counter item)
        {
            return await _counterRepository.Create(item);
        }

        public Task<Counter> Get(int id)
        {
            return _counterRepository.Get(id);
        }

        public async Task<Counter> Update(int id)
        {
            return await _counterRepository.Update(id);
        }
    }
}
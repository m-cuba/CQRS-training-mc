using Microsoft.Extensions.Caching.Distributed;
using Sportsbook.ServiceTemplate.Core.Repositories;

namespace Sportsbook.ServiceTemplate.Infrastructure.Repositories
{
    public class CachedBaseRepository : ICachedRepository
    {
        private readonly ICachedRepository _memberRepository;
        private readonly IDistributedCache _distributedCache;

        public CachedBaseRepository(ICachedRepository memberRepository, IDistributedCache distributedCache)
        {
            _memberRepository = memberRepository;
            _distributedCache = distributedCache;
        }


    }
}

using FinBeat.Domain;
using FinBeat.Domain.Entities;
using FinBeat.Domain.Interfaces;

namespace FinBeat.Infrastructure.Persistence
{
    public class ApiLogRepository : IApiLogRepository
    {
        private readonly ApplicationDbContext _context;

        public ApiLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(ApiLog log)
        {
            _context.ApiLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}

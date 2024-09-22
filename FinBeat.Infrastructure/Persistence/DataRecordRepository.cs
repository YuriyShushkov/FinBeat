using FinBeat.Domain;
using FinBeat.Domain.Entities;
using FinBeat.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinBeat.Infrastructure.Persistence
{
    public class DataRecordRepository : IDataRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public DataRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DataRecord>> GetAllAsync(int? code = null)
        {
            IQueryable<DataRecord> query = _context.DataRecords;

            if (code.HasValue)
            {
                query = query.Where(r => r.Code == code.Value);
            }

            return await query.OrderBy(r => r.Id).ToListAsync();
        }

        public async Task SaveAsync(IEnumerable<DataRecord> records)
        {
            _context.DataRecords.AddRange(records);
            await _context.SaveChangesAsync();
        }

        public async Task ClearAsync()
        {
            _context.DataRecords.RemoveRange(_context.DataRecords);
            await _context.SaveChangesAsync();
        }
    }
}

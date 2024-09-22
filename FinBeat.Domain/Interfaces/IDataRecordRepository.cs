using FinBeat.Domain.Entities;

namespace FinBeat.Domain.Interfaces
{
    public interface IDataRecordRepository
    {
        Task<IEnumerable<DataRecord>> GetAllAsync(int? code = null);
        Task SaveAsync(IEnumerable<DataRecord> records);
        Task ClearAsync();
    }
}

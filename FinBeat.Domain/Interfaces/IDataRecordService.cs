using FinBeat.Domain.Entities;

namespace FinBeat.Domain.Interfaces
{
    public interface IDataRecordService
    {
        Task<IEnumerable<DataRecord>> GetDataRecordsAsync(int? code = null);
        Task SaveDataRecordsAsync(List<Dictionary<int, string>> rawData);
    }
}
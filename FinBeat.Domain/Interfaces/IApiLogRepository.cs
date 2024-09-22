using FinBeat.Domain.Entities;

namespace FinBeat.Domain.Interfaces
{
    public interface IApiLogRepository
    {
        Task SaveAsync(ApiLog logs);
    }
}

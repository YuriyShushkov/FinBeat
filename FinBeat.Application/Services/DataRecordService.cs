using FinBeat.Application.DTOs;
using FinBeat.Domain.Entities;
using FinBeat.Domain.Interfaces;
using FluentValidation;

namespace FinBeat.Application.Services
{
    public class DataRecordService : IDataRecordService
    {
        private readonly IDataRecordRepository _repository;
        private readonly IValidator<DataRecordDto> _validator;

        public DataRecordService(IDataRecordRepository repository, IValidator<DataRecordDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<DataRecord>> GetDataRecordsAsync(int? code = null)
            => await _repository.GetAllAsync(code);

        public async Task SaveDataRecordsAsync(List<Dictionary<int, string>> rawData)
        {
            var recordDtos = rawData.SelectMany(d => d.Select(pair => new DataRecordDto(pair.Key, pair.Value))).ToList();

            foreach (var recordDto in recordDtos)
            {
                var validationResult = _validator.Validate(recordDto);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
            }

            var records = recordDtos.Select(dto => new DataRecord(dto.Code, dto.Value))
                                    .OrderBy(r => r.Code)
                                    .ToList();

            await _repository.ClearAsync();
            await _repository.SaveAsync(records);
        }
    }
}

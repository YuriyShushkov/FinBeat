using FinBeat.Application.Services;
using FinBeat.Application.Validators;
using FinBeat.Domain.Entities;
using FinBeat.Domain.Interfaces;
using Moq;

namespace FinBeat.Tests.Domain
{
    public class DataRecordServiceTests
    {
        private readonly Mock<IDataRecordRepository> _repositoryMock;
        private readonly DataRecordService _service;

        public DataRecordServiceTests()
        {
            _repositoryMock = new Mock<IDataRecordRepository>();
            _service = new DataRecordService(_repositoryMock.Object, new DataRecordValidator());
        }

        [Fact]
        public async Task SaveDataRecordsAsync_Should_Save_Sorted_Data()
        {
            // Arrange
            var inputData = new List<Dictionary<int, string>>
            {
                new Dictionary<int, string> { { 10, "Value10" } },
                new Dictionary<int, string> { { 5, "Value5" } },
                new Dictionary<int, string> { { 1, "Value1" } }
            };

            // Act
            await _service.SaveDataRecordsAsync(inputData);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(It.Is<List<DataRecord>>(data =>
                data[0].Code == 1 && data[1].Code == 5 && data[2].Code == 10
            )), Times.Once);
        }
    }
}

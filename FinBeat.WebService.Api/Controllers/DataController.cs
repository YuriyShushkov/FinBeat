using FinBeat.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinBeat.WebService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly DataRecordService _dataRecordService;

        public DataController(DataRecordService dataRecordService)
        {
            _dataRecordService = dataRecordService;
        }

        // Метод для сохранения данных
        [HttpPost]
        public async Task<IActionResult> SaveData([FromBody] List<Dictionary<int, string>> data)
        {
            await _dataRecordService.SaveDataRecordsAsync(data);
            return Ok(new { message = "Data saved successfully" });
        }

        // Метод для получения данных
        [HttpGet]
        public async Task<IActionResult> GetData([FromQuery] int? code = null)
        {
            var data = await _dataRecordService.GetDataRecordsAsync(code);
            return Ok(data);
        }
    }
}

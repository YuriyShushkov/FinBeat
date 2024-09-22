namespace FinBeat.Domain.Entities
{
    public class DataRecord
    {
        public int Id { get; private set; }
        public int Code { get; private set; }
        public string Value { get; private set; }

        // Конструктор для создания новой записи
        public DataRecord(int code, string value)
        {
            Code = code;
            Value = value;
        }
    }
}

using SQLite;

namespace AssistidCollector1.Storage
{
    public class StorageModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Intervention { get; set; }

        public string CSV { get; set; }
    }
}

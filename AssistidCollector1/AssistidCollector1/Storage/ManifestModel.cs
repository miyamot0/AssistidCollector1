using SQLite;

namespace AssistidCollector1.Storage
{
    /// <summary>
    /// Manifest serializer for DB
    /// </summary>
    public class ManifestModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string JSON { get; set; }
    }
}

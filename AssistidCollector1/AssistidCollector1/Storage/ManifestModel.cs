﻿using SQLite;

namespace AssistidCollector1.Storage
{
    public class ManifestModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string JSON { get; set; }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistidCollector1.Storage
{
    public class ApplicationDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ApplicationDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ManifestModel>().Wait();
        }

        public Task<List<ManifestModel>> GetManifestAsync()
        {
            return database.Table<ManifestModel>().ToListAsync();
        }

        public Task<int> SaveItemAsync(ManifestModel item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(ManifestModel item)
        {

            return database.UpdateAsync(item);
        }
    }
}

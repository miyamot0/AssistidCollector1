using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssistidCollector1.Storage
{
    /// <summary>
    /// Application database
    /// </summary>
    public class ApplicationDatabase
    {
        readonly SQLiteAsyncConnection database;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbPath"></param>
        public ApplicationDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ManifestModel>().Wait();
            database.CreateTableAsync<StorageModel>().Wait();
        }

        /// <summary>
        /// Get manifest from db
        /// </summary>
        /// <returns></returns>
        public Task<List<ManifestModel>> GetManifestAsync()
        {
            return database.Table<ManifestModel>().ToListAsync();
        }

        /// <summary>
        /// Get Data from db
        /// </summary>
        /// <returns></returns>
        public Task<List<StorageModel>> GetDataAsync()
        {
            return database.Table<StorageModel>().ToListAsync();
        }

        /// <summary>
        /// Save item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<int> SaveItemAsync(ManifestModel item)
        {
            return database.InsertAsync(item);
        }

        /// <summary>
        /// Save item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<int> SaveItemAsync(StorageModel item)
        {
            return database.InsertAsync(item);
        }

        /// <summary>
        /// Save item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<int> UpdateItemAsync(ManifestModel item)
        {
            return database.UpdateAsync(item);
        }
    }
}

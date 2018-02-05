namespace AssistidCollector1.Interfaces
{
    public interface InterfaceSaveLoad
    {
        /// <summary>
        /// Build local path
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string GetLocalFilePath(string filename);

        /// <summary>
        /// Save text file to personal folder
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="text"></param>
        void SaveFile(string filename, string text);

        /// <summary>
        /// Load text file from personal folder
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string LoadFile(string filename);

        /// <summary>
        /// Check if file exists in personal folder
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool FileExists(string filename);
    }
}

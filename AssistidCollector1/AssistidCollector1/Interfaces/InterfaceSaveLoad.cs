namespace AssistidCollector1.Interfaces
{
    public interface InterfaceSaveLoad
    {
        string GetLocalFilePath(string filename);

        void SaveFile(string filename, string text);

        string LoadFile(string filename);

        bool FileExists(string filename);
    }
}

namespace AssistidCollector1.Interfaces
{
    public interface InterfaceSaveLoad
    {
        void SaveFile(string filename, string text);

        string LoadFile(string filename);

        bool FileExists(string filename);
    }
}

namespace M4Graphs.Parsers
{
    public interface IModelParser
    {
        void SetFilePath(string filePath);
        void SetModelString(string modelString);
        void DisableFileCaching();
    }
}

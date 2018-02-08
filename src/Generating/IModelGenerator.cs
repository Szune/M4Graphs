using M4Graphs.Core.Elements;

namespace M4Graphs.Generators
{
    public interface IModelGenerator
    {
        void SetStartNode(DefaultNodeElement start);
        void SetMargins(int xMargin, int yMargin);
    }
}

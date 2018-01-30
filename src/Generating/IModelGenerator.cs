using M4Graphs.Core.ModelElements;

namespace M4Graphs.Generators
{
    public interface IModelGenerator
    {
        void SetStartNode(ModelNode start);
        void SetMargins(int xMargin, int yMargin);
    }
}

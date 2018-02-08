using M4Graphs.Core;
using M4Graphs.Parsers.Graphml.Elements;

namespace M4Graphs.Wpf.Rendering
{
    public static class WpfRenderer
    {
        public static GraphmlRenderer Graphml(ElementCollection<GraphmlNodeElement, GraphmlEdgeElement> elements)
        {
            return new GraphmlRenderer(elements);
        }
    }
}

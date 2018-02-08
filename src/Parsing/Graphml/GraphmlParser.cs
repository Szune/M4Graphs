using M4Graphs.Core;
using System;
using System.IO;
using M4Graphs.Parsers.Graphml.Elements;

namespace M4Graphs.Parsers.Graphml
{
    /// <summary>
    /// Parser for .graphml files.
    /// </summary>
    public class GraphmlParser : IModel<GraphmlNodeElement, GraphmlEdgeElement>, IModelParser
    {
        private ElementCollection<GraphmlNodeElement, GraphmlEdgeElement> _elements;

        private string _filePath;
        private string _graphml;

        private enum ReadFrom
        {
            Error = 0,
            None,
            File,
            String,
            FileRepeatedly
        }

        private ReadFrom _readFrom = ReadFrom.None;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public GraphmlParser()
        {

        }

        private GraphmlParser(ElementCollection<GraphmlNodeElement, GraphmlEdgeElement> elements)
        {
            _elements = elements;
            _readFrom = ReadFrom.None;
        }

        public void SetFilePath(string filePath)
        {
            _filePath = filePath;
            if (_readFrom == ReadFrom.FileRepeatedly) return;
            _readFrom = ReadFrom.File;
        }

        public void SetModelString(string modelString)
        {
            _graphml = modelString;
            _readFrom = ReadFrom.String;
        }

        public void DisableFileCaching()
        {
            _readFrom = ReadFrom.FileRepeatedly;
        }

        /// <summary>
        /// Returns a new <see cref="GraphmlParser"/> containing elements parsed by reading a yEd .graphml file.
        /// </summary>
        /// <param name="filePath">The path to the .graphml file.</param>
        /// <returns></returns>
        public static GraphmlParser GraphmlFromFile(string filePath)
        {
            using (var text = new StreamReader(filePath))
            {
                return new GraphmlParser(GraphmlStringParser.GetElements(text.ReadToEnd()));
            }
        }

        /// <summary>
        /// Returns a new <see cref="GraphmlParser"/> containing elements parsed by reading a yEd graphml string.
        /// </summary>
        /// <param name="graphml">The entire graphml string.</param>
        /// <returns></returns>
        public static GraphmlParser GraphmlFromString(string graphml)
        {
            return new GraphmlParser(GraphmlStringParser.GetElements(graphml));
        }

        /// <summary>
        /// Returns the elements read.
        /// </summary>
        /// <returns></returns>
        public ElementCollection<GraphmlNodeElement, GraphmlEdgeElement> GetElements()
        {
            switch(_readFrom)
            {
                case ReadFrom.String:
                    if (_elements?.Count > 0)
                        return _elements;
                    _elements = GraphmlStringParser.GetElements(_graphml);
                    return _elements;
                case ReadFrom.File:
                    if (_elements?.Count > 0)
                        return _elements;
                    _elements = ReadFile(_filePath);
                    return _elements;
                case ReadFrom.FileRepeatedly:
                    _elements = ReadFile(_filePath);
                    return _elements;
                case ReadFrom.None:
                    return null;
                default:
                    throw new InvalidOperationException($"Unexpected type of reading {_readFrom.ToString()}");
            }
        }

        private ElementCollection<GraphmlNodeElement, GraphmlEdgeElement> ReadFile(string filePath)
        {
            using (var text = new StreamReader(filePath))
            {
                return GraphmlStringParser.GetElements(text.ReadToEnd());
            }
        }

        public void ClearElements()
        {
            _elements.Clear();
        }
    }
}

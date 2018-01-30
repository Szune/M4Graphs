using M4Graphs.Core;
using M4Graphs.Core.DrawableModelElements;
using System;
using System.IO;

namespace M4Graphs.Parsers.Graphml
{
    /// <summary>
    /// An <see cref="IModel"/> reading elements from a file.
    /// </summary>
    public class GraphmlParser : IModel, IModelParser
    {
        private DrawableElementCollection _elements;

        private int _xOffset;
        private int _yOffset;

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

        public void SetOffset(int xOffset, int yOffset)
        {
            _xOffset = xOffset;
            _yOffset = yOffset;
        }

        private ReadFrom _readFrom = ReadFrom.None;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public GraphmlParser()
        {

        }

        private GraphmlParser(DrawableElementCollection elements)
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

        public void SetModelString(string graphml)
        {
            _graphml = graphml;
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
        /// <param name="xOffset">Offsets all elements' x-coordinate in the model by the specified amount.</param>
        /// <param name="yOffset">Offsets all elements' y-coordinate in the model by the specified amount.</param>
        /// <returns></returns>
        public static GraphmlParser GraphmlFromFile(string filePath, int xOffset = 0, int yOffset = 0)
        {
            using (var text = new StreamReader(filePath))
            {
                return new GraphmlParser(Graphml.GraphmlStringParser.ToDrawableElementCollection(text.ReadToEnd(), xOffset, yOffset));
            }
        }

        /// <summary>
        /// Returns a new <see cref="GraphmlParser"/> containing elements parsed by reading a yEd graphml string.
        /// </summary>
        /// <param name="graphml">The entire graphml string.</param>
        /// <param name="xOffset">Offsets all elements' x-coordinate in the model by the specified amount.</param>
        /// <param name="yOffset">Offsets all elements' y-coordinate in the model by the specified amount.</param>
        /// <returns></returns>
        public static GraphmlParser GraphmlFromString(string graphml, int xOffset = 0, int yOffset = 0)
        {
            return new GraphmlParser(Graphml.GraphmlStringParser.ToDrawableElementCollection(graphml, xOffset, yOffset));
        }

        /// <summary>
        /// Returns the elements read.
        /// </summary>
        /// <returns></returns>
        public DrawableElementCollection GetElements()
        {
            switch(_readFrom)
            {
                case ReadFrom.String:
                    if (_elements?.Count > 0)
                        return _elements;
                    _elements = Graphml.GraphmlStringParser.ToDrawableElementCollection(_graphml, _xOffset, _yOffset);
                    return _elements;
                case ReadFrom.File:
                    if (_elements?.Count > 0)
                        return _elements;
                    _elements = ReadFile(_filePath, _xOffset, _yOffset);
                    return _elements;
                case ReadFrom.FileRepeatedly:
                    _elements = ReadFile(_filePath, _xOffset, _yOffset);
                    return _elements;
                case ReadFrom.None:
                    return null;
                default:
                    throw new InvalidOperationException($"Unexpected type of reading {_readFrom.ToString()}");
            }
        }

        private DrawableElementCollection ReadFile(string filePath, int xOffset, int yOffset)
        {
            using (var text = new StreamReader(filePath))
            {
                return Graphml.GraphmlStringParser.ToDrawableElementCollection(text.ReadToEnd(), xOffset, yOffset);
            }
        }

        public void ClearElements()
        {
            _elements.Clear();
        }
    }
}

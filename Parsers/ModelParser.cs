using M4Graphs.Parsers.Graphml;
using System;
using System.Collections.Generic;

namespace M4Graphs.Parsers
{
    public sealed class ModelParser
    {
        public static ModelParser<GraphmlParser> Graphml => new ModelParser<GraphmlParser>();
    }
    /// <summary>
    /// Builder for <see cref="GraphmlParser"/>s.
    /// </summary>
    public class ModelParser<TModelParser> where TModelParser : IModelParser
    {
        private Stack<Action<TModelParser>> _buildJobs = new Stack<Action<TModelParser>>();
        private TModelParser _reader;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ModelParser()
        {
            _reader = Activator.CreateInstance<TModelParser>();
        }

        public ModelParser<TModelParser> Offset(int xOffset, int yOffset)
        {
            Action<TModelParser> setOffset = (r) => r.SetOffset(xOffset, yOffset);
            _buildJobs.Push(setOffset);
            return this;
        }

        public ModelParser<TModelParser> FromFile(string filePath)
        {
            Action<TModelParser> setFilePath = (r) => r.SetFilePath(filePath);
            _buildJobs.Push(setFilePath);
            return this;
        }

        public ModelParser<TModelParser> FromString(string modelString)
        {
            Action<TModelParser> setGraphmlString = (r) => r.SetModelString(modelString);
            _buildJobs.Push(setGraphmlString);
            return this;
        }

        public ModelParser<TModelParser> NoCache()
        {
            Action<TModelParser> disableFileCaching = (r) => r.DisableFileCaching();
            _buildJobs.Push(disableFileCaching);
            return this;
        }

        public TModelParser Build()
        {
            while(_buildJobs.Count > 0)
                _buildJobs.Pop()(_reader);
            return _reader;
        }


    }
}

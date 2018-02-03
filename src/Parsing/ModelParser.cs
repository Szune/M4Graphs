using M4Graphs.Parsers.Graphml;
using System;
using System.Collections.Generic;

namespace M4Graphs.Parsers
{
    public static class ModelParser
    {
        public static ModelParser<GraphmlParser> Graphml => new ModelParser<GraphmlParser>();
    }
    /// <summary>
    /// Builder for <see cref="GraphmlParser"/>s.
    /// </summary>
    public class ModelParser<TModelParser> where TModelParser : IModelParser
    {
        private readonly Stack<Action<TModelParser>> _buildJobs = new Stack<Action<TModelParser>>();
        private readonly TModelParser _reader;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ModelParser()
        {
            _reader = Activator.CreateInstance<TModelParser>();
        }

        public ModelParser<TModelParser> Offset(int xOffset, int yOffset)
        {
            void SetOffset(TModelParser r) => r.SetOffset(xOffset, yOffset);
            _buildJobs.Push(SetOffset);
            return this;
        }

        public ModelParser<TModelParser> FromFile(string filePath)
        {
            void SetFilePath(TModelParser r) => r.SetFilePath(filePath);
            _buildJobs.Push(SetFilePath);
            return this;
        }

        public ModelParser<TModelParser> FromString(string modelString)
        {
            void SetGraphmlString(TModelParser r) => r.SetModelString(modelString);
            _buildJobs.Push(SetGraphmlString);
            return this;
        }

        public ModelParser<TModelParser> NoCache()
        {
            void DisableFileCaching(TModelParser r) => r.DisableFileCaching();
            _buildJobs.Push(DisableFileCaching);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4Graphs.Core
{
    /// <summary>
    /// Builder for <see cref="ModelReader"/>s.
    /// </summary>
    public class ModelReaderBuilder
    {
        private Stack<Action<ModelReader>> _buildJobs = new Stack<Action<ModelReader>>();
        private ModelReader _reader;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ModelReaderBuilder()
        {
            _reader = new ModelReader();
        }

        public ModelReaderBuilder Offset(int xOffset, int yOffset)
        {
            Action<ModelReader> setOffset = (r) => r.SetOffset(xOffset, yOffset);
            _buildJobs.Push(setOffset);
            return this;
        }

        public ModelReaderBuilder FromFile(string filePath)
        {
            Action<ModelReader> setFilePath = (r) => r.SetFilePath(filePath);
            _buildJobs.Push(setFilePath);
            return this;
        }

        public ModelReaderBuilder FromString(string graphml)
        {
            Action<ModelReader> setGraphmlString = (r) => r.SetGraphmlString(graphml);
            _buildJobs.Push(setGraphmlString);
            return this;
        }

        public ModelReaderBuilder NoCache()
        {
            Action<ModelReader> disableFileCaching = (r) => r.DisableFileCaching();
            _buildJobs.Push(disableFileCaching);
            return this;
        }

        public ModelReader Build()
        {
            while(_buildJobs.Count > 0)
                _buildJobs.Pop()(_reader);
            return _reader;
        }


    }
}

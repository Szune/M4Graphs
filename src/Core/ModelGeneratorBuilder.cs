using System;
using System.Collections;
using System.Collections.Generic;

namespace M4Graphs.Core
{
    /// <summary>
    /// Builder for <see cref="ModelGenerator"/>s.
    /// </summary>
    public class ModelGeneratorBuilder
    {
        private Stack<Action<ModelGenerator>> _buildJobs = new Stack<Action<ModelGenerator>>();
        private ModelGenerator _generator;
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ModelGeneratorBuilder()
        {
            _generator = new ModelGenerator();
        }

        /// <summary>
        /// Sets the margins used when drawing.
        /// </summary>
        /// <param name="xMargin"></param>
        /// <param name="yMargin"></param>
        public ModelGeneratorBuilder Margins(int xMargin, int yMargin)
        {
            Action<ModelGenerator> setMargins = (g) => g.SetMargins(xMargin, yMargin);
            _buildJobs.Push(setMargins);
            return this;
        }

        /// <summary>
        /// Sets the start node.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        public ModelGeneratorBuilder StartNode(string id, string text)
        {
            Action<ModelGenerator> setStartNode = (g) => g.SetStartNode(ModelElementFactory.CreateNode(id, text));
            _buildJobs.Push(setStartNode);
            return this;
        }

        /// <summary>
        /// Builds the <see cref="ModelGenerator"/>.
        /// </summary>
        /// <returns></returns>
        public ModelGenerator Build()
        {
            while(_buildJobs.Count > 0)
                _buildJobs.Pop()(_generator); // very pretty syntax
            return _generator;
        }
    }
}
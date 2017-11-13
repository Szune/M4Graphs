using System.Collections.Generic;
using System.Linq;
using M4Graphs.Core.General;
using M4Graphs.Core.ModelElements;
using M4Graphs.Core.DrawableModelElements;
using M4Graphs.Core.Converters;
using System.IO;

namespace M4Graphs.Core
{
    /// <summary>
    /// Builders for <see cref="IModel"/>s.
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Set up an <see cref="IModel"/> that reads from a file or string.
        /// </summary>
        public static ModelReaderBuilder Reader => new ModelReaderBuilder();
        /// <summary>
        /// Set up an <see cref="IModel"/> that generates elements at runtime.
        /// </summary>
        public static ModelGeneratorBuilder Generator => new ModelGeneratorBuilder();
    }
}

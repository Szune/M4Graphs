using M4Graphs.Core.Converters;
using M4Graphs.Core.Converters.Graphml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4Graphs.CoreTest
{
    [TestClass]
    public class ConverterTest
    {
        [TestMethod]
        public void ManualTest_TestGraphmlConversion()
        {
            var text = new StreamReader(@"E:\exempel.graphml").ReadToEnd();
            var tree = Graphml.ToDrawableElementCollection(text);
        }
    }
}

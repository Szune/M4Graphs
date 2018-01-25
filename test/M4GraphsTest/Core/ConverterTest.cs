using M4Graphs.Parsers.Graphml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace M4GraphsTest.Core
{
    [TestClass]
    public class ConverterTest
    {
        [TestMethod]
        public void ManualTest_TestGraphmlConversion()
        {
            var text = new StreamReader(@"E:\exempel.graphml").ReadToEnd();
            var tree = GraphmlStringParser.ToDrawableElementCollection(text);
        }
    }
}

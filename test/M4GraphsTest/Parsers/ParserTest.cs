using M4Graphs.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace M4GraphsTest.Core
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void ManualTest_TestGraphmlConversion()
        {
            var text = new StreamReader(@"E:\exempel.graphml").ReadToEnd();
            var tree = ModelParser.Graphml.FromString(text).Build().GetElements();
            tree.Clear();
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using M4Graphs.Generators;
using M4Graphs.Parsers;

namespace M4GraphsTest.Core
{
    [TestClass]
    public class BuilderTest
    {
        [TestMethod]
        public void ModelGeneratorBuilder_Order_Of_Jobs_Should_Not_Matter()
        {
            var generator = ModelGenerator.Default.Margins(10, 11).StartNode("n0", "Start").Build();
            generator.Margins.X.Should().Be(10);
            generator.Margins.Y.Should().Be(11);
            generator.StartNode.Id.Should().Be("n0");
        }

        [TestMethod]
        public void ModelReaderBuilder_Order_Of_Jobs_Should_Not_Matter()
        {
            var reader = ModelParser.Graphml.Offset(5, 6).NoCache().Build();
        }
    }
}

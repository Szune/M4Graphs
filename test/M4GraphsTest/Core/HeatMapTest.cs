using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using M4Graphs.Core;
using FluentAssertions;

namespace M4GraphsTest.Core
{
    [TestClass]
    public class HeatMapTest
    {
        HeatMap map;
        [TestInitialize]
        public void Initialize()
        {
            map = new HeatMap();
        }

        [TestMethod]
        public void GetHeat_Throws_If_Element_Does_Not_Exist_In_HeatMap()
        {
            Action act = () => map.GetHeat("e1");
            act.Should().ThrowExactly<KeyNotFoundException>();
        }

        [TestMethod]
        public void GetHeat_Returns_100_Percent_If_There_Is_Only_One_Element_In_HeatMap()
        {
            // setup
            map.AddHeat("n1");
            map.AddHeat("n1");
            // assert
            map.GetHeat("n1").Should().BeApproximately(1, 0.0000000001);
        }
        
        [TestMethod]
        public void GetHeat_Returns_50_Percent_If_There_Are_Only_Two_Elements_With_Same_Amount_Of_Hits()
        {
            // setup
            map.AddHeat("n1");
            map.AddHeat("e1");
            // assert
            map.GetHeat("n1").Should().BeApproximately(0.5, 0.0001);
        }

        [TestMethod]
        public void GetHeat_Return_40_Percent_When_4_Out_Of_10_Hits_On_One_Element()
        {
            // setup
            for (int i = 0; i < 4; i++)
            {
                map.AddHeat("n1");
                map.AddHeat("e1");
            }
            map.AddHeat("n2");
            map.AddHeat("n2");
            // assert
            map.GetHeat("n1").Should().BeApproximately(0.4, 0.0001);
            map.GetHeat("e1").Should().BeApproximately(0.4, 0.0001);
        }

        [TestMethod]
        public void GetHeat_Return_20_Percent_When_2_Out_Of_10_Hits_On_One_Element()
        {
            // setup
            for (int i = 0; i < 4; i++)
            {
                map.AddHeat("n1");
                map.AddHeat("e1");
            }
            map.AddHeat("n2");
            map.AddHeat("n2");
            // assert
            map.GetHeat("n2").Should().BeApproximately(0.2, 0.0001);
        }

        [TestMethod]
        public void AddHeat_should_throw_if_id_is_null()
        {
            Action act = () => map.AddHeat(null);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void GetVisitedElements_returns_all_visited_elements()
        {
            map.AddHeat("n1");
            map.AddHeat("n2");
            map.GetVisitedElements().Should().Contain("n1", "n2");
        }

        [TestMethod]
        public void GetVisitedElements_should_return_empty_IEnumerable_when_no_elements_have_been_visited()
        {
            map.GetVisitedElements().Should().BeEmpty();
        }

        [TestMethod]
        public void Reset_should_reset_visited_elements()
        {
            map.AddHeat("n1");
            map.Reset();
            map.GetVisitedElements().Should().BeEmpty();
        }

        [TestMethod]
        public void Reset_should_reset_total_hits()
        {
                               // assumptions: (correct if total hits are reset)
            map.AddHeat("n1"); // n1 heat = 1
            map.Reset();       // n1 heat = non-existent
            map.AddHeat("n1"); // n1 heat = 1
            map.AddHeat("n2"); // n1 heat = 0.5 (2 different elements have been visited an equal amount of times)
            map.GetHeat("n1").Should().BeApproximately(0.5, 0.0001);
        }

        [TestMethod]
        public void TryGetHeat_should_return_true_if_id_is_present()
        {
            map.AddHeat("n1");
            map.TryGetHeat("n1", out _).Should().BeTrue();
        }

        [TestMethod]
        public void TryGetHeat_should_return_false_if_id_is_not_present()
        {
            map.TryGetHeat("n1", out _).Should().BeFalse();
        }
    }
}

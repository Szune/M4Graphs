using System;
using FluentAssertions;
using M4Graphs.Core.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace M4GraphsTest.Core
{
    [TestClass]
    public class LevelListTest
    {
        private LevelList<int> _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new LevelList<int> { 1, 2, 3 };
        }

        [TestMethod]
        public void IsAtFirst_should_return_true_when_first_constructed()
        {
            _sut.IsAtFirst.Should().BeTrue();
        }

        [TestMethod]
        public void IsAtFirst_should_return_false_if_Current_is_not_first_index()
        {
            _sut.SelectNext();
            _sut.IsAtFirst.Should().BeFalse();
        }

        [TestMethod]
        public void IsAtLast_should_return_false_if_Current_is_not_last_index()
        {
            _sut.IsAtLast.Should().BeFalse();
        }

        [TestMethod]
        public void IsAtLast_should_return_true_if_Current_is_last_index()
        {
            _sut.SelectLast();
            _sut.IsAtLast.Should().BeTrue();
        }

        [TestMethod]
        public void SelectFirst_should_return_first_item()
        {
            _sut.SelectFirst().Should().Be(1);
        }

        [TestMethod]
        public void SelectLast_should_return_last_item()
        {
            _sut.SelectLast().Should().Be(3);
        }

        [TestMethod]
        public void SelectLast_on_empty_LevelList_should_throw_ArgumentOutOfRangeException()
        {
            var sut = new LevelList<int>();
            Action act = () => sut.SelectLast();
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }


        [TestMethod]
        public void SelectFirst_on_empty_LevelList_should_throw_ArgumentOutOfRangeException()
        {
            var sut = new LevelList<int>();
            Action act = () => sut.SelectFirst();
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void SelectNext_on_empty_LevelList_should_throw_ArgumentOutOfRangeException()
        {
            var sut = new LevelList<int>();
            Action act = () => sut.SelectNext();
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void SelectPrevious_on_empty_LevelList_should_throw_ArgumentOutOfRangeException()
        {
            var sut = new LevelList<int>();
            Action act = () => sut.SelectPrevious();
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void SelectNext_should_return_the_next_item()
        {
            _sut.SelectNext().Should().Be(2);
        }

        [TestMethod]
        public void SelectNext_should_return_the_last_item_if_next_is_out_of_range()
        {
            _sut.SelectLast();
            _sut.SelectNext().Should().Be(3);
        }

        [TestMethod]
        public void SelectPrevious_should_return_the_previous_item()
        {
            _sut.SelectLast();
            _sut.SelectPrevious().Should().Be(2);
        }

        [TestMethod]
        public void SelectPrevious_should_return_the_first_item_if_previous_is_out_of_range()
        {
            _sut.SelectPrevious().Should().Be(1);
        }

        [TestMethod]
        public void IEnumerable_foreach_should_return_items_in_order_of_insertion()
        {
            var c = 0;
            foreach (var lvl in _sut)
            {
                c++;
                lvl.Should().Be(c);
            }
        }

        [TestMethod]
        public void Count_should_return_amount_of_items_in_list()
        {
            _sut.Count.Should().Be(3);
        }

        [TestMethod]
        public void Clear_clears_the_list()
        {
            _sut.Clear();
        }
    }
}

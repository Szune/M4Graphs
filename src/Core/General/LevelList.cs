using System.Collections;
using System.Collections.Generic;

namespace M4Graphs.Core.General
{
    /// <summary>
    /// List class consisting of levels.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class LevelList<TType> : IEnumerable<TType>
    {
        private readonly List<TType> _levels = new List<TType>();


        /// <summary>
        /// Returns a value indicating if the current level is at the bottom of the list.
        /// </summary>
        public bool IsAtFirst => _currentIndex == 0;
        /// <summary>
        /// Returns a value indicating if the current level is at the top of the list.
        /// </summary>
        public bool IsAtLast => _currentIndex == _levels.Count - 1;

        private int _currentIndex;

        /// <summary>
        /// Returns the amount of levels contained in the LevelList.
        /// </summary>
        public int Count => _levels.Count;

        /// <summary>
        /// Returns the current level.
        /// </summary>
        public TType Current => _levels[_currentIndex];

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public LevelList() { }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="currentLevel">The zero-based level to use.</param>
        public LevelList(int currentLevel)
        {
            _currentIndex = currentLevel;
        }

        /// <summary>
        /// Adds another level to the end of the list.
        /// </summary>
        /// <param name="item">The level to add.</param>
        public void Add(TType item)
        {
            _levels.Add(item);
        }

        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            _levels.Clear();
        }

        /// <summary>
        /// Sets the next level as current and returns it.
        /// </summary>
        public TType SelectNext()
        {
            if (IsAtLast) return Current;
            _currentIndex++;
            return Current;
        }

        /// <summary>
        /// Sets previous level as current and returns it.
        /// </summary>
        public TType SelectPrevious()
        {
            if (IsAtFirst) return Current;
            _currentIndex--;
            return Current;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="LevelList{TType}"/>.
        /// </summary>
        public IEnumerator<TType> GetEnumerator()
        {
            return _levels.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="LevelList{TType}"/>.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _levels.GetEnumerator();
        }

        /// <summary>
        /// Selects and returns the first level.
        /// </summary>
        public TType SelectFirst()
        {
            _currentIndex = 0;
            return Current;
        }

        /// <summary>
        /// Selects and returns the last level.
        /// </summary>
        public TType SelectLast()
        {
            var lastIndex = _levels.Count - 1;
            _currentIndex = lastIndex < 0 ? 0 : lastIndex;
            return Current;
        }
    }
}

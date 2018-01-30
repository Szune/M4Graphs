using System.Collections;
using System.Collections.Generic;

namespace M4Graphs.Core.General
{
    /// <summary>
    /// A zero-based list class consisting of levels.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class LevelList<TType> : IEnumerable<TType>
    {
        private List<TType> _levels = new List<TType>();


        /// <summary>
        /// Returns a value indicating if the current level is at either the bottom or the top of the list.
        /// </summary>
        public bool IsAtBoundary => IsAtBottom || IsAtTop;
        /// <summary>
        /// Returns a value indicating if the current level is at the bottom of the list.
        /// </summary>
        public bool IsAtBottom => _currentIndex == 0;
        /// <summary>
        /// Returns a value indicating if the current level is at the top of the list.
        /// </summary>
        public bool IsAtTop => _currentIndex == _levels.Count - 1;

        private int _currentIndex;

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
        /// Goes down a level and returns it.
        /// </summary>
        /// <returns></returns>
        public TType Down()
        {
            if (IsAtBottom) return Current;
            _currentIndex--;
            return Current;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="LevelList{TType}"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TType> GetEnumerator()
        {
            return _levels.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="LevelList{TType}"/>.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _levels.GetEnumerator();
        }

        /// <summary>
        /// Goes up a level and returns it.
        /// </summary>
        /// <returns></returns>
        public TType Up()
        {
            if (IsAtTop) return Current;
            _currentIndex++;
            return Current;
        }
    }
}

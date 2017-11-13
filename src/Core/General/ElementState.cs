using System;

namespace M4Graphs.Core.General
{
    [Flags]
    public enum ElementState
    {
        None = 0,
        Normal = 1,
        Activated = 2,
        Filtered = 4
    }

    public static class ElementStateExtension
    {
        public static ElementState AddFlag(this ElementState state, ElementState flag)
        {
            if(!state.HasFlag(flag)) // only add the flag if state doesn't already have it
                return state |= flag; // adds flag
            return state;
        }

        public static ElementState RemoveFlag(this ElementState state, ElementState flag)
        {
            if(state.HasFlag(flag)) // only remove the flag if state actually has it
                return state &= ~flag; // removes flag
            return state;
        }
    }
}

using System;

namespace M4Graphs.Core.General
{
    [Flags]
    public enum ElementStates
    {
        None = 0,
        Normal = 1,
        Activated = 2,
        Filtered = 4
    }

    public static class ElementStateExtension
    {
        public static ElementStates AddFlag(this ElementStates states, ElementStates flag)
        {
            if(!states.HasFlag(flag)) // only add the flag if states doesn't already have it
                return states |= flag; // adds flag
            return states;
        }

        public static ElementStates RemoveFlag(this ElementStates states, ElementStates flag)
        {
            if(states.HasFlag(flag)) // only remove the flag if states actually has it
                return states &= ~flag; // removes flag
            return states;
        }
    }
}

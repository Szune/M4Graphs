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
            if (states.HasFlag(flag)) return states;
            states |= flag; // adds flag
            return states; 

        }

        public static ElementStates RemoveFlag(this ElementStates states, ElementStates flag)
        {
            if (!states.HasFlag(flag)) return states; // only remove the flag if states actually has it
            states &= ~flag; // removes flag
            return states; 
        }
    }
}

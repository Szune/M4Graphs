namespace M4Graphs.Core.Interfaces
{
    /// <summary>
    /// Represents a class that can be activated and deactivated.
    /// </summary>
    public interface IActivatable
    {
        /// <summary>
        /// Activates the object.
        /// </summary>
        void Activate();
        /// <summary>
        /// Deactivates the object.
        /// </summary>
        void Deactivate();
    }
}

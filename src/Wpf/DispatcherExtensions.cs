using System;
using System.Windows.Threading;

namespace M4Graphs.Wpf
{
    /// <summary>
    /// Extension method(s) for <see cref="Dispatcher"/>s.
    /// </summary>
    public static class DispatcherExtensions
    {
        /// <summary>
        /// Invokes the specified action on the UI thread.
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <param name="toPerform"></param>
        public static void InvokeOnUIThread(this Dispatcher dispatcher, Action toPerform)
        {
            if (dispatcher == null) return;
            if (toPerform == null) return;
            if (dispatcher.CheckAccess())
                toPerform();
            else
                dispatcher.Invoke(toPerform);
        }
    }
}

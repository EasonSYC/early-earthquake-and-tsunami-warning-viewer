using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.Events
{
    /// <summary>
    /// The event arguments for the language changed event.
    /// </summary>
    internal class LanguagedChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The new language that has been set.
        /// </summary>
        public required CultureInfo Language { get; init; }
    }
}

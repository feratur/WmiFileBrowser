using System;
using System.Collections.Generic;

namespace WmiFileBrowser.Interfaces
{
    /// <summary>
    /// The path of the object (file or directory).
    /// </summary>
    public interface IFilePath : IEquatable<IFilePath>
    {
        /// <summary>
        /// The letter of the logical disk (default char to display drive list).
        /// </summary>
        char DriveLetter { get; }

        /// <summary>
        /// The list of the path nodes (without slashes).
        /// </summary>
        IEnumerable<string> PathNodes { get; }

        /// <summary>
        /// Returns the copy of the path object.
        /// </summary>
        /// <returns>The copy of the path.</returns>
        IFilePath Copy();
    }
}
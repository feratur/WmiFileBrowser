using System;
using System.Collections.Generic;

namespace WmiFileBrowser.Interfaces
{
    public interface IFilePath : IEquatable<IFilePath>
    {
        char DriveLetter { get; }

        IEnumerable<string> PathNodes { get; }

        IFilePath Copy();
    }
}
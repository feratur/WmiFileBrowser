using System.Collections.Generic;

namespace WmiFileBrowser.Interfaces
{
    public interface IFilePath
    {
        char DriveLetter { get; }

        IEnumerable<string> PathNodes { get; }
    }
}
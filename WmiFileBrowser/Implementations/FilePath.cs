using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WmiFileBrowser.Interfaces;

namespace WmiFileBrowser.Implementations
{
    class FilePath : IFilePath
    {
        private readonly List<string> _pathNodes = new List<string>();
        private char _driveLetter;

        public char DriveLetter
        {
            get { return _driveLetter; }
            private set { _driveLetter = char.ToUpper(value); }
        }

        public IEnumerable<string> PathNodes
        {
            get { return _pathNodes; }
        }

        public FilePath()
        {
        }

        public FilePath(char drive, IEnumerable<string> nodes)
        {
            DriveLetter = drive;
            _pathNodes.AddRange(nodes);
        }

        public static FilePath ParsePath(string path)
        {
            var result = new FilePath();

            if (string.IsNullOrWhiteSpace(path) || 
                path.Length < 2 || 
                !char.IsLetter(path[0]) || 
                path[1] != ':' ||
                path.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                return result;

            result.DriveLetter = path[0];
            path = path.Replace('/', '\\');
            var nodes = path.Split('\\');
            if (nodes[0].Length != 2 || nodes.Any(string.IsNullOrWhiteSpace))
                return result;

            result._pathNodes.AddRange(nodes.Skip(1));
            return result;
        }

        public IFilePath Copy()
        {
            return new FilePath(DriveLetter, _pathNodes);
        }

        public bool Equals(IFilePath other)
        {
            return other != null && other.DriveLetter == DriveLetter &&
                   other.PathNodes.SequenceEqual(PathNodes, StringComparer.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return DriveLetter == default(char)
                ? string.Empty
                : string.Format("{0}:\\{1}", DriveLetter, string.Join("\\", PathNodes));
        }
    }
}
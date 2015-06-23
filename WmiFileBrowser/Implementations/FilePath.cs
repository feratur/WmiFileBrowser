﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmiFileBrowser.Interfaces;

namespace WmiFileBrowser.Implementations
{
    class FilePath : IFilePath
    {
        public char DriveLetter { get; set; }
        public IEnumerable<string> PathNodes { get; set; }
    }
}

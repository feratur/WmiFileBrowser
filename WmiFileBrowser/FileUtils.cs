using System.Collections.Generic;
using System.Management;
using System.Text;
using WmiFileBrowser.Auxiliary;
using WmiFileBrowser.Implementations;
using WmiFileBrowser.Interfaces;

namespace WmiFileBrowser
{
    static class FileUtils
    {
        static string GetPath(IFilePath path)
        {
            var sb = new StringBuilder(@"\\");
            foreach (var node in path.PathNodes)
                sb.Append(node + @"\\");
            return string.Format("drive = '{0}:' and path = '{1}'", path.DriveLetter, sb);
        }

        public static List<IFileDescriptor> Browse(ManagementScope scope,
            IDictionary<ObjectType, ObjectInfoContainer> objectInfo, IFilePath filePath)
        {
            var result = new List<IFileDescriptor>();
            if (filePath.DriveLetter == default(char))
            {
                var driveInfo = objectInfo[ObjectType.Drive];
                PopulateList(scope, driveInfo, null, result);
            }
            else
            {
                var path = GetPath(filePath);

                var directoryInfo = objectInfo[ObjectType.Directory];
                PopulateList(scope, directoryInfo, path, result);

                var fileInfo = objectInfo[ObjectType.File];
                PopulateList(scope, fileInfo, path, result);
            }
            return result;
        }

        private static void PopulateList(ManagementScope scope, ObjectInfoContainer info, string condition,
            ICollection<IFileDescriptor> list)
        {
            foreach (var wmiFile in WmiUtils.GetWmiQuery(scope, info.ClassName, condition, info.Properties))
            {
                using (wmiFile)
                {
                    var file = new FileSystemObject(info);
                    file.PopulateProperties(wmiFile);
                    list.Add(file);
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using WmiFileBrowser.Auxiliary;
using WmiFileBrowser.Implementations;
using WmiFileBrowser.Interfaces;

namespace WmiFileBrowser.Utils
{
    static class FileUtils
    {
        private static string GetSearchCondition(IFilePath path)
        {
            var sb = new StringBuilder(@"\\");
            foreach (var node in path.PathNodes)
                sb.Append(node + @"\\");
            return string.Format("drive = '{0}:' and path = '{1}'", path.DriveLetter, sb);
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

        public static List<IFileDescriptor> Browse(ManagementScope scope,
            IDictionary<ObjectType, ObjectInfoContainer> objectInfo, IFilePath filePath, bool getFiles,
            string[] extensions)
        {
            var result = new List<IFileDescriptor>();

            if (filePath.DriveLetter == default(char))
            {
                var driveInfo = objectInfo[ObjectType.Drive];
                PopulateList(scope, driveInfo, null, result);
            }
            else
            {
                var path = GetSearchCondition(filePath);

                var directoryInfo = objectInfo[ObjectType.Directory];
                PopulateList(scope, directoryInfo, path, result);

                if (getFiles)
                {
                    var fileInfo = objectInfo[ObjectType.File];
                    PopulateList(scope, fileInfo,
                        path +
                        (extensions != null && extensions.Any()
                            ? string.Format(" and ({0})",
                                string.Join(" or ", extensions.Select(p => string.Format("extension = '{0}'", p))))
                            : string.Empty), result);
                }
            }

            return result;
        }
    }
}

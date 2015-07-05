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
        private static string GetPathSearchCondition(IFilePath path)
        {
            var sb = new StringBuilder(@"\\");
            foreach (var node in path.PathNodes)
                sb.Append(node + @"\\");
            return string.Format("drive = '{0}:' and path = '{1}'", path.DriveLetter, sb);
        }

        private static string GetCheckCondition(IFilePath path)
        {
            var sb = new StringBuilder();
            string last = null;
            foreach (var node in path.PathNodes)
            {
                sb.Append(last + @"\\");
                last = node;
            }
            return string.Format("drive = '{0}:' and path = '{1}' and name = '{2}'", path.DriveLetter, sb,
                path.ToString().Replace(@"\", @"\\"));
        }

        private static void PopulateList(ManagementScope scope, IDictionary<ObjectType, ObjectInfoContainer> objectInfo,
            ObjectType type, string condition, ICollection<IFileDescriptor> list)
        {
            var info = objectInfo[type];
            foreach (var wmiFile in WmiUtils.GetWmiQuery(scope, info.ClassName, condition, info.Properties))
            {
                using (wmiFile)
                {
                    var file = new FileSystemObject(type, info.Properties);
                    file.PopulateProperties(wmiFile);
                    list.Add(file);
                }
            }
        }

        public static bool CheckIfObjectExists(ManagementScope scope, IFilePath filePath, bool isFile)
        {
            using (
                var wmiObject = WmiUtils.GetFirstOrDefaultWmiObject(scope, isFile ? "cim_datafile" : "win32_directory",
                    GetCheckCondition(filePath), new string[0]))
            {
                return wmiObject != null;
            }
        }

        public static List<IFileDescriptor> Browse(ManagementScope scope,
            IDictionary<ObjectType, ObjectInfoContainer> objectInfo, IFilePath filePath, bool getFiles,
            string[] extensions)
        {
            var result = new List<IFileDescriptor>();

            if (filePath.DriveLetter == default(char))
                PopulateList(scope, objectInfo, ObjectType.Drive, null, result);
            else
            {
                var path = GetPathSearchCondition(filePath);

                PopulateList(scope, objectInfo, ObjectType.Directory, path, result);

                if (getFiles)
                {
                    PopulateList(scope, objectInfo, ObjectType.File,
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

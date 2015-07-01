using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security;

namespace WmiFileBrowser.Utils
{
    static class WmiUtils
    {
        private static ConnectionOptions CreateConnectionOptions(string login, SecureString password)
        {
            return new ConnectionOptions
            {
                Authentication = AuthenticationLevel.PacketPrivacy,
                EnablePrivileges = true,
                Impersonation = ImpersonationLevel.Impersonate,
                Locale = "MS_409",
                SecurePassword = password,
                Username = login
            };
        }

        private static ManagementScope CreateScope(string address, string wmiNamespace, ConnectionOptions options)
        {
            return
                new ManagementScope(
                    new ManagementPath
                    {
                        NamespacePath = wmiNamespace,
                        Server = address
                    }, options);
        }

        private static EnumerationOptions GetEnumerationOptions(int blockSize)
        {
            return new EnumerationOptions
            {
                BlockSize = blockSize,
                DirectRead = true,
                EnsureLocatable = true,
                EnumerateDeep = false,
                ReturnImmediately = false,
                Rewindable = false
            };
        }

        private static ManagementObjectCollection GetManagementCollection(ManagementScope scope, string className,
            string condition, string[] properties, int blockSize)
        {
            using (
                var searcher = new ManagementObjectSearcher(scope, new SelectQuery(className, condition, properties),
                    GetEnumerationOptions(blockSize)))
            {
                return searcher.Get();
            }
        }

        public static ManagementScope GetConnectedScope(string address, string wmiNamespace, string login = null,
            SecureString password = null)
        {
            var scope = CreateScope(address, wmiNamespace, CreateConnectionOptions(login, password));
            ConnectScope(scope);
            return scope;
        }

        public static void DisconnectScope(ManagementScope scope)
        {
            var server = scope.Path.Server;
            scope.Path.Server = string.Empty;
            scope.Path.Server = server;
        }
        
        public static void ConnectScope(ManagementScope scope)
        {
            try
            {
                scope.Connect();
            }
            catch (ManagementException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ManagementStatus.LocalCredentials:
                        scope =
                            new ManagementScope(string.IsNullOrEmpty(scope.Path.Server)
                                ? scope.Path.NamespacePath
                                : @"\\" + scope.Path.Server + @"\" + scope.Path.NamespacePath);
                        scope.Connect();
                        break;
                    default:
                        throw;
                }
            }
        }

        public static IEnumerable<ManagementObject> GetWmiQuery(ManagementScope scope, string className,
            string condition = null, string[] properties = null, int blockSize = 100)
        {
            using (var collection = GetManagementCollection(scope, className, condition, properties, blockSize))
            {
                foreach (var obj in collection.Cast<ManagementObject>())
                {
                    yield return obj;
                }
            }
        }

        public static ManagementObject GetFirstOrDefaultWmiObject(ManagementScope scope, string className,
            string condition = null, string[] properties = null)
        {
            return GetWmiQuery(scope, className, condition, properties, 1).FirstOrDefault();
        }
    }
}

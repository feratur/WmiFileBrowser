using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security;

namespace WmiFileBrowser
{
    static class WmiUtils
    {
        static ConnectionOptions CreateConnectionOptions(string login, SecureString password)
        {
            return new ConnectionOptions
            {
                Authentication = AuthenticationLevel.PacketPrivacy,
                EnablePrivileges = true,
                Impersonation = ImpersonationLevel.Impersonate,
                Locale = "MS_409",
                SecurePassword = password != null ? password.Copy() : null,
                Username = login
            };
        }

        static ManagementScope CreateScope(string address, string wmiNamespace, ConnectionOptions options)
        {
            return
                new ManagementScope(
                    new ManagementPath
                    {
                        NamespacePath = wmiNamespace,
                        Server = address
                    }, options);
        }

        static EnumerationOptions GetEnumerationOptions(int blockSize)
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

        public static ManagementScope GetConnectedScope(string address, string wmiNamespace, string login = null,
            SecureString password = null)
        {
            var scope = CreateScope(address, wmiNamespace, CreateConnectionOptions(login, password));
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
                            new ManagementScope(string.IsNullOrEmpty(address)
                                ? wmiNamespace
                                : @"\\" + address + @"\" + wmiNamespace);
                        scope.Connect();
                        break;
                    default:
                        throw;
                }
            }
            return scope;
        }

        public static IEnumerable<ManagementObject> GetWmiQuery(ManagementScope scope, string className,
            string condition = null, string[] properties = null, int blockSize = 100)
        {
            using (
                var searcher = new ManagementObjectSearcher(scope, new SelectQuery(className, condition, properties),
                    GetEnumerationOptions(blockSize)))
            {
                return searcher.Get().Cast<ManagementObject>();
            }
        }
    }
}

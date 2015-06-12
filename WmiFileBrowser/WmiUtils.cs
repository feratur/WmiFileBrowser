using System.Management;
using System.Security;

namespace WmiFileBrowser
{
    static class WmiUtils
    {
        public static ConnectionOptions CreateConnectionOptions(string login, SecureString password)
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

        public static ManagementScope CreateScope(string address, string wmiNamespace, ConnectionOptions options)
        {
            return
                new ManagementScope(
                    new ManagementPath
                    {
                        NamespacePath = wmiNamespace,
                        Server = address
                    }, options);
        }

        public static ManagementScope GetConnectedScope(string address, string wmiNamespace, string login,
            SecureString password)
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
    }
}

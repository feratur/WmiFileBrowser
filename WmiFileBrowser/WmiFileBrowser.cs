using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security;
using WmiFileBrowser.Auxiliary;
using WmiFileBrowser.Implementations;
using WmiFileBrowser.Interfaces;
using WmiFileBrowser.Utils;

namespace WmiFileBrowser
{
    /// <summary>
    /// A class that provides access to the filesystem of remote computer through WMI.
    /// </summary>
    public class WmiFileBrowser
    {
        private ManagementScope _scope;
        private readonly Stack<IFilePath> _currentHistory = new Stack<IFilePath>();
        private readonly Stack<IFilePath> _forwardHistory = new Stack<IFilePath>();

        /// <summary>
        /// If set to true the GetData() method will return all the properties of WMI objects.
        /// </summary>
        public bool ReturnFullInfo { get; set; }

        /// <summary>
        /// If set to true the GetData() method will not return file objects.
        /// </summary>
        public bool ShowDirectoriesOnly { get; set; }

        /// <summary>
        /// Can be set to return only files of certain extensions.
        /// </summary>
        public string[] FileExtensions { get; set; }

        /// <summary>
        /// Indicates whether GoBack() method is available.
        /// </summary>
        public bool IsBackAvailable
        {
            get { return _currentHistory.Count > 1; }
        }

        /// <summary>
        /// Indicates whether GoForward() method is available.
        /// </summary>
        public bool IsForwardAvailable
        {
            get { return _forwardHistory.Any(); }
        }

        /// <summary>
        /// Returns the current path of the browser in string format.
        /// </summary>
        public string CurrentPath
        {
            get { return _currentHistory.Any() ? _currentHistory.Peek().ToString() : null; }
        }

        /// <summary>
        /// Indicates whether the browser is connected to the computer and is ready for use.
        /// </summary>
        public bool IsInitialized
        {
            get { return _scope != null; }
        }

        /// <summary>
        /// Returns the current path of the browser as a copy of an IFilePath object.
        /// </summary>
        /// <returns>A copy of the current IFilePath object.</returns>
        public IFilePath GetCurrentPath()
        {
            return _currentHistory.Any() ? _currentHistory.Peek().Copy() : null;
        }

        /// <summary>
        /// Sets the previous path as the current path.
        /// </summary>
        public void GoBack()
        {
            if (IsBackAvailable)
                _forwardHistory.Push(_currentHistory.Pop());
        }

        /// <summary>
        /// Goes forward while accessing browser history.
        /// </summary>
        public void GoForward()
        {
            if (IsForwardAvailable)
                _currentHistory.Push(_forwardHistory.Pop());
        }

        /// <summary>
        /// Returns files and directories that reside inside the current directory.
        /// </summary>
        /// <returns>A list of IFileDescriptor objects or null if the browser is not initialized.</returns>
        public List<IFileDescriptor> GetData()
        {
            return IsInitialized
                ? FileUtils.Browse(_scope,
                    ReturnFullInfo ? ObjectInfoProvider.FullObjectInfo : ObjectInfoProvider.ShortObjectInfo,
                    _currentHistory.Peek(), !ShowDirectoriesOnly, FileExtensions)
                : null;
        }

        private void ClearHistory()
        {
            _currentHistory.Clear();
            _forwardHistory.Clear();
        }

        /// <summary>
        /// A method that is used to initialize the browser and establish the connection to the host.
        /// </summary>
        /// <param name="address">The address of the local or remote computer.</param>
        /// <param name="login">Username for login.</param>
        /// <param name="password">Password for login.</param>
        public void ConnectToHost(string address, string login = null, SecureString password = null)
        {
            _scope = WmiUtils.GetConnectedScope(address, @"root\cimv2", login, password);
            ClearHistory();
            _currentHistory.Push(new FilePath());
        }

        private void GoToPathBase(IFilePath path)
        {
            if (!_currentHistory.Any() || _currentHistory.Peek().Equals(path))
                return;

            _currentHistory.Push(path);
            _forwardHistory.Clear();
        }

        /// <summary>
        /// Set the directory as the current path.
        /// </summary>
        /// <param name="path">IFilePath object representing the desired directory.</param>
        public void GoToPath(IFilePath path)
        {
            GoToPathBase(path.Copy());
        }

        /// <summary>
        /// Set the directory as the current path.
        /// </summary>
        /// <param name="path">String representation of the desired directory.</param>
        public void GoToPath(string path)
        {
            GoToPathBase(FilePath.ParsePath(path));
        }

        private void SetInitialPathBase(IFilePath path)
        {
            ClearHistory();
            _currentHistory.Push(path);
        }

        /// <summary>
        /// Clear browser history and set the path as the current directory.
        /// </summary>
        /// <param name="path">IFilePath object representing the desired directory.</param>
        public void SetInitialPath(IFilePath path)
        {
            SetInitialPathBase(path.Copy());
        }

        /// <summary>
        /// Clear browser history and set the path as the current directory.
        /// </summary>
        /// <param name="path">String representation of the desired directory.</param>
        public void SetInitialPath(string path)
        {
            SetInitialPathBase(FilePath.ParsePath(path));
        }

        /// <summary>
        /// Can be used to reconnect to the host if the connection was lost.
        /// </summary>
        public void Reconnect()
        {
            if (!IsInitialized)
                return;

            WmiUtils.DisconnectScope(_scope);
            WmiUtils.ConnectScope(_scope);
        }
    }
}
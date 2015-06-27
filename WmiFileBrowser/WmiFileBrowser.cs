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
    public class WmiFileBrowser
    {
        private ManagementScope _scope;
        private readonly Stack<IFilePath> _currentHistory = new Stack<IFilePath>();
        private readonly Stack<IFilePath> _forwardHistory = new Stack<IFilePath>();

        public bool ReturnFullInfo { get; set; }

        public bool ShowDirectoriesOnly { get; set; }

        public string[] FileExtensions { get; set; }

        public bool IsBackAvailable
        {
            get { return _currentHistory.Count > 1; }
        }

        public bool IsForwardAvailable
        {
            get { return _forwardHistory.Any(); }
        }

        public string CurrentPath
        {
            get { return _currentHistory.Any() ? _currentHistory.Peek().ToString() : null; }
        }

        public bool IsInitialized
        {
            get { return _scope != null; }
        }

        public IFilePath GetCurrentPath()
        {
            return _currentHistory.Any() ? _currentHistory.Peek().Copy() : null;
        }

        public void GoBack()
        {
            if (IsBackAvailable)
                _forwardHistory.Push(_currentHistory.Pop());
        }

        public void GoForward()
        {
            if (IsForwardAvailable)
                _currentHistory.Push(_forwardHistory.Pop());
        }

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

        public void GoToPath(IFilePath path)
        {
            GoToPathBase(path.Copy());
        }

        public void GoToPath(string path)
        {
            GoToPathBase(FilePath.ParsePath(path));
        }

        private void SetInitialPathBase(IFilePath path)
        {
            ClearHistory();
            _currentHistory.Push(path);
        }

        public void SetInitialPath(IFilePath path)
        {
            SetInitialPathBase(path.Copy());
        }

        public void SetInitialPath(string path)
        {
            SetInitialPathBase(FilePath.ParsePath(path));
        }

        public void Reconnect()
        {
            if (!IsInitialized)
                return;

            WmiUtils.DisconnectScope(_scope);
            WmiUtils.ConnectScope(_scope);
        }
    }
}
using System;
using System.Linq;
using System.Windows.Forms;
using WmiFileBrowser.Auxiliary;
using WmiFileBrowser.Interfaces;

namespace TestForm
{
    public partial class BrowserForm : Form
    {
        private readonly WmiFileBrowser.WmiFileBrowser _browser;
        
        public BrowserForm(WmiFileBrowser.WmiFileBrowser browser)
        {
            _browser = browser;
            InitializeComponent();
        }

        private void LoadData()
        {
            listViewFiles.BeginUpdate();

            try
            {
                buttonBack.Enabled = _browser.IsBackAvailable;
                buttonForward.Enabled = _browser.IsForwardAvailable;
                textBoxPath.Text = _browser.CurrentPath;
                listViewFiles.Items.Clear();
                foreach (var file in _browser.GetData())
                {
                    string[] properties;
                    var imageIndex = 0;

                    switch (file.Type)
                    {
                        case ObjectType.File:
                            properties = new[]
                            {
                                (string) file.GetPropertyValue("FileName") + '.' +
                                (string) file.GetPropertyValue("Extension")
                            };
                            imageIndex = 2;
                            break;
                        case ObjectType.Directory:
                            properties = new[] {(string) file.GetPropertyValue("FileName")};
                            imageIndex = 1;
                            break;
                        case ObjectType.Drive:
                            properties = new[] {(string) file.GetPropertyValue("DriveLetter")};
                            imageIndex = 0;
                            break;
                        default:
                            properties = new string[0];
                            break;
                    }
                    var fileCopy = file;
                    listViewFiles.Items.Add(new ListViewItem(properties)
                    {
                        ImageIndex = imageIndex,
                        Tag = file,
                        ToolTipText =
                            string.Join(Environment.NewLine,
                                file.PropertyNames.Select(p => string.Format("{0}: {1}", p, fileCopy.GetPropertyValue(p))))
                    });
                }
            }
            finally
            {
                listViewFiles.EndUpdate();
            }
        }

        private void BrowserForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void listViewFiles_DoubleClick(object sender, EventArgs e)
        {
            if (listViewFiles.SelectedIndices.Count != 1)
                return;

            var selected = (IFileDescriptor) listViewFiles.SelectedItems[0].Tag;
            if (selected.Type == ObjectType.File)
                return;
            _browser.GoToPath((string)selected.GetPropertyValue("Name"));
            LoadData();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (!_browser.IsBackAvailable)
                return;

            _browser.GoBack();
            LoadData();
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            if (!_browser.IsForwardAvailable)
                return;

            _browser.GoForward();
            LoadData();
        }

        private void listViewFiles_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Back:
                    buttonBack_Click(this, EventArgs.Empty);
                    break;
                case (char)Keys.Enter:
                    listViewFiles_DoubleClick(this, EventArgs.Empty);
                    break;
            }
        }

        private void listViewFiles_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.XButton1:
                    buttonBack_Click(this, EventArgs.Empty);
                    break;
                case MouseButtons.XButton2:
                    buttonForward_Click(this, EventArgs.Empty);
                    break;
            }
        }

        private void checkBoxFull_CheckedChanged(object sender, EventArgs e)
        {
            _browser.ReturnFullInfo = checkBoxFull.Checked;
        }
    }
}

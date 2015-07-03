using System;
using System.Security;
using System.Windows.Forms;

namespace TestForm
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxLoginInfo.Enabled = radioButtonRemote.Checked;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            var browser = new WmiFileBrowser.WmiFileBrowser();

            try
            {
                if (radioButtonLocal.Checked)
                    browser.ConnectToHost("localhost");
                else
                {
                    using (var password = new SecureString())
                    {
                        foreach (var character in textBoxPassword.Text)
                            password.AppendChar(character);

                        browser.ConnectToHost(textBoxAddress.Text, textBoxUsername.Text, password);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Connection to host failed." + Environment.NewLine + Environment.NewLine + ex.Message,
                    @"Connection failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            using (var dlg = new BrowserForm(browser))
                dlg.ShowDialog();
        }
    }
}

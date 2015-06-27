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
            groupBox1.Enabled = radioButtonRemote.Checked;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            var browser = new WmiFileBrowser.WmiFileBrowser();

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

            using (var dlg = new BrowserForm(browser))
                dlg.ShowDialog();
        }
    }
}

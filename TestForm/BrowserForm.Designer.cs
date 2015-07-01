namespace TestForm
{
    partial class BrowserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserForm));
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.checkBoxFull = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listViewFiles
            // 
            this.listViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.Location = new System.Drawing.Point(12, 38);
            this.listViewFiles.MultiSelect = false;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.ShowItemToolTips = true;
            this.listViewFiles.Size = new System.Drawing.Size(585, 184);
            this.listViewFiles.SmallImageList = this.imageListIcons;
            this.listViewFiles.TabIndex = 0;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.DoubleClick += new System.EventHandler(this.listViewFiles_DoubleClick);
            this.listViewFiles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listViewFiles_KeyPress);
            this.listViewFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewFiles_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 300;
            // 
            // imageListIcons
            // 
            this.imageListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcons.ImageStream")));
            this.imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIcons.Images.SetKeyName(0, "drive.png");
            this.imageListIcons.Images.SetKeyName(1, "help_folder.png");
            this.imageListIcons.Images.SetKeyName(2, "iconFile.gif");
            this.imageListIcons.Images.SetKeyName(3, "Back.png");
            this.imageListIcons.Images.SetKeyName(4, "Forward.png");
            // 
            // buttonBack
            // 
            this.buttonBack.ImageIndex = 3;
            this.buttonBack.ImageList = this.imageListIcons;
            this.buttonBack.Location = new System.Drawing.Point(12, 9);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(30, 23);
            this.buttonBack.TabIndex = 1;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.ImageIndex = 4;
            this.buttonForward.ImageList = this.imageListIcons;
            this.buttonForward.Location = new System.Drawing.Point(48, 9);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(30, 23);
            this.buttonForward.TabIndex = 2;
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.Location = new System.Drawing.Point(84, 11);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(513, 20);
            this.textBoxPath.TabIndex = 3;
            // 
            // checkBoxFull
            // 
            this.checkBoxFull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxFull.AutoSize = true;
            this.checkBoxFull.Location = new System.Drawing.Point(12, 228);
            this.checkBoxFull.Name = "checkBoxFull";
            this.checkBoxFull.Size = new System.Drawing.Size(79, 17);
            this.checkBoxFull.TabIndex = 4;
            this.checkBoxFull.Text = "Get full info";
            this.checkBoxFull.UseVisualStyleBackColor = true;
            this.checkBoxFull.CheckedChanged += new System.EventHandler(this.checkBoxFull_CheckedChanged);
            // 
            // BrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 253);
            this.Controls.Add(this.checkBoxFull);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.buttonForward);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.listViewFiles);
            this.Name = "BrowserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Browser";
            this.Load += new System.EventHandler(this.BrowserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.ImageList imageListIcons;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.CheckBox checkBoxFull;
    }
}
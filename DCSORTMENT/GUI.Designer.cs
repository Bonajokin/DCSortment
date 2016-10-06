namespace DCSortment
{
    partial class GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.spreadsheetText = new System.Windows.Forms.Label();
            this.xLSpreadsheetLocation = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sortingMethods = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchingTag = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.replacingTag = new System.Windows.Forms.TextBox();
            this.run = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.opStatusName = new System.Windows.Forms.ToolStripStatusLabel();
            this.opProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.sortModeLabel = new System.Windows.Forms.Label();
            this.sortmentStatus = new System.Windows.Forms.Label();
            this.filesLocation = new System.Windows.Forms.TextBox();
            this.fileLocationLabel = new System.Windows.Forms.Label();
            this.fileBrowse = new System.Windows.Forms.Button();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // spreadsheetText
            // 
            this.spreadsheetText.AutoSize = true;
            this.spreadsheetText.Location = new System.Drawing.Point(13, 13);
            this.spreadsheetText.Name = "spreadsheetText";
            this.spreadsheetText.Size = new System.Drawing.Size(130, 13);
            this.spreadsheetText.TabIndex = 0;
            this.spreadsheetText.Text = "Spreadsheet File Location";
            // 
            // xLSpreadsheetLocation
            // 
            this.xLSpreadsheetLocation.Location = new System.Drawing.Point(16, 29);
            this.xLSpreadsheetLocation.Name = "xLSpreadsheetLocation";
            this.xLSpreadsheetLocation.Size = new System.Drawing.Size(581, 20);
            this.xLSpreadsheetLocation.TabIndex = 1;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(466, 56);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(131, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sortment Methods";
            // 
            // sortingMethods
            // 
            this.sortingMethods.FormattingEnabled = true;
            this.sortingMethods.Items.AddRange(new object[] {
            "Weighted Alphabet",
            "Preordered Dataset",
            "Double Rating"});
            this.sortingMethods.Location = new System.Drawing.Point(19, 133);
            this.sortingMethods.Name = "sortingMethods";
            this.sortingMethods.Size = new System.Drawing.Size(124, 82);
            this.sortingMethods.TabIndex = 4;
            this.sortingMethods.SelectedIndexChanged += new System.EventHandler(this.sortingMethods_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tag Definitions";
            // 
            // searchingTag
            // 
            this.searchingTag.Location = new System.Drawing.Point(264, 140);
            this.searchingTag.Name = "searchingTag";
            this.searchingTag.Size = new System.Drawing.Size(136, 20);
            this.searchingTag.TabIndex = 6;
            this.searchingTag.TextChanged += new System.EventHandler(this.searchingTag_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Searching/Prefix 1:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Replacing/Prefix 2:";
            // 
            // replacingTag
            // 
            this.replacingTag.Location = new System.Drawing.Point(264, 175);
            this.replacingTag.Name = "replacingTag";
            this.replacingTag.Size = new System.Drawing.Size(136, 20);
            this.replacingTag.TabIndex = 9;
            this.replacingTag.TextChanged += new System.EventHandler(this.replacingTag_TextChanged);
            // 
            // run
            // 
            this.run.Location = new System.Drawing.Point(464, 192);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(133, 23);
            this.run.TabIndex = 10;
            this.run.Text = "Run";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opStatusName,
            this.opProgress});
            this.statusBar.Location = new System.Drawing.Point(0, 227);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(611, 22);
            this.statusBar.TabIndex = 11;
            this.statusBar.Text = "statusStrip1";
            // 
            // opStatusName
            // 
            this.opStatusName.Name = "opStatusName";
            this.opStatusName.Size = new System.Drawing.Size(0, 17);
            // 
            // opProgress
            // 
            this.opProgress.Name = "opProgress";
            this.opProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // sortModeLabel
            // 
            this.sortModeLabel.AutoSize = true;
            this.sortModeLabel.Location = new System.Drawing.Point(380, 233);
            this.sortModeLabel.Name = "sortModeLabel";
            this.sortModeLabel.Size = new System.Drawing.Size(82, 13);
            this.sortModeLabel.TabIndex = 12;
            this.sortModeLabel.Text = "Sortment Mode:";
            // 
            // sortmentStatus
            // 
            this.sortmentStatus.AutoSize = true;
            this.sortmentStatus.Location = new System.Drawing.Point(462, 233);
            this.sortmentStatus.Name = "sortmentStatus";
            this.sortmentStatus.Size = new System.Drawing.Size(0, 13);
            this.sortmentStatus.TabIndex = 13;
            // 
            // filesLocation
            // 
            this.filesLocation.Location = new System.Drawing.Point(17, 85);
            this.filesLocation.Name = "filesLocation";
            this.filesLocation.Size = new System.Drawing.Size(580, 20);
            this.filesLocation.TabIndex = 15;
            // 
            // fileLocationLabel
            // 
            this.fileLocationLabel.AutoSize = true;
            this.fileLocationLabel.Location = new System.Drawing.Point(14, 69);
            this.fileLocationLabel.Name = "fileLocationLabel";
            this.fileLocationLabel.Size = new System.Drawing.Size(72, 13);
            this.fileLocationLabel.TabIndex = 14;
            this.fileLocationLabel.Text = "Files Location";
            // 
            // fileBrowse
            // 
            this.fileBrowse.Location = new System.Drawing.Point(466, 116);
            this.fileBrowse.Name = "fileBrowse";
            this.fileBrowse.Size = new System.Drawing.Size(131, 23);
            this.fileBrowse.TabIndex = 16;
            this.fileBrowse.Text = "Browse";
            this.fileBrowse.UseVisualStyleBackColor = true;
            this.fileBrowse.Click += new System.EventHandler(this.fileBrowse_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 249);
            this.Controls.Add(this.fileBrowse);
            this.Controls.Add(this.filesLocation);
            this.Controls.Add(this.fileLocationLabel);
            this.Controls.Add(this.sortmentStatus);
            this.Controls.Add(this.sortModeLabel);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.run);
            this.Controls.Add(this.replacingTag);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchingTag);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sortingMethods);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.xLSpreadsheetLocation);
            this.Controls.Add(this.spreadsheetText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI";
            this.ShowIcon = false;
            this.Text = "DCSortment";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label spreadsheetText;
        private System.Windows.Forms.TextBox xLSpreadsheetLocation;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox sortingMethods;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox searchingTag;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox replacingTag;
        private System.Windows.Forms.Button run;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel opStatusName;
        private System.Windows.Forms.ToolStripProgressBar opProgress;
        private System.Windows.Forms.Label sortModeLabel;
        private System.Windows.Forms.Label sortmentStatus;
        private System.Windows.Forms.TextBox filesLocation;
        private System.Windows.Forms.Label fileLocationLabel;
        private System.Windows.Forms.Button fileBrowse;
    }
}
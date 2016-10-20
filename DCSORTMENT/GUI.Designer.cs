/*
MIT License

Copyright(c) 2016 Otis Bailey

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
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
            this.SortmentMethodsLabel = new System.Windows.Forms.Label();
            this.sortingMethods = new System.Windows.Forms.ListBox();
            this.TagDefinitionsLabel = new System.Windows.Forms.Label();
            this.prefix1 = new System.Windows.Forms.TextBox();
            this.prefix1Label = new System.Windows.Forms.Label();
            this.prefix2Label = new System.Windows.Forms.Label();
            this.prefix2 = new System.Windows.Forms.TextBox();
            this.run = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.opStatusName = new System.Windows.Forms.ToolStripStatusLabel();
            this.opProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.sortModeLabel = new System.Windows.Forms.Label();
            this.sortmentStatus = new System.Windows.Forms.Label();
            this.filesLocation = new System.Windows.Forms.TextBox();
            this.fileLocationLabel = new System.Windows.Forms.Label();
            this.fileBrowse = new System.Windows.Forms.Button();
            this.doubleRatingLB = new System.Windows.Forms.ListBox();
            this.initialLB = new System.Windows.Forms.ListBox();
            this.searchingTagBox = new System.Windows.Forms.TextBox();
            this.searchingTagLabel = new System.Windows.Forms.Label();
            this.replacingTagBox = new System.Windows.Forms.TextBox();
            this.replacingTagLabel = new System.Windows.Forms.Label();
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
            this.xLSpreadsheetLocation.TextChanged += new System.EventHandler(this.xLSpreadsheetLocation_TextChanged);
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
            // SortmentMethodsLabel
            // 
            this.SortmentMethodsLabel.AutoSize = true;
            this.SortmentMethodsLabel.Location = new System.Drawing.Point(16, 116);
            this.SortmentMethodsLabel.Name = "SortmentMethodsLabel";
            this.SortmentMethodsLabel.Size = new System.Drawing.Size(93, 13);
            this.SortmentMethodsLabel.TabIndex = 3;
            this.SortmentMethodsLabel.Text = "Sortment Methods";
            // 
            // sortingMethods
            // 
            this.sortingMethods.FormattingEnabled = true;
            this.sortingMethods.Items.AddRange(new object[] {
            "Weighted Alphabet",
            "Preordered Dataset"});
            this.sortingMethods.Location = new System.Drawing.Point(19, 133);
            this.sortingMethods.Name = "sortingMethods";
            this.sortingMethods.Size = new System.Drawing.Size(124, 121);
            this.sortingMethods.TabIndex = 4;
            this.sortingMethods.SelectedIndexChanged += new System.EventHandler(this.sortingMethods_SelectedIndexChanged);
            // 
            // TagDefinitionsLabel
            // 
            this.TagDefinitionsLabel.AutoSize = true;
            this.TagDefinitionsLabel.Location = new System.Drawing.Point(163, 116);
            this.TagDefinitionsLabel.Name = "TagDefinitionsLabel";
            this.TagDefinitionsLabel.Size = new System.Drawing.Size(78, 13);
            this.TagDefinitionsLabel.TabIndex = 5;
            this.TagDefinitionsLabel.Text = "Tag Definitions";
            // 
            // prefix1
            // 
            this.prefix1.Location = new System.Drawing.Point(264, 140);
            this.prefix1.Name = "prefix1";
            this.prefix1.Size = new System.Drawing.Size(136, 20);
            this.prefix1.TabIndex = 6;
            this.prefix1.TextChanged += new System.EventHandler(this.searchingTag_TextChanged);
            // 
            // prefix1Label
            // 
            this.prefix1Label.AutoSize = true;
            this.prefix1Label.Location = new System.Drawing.Point(163, 143);
            this.prefix1Label.Name = "prefix1Label";
            this.prefix1Label.Size = new System.Drawing.Size(80, 13);
            this.prefix1Label.TabIndex = 7;
            this.prefix1Label.Text = "Searching Tag:";
            // 
            // prefix2Label
            // 
            this.prefix2Label.AutoSize = true;
            this.prefix2Label.Location = new System.Drawing.Point(163, 174);
            this.prefix2Label.Name = "prefix2Label";
            this.prefix2Label.Size = new System.Drawing.Size(80, 13);
            this.prefix2Label.TabIndex = 8;
            this.prefix2Label.Text = "Replacing Tag:";
            // 
            // prefix2
            // 
            this.prefix2.Location = new System.Drawing.Point(264, 171);
            this.prefix2.Name = "prefix2";
            this.prefix2.Size = new System.Drawing.Size(136, 20);
            this.prefix2.TabIndex = 9;
            this.prefix2.TextChanged += new System.EventHandler(this.replacingTag_TextChanged);
            // 
            // run
            // 
            this.run.Location = new System.Drawing.Point(464, 231);
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
            this.statusBar.Location = new System.Drawing.Point(0, 257);
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
            this.sortModeLabel.Location = new System.Drawing.Point(380, 263);
            this.sortModeLabel.Name = "sortModeLabel";
            this.sortModeLabel.Size = new System.Drawing.Size(82, 13);
            this.sortModeLabel.TabIndex = 12;
            this.sortModeLabel.Text = "Sortment Mode:";
            // 
            // sortmentStatus
            // 
            this.sortmentStatus.AutoSize = true;
            this.sortmentStatus.Location = new System.Drawing.Point(462, 263);
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
            // doubleRatingLB
            // 
            this.doubleRatingLB.FormattingEnabled = true;
            this.doubleRatingLB.Items.AddRange(new object[] {
            "Double Rating"});
            this.doubleRatingLB.Location = new System.Drawing.Point(19, 132);
            this.doubleRatingLB.Name = "doubleRatingLB";
            this.doubleRatingLB.Size = new System.Drawing.Size(124, 121);
            this.doubleRatingLB.TabIndex = 17;
            this.doubleRatingLB.SelectedIndexChanged += new System.EventHandler(this.doubleRatingLB_SelectedIndexChanged);
            // 
            // initialLB
            // 
            this.initialLB.FormattingEnabled = true;
            this.initialLB.Location = new System.Drawing.Point(19, 132);
            this.initialLB.Name = "initialLB";
            this.initialLB.Size = new System.Drawing.Size(124, 121);
            this.initialLB.TabIndex = 18;
            // 
            // searchingTagBox
            // 
            this.searchingTagBox.Location = new System.Drawing.Point(264, 201);
            this.searchingTagBox.Name = "searchingTagBox";
            this.searchingTagBox.Size = new System.Drawing.Size(136, 20);
            this.searchingTagBox.TabIndex = 19;
            this.searchingTagBox.TextChanged += new System.EventHandler(this.doubleRatingSearchingTag_TextChanged);
            // 
            // searchingTagLabel
            // 
            this.searchingTagLabel.AutoSize = true;
            this.searchingTagLabel.Location = new System.Drawing.Point(163, 204);
            this.searchingTagLabel.Name = "searchingTagLabel";
            this.searchingTagLabel.Size = new System.Drawing.Size(80, 13);
            this.searchingTagLabel.TabIndex = 20;
            this.searchingTagLabel.Text = "Searching Tag:";
            // 
            // replacingTagBox
            // 
            this.replacingTagBox.Location = new System.Drawing.Point(264, 231);
            this.replacingTagBox.Name = "replacingTagBox";
            this.replacingTagBox.Size = new System.Drawing.Size(136, 20);
            this.replacingTagBox.TabIndex = 21;
            this.replacingTagBox.TextChanged += new System.EventHandler(this.replacingTagBox_TextChanged);
            // 
            // replacingTagLabel
            // 
            this.replacingTagLabel.AutoSize = true;
            this.replacingTagLabel.Location = new System.Drawing.Point(163, 234);
            this.replacingTagLabel.Name = "replacingTagLabel";
            this.replacingTagLabel.Size = new System.Drawing.Size(80, 13);
            this.replacingTagLabel.TabIndex = 22;
            this.replacingTagLabel.Text = "Replacing Tag:";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 279);
            this.Controls.Add(this.replacingTagLabel);
            this.Controls.Add(this.replacingTagBox);
            this.Controls.Add(this.searchingTagLabel);
            this.Controls.Add(this.searchingTagBox);
            this.Controls.Add(this.initialLB);
            this.Controls.Add(this.doubleRatingLB);
            this.Controls.Add(this.fileBrowse);
            this.Controls.Add(this.filesLocation);
            this.Controls.Add(this.fileLocationLabel);
            this.Controls.Add(this.sortmentStatus);
            this.Controls.Add(this.sortModeLabel);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.run);
            this.Controls.Add(this.prefix2);
            this.Controls.Add(this.prefix2Label);
            this.Controls.Add(this.prefix1Label);
            this.Controls.Add(this.prefix1);
            this.Controls.Add(this.TagDefinitionsLabel);
            this.Controls.Add(this.sortingMethods);
            this.Controls.Add(this.SortmentMethodsLabel);
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
        private System.Windows.Forms.Label SortmentMethodsLabel;
        private System.Windows.Forms.ListBox sortingMethods;
        private System.Windows.Forms.Label TagDefinitionsLabel;
        private System.Windows.Forms.TextBox prefix1;
        private System.Windows.Forms.Label prefix1Label;
        private System.Windows.Forms.Label prefix2Label;
        private System.Windows.Forms.TextBox prefix2;
        private System.Windows.Forms.Button run;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel opStatusName;
        private System.Windows.Forms.ToolStripProgressBar opProgress;
        private System.Windows.Forms.Label sortModeLabel;
        private System.Windows.Forms.Label sortmentStatus;
        private System.Windows.Forms.TextBox filesLocation;
        private System.Windows.Forms.Label fileLocationLabel;
        private System.Windows.Forms.Button fileBrowse;
        private System.Windows.Forms.ListBox doubleRatingLB;
        private System.Windows.Forms.ListBox initialLB;
        private System.Windows.Forms.TextBox searchingTagBox;
        private System.Windows.Forms.Label searchingTagLabel;
        private System.Windows.Forms.TextBox replacingTagBox;
        private System.Windows.Forms.Label replacingTagLabel;
    }
}
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DCSortment
{
    public partial class GUI : Form, IGuiUpdate
    {

   

        IGuiUpdate IGui;

        //GUI Updating Interface Implementations
        private List<House> houses;
        public List<House> Houses
        {
            get { return houses; }
            set { this.Invoke((MethodInvoker)delegate { houses = value; }); }

        }

        private List<House> sortedHouseList;
        public List<House> SortedHouseList
        {
            get { return sortedHouseList; }
            set { this.Invoke((MethodInvoker)delegate { sortedHouseList = value; }); }

        }

        private List<House> secondRatingList;
        public List<House> SecondRatingList
        {
            get { return secondRatingList; }
            set { this.Invoke((MethodInvoker)delegate { secondRatingList = value; }); }

        }

        public string xLSpreadSheetText
        {
            get { return xLSpreadsheetLocation.Text; }
            set { this.Invoke((MethodInvoker)delegate { xLSpreadsheetLocation.Text = value; }); }
        }

        public string _SMsearchingTag {
            get {return prefix1.Text; }
            set { this.Invoke((MethodInvoker)delegate { prefix1.Text = value; }); }
        }

        private string dataSetName;
        public string DataSetName {
            get { return dataSetName; }
            set { dataSetName = value; }
        }

        public string _SMreplacingTag
        {
            get {return prefix2.Text; }
            set { this.Invoke((MethodInvoker)delegate { prefix2.Text = value; }); }
        }

        public string _DRsearchingTag
        {
            get {return searchingTagBox.Text; }
            set { this.Invoke((MethodInvoker)delegate { searchingTagBox.Text = value; }); }
        }

        public string _DRreplacingTag
        {
            get {return replacingTagBox.Text; }
            set { this.Invoke((MethodInvoker)delegate { replacingTagBox.Text = value; }); }
        }

        private bool doubleRatingMode;
        public bool DoubleRatingMode
        {
            get {return doubleRatingMode; }
            set {doubleRatingMode = value; }
        }

        private bool doubleRatingEnabled;
        public bool DoubleRatingEnabled
        {
            get {return doubleRatingEnabled; }
            set {doubleRatingEnabled = value; }
        }

        private bool standardModeEnabled;
        public bool StandardModeEnabled
        {
            get {return standardModeEnabled;}
            set {standardModeEnabled = value; }
        }

        public string FilesLocation
        {
            get { return filesLocation.Text; }
            set { this.Invoke((MethodInvoker)delegate { filesLocation.Text = value; }); }
        }

        public string StatusBarText
        {
            get { return opStatusName.Text; }
            set { this.Invoke((MethodInvoker)delegate { opStatusName.Text = value; }); }
        }

        public bool ProgressBarVisible
        {
            get { return opProgress.Visible; }
            set { this.Invoke((MethodInvoker)delegate { opProgress.Visible = value; }); }
        }

        public int ProgressBarValue
        {
            get { return opProgress.Value; }
            set { this.Invoke((MethodInvoker)delegate { opProgress.Value = value; }); }
        }

        // End GUI Update Interface Implementations




        public GUI()
        {
            InitializeComponent();
            IGui = this;
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            opProgress.Visible = false;
            sortModeLabel.Visible = false;
            doubleRatingLB.Visible = false;
            doubleRatingLB.Enabled = false;
            sortingMethods.Visible = false;
            sortingMethods.Enabled = false;
            searchingTagLabel.Visible = false;
            replacingTagLabel.Visible = false;
            searchingTagBox.Visible = false;
            searchingTagBox.Enabled = false;
            replacingTagBox.Visible = false;
            replacingTagBox.Visible = false;
        }


        private void fileBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult dr = fbd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                filesLocation.Text = fbd.SelectedPath;
                IGui.FilesLocation = filesLocation.Text;
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Microsoft Excel Spreedsheet File (.xlsx)| *.xlsx";
            System.Windows.Forms.DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                IGui.DataSetName = ofd.FileName;
                Workers worker = new Workers(IGui);
                worker.runXLWorker();
               
            }

       
        }

        private void searchingTag_TextChanged(object sender, EventArgs e)
        {
            IGui._SMsearchingTag = prefix1.Text;
        }

        private void replacingTag_TextChanged(object sender, EventArgs e)
        {
            IGui._SMreplacingTag = prefix2.Text;
        }

        private void doubleRatingSearchingTag_TextChanged(object sender, EventArgs e)
        {
            IGui._DRsearchingTag = searchingTagBox.Text;
        }

        private void replacingTagBox_TextChanged(object sender, EventArgs e)
        {
           IGui._DRreplacingTag = replacingTagBox.Text;
        }

        private void run_Click(object sender, EventArgs e)
        {
            
                if (allowRun())
                {
                    Workers worker = new Workers(IGui);
                    worker.runRenameWorker();
                }           
                     

        }

        private void xLSpreadsheetLocation_TextChanged(object sender, EventArgs e)
        {
            doubleRatingLB.ClearSelected();
            sortingMethods.ClearSelected();

            if (IGui.StandardModeEnabled)
            {
                prefix1Label.Text = "Searching Tag:";
                prefix2Label.Text = "Replacing Tag:";
                doubleRatingLB.Visible = false;
                doubleRatingLB.Enabled = false;
                sortingMethods.Visible = true;
                sortingMethods.Enabled = true;
                initialLB.Visible = false;
                initialLB.Enabled = false;
                sortmentStatus.Visible = false;
                sortModeLabel.Visible = false;
                prefix1.Clear();
                prefix2.Clear();
                searchingTagBox.Clear();
                replacingTagBox.Clear();
                searchingTagLabel.Visible = false;
                replacingTagLabel.Visible = false;
                searchingTagBox.Visible = false;
                searchingTagBox.Enabled = false;
                replacingTagBox.Visible = false;
                replacingTagBox.Visible = false;

            }
            else {
                prefix1Label.Text = "Prefix 1:";
                prefix2Label.Text = "Prefix 2:"; 
                doubleRatingLB.Visible = true;
                doubleRatingLB.Enabled = true;
                sortingMethods.Visible = false;
                sortingMethods.Enabled = false;
                initialLB.Visible = false;
                initialLB.Enabled = false;
                sortmentStatus.Visible = false;
                sortModeLabel.Visible = false;
                searchingTagLabel.Visible = true;
                replacingTagLabel.Visible = true;
                prefix1.Clear();
                prefix2.Clear();
                searchingTagBox.Clear();
                replacingTagBox.Clear();
                searchingTagBox.Visible = true;
                searchingTagBox.Enabled = true;
                replacingTagBox.Visible = true;
                replacingTagBox.Visible = true;
            }
        }

        private void doubleRatingLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (doubleRatingLB.SelectedIndex)
            {
                case 0:
                    {
                        IGui.SortedHouseList = houses.OrderByDescending(house => house.rating[0]).ThenBy(house => house.houseName).ToList();
                        IGui.SecondRatingList = houses.OrderByDescending(house => house.rating[1]).ThenBy(house => house.houseName).ToList();
                        opStatusName.Text = "";
                        opProgress.Visible = false;
                        searchingTagLabel.Visible = true;
                        replacingTagLabel.Visible = true;
                        searchingTagBox.Visible = true;
                        searchingTagBox.Enabled = true;
                        replacingTagBox.Visible = true;
                        replacingTagBox.Visible = true;
                        sortModeLabel.Visible = true;
                        sortmentStatus.Visible = true;
                        IGui.DoubleRatingMode = true;
                        sortmentStatus.Text = "Double Rating";
                        break;
                    }
            }
     

        }


        private void sortingMethods_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!IGui.DoubleRatingEnabled)
            {

                switch (sortingMethods.SelectedIndex)
                {

                    case 0:
                        {
                            IGui.SortedHouseList = houses.OrderByDescending(house => house.rating[0]).ThenBy(house => house.houseName).ToList();
                            opStatusName.Text = "";
                            opProgress.Visible = false;
                            sortModeLabel.Visible = true;
                            sortmentStatus.Visible = true;
                            searchingTagLabel.Visible = false;
                            replacingTagLabel.Visible = false;
                            searchingTagBox.Visible = false;
                            searchingTagBox.Enabled = false;
                            replacingTagBox.Visible = false;
                            replacingTagBox.Visible = false;
                            IGui.DoubleRatingMode = false;
                            sortmentStatus.Text = "Weighted Alphabet";
                            break;
                        }

                    case 1:
                        {
                            IGui.SortedHouseList = houses;
                            opStatusName.Text = "";
                            opProgress.Visible = false;
                            searchingTagLabel.Visible = false;
                            replacingTagLabel.Visible = false;
                            searchingTagBox.Visible = false;
                            searchingTagBox.Enabled = false;
                            replacingTagBox.Visible = false;
                            replacingTagBox.Visible = false;
                            sortModeLabel.Visible = true;
                            sortmentStatus.Visible = true;
                            IGui.DoubleRatingMode = false;
                            sortmentStatus.Text = "Preordered Dataset";
                            break;
                        }

                }
            } 



        }


        private bool allowRun()
        {
            switch (standardModeEnabled)
            {
                //If standard mode was enabled make sure an index was selected in its list box and ensure all tag definitons were filled out if everything checks out allow the run if not don't allow it.
                case true:
                    {
                        if (sortingMethods.SelectedItems.Count < 1)
                        {
                            opStatusName.Text = "Error: Please select an item from sortment methods before running!!";
                            opProgress.Visible = false;
                            return false;
                        }
                        else if(prefix1.Text == "")
                        {
                            opStatusName.Text = "Error: Please ensure all tag definitions are completed before running!!";
                            opProgress.Visible = false;
                            return false;
                        }
                        else if (prefix2.Text == "")
                        {
                            opStatusName.Text = "Error: Please ensure all tag definitions are completed before running!!";
                            opProgress.Visible = false;
                            return false;
                        } 
                        else {
                            return true;
                        }
                    }

                //If standard mode was not enabled we goto double rating mode make sure an index was selected in its list box and ensure all tag definitons were filled out if everything checks out allow the run if not don't allow it.
                case false:
                    {
                        if (doubleRatingLB.SelectedItems.Count < 1)
                        {
                            opStatusName.Text = "Error: Please select an item from sortment methods before running!!";
                            opProgress.Visible = false;
                            return false;
                        }
                        else if (prefix1.Text == "" || prefix2.Text == "")
                        {
                            opStatusName.Text = "Error: Please ensure all tag definitions are completed before running!!";
                            opProgress.Visible = false;
                            return false;
                        }
                        else if (_DRsearchingTag == "" || _DRreplacingTag == "")
                        {
                            opStatusName.Text = "Error: Please ensure all tag definitions are completed before running!!";
                            opProgress.Visible = false;
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                       
                    }
            }

            //By default we should never get here but just incase we do don't allow the run to preserve potential damage to files
            return false;
        }

    }

    

 interface IGuiUpdate{

        string FilesLocation { get; set; }
        string DataSetName { get; set; }
        string xLSpreadSheetText { get; set; }
        string StatusBarText { get; set; }
        int ProgressBarValue { get; set; }
        bool ProgressBarVisible { get; set; }
        string _SMsearchingTag { get; set; }
        string _SMreplacingTag { get; set; }
        string _DRsearchingTag { get; set; }
        string _DRreplacingTag { get; set; }

        bool DoubleRatingMode { get; set; }
        bool DoubleRatingEnabled { get; set; }
        bool StandardModeEnabled { get; set; }

        List<House> Houses { get; set; }
        List<House> SecondRatingList { get; set; }
        List<House> SortedHouseList { get; set; }
    }


}

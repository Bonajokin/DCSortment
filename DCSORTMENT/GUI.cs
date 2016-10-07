using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace DCSortment
{
    public partial class GUI : Form
    {
        string dataSetName;
        string userDefTag;
        string userChngTag;
        string _namingUpperPosition = "AA";
        string _namingUpperPositionR2 = "AA";
        string _namingLowerPosition = "aa";
        string _namingLowerPositionR2 = "aa";

        bool doubleRatingMode;
        bool doubleRatingEnabled;
        bool standardModeEnabled;

        //List Variables
        List<House> houses = new List<House>();
        List<House> secondRatingList = new List<House>();
        List<House> SortedHouseList = null;
        List<string> fileNames;
        List<string> cleanFileNames = new List<string>();


        public GUI()
        {
            InitializeComponent();
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            opProgress.Visible = false;
            sortModeLabel.Visible = false;
            doubleRatingLB.Visible = false;
            doubleRatingLB.Enabled = false;
            sortingMethods.Visible = false;
            sortingMethods.Enabled = false;


        }

        private void fileBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult dr = fbd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                filesLocation.Text = fbd.SelectedPath;
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Microsoft Excel Spreedsheet File (.xlsx)| *.xlsx";
            System.Windows.Forms.DialogResult dr = ofd.ShowDialog();
            dataSetName = ofd.FileName;
            if (dr == DialogResult.OK)
            {
                opStatusName.Text = "Parsing Spreadsheet: ";

                BackgroundWorker w = new BackgroundWorker();
                w.WorkerSupportsCancellation = true;
                w.WorkerReportsProgress = true;
                w.DoWork += XlSSworker_DoWork;
                w.ProgressChanged += XlSSworker_ProgressChanged;
                w.RunWorkerCompleted += XlSSworker_RunWorkerCompleted;
                w.RunWorkerAsync();
               
            }

       
        }

        private void searchingTag_TextChanged(object sender, EventArgs e)
        {
            userDefTag = searchingTag.Text;
        }
     

        private void replacingTag_TextChanged(object sender, EventArgs e)
        {
            userChngTag = replacingTag.Text;  
        }

        private void run_Click(object sender, EventArgs e)
        {
            opStatusName.Text = "Renaming Files: ";
            try
            {
                fileNames = Directory.GetFiles(filesLocation.Text).ToList();
                BackgroundWorker w = new BackgroundWorker();
                w.WorkerSupportsCancellation = true;
                w.WorkerReportsProgress = true;
                w.DoWork += renameWorker_DoWork;
                w.RunWorkerCompleted += renameWorker_RunWorkerCompleted;
                w.ProgressChanged += renameWorker_ProgressChanged;
                w.RunWorkerAsync();
            }
            catch (DirectoryNotFoundException)
            {

                MessageBox.Show("Files Directory not found");

            }


        }

        private void xLSpreadsheetLocation_TextChanged(object sender, EventArgs e)
        {
            if (standardModeEnabled)
            {
                doubleRatingLB.Visible = false;
                doubleRatingLB.Enabled = false;
                sortingMethods.Visible = true;
                sortingMethods.Enabled = true;
                initialLB.Visible = false;
                initialLB.Enabled = false;
                sortmentStatus.Visible = false;
                sortModeLabel.Visible = false;

            }
            else {

                doubleRatingLB.Visible = true;
                doubleRatingLB.Enabled = true;
                sortingMethods.Visible = false;
                sortingMethods.Enabled = false;
                initialLB.Visible = false;
                initialLB.Enabled = false;
                sortmentStatus.Visible = false;
                sortModeLabel.Visible = false;

            }
        }

        private void doubleRatingLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            _namingUpperPosition = "AA";
            _namingUpperPositionR2 = "AA";
            _namingLowerPosition = "aa";
            _namingLowerPositionR2 = "aa";

       
            SortedHouseList = houses.OrderByDescending(house => house.rating[0]).ThenBy(house => house.houseName).ToList();
            secondRatingList = houses.OrderByDescending(house => house.rating[1]).ThenBy(house => house.houseName).ToList();
            sortModeLabel.Visible = true;
            sortmentStatus.Visible = true;
            doubleRatingMode = true;
            sortmentStatus.Text = "Double Rating";

        }


        private void sortingMethods_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!doubleRatingEnabled)
            {

                switch (sortingMethods.SelectedIndex)
                {

                    case 0:
                        {

                            _namingUpperPosition = "AA";
                            _namingUpperPositionR2 = "AA";
                            _namingLowerPosition = "aa";
                            _namingLowerPositionR2 = "aa";
                           
                          
                            SortedHouseList = houses.OrderByDescending(house => house.rating[0]).ThenBy(house => house.houseName).ToList();
                            sortModeLabel.Visible = true;
                            sortmentStatus.Visible = true;
                            doubleRatingMode = false;
                            sortmentStatus.Text = "Weighted Alphabet";
                            break;
                        }

                    case 1:
                        {
                            _namingUpperPosition = "AA";
                            _namingUpperPositionR2 = "AA";
                            _namingLowerPosition = "aa";
                            _namingLowerPositionR2 = "aa";

                          
                            SortedHouseList = houses;
                            sortModeLabel.Visible = true;
                            sortmentStatus.Visible = true;
                            doubleRatingMode = false;
                            sortmentStatus.Text = "Preordered Dataset";
                            break;
                        }

                }
            } 



        }

        //Helping Methods and Classes

        //Method that controls the naming convention incrementation.
        public static string incrementNamingConvention(string theString, bool isUpper)
        {
            char[] theCharString = theString.ToCharArray();

            switch (isUpper)
            {

                case true:
                    {
                        if (((int)theCharString[1] + 1) > 90)
                        {
                            return ((char)((int)theCharString[0] + 1)).ToString() + ((char)(65)).ToString();
                        }
                        else
                        {
                            return theCharString[0].ToString() + ((char)((int)theCharString[1] + 1)).ToString();
                        }

                    }
                case false:
                    {
                        if (((int)theCharString[1] + 1) > 122)
                        {
                            return ((char)((int)theCharString[0] + 1)).ToString() + ((char)(97)).ToString();
                        }
                        else
                        {
                            return theCharString[0].ToString() + ((char)((int)theCharString[1] + 1)).ToString();
                        }

                    }

            }

            return null;

        }





        //Background Worker Section

        private void renameWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            opProgress.Visible = true;
            opProgress.Value = e.ProgressPercentage;
        }

        private void renameWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if ((e.Cancelled == true))
            {
                opStatusName.Text = "Error: The file selected is not an Excel Spreadsheet or it is corrupted please try again.";
            }

            else if (!(e.Error == null))
            {
                opStatusName.Text = "Error: The file selected is not an Excel Spreadsheet or it is corrupted please try again.";
            }

            else
            {
                opStatusName.Text += "Done!";

            }
        }

        private void renameWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            // Get the list of files in the directory that need to be renamed and prepare the filenames to be cleaned.
            fileNames = Directory.GetFiles(filesLocation.Text).ToList();
            string completedDirectory = filesLocation.Text;
            completedDirectory = Regex.Replace(filesLocation.Text, @"\\", ".") + "\\\\" ;

            //Go through each file name and remove the complete file directory leaving only the name
            foreach (string filename in fileNames)
            {
                string[] splitName = Regex.Split(filename, @completedDirectory);
                cleanFileNames.Add(splitName[1]);
            }

            //File rename variables
            House currentHouse;
            int indexOfHouseFile;
            string renameName;
            string[] fileExt;


            switch (doubleRatingMode)
            {
                case true:
                    {


                        List<string> renameNames = new List<string>();
                        List<string> cleanFileNameCopy = cleanFileNames.ToList();
                        List<string> FinalFileNames = cleanFileNames.ToList();

                        int totalEntries;
                        totalEntries = cleanFileNames.Count + cleanFileNameCopy.Count + FinalFileNames.Count;

                        decimal increasingPercent = totalEntries * (decimal)0.10;
                        decimal currentWorkCompleted = 0;




                        //Dictionary Stuff
                        Dictionary<string, int> houseIndex = new Dictionary<string, int>();
                        int i = 0;

                        SortedHouseList.ForEach(x => houseIndex.Add(x.houseName, i++));



                        //For every house name in the sorted house list thats already in order
                        foreach (House name in SortedHouseList)
                        {

                            {

                                //Find the index of the current file thats in the filelist
                                currentHouse = name;
                                indexOfHouseFile = cleanFileNames.FindIndex(x => x.Contains(currentHouse.houseName));

                                //While the filelist actually has instances of that filename 
                                while (cleanFileNames.Exists(x => x.Contains(currentHouse.houseName)))
                                {

                                    //Make sure theres a real index found
                                    if (indexOfHouseFile != -1)
                                    {
                                        //If the file contains "NEW" and it contains the current house name
                                        if (cleanFileNames[indexOfHouseFile].Contains(currentHouse.houseName))
                                        {
                                            //Determine the appropriate rename name and then rename the file
                                            if (currentHouse.rating[0] == 0.00)
                                            {
                                                renameName = userDefTag + _namingUpperPosition + "_" + "UNKNOWN";
                                            }
                                            else
                                            {
                                                renameName = userDefTag + _namingUpperPosition + "_" + currentHouse.rating[0].ToString("0.00");
                                            }

                                            _namingUpperPosition = incrementNamingConvention(_namingUpperPosition, true);
                                            renameNames.Add(renameName);
                                        }


                                        //Once we've found and rename the file we can remove it from the list and then read in the next file
                                        cleanFileNames.RemoveAt(indexOfHouseFile);

                                        currentWorkCompleted += increasingPercent;
                                        if (((currentWorkCompleted / totalEntries) * 100) <= 100)
                                        {
                                            worker.ReportProgress(Convert.ToInt32((currentWorkCompleted / totalEntries) * 100));
                                        }

                                    }
                                }
                            }
                        }

                        foreach (House name in secondRatingList)
                        {
                            {

                                //Find the index of the current file thats in the filelist
                                currentHouse = name;
                                indexOfHouseFile = cleanFileNameCopy.FindIndex(x => x.Contains(currentHouse.houseName));

                                //While the filelist actually has instances of that filename 
                                while (cleanFileNameCopy.Exists(x => x.Contains(currentHouse.houseName)))
                                {

                                    //Make sure theres a real index found
                                    if (indexOfHouseFile != -1)
                                    {
                                        //If the file contains "NEW" and it contains the current house name
                                        if (cleanFileNameCopy[indexOfHouseFile].Contains(currentHouse.houseName))
                                        {
                                            //Determine the appropriate rename name and then rename the file
                                            if (currentHouse.rating[1] == 0.00)
                                            {
                                                renameNames[houseIndex[currentHouse.houseName]] = renameNames[houseIndex[currentHouse.houseName]] + "_" + userChngTag + _namingUpperPositionR2 + "_" + "UNKNOWN";
                                            }
                                            else
                                            {
                                                renameNames[houseIndex[currentHouse.houseName]] = renameNames[houseIndex[currentHouse.houseName]] + "_" + userChngTag + _namingUpperPositionR2 + "_" + currentHouse.rating[1].ToString("0.00");
                                            }

                                            _namingUpperPositionR2 = incrementNamingConvention(_namingUpperPositionR2, true);
                                        }


                                        //Once we've found and rename the file we can remove it from the list and then read in the next file
                                        cleanFileNameCopy.RemoveAt(indexOfHouseFile);

                                        currentWorkCompleted += increasingPercent;
                                        if (((currentWorkCompleted / totalEntries) * 100) <= 100)
                                        {
                                            worker.ReportProgress(Convert.ToInt32((currentWorkCompleted / totalEntries) * 100));
                                        }

                                    }
                                }
                            }

                        }

                    
            




                        foreach (House name in SortedHouseList)
                        {
                            currentHouse = name;
                            indexOfHouseFile = FinalFileNames.FindIndex(x => x.Contains(currentHouse.houseName));


                            while (FinalFileNames.Exists(x => x.Contains(currentHouse.houseName)))
                            {
                                fileExt = FinalFileNames[indexOfHouseFile].Split('.');

                                File.Move(filesLocation.Text + "\\" + FinalFileNames[indexOfHouseFile], filesLocation.Text + "\\" + renameNames[houseIndex[currentHouse.houseName]] + "." + fileExt[1]);

                                //Once we've found and rename the file we can remove it from the list and then read in the next file
                                FinalFileNames.RemoveAt(indexOfHouseFile);

                                currentWorkCompleted += increasingPercent;
                                if (((currentWorkCompleted / totalEntries) * 100) <= 100)
                                {
                                    worker.ReportProgress(Convert.ToInt32((currentWorkCompleted / totalEntries) * 100));
                                }

                            }
                        }


                        break;
                    }
                case false:
                    {

                        int totalEntries;
                        totalEntries = cleanFileNames.Count;

                        decimal increasingPercent = totalEntries * (decimal)0.10;
                        decimal currentWorkCompleted = 0;


                        userDefTag.Insert(0, "_");
                        userChngTag.Insert(0, "_");

                        //For every house name in the sorted house list thats already in order
                        foreach (House name in SortedHouseList)
                        {
                            {

                                //Find the index of the current file thats in the filelist
                                currentHouse = name;
                                indexOfHouseFile = cleanFileNames.FindIndex(x => x.Contains(currentHouse.houseName));

                                //While the filelist actually has instances of that filename 
                                while (cleanFileNames.Exists(x => x.Contains(currentHouse.houseName)))
                                {

                                    //Make sure theres a real index found
                                    if (indexOfHouseFile != -1)
                                    {
                                        //If the file contains "NEW" and it contains the current house name
                                        if (cleanFileNames[indexOfHouseFile].CaseInsensitiveContains(userDefTag) && cleanFileNames[indexOfHouseFile].Contains(currentHouse.houseName))
                                        {
                                            //Determine the appropriate rename name and then rename the file
                                            if (currentHouse.rating[0] == 0.00)
                                            {
                                                renameName = _namingUpperPosition + "_" + "UNKNOWN";
                                            }
                                            else
                                            {
                                                renameName = _namingUpperPosition + "_" + (currentHouse.rating[0]).ToString("0.00");
                                            }

                                            _namingUpperPosition = incrementNamingConvention(_namingUpperPosition, true);
                                            fileExt = cleanFileNames[indexOfHouseFile].Split('.');
                                            string test = filesLocation + cleanFileNames[indexOfHouseFile];
                                            File.Move(filesLocation.Text + "\\" + cleanFileNames[indexOfHouseFile], filesLocation.Text + "\\" + renameName + "." + fileExt[1]);

                                            currentWorkCompleted += increasingPercent;
                                            if (((currentWorkCompleted / totalEntries) * 100) <= 100)
                                            {
                                                worker.ReportProgress(Convert.ToInt32((currentWorkCompleted / totalEntries) * 100));
                                            }
                                        }

                                        //If the file does not contain "NEW" and it contains the current house name
                                        if (!cleanFileNames[indexOfHouseFile].CaseInsensitiveContains(userDefTag) && cleanFileNames[indexOfHouseFile].Contains(currentHouse.houseName))
                                        {

                                            //Determine the appropriate rename name and then rename the file

                                            if (currentHouse.rating[0] == 0.00)
                                            {
                                                renameName = _namingUpperPosition + "_" + "UNKNOWN" + "_" + userChngTag;
                                            }
                                            else
                                            {
                                                renameName = _namingLowerPosition + "_" + (currentHouse.rating[0]).ToString("0.00") + "_" + userChngTag;
                                            }

                                            _namingLowerPosition = incrementNamingConvention(_namingLowerPosition, false);
                                            fileExt = cleanFileNames[indexOfHouseFile].Split('.');
                                            string test = filesLocation.Text + renameName + "." + fileExt[1];
                                            File.Move(filesLocation.Text + "\\" + cleanFileNames[indexOfHouseFile], filesLocation.Text + "\\" + renameName + "." + fileExt[1]);

                                        }

                                        //Once we've found and rename the file we can remove it from the list and then read in the next file
                                        cleanFileNames.RemoveAt(indexOfHouseFile);

                                        currentWorkCompleted += increasingPercent;
                                        if (((currentWorkCompleted / totalEntries) * 100) <= 100)
                                        {
                                            worker.ReportProgress(Convert.ToInt32((currentWorkCompleted / totalEntries) * 100));
                                        }

                                    }
                                }
                            }
                        }

                        break;
                    }
            }
        }




        private void XlSSworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                opStatusName.Text = "Error: The file selected is not an Excel Spreadsheet or it is corrupted please try again.";
            }

            else if (!(e.Error == null))
            {
                opStatusName.Text = "Error: The file selected is not an Excel Spreadsheet or it is corrupted please try again.";
            }

            else
            {
                opStatusName.Text += "Done!";
                xLSpreadsheetLocation.Text = dataSetName;
            }
        }

        private void XlSSworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            opProgress.Visible = true;
            opProgress.Value = e.ProgressPercentage;
        }

        private void XlSSworker_DoWork(object sender, DoWorkEventArgs e)
        {

            BackgroundWorker worker = sender as BackgroundWorker;

            houses.Clear();

            //Excel Variables
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(dataSetName);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            //Reading and storing input dataset
            string input = "";
            double num;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            int totalEntries = (rowCount * colCount) - 2;
            decimal increasingPercent = totalEntries * (decimal)0.10;
            decimal currentWorkCompleted = 0;

            if (colCount < 3)
            {
                doubleRatingEnabled = false;
                standardModeEnabled = true;
            } else if (colCount == 3)
            {
                doubleRatingEnabled = true;
                standardModeEnabled = false;
            }

            for (int i = 2; i <= rowCount; i++)
            {
                House temp = new House();

                for (int j = 1; j <= colCount; j++)
                {


                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {

                        //If the current value being parsed is a number
                        if (double.TryParse(xlRange.Cells[i, j].Value2.ToString(), out num))
                        {
                            // Make a new House object and set its name and rating, then add it to the list of houses.
                            temp.rating.Add(Double.Parse(num.ToString("0.00")));

                        }

                        // If the current value isn't a number then it must be a house name so we store it while we wait for its rating.
                        else
                        {
                            input = xlRange.Cells[i, j].Value2.ToString();
                            temp.houseName = input;
                        }

                    }

                    currentWorkCompleted += increasingPercent;
                    if (((currentWorkCompleted / totalEntries) * 100) <= 100)
                    {
                        worker.ReportProgress(Convert.ToInt32((currentWorkCompleted / totalEntries) * 100));
                    }
                }

                houses.Add(temp);

                currentWorkCompleted += increasingPercent;
                if (((currentWorkCompleted / totalEntries) * 100) <= 100)
                {
                    worker.ReportProgress(Convert.ToInt32((currentWorkCompleted / totalEntries) * 100));
                }

            }

            GC.Collect();
            GC.WaitForPendingFinalizers();


            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            xlWorkbook.Close(0);
            
            Marshal.ReleaseComObject(xlWorkbook);

            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

        }

        
    }

    //Class to hold the house name and its corresponding rating.
    public class House
    {

        public string houseName { get; set; }
        public List<double> rating = new List<double>();


    }

    public static class Extensions
    {
        public static bool CaseInsensitiveContains(this string text, string value,
            StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;

        }




    }
}

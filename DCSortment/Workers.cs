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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

namespace DCSortment
{

    //House object to hold its name and its rating(s)
    public class House
    {

        public string houseName { get; set; }
        public List<double> rating = new List<double>();


    }

    //CaseInsensitiveContains Extension
    public static class Extensions
    {
        public static bool CaseInsensitiveContains(this string text, string value,
            StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;

        }
    }


    class Workers : IDisposable{

        // GUI interface identifier allowing us to talk to the interface which will talk to the GUI to make changes when needed
        private readonly IGuiUpdate IGui;

        //Location of the files folder selected and the base for the naming conventions.
        string filesLocation;
        string _namingUpperPosition;
        string _namingUpperPositionR2;
        string _namingLowerPosition;
        string _namingLowerPositionR2;


        //List Variables  
        List<string> fileNames;
        List<string> cleanFileNames = new List<string>();
        

        BackgroundWorker worker = new BackgroundWorker();

        public Workers(IGuiUpdate gui)
        {
            this.IGui = gui;
            this.filesLocation = IGui.FilesLocation;
            this._namingUpperPosition = "AA";
            this._namingUpperPositionR2 = "AA";
            this._namingLowerPosition = "aa";
            this._namingLowerPositionR2 = "aa";
            
            
            
        }

        //Starts the excel spreadsheet worker that parses the spreadsheet dataset.
        public void runXLWorker()
        {
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.DoWork += XlSSworker_DoWork;
            worker.ProgressChanged += XlSSworker_ProgressChanged;
            worker.RunWorkerCompleted += XlSSworker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }
        
        //Starts the renaming worker which does all the processing of the data set and finally rename the files
        public void runRenameWorker()
        {
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.DoWork += renameWorker_DoWork;
            worker.ProgressChanged += renameWorker_ProgressChanged;
            worker.RunWorkerCompleted += renameWorker_RunWorkerCompleted;
            worker.RunWorkerAsync();

        }

        //Rename worker bindings Start
        private void renameWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            IGui.ProgressBarVisible = true;
            IGui.ProgressBarValue = e.ProgressPercentage;
        }

        private void renameWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if ((e.Cancelled == true))
            {
                IGui.StatusBarText = "Error: The file selected is not an Excel Spreadsheet or it is corrupted.";
            }

            else if (!(e.Error == null))
            {
                IGui.StatusBarText = "Error: The file selected is not an Excel Spreadsheet or it is corrupted.";
            }

            else
            {
                IGui.StatusBarText += "Done!";

            }
            Dispose();
        }

        private void renameWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            IGui.StatusBarText = "Renaming Files: ";

            // Get the list of files in the directory that need to be renamed and prepare the filenames to be cleaned.
            fileNames = Directory.GetFiles(filesLocation).ToList();
            string completedDirectory = filesLocation;
            completedDirectory = Regex.Replace(filesLocation, @"\\", ".") + "\\\\";

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

            bool allowRename = false;
         


            switch (IGui.DoubleRatingMode)
            {
                case true:
                    {


                        List<string> renameNames = new List<string>();
                        List<string> cleanFileNameCopy = cleanFileNames.ToList();
                        List<string> FinalFileNames = cleanFileNames.ToList();

                        //Progress tracking variables to update the UI as work as being completed on the progress bar.
                        int totalEntries;
                        totalEntries = cleanFileNames.Count + cleanFileNameCopy.Count + FinalFileNames.Count;

                        decimal increasingPercent = totalEntries * (decimal)0.10;
                        decimal currentWorkCompleted = 0;

                        //Insert an underscore at the front of the users tags for more accurate searching conditions.
                        IGui._DRsearchingTag.Insert(0, "_");
                        IGui._DRreplacingTag.Insert(0, "_");


                        //Dictionaries to hold the first and second half of the rename name linked with their appropriate filename.
                        Dictionary<string, string> Rating1Renames = new Dictionary<string, string>();
                        Dictionary<string, string> Rating2Renames = new Dictionary<string, string>();



                        foreach (House name in IGui.SortedHouseList)
                        {
                            //Find the index of the current file thats in the filelist
                            currentHouse = name;
                            indexOfHouseFile = cleanFileNames.FindIndex(x => x.CaseInsensitiveContains(currentHouse.houseName));

                            //While the filelist actually has instances of that filename 
                            while (cleanFileNames.Exists(x => x.CaseInsensitiveContains(currentHouse.houseName)))
                            {

                                //Make sure theres a real index found
                                if (indexOfHouseFile != -1)
                                {
                                    //If the file contains the users searching tag and it contains the current house name
                                    if (cleanFileNames[indexOfHouseFile].CaseInsensitiveContains(currentHouse.houseName))
                                    {
                                        if (cleanFileNames[indexOfHouseFile].CaseInsensitiveContains(IGui._DRsearchingTag))
                                        {
                                            //Determine the appropriate rename name and save it for later
                                            allowRename = true;
                                            // If the house has no rating then its rating becomes UNKNOWN 
                                            if (currentHouse.rating[0] == 0.00)
                                            {
                                                renameName = IGui._SMsearchingTag + _namingUpperPosition + "_" + "UNKNOWN";
                                                _namingUpperPosition = incrementNamingConvention(_namingUpperPosition, true);
                                                Rating1Renames.Add(cleanFileNames[indexOfHouseFile], renameName);
                                            }

                                            else if (currentHouse.rating[0] == -1)
                                            {
                                                renameName = "";

                                                Rating1Renames.Add(cleanFileNames[indexOfHouseFile], renameName);
                                            }

                                            //If it does have a rating (_SMsearchingTag equals Prefix1 in Double Rating mode) the format will be Prefix1 + Next Open Naming Convention + Rating 1
                                            else
                                            {
                                                renameName = IGui._SMsearchingTag + _namingUpperPosition + "_" + currentHouse.rating[0].ToString("0.00");
                                                _namingUpperPosition = incrementNamingConvention(_namingUpperPosition, true);
                                                Rating1Renames.Add(cleanFileNames[indexOfHouseFile], renameName);
                                            }
                                        }

                                        else if (!cleanFileNames[indexOfHouseFile].CaseInsensitiveContains(IGui._DRsearchingTag))
                                        {
                                            //Determine the appropriate rename name and then rename the file

                                            // If the house has no rating then its rating becomes UNKNOWN 
                                            if (currentHouse.rating[0] == 0.00)
                                            {
                                                renameName = IGui._SMsearchingTag + _namingLowerPosition + "_" + "UNKNOWN" + "_";
                                                _namingLowerPosition = incrementNamingConvention(_namingLowerPosition, false);
                                                Rating1Renames.Add(cleanFileNames[indexOfHouseFile], renameName);
                                            }

                                            else if (currentHouse.rating[0] == -1)
                                            {
                                                renameName = "";
                                                Rating1Renames.Add(cleanFileNames[indexOfHouseFile], renameName);
                                            }

                                            else
                                            {
                                                renameName = IGui._SMsearchingTag + _namingLowerPosition + "_" + currentHouse.rating[0].ToString("0.00");
                                                _namingLowerPosition = incrementNamingConvention(_namingLowerPosition, false);
                                                Rating1Renames.Add(cleanFileNames[indexOfHouseFile], renameName);
                                            }
                                        }
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

                        foreach (House name in IGui.SecondRatingList)
                        {
                            //Find the index of the current file thats in the filelist
                            currentHouse = name;
                            indexOfHouseFile = cleanFileNameCopy.FindIndex(x => x.CaseInsensitiveContains(currentHouse.houseName));

                            //While the filelist actually has instances of that filename 
                            while (cleanFileNameCopy.Exists(x => x.CaseInsensitiveContains(currentHouse.houseName)))
                            {

                                //Make sure theres a real index found
                                if (indexOfHouseFile != -1)
                                {
                                    //If the file contains "NEW" and it contains the current house name
                                    if (cleanFileNameCopy[indexOfHouseFile].CaseInsensitiveContains(currentHouse.houseName))
                                    {
                                        if (cleanFileNameCopy[indexOfHouseFile].CaseInsensitiveContains(IGui._DRsearchingTag))
                                        {

                                            allowRename = true;
                                            //Determine the appropriate rename name and then rename the file

                                            // If the house has no rating then its rating becomes UNKNOWN 
                                            if (currentHouse.rating[1] == 0.00)
                                            {
                                                renameName = "_" + IGui._SMreplacingTag + _namingUpperPositionR2 + "_" + "UNKNOWN";
                                                _namingUpperPositionR2 = incrementNamingConvention(_namingUpperPositionR2, true);
                                                Rating2Renames.Add(cleanFileNameCopy[indexOfHouseFile], renameName);
                                            }

                                            else if (currentHouse.rating[1] == -1)
                                            {
                                                renameName = "";
                                                Rating2Renames.Add(cleanFileNameCopy[indexOfHouseFile], renameName);
                                            }

                                            else
                                            {
                                                if (currentHouse.rating[0] != -1)
                                                {
                                                    renameName = "_" + IGui._SMreplacingTag + _namingUpperPositionR2 + "_" + currentHouse.rating[1].ToString("0.00");
                                                    _namingUpperPositionR2 = incrementNamingConvention(_namingUpperPositionR2, true);
                                                    Rating2Renames.Add(cleanFileNameCopy[indexOfHouseFile], renameName);
                                                }
                                                else
                                                {
                                                    renameName = IGui._SMreplacingTag + _namingUpperPositionR2 + "_" + currentHouse.rating[1].ToString("0.00");
                                                    _namingUpperPositionR2 = incrementNamingConvention(_namingUpperPositionR2, true);
                                                    Rating2Renames.Add(cleanFileNameCopy[indexOfHouseFile], renameName);
                                                }
                                            }

                                        }

                                        else if (!cleanFileNameCopy[indexOfHouseFile].CaseInsensitiveContains(IGui._DRsearchingTag))
                                        {
                                            //Determine the appropriate rename name and then rename the file

                                            // If the house has no rating then its rating becomes UNKNOWN 
                                            if (currentHouse.rating[1] == 0.00)
                                            {
                                                renameName = "_" + IGui._SMreplacingTag + _namingLowerPositionR2 + "_" + "UNKNOWN" + "_" +IGui._DRreplacingTag;
                                                _namingLowerPositionR2 = incrementNamingConvention(_namingLowerPositionR2, false);
                                                Rating2Renames.Add(cleanFileNameCopy[indexOfHouseFile], renameName);
                                            }

                                            else if (currentHouse.rating[1] == -1)
                                            {
                                                renameName = "_" + IGui._DRreplacingTag;
                                                Rating2Renames.Add(cleanFileNameCopy[indexOfHouseFile], renameName);
                                            }

                                            else
                                            {
                                                if (currentHouse.rating[0] != -1)
                                                {
                                                    renameName = "_" + IGui._SMreplacingTag + _namingLowerPositionR2 + "_" + currentHouse.rating[1].ToString("0.00") + "_" + IGui._DRreplacingTag;
                                                    _namingLowerPositionR2 = incrementNamingConvention(_namingLowerPositionR2, false);
                                                    Rating2Renames.Add(cleanFileNameCopy[indexOfHouseFile], renameName);
                                                }
                                                else
                                                {
                                                    renameName = IGui._SMreplacingTag + _namingLowerPositionR2 + "_" + currentHouse.rating[1].ToString("0.00") + "_" + IGui._DRreplacingTag;
                                                    _namingLowerPositionR2 = incrementNamingConvention(_namingLowerPositionR2, false);
                                                    Rating2Renames.Add(cleanFileNameCopy[indexOfHouseFile], renameName);
                                                }
                                            }

                                           

                                        }
                                    }


                                    //Once we've found and rename the file we can remove it from the list and then read in the next file
                                    cleanFileNameCopy.RemoveAt(indexOfHouseFile);

                                    //Increase work completed by the predetermined increasing percent per work completed and report progress.
                                    currentWorkCompleted += increasingPercent;
                                    if (((currentWorkCompleted / totalEntries) * 100) <= 100)
                                    {
                                        worker.ReportProgress(Convert.ToInt32((currentWorkCompleted / totalEntries) * 100));
                                    }

                                }
                            }
                        }

                        if (allowRename == true)
                        {
                            //Renaming of files
                            foreach (String fileName in FinalFileNames)
                            {
                                //Split the filename on the '.' leaving us with the filename itself and then the file extension.
                                fileExt = fileName.Split('.');

                                //Rename the current fileName to its appropriate name by using the dictionaries. Convention here is Location of Files + filename -> location of files + R1Rename + R2Rename + file extension.
                                File.Move(filesLocation + "\\" + fileName, filesLocation + "\\" + Rating1Renames[fileName] + Rating2Renames[fileName] + "." + fileExt[1]);

                                //Increase work completed by the predetermined increasing percent per work completed and report progress.
                                currentWorkCompleted += increasingPercent;
                                if (((currentWorkCompleted / totalEntries) * 100) <= 100)
                                {
                                    worker.ReportProgress(Convert.ToInt32((currentWorkCompleted / totalEntries) * 100));
                                }
                            }
                        } else
                        {
                            IGui.ProgressBarVisible = false;
                            IGui.StatusBarText = "Error: Searching Tag not found, please check it for correctness.";
                        }


                        break;
                    }


                case false:
                    {

                        int totalEntries;
                        totalEntries = cleanFileNames.Count;

                        decimal increasingPercent = totalEntries * (decimal)0.10;
                        decimal currentWorkCompleted = 0;


                        IGui._SMsearchingTag.Insert(0, "_");
                        IGui._SMreplacingTag.Insert(0, "_");

                        //For every house name in the sorted house list thats already in order
                        foreach (House name in IGui.SortedHouseList)
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
                                        //If the file contains the searching tag and it contains the current house name
                                        if (cleanFileNames[indexOfHouseFile].CaseInsensitiveContains(IGui._SMsearchingTag) && cleanFileNames[indexOfHouseFile].Contains(currentHouse.houseName))
                                        {
                                            //Determine the appropriate rename name and then rename the file

                                            // If the house has no rating then its rating becomes UNKNOWN 
                                            if (currentHouse.rating[0] == 0.00)
                                            {
                                                renameName = _namingUpperPosition + "_" + "UNKNOWN";
                                            }
                                            else
                                            {
                                                renameName = _namingUpperPosition + "_" + (currentHouse.rating[0]).ToString("0.00");
                                            }

                                            //increment naming convention
                                            _namingUpperPosition = incrementNamingConvention(_namingUpperPosition, true);
                                            
                                            //Split on file extension and then rename the files.
                                            fileExt = cleanFileNames[indexOfHouseFile].Split('.');
                                            File.Move(filesLocation + "\\" + cleanFileNames[indexOfHouseFile], filesLocation + "\\" + renameName + "." + fileExt[1]);

                                            //Increase work completed by the predetermined increasing percent per work completed and report progress.
                                            currentWorkCompleted += increasingPercent;
                                            if (((currentWorkCompleted / totalEntries) * 100) <= 100)
                                            {
                                                worker.ReportProgress(Convert.ToInt32((currentWorkCompleted / totalEntries) * 100));
                                            }
                                        }

                                        //If the file does not contain "NEW" and it contains the current house name
                                        if (!cleanFileNames[indexOfHouseFile].CaseInsensitiveContains(IGui._SMsearchingTag) && cleanFileNames[indexOfHouseFile].Contains(currentHouse.houseName))
                                        {

                                            //Determine the appropriate rename name and then rename the file

                                            if (currentHouse.rating[0] == 0.00)
                                            {
                                                renameName = _namingUpperPosition + "_" + "UNKNOWN" + "_" + IGui._SMreplacingTag;
                                            }
                                            else
                                            {
                                                renameName = _namingLowerPosition + "_" + (currentHouse.rating[0]).ToString("0.00") + "_" + IGui._SMreplacingTag;
                                            }
                                            //Increment naming convention
                                            _namingLowerPosition = incrementNamingConvention(_namingLowerPosition, false);

                                            //Split file name and extension and then rename the files.
                                            fileExt = cleanFileNames[indexOfHouseFile].Split('.');
                                            File.Move(filesLocation + "\\" + cleanFileNames[indexOfHouseFile], filesLocation + "\\" + renameName + "." + fileExt[1]);

                                        }

                                        //Once we've found and rename the file we can remove it from the list and then read in the next file
                                        cleanFileNames.RemoveAt(indexOfHouseFile);

                                        //Increase work completed by the predetermined increasing percent per work completed and report progress.
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

        //Rename Worker Bindings end

        //Excel Spreadsheet parse worker start
        private void XlSSworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                IGui.StatusBarText = "Error: The file selected is not an Excel Spreadsheet or it is corrupted please try again.";
            }

            else if (!(e.Error == null))
            {
                IGui.StatusBarText = "Error: The file selected is not an Excel Spreadsheet or it is corrupted please try again.";
            }

            else
            {
                IGui.StatusBarText += "Done!";
            }
            //Update the GUIs spreadsheet location after we've parsed it and dispose the worker.
            IGui.xLSpreadSheetText = IGui.DataSetName;
            Dispose();
            
        }

        private void XlSSworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            IGui.ProgressBarVisible = true;
            IGui.ProgressBarValue = e.ProgressPercentage;
        }

        private void XlSSworker_DoWork(object sender, DoWorkEventArgs e)
        {

            BackgroundWorker worker = sender as BackgroundWorker;

            //Inform the user that we are parsing the spreadsheet
            IGui.StatusBarText = "Parsing Spreadsheet: ";


            IGui.Houses = new List<House>();

            //Excel Variables
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(IGui.DataSetName);
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

            //Determine the available sorting methods for this dataset.
            if (colCount < 3)
            {
                IGui.DoubleRatingEnabled = false;
                IGui.StandardModeEnabled = true;
            }
            else if (colCount == 3)
            {
                IGui.DoubleRatingEnabled = true;
                IGui.StandardModeEnabled = false;
            }

            //Start reading the data from the spreadsheet.
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
                        else if(xlRange.Cells[i, j].Value2.ToString() ==  "DNE" || xlRange.Cells[i, j].Value2.ToString() == "dne")
                        {
                            temp.rating.Add(-1);
                        }

                        else
                           
                        {
                            input = xlRange.Cells[i, j].Value2.ToString();
                            temp.houseName = input;
                        }

                    }
                    //Increase work completed by the predetermined increasing percent per work completed and report progress.
                    currentWorkCompleted += increasingPercent;
                    if (((currentWorkCompleted / totalEntries) * 100) <= 100)
                    {
                        worker.ReportProgress(Convert.ToInt32((currentWorkCompleted / totalEntries) * 100));
                    }
                }

                //Add the house object to its storing list.
                IGui.Houses.Add(temp);

                //Increase work completed by the predetermined increasing percent per work completed and report progress.
                currentWorkCompleted += increasingPercent;
                if (((currentWorkCompleted / totalEntries) * 100) <= 100)
                {
                    worker.ReportProgress(Convert.ToInt32((currentWorkCompleted / totalEntries) * 100));
                }

            }

            //Do garbage collection after parsing the spreadsheet and close out the open excel files.
            GC.Collect();
            GC.WaitForPendingFinalizers();


            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            xlWorkbook.Close(0);

            Marshal.ReleaseComObject(xlWorkbook);

            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

        }

        //Excel Spreadsheet parse worker end

        //Method that determines the next appropriate naming convention.
        private string incrementNamingConvention(string theString, bool isUpper)
        {
            // Take in the string and convert it to a character array.
            char[] theCharString = theString.ToCharArray();

            //Switched based on if the naming convetion is in uppercase Ex.'AA' or lower case Ex.'aa'
            switch (isUpper)
            {

                case true:
                    {
                        //Check to see if the next character is not Z. If the next character is not z we can can keep the front character and just increment the second character to the next
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

        //Dispose the workers once they are completed.
        public void Dispose()
        {
            worker.Dispose();
        }
    }

}













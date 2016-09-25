using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace DCSortment
{
    class Program
    {
        static string _namingUpperPosition = "AA";
        static string _namingLowerPosition = "aa";
     
        

        static void Main(string[] args)
        {
            string currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            string xlFileName;
            int programMode = 0;
            bool retryFileOpen = true;

            //List Variables
            List<House> houses = new List<House>();
            List<House> SortedHouseList = null;
            List<string> fileNames;
            List<string> cleanFileNames = new List<string>();

            //Excel Variables
            Excel.Application xlApp = new Excel.Application();

            Console.WriteLine("Welcome to DCSortment \n" );

            Console.WriteLine("\nPlease enter the name of the excel file you wish to use: ");

            xlFileName = Console.ReadLine();

            while (retryFileOpen)
            {
                try
                {
                    Excel.Workbook xltestWorkbook = xlApp.Workbooks.Open(currentDirectory + xlFileName + ".xlsx");
                    retryFileOpen = false;
                    xltestWorkbook.Close();
                }
                catch (COMException)
                {
                    Console.WriteLine("\n\nFilename not found.");
                    Console.WriteLine("\nPlease enter the name of the excel file you wish to use: ");
                    xlFileName = Console.ReadLine();
                }
            }



            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(currentDirectory + xlFileName + ".xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            //Reading and storing input dataset
            string input = "";
            double num;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            for (int i = 2; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {


                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {

                        //If the current value being parsed is a number
                        if (double.TryParse(xlRange.Cells[i, j].Value2.ToString(), out num))
                        {
                            // Make a new House object and set its name and rating, then add it to the list of houses.
                            House temp = new House();
                            temp.houseName = input;
                            temp.rating = num;
                            houses.Add(temp);
                        }

                        // If the current value isn't a number then it must be a house name so we store it while we wait for its rating.
                        else
                        {
                            input = xlRange.Cells[i, j].Value2.ToString();
                        }
                    }
                }


            }

            GC.Collect();
            GC.WaitForPendingFinalizers();


            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            //End of Reading and storing input


            //Sort list
            bool isValid = false;
           
            Console.WriteLine("\n\nSorting Formats:"
                + "\n1. By Weighted Alphabetical."
                + "\n2. Preordered Dataset."
                + "\n3. Exit Program."
                            );


            // Read input from console ensuring it is an integer
            bool acceptableNumA = false;

            do
            {

                Console.WriteLine("\nPlease select a format: ");

                try
                {
                    programMode = Convert.ToInt32(Console.ReadLine());
                    acceptableNumA = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("\n\nInvalid input");
                }

            } while (!acceptableNumA);
            
            //Until the user selects a clear format 
            while (!isValid)
            {

                switch (programMode)
                {
       
                    case 1:
                        {
                            SortedHouseList = houses.OrderByDescending(house => house.rating).ThenBy(house => house.houseName).ToList();
                            isValid = true;
                            break;
                        }

                    case 2:
                        {
                            SortedHouseList = houses;
                            isValid = true;
                            break;
                        }
                    case 3:
                        System.Environment.Exit(1);
                        break;

                    default:
                        {
                            bool acceptableNum = false;
                            do
                            {

                                Console.WriteLine("\n\nInvalid input");
                                Console.WriteLine("\nPlease select a format: ");

                                try
                                {
                                    programMode = Convert.ToInt32(Console.ReadLine());
                                    acceptableNum = true;
                                }
                                catch (System.FormatException)
                                {

                                }

                            } while (!acceptableNum);
                            break;
                        }
                }

            }
           
            try
            {
                fileNames = Directory.GetFiles(currentDirectory + "Files\\").ToList();
            }
            catch (DirectoryNotFoundException)
            {

                Console.WriteLine("\nThe working file directory was not found. Please Ensure that the folder named \"Files\" has been created in the same directory as the program.)");
                Console.Read();
            }


                Console.WriteLine("\nProcess Initiated....");

                //Get the list of files in the directory that need to be renamed and prepare the filenames to be cleaned.
                fileNames = Directory.GetFiles(currentDirectory + "Files\\").ToList();
                char[] splitCase = (currentDirectory + "Files\\").ToCharArray();
                string completedDirectory= currentDirectory + "Files\\";
                completedDirectory = Regex.Replace(completedDirectory, @"\\",".");
                
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

                //For every house name in the sorted house list thats already in order
                foreach (House name in SortedHouseList) {                   
                    {

                        //Find the index of the current file thats in the filelist
                        currentHouse = name;
                        indexOfHouseFile = cleanFileNames.FindIndex(x => x.Contains(currentHouse.houseName));
                           
                        //While the filelist actually has instances of that filename 
                        while (cleanFileNames.Exists(x => x.Contains(currentHouse.houseName))) { 

                            //Make sure theres a real index found
                            if (indexOfHouseFile != -1)
                            {
                                //If the file contains "NEW" and it contains the current house name
                                if (cleanFileNames[indexOfHouseFile].Contains("NEW") && cleanFileNames[indexOfHouseFile].Contains(currentHouse.houseName))
                                {
                                    //Determine the appropriate rename name and then rename the file
                                    renameName = _namingUpperPosition + "_" + currentHouse.rating;
                                    incrementNamingConvention(_namingUpperPosition, true);
                                    fileExt = cleanFileNames[indexOfHouseFile].Split('.');
                                    File.Move(currentDirectory + "Files\\" + cleanFileNames[indexOfHouseFile], currentDirectory + "Files\\" + renameName + "." + fileExt[1]);

                                }

                                //If the file does not contain "NEW" and it contains the current house name
                                if (!cleanFileNames[indexOfHouseFile].Contains("NEW") && cleanFileNames[indexOfHouseFile].Contains(currentHouse.houseName))
                                {

                                    //Determine the appropriate rename name and then rename the file
                                    renameName = _namingLowerPosition + "_" + currentHouse.rating + "_" + "CHNGTAG";
                                    incrementNamingConvention(_namingLowerPosition, false);
                                    fileExt = cleanFileNames[indexOfHouseFile].Split('.');
                                    File.Move(currentDirectory + "Files\\" + cleanFileNames[indexOfHouseFile], currentDirectory + "Files\\" + renameName + "." + fileExt[1]);

                                }

                                //Once we've found and rename the file we can remove it from the list and then read in the next file
                                cleanFileNames.RemoveAt(indexOfHouseFile);

                                }
                            }
                        }
                    }

                Console.WriteLine("Process Completed!");
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadLine();
        }

        //Method that controls the naming convention incrementation.
        public static void incrementNamingConvention(string theString, bool isUpper)
        {    
            char[] theCharString = theString.ToCharArray();

            switch (isUpper)
            {

                case true:
                    {
                        if (((int)theCharString[1] + 1) > 90)
                        {
                            _namingUpperPosition = ((char)((int)theCharString[0] + 1)).ToString() + ((char)(65)).ToString();
                        }
                        else
                        {
                            _namingUpperPosition = theCharString[0].ToString() + ((char)((int)theCharString[1] + 1)).ToString();
                        }
                        break;
                    }
                case false:
                    {
                        if (((int)theCharString[1] + 1) > 122)
                        {
                            _namingLowerPosition = ((char)((int)theCharString[0] + 1)).ToString() + ((char)(97)).ToString();
                        }
                        else
                        {
                            _namingLowerPosition = theCharString[0].ToString() + ((char)((int)theCharString[1] + 1)).ToString();
                        }
                        break;
                    }

            }

           
        }


    }

    //Class to hold the house name and its corresponding rating.
    public class House{

        public string houseName { get; set; }
        public double rating { get; set; }
    }



}






  

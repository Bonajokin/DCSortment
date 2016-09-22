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

            //List Variables
            List<House> houses = new List<House>();
            List<House> SortedHouseList;
            List<string> fileNames;
            List<string> cleanFileNames = new List<string>();

            //Excel Variables
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(currentDirectory + "inputTest.xlsx");
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
            }

            catch (COMException)
            {

                Console.WriteLine("\nThe required dataset .xlsx file was not found. Please ensure \"dataset.xlsx\" is in the current directory as the executeable.");

            }


            //Sort list

            SortedHouseList = houses.OrderByDescending(house => house.rating).ThenBy(house => house.houseName).ToList();

            
            foreach (House house in SortedHouseList) {
                Console.WriteLine(house.houseName + " -> " + house.rating);
            }


            try
            {
                fileNames = Directory.GetFiles(currentDirectory + "Files\\").ToList();
                char[] splitCase = (currentDirectory + "Files\\").ToCharArray();
                string completedDirectory= currentDirectory + "Files\\";
                completedDirectory = Regex.Replace(completedDirectory, @"\\",".");
                



                foreach (string filename in fileNames)
                {
                    string[] splitName = Regex.Split(filename, @completedDirectory);
                    cleanFileNames.Add(splitName[1]);
                }


                foreach (string filename in cleanFileNames)
                {
                    Console.WriteLine(filename);
                }


                //sortedHouseList and cleanFileNames
                House currentHouse;
                int indexOfHouseFile;
                string renameName;

                currentHouse = SortedHouseList.First();
                indexOfHouseFile = 

                if (cleanFileNames[indexOfHouseFile].Contains("_NEW"))
                {
                    renameName = _namingUpperPosition + "_" + currentHouse.rating;
                    incrementNamingConvention(_namingUpperPosition, true);
                    Console.WriteLine(renameName + " - Next Convetion -> " + _namingUpperPosition);
                } else
                {
                    renameName = _namingLowerPosition + "_" + currentHouse.rating + "_" + "CHNGTAG";
                    incrementNamingConvention(_namingLowerPosition, false);
                    Console.WriteLine(renameName + " - Next Convetion -> " + _namingLowerPosition);
                }


            }

            catch (DirectoryNotFoundException)
            {

                Console.WriteLine("\nThe working file directory was not found. Please Ensure that the folder named \"Files\" has been created in the same directory as the program.)");

            }

            



            Console.Read();
        }

        public static string incrementNamingConvention(string theString, bool isUpper)
        {
            string incrementedString = null;
            char[] theCharString = theString.ToCharArray();

            switch (isUpper)
            {

                case true:

                    if (((int)theCharString[1] + 1) > 90)
                    {
                        incrementedString = ((char)((int)theCharString[0] + 1)).ToString() + ((char)(65)).ToString();
                    }
                    else
                    {
                        incrementedString = theCharString[0].ToString() + ((char)((int)theCharString[1] + 1)).ToString();
                    }
                    break;

                case false:

                    if (((int)theCharString[1] + 1) > 122)
                    {
                        incrementedString = ((char)((int)theCharString[0] + 1)).ToString() + ((char)(97)).ToString();
                    }
                    else
                    {
                        incrementedString = theCharString[0].ToString() + ((char)((int)theCharString[1] + 1)).ToString();
                    }
                    break;


            }

            return incrementedString;
        }


    }

    public class House{

        public string houseName { get; set; }
        public double rating { get; set; }

    }



}






  

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace DCSortment
{
    class Program
    {


        static void Main(string[] args)
        {


            //Excel Variables
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Otis\Documents\Visual Studio 2015\Projects\DCSortment\inputTest.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            List<House> houses = new List<House>();
            List<House> SortedList;

            //Reading and storing input dataset
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            string input = "";
            double num;


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

            SortedList = houses.OrderByDescending(house => house.rating).ThenBy(house => house.houseName).ToList();

            //





            Console.Read();
        }


    }

    public class House{

        public string houseName { get; set; }
        public double rating { get; set; }

    }



}






  

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace DCSortment
{
    class Program
    {
        static string _namingUpperPosition = "AA";
        static string _namingUpperPositionR2 = "AA";
        static string _namingLowerPosition = "aa";
        static string _namingLowerPositionR2 = "aa";

        [STAThread]
        static void Main(string[] args)
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI());

        }

    }

}










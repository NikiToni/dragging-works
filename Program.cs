using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_drag_test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 viewForm = new Form1();



            viewForm.FirstPlayerColor = Colors.black; //changing this will change the order of the cells in the visible board
            ChessBoard.repartFigures(Colors.black);

            Application.Run(viewForm);  //aparently this is stopping the execution flow
            
            //code here will not execute until previous comand finishes somehow
            //just like console.readLine()  waits for input and programing flow does nothing until gets that input

        }
    }
}

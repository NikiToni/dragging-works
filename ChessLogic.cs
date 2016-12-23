using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_drag_test
{
    static class ChessLogic
    {
        public static void setPossibleCells(int i, int j)
        {
            for (int n = 0; n < 8; n++)
            {
                for (int m = 0; m < 8; m++)
                {
                    ChessBoard.getCellFromIndex(n, m).Possible = false;
                }
            }
            //i++;
            //j++;
            int testI = i;
            int testJ = j;
            
            while (testI >= 0 && testJ >= 0 && testI < 8 && testJ < 8)
            {
                ChessBoard.getCellFromIndex(testI++, testJ++).Possible = true;
            }

            testI = i-1;
            testJ = j-1;

            while (testI >= 0 && testJ >= 0 && testI < 8 && testJ < 8)
            {
                ChessBoard.getCellFromIndex(testI--, testJ--).Possible = true;
            }

            testI = i-1;
            testJ = j+1;

            while (testI >= 0 && testJ >= 0 && testI < 8 && testJ < 8)
            {
                ChessBoard.getCellFromIndex(testI--, testJ++).Possible = true;
            }

            testI = i+1;
            testJ = j-1;

            while (testI >= 0 && testJ >= 0 && testI < 8 && testJ < 8)
            {
                ChessBoard.getCellFromIndex(testI++, testJ--).Possible = true;
            }
        }
        






        /*
                public static Figure[] getFiguresOnTable()
                {
                    Figure[] retFigArr;
                    int numbOfFigOnTable = 0;

                    foreach (Figure figure in FiguresBox.Figures)
                    {
                        if (figure.OnTable)
                        {
                            numbOfFigOnTable++;
                        }
                    }

                    retFigArr = new Figure[numbOfFigOnTable];

                    int i = 0;

                    foreach (Figure figure in FiguresBox.Figures)
                    {
                        if (figure.OnTable)
                        {
                            retFigArr[i] = figure;
                            i++;
                        }
                    }

                    return retFigArr;
                }
                */
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_drag_test
{
    static class ChessBoard
    {
        private static readonly int heightTable;
        private static readonly int widthTable;
        private static int cellWidth;
        private static int cellHeight;
        private static Cell[,] cells;

        static ChessBoard()
        {
            heightTable = 600;  //NOTE: USe 600, because this way the cell size is 24 and this is the size of the Images used to draw the figures
            widthTable = heightTable;
            cellWidth = widthTable / 8;
            cellHeight = cellWidth;

            cells = new Cell[8, 8];

            //populate the structure on the table
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(true); //empty cell
                }
            }
        }

        public static void repartFigures(Colors firstPlayerColor)
        {
            if (firstPlayerColor == Colors.black) {
                for (int i = 0; i < 8; i++)
                {
                    cells[i, 0].MyFigure = FiguresBox.WhiteFigures[i];
                    cells[i, 1].MyFigure = FiguresBox.WhiteFigures[i + 8];
                    cells[i, 6].MyFigure = FiguresBox.BlackFigures[i + 8];
                    cells[i, 7].MyFigure = FiguresBox.BlackFigures[i];
                }
            } else {
                for (int i = 0; i < 8; i++)
                {
                    cells[i, 0].MyFigure = FiguresBox.BlackFigures[i];
                    cells[i, 1].MyFigure = FiguresBox.BlackFigures[i + 8];
                    cells[i, 6].MyFigure = FiguresBox.WhiteFigures[i + 8];
                    cells[i, 7].MyFigure = FiguresBox.WhiteFigures[i];
                }
            }
        }

        public static Cell getCellFromIndex(int i, int j)
        {
            return cells[i, j];
        }

        public static int BoardWidth
        {
            get { return widthTable; }
        }

        public static int BoardHeight
        {
            get { return heightTable; }
        }

        public static int CellWidth
        {
            get { return cellWidth; }
        }

        public static int CellHeight
        {
            get { return cellHeight; }
        }
    }


}

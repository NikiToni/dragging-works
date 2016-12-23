using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_drag_test
{
    static class FiguresBox
    {
        private static Figure[] figuresArray;
        private static Figure[] blackFiguresArray;
        private static Figure[] whiteFiguresArray;

        static FiguresBox()
        {
            figuresArray = new Figure[] {
                                new Figure(Types.Rook, Colors.white),
                                new Figure(Types.Knight, Colors.white),
                                new Figure(Types.Bishop, Colors.white),
                                new Figure(Types.Queen, Colors.white),
                                new Figure(Types.King, Colors.white),
                                new Figure(Types.Bishop, Colors.white),
                                new Figure(Types.Knight, Colors.white),
                                new Figure(Types.Rook, Colors.white),

                                new Figure(Types.Pawn, Colors.white),
                                new Figure(Types.Pawn, Colors.white),
                                new Figure(Types.Pawn, Colors.white),
                                new Figure(Types.Pawn, Colors.white),
                                new Figure(Types.Pawn, Colors.white),
                                new Figure(Types.Pawn, Colors.white),
                                new Figure(Types.Pawn, Colors.white),
                                new Figure(Types.Pawn, Colors.white),

                                new Figure(Types.Rook, Colors.black),
                                new Figure(Types.Knight, Colors.black),
                                new Figure(Types.Bishop, Colors.black),
                                new Figure(Types.Queen, Colors.black),
                                new Figure(Types.King, Colors.black),
                                new Figure(Types.Bishop, Colors.black),
                                new Figure(Types.Knight, Colors.black),
                                new Figure(Types.Rook, Colors.black),

                                new Figure(Types.Pawn, Colors.black),
                                new Figure(Types.Pawn, Colors.black),
                                new Figure(Types.Pawn, Colors.black),
                                new Figure(Types.Pawn, Colors.black),
                                new Figure(Types.Pawn, Colors.black),
                                new Figure(Types.Pawn, Colors.black),
                                new Figure(Types.Pawn, Colors.black),
                                new Figure(Types.Pawn, Colors.black)
                        };

            whiteFiguresArray = new Figure[16];
            blackFiguresArray = new Figure[16];

            for (int i = 0; i < 16; i++)
            {
                whiteFiguresArray[i] = figuresArray[i];
                blackFiguresArray[i] = figuresArray[i + 16];
            }
        }

        public static Figure[] Figures
        {
            get { return figuresArray; }
        }

        public static Figure[] WhiteFigures
        {
            get { return whiteFiguresArray; }
        }

        public static Figure[] BlackFigures
        {
            get { return blackFiguresArray; }
        }
    }
}

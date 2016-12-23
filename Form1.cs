using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace chess_drag_test
{
    public partial class Form1 : Form
    {

        //only the second figure is dragable for some reason(error in the code)
        //the rest of classes, I use them to generate figures for my tests. but they could be used as some starting point

        // desired Method one:
        //
        // ChessLogic.getPossibleCells(indexWidth, indexHeight);    //height and with are some ways to say i and j
        //
        // this I expect to return an array of elements(or indexes) with the positions where the figure can move
        // ChessLogic will know which figure I am trying to move because the method is passing the index of the square where the figure is located 
        // ChessLogic will know what type and color of figure we have at that cell/position/square and will do the logic
        // when this Form recieves the returned array with posssible moves, it will draw that squares in different color to give hint to the user

        // desired Method two:
        //
        // ChessLogic.tryToMoveFromTo(sourceIndWidht, sourceIndHeight, destIndWidht, destIndHeight);  //or i,j
        //
        // the form will send this to ChessLogic when MouseUp event happens.
        // source indexes are the indexes of the square the mouse was Downed(the figure to move is there)
        // dest indexes are the square where the mouse was Upped(the place figure wants to move to is there)
        //
        // ChessLogic can call its own getPossibleCells(indexWidth, indexHeight); to determine first the possible cells
        // and furthermore to update its own track of the game state(mark figures as not on table, put the new figure on its new cell etc.)
        // Form need a response bool because it needs to know if has to draw the new figure in the place of the beaten or if can not move to turn back
        // etc.


        //the usual: 
        //          Console.WriteLine("Choise your NickName: ");
        //          string playerNickName = Console.ReadLine();
        //
        //would have to happen from inside the Form but I have not tried it yet.

        //there will be later butons in the form for "choice color to play with" and "surrender" and "new game" or even "Show best players" etc.
        //but i haven't tried this either



        private double tempDist;
        private double nearDistance;
        private SolidBrush rectangleFillDark = new SolidBrush(Color.BlueViolet);
        private SolidBrush rectangleFillLight = new SolidBrush(Color.LightCyan);
        private SolidBrush rectangleFillPossible = new SolidBrush(Color.FromArgb(127, 255, 255, 0));
        
        private bool lightSquare = false;
        private bool mouseDowned = false;

        //Note the orden of this array should correspond to the indexes in the enums Types and Colors
        private visualFigure[,] possibleFigures = new visualFigure[,] {
                              { new visualFigure("blackPawn.png", (Types)0, (Colors) 0),
                                new visualFigure("blackBishop.png", (Types)1, (Colors) 0),
                                new visualFigure("blackRook.png", (Types)2, (Colors) 0),
                                new visualFigure("blackKnight.png", (Types)3, (Colors) 0),
                                new visualFigure("blackKing.png", (Types)4, (Colors) 0),
                                new visualFigure("blackQueen.png",(Types)5, (Colors) 0)
                                 },

                              { new visualFigure("whitePawn.png", (Types)0, (Colors) 1),
                                new visualFigure("whiteBishop.png", (Types)1, (Colors) 1),
                                new visualFigure("whiteRook.png", (Types)2, (Colors) 1),
                                new visualFigure("whiteKnight.png", (Types)3, (Colors) 1),
                                new visualFigure("whiteKing.png", (Types)4, (Colors) 1),
                                new visualFigure("whiteQueen.png",(Types)5, (Colors) 1) }
        };

        private int[] figureOnFocusIndex = new int[2];
        private int[] squareOnFocusIndex = new int[2];

        public Form1()
        {
            figureOnFocusIndex[0] = 200;
            figureOnFocusIndex[1] = 200;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
 //           pictureBox1.Invalidate();
        }

        private Point startingPoint = Point.Empty;
        private Point movingPoint = Point.Empty;

        void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            figureOnFocusIndex[0] = 200;
            figureOnFocusIndex[1] = 200;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (!ChessBoard.getCellFromIndex(i, j).Empty)
                    {
                        if (testAlphaCollision(ChessBoard.getCellFromIndex(i, j).MyFigure, j, i, e.Location.X, e.Location.Y))
                        {
                            figureOnFocusIndex[0] = i;
                            figureOnFocusIndex[1] = j;
                            mouseDowned = true;

                            startingPoint = new Point(e.Location.X - i * ChessBoard.CellWidth,
                                                      e.Location.Y - j * ChessBoard.CellHeight);
                            
                            ChessLogic.setPossibleCells(i, j);  // mark the possible Cells

                            break;
                        }
                    }
                }
            }
        }

        void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDowned = false;
            figureOnFocusIndex[0] = 200;
            figureOnFocusIndex[1] = 200;
            pictureBox1.Invalidate();
        }

        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDowned)
            {
                movingPoint = new Point(e.Location.X - startingPoint.X,
                                        e.Location.Y - startingPoint.Y);
                
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            //draw the backgrond
            e.Graphics.Clear(Color.Teal);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (lightSquare)
                    {
                        if (ChessBoard.getCellFromIndex(i, j).Possible && mouseDowned)
                        {
                            e.Graphics.FillRectangle(rectangleFillPossible, i * ChessBoard.CellWidth, j * ChessBoard.CellHeight, ChessBoard.CellWidth, ChessBoard.CellHeight);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(rectangleFillLight, i * ChessBoard.CellWidth, j * ChessBoard.CellHeight, ChessBoard.CellWidth, ChessBoard.CellHeight);
                        }
                    }else
                    {
                        if (ChessBoard.getCellFromIndex(i, j).Possible && mouseDowned)
                        {
                            e.Graphics.FillRectangle(rectangleFillPossible, i * ChessBoard.CellWidth, j * ChessBoard.CellHeight, ChessBoard.CellWidth, ChessBoard.CellHeight);
                        }
                    }
                    lightSquare = !lightSquare;
                }
                lightSquare = !lightSquare;
            }

            //draw the figures

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (!ChessBoard.getCellFromIndex(i,j).Empty)
                    {
                        if (i != figureOnFocusIndex[0] || j != figureOnFocusIndex[1])
                        {
                            e.Graphics.DrawImage(possibleFigures[(int)ChessBoard.getCellFromIndex(i, j).MyFigure.Color,
                                                                 (int)ChessBoard.getCellFromIndex(i, j).MyFigure.Type].image, 
                                                                 i * ChessBoard.CellWidth, 
                                                                 j * ChessBoard.CellHeight, 
                                                                 ChessBoard.CellWidth, 
                                                                 ChessBoard.CellHeight);
                        }
                    }
                }
            }


            if (mouseDowned)
            {
                nearDistance = 1000;
                
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        tempDist = Math.Sqrt((i * ChessBoard.CellWidth - movingPoint.X) * (i * ChessBoard.CellHeight - movingPoint.X) + 
                                             (j * ChessBoard.CellWidth - movingPoint.Y) * (j * ChessBoard.CellHeight - movingPoint.Y));
                        if (tempDist < nearDistance)
                        {
                            nearDistance = tempDist;
                            squareOnFocusIndex[0] = i;
                            squareOnFocusIndex[1] = j;
                        }
                    }
                }

                e.Graphics.DrawRectangle(new Pen(Color.Black, 5), squareOnFocusIndex[0] * ChessBoard.CellWidth,
                                                                  squareOnFocusIndex[1] * ChessBoard.CellHeight,
                                                                  ChessBoard.CellWidth, ChessBoard.CellHeight);

                e.Graphics.DrawImage(possibleFigures[(int)ChessBoard.getCellFromIndex(figureOnFocusIndex[0], figureOnFocusIndex[1]).MyFigure.Color,
                                                     (int)ChessBoard.getCellFromIndex(figureOnFocusIndex[0], figureOnFocusIndex[1]).MyFigure.Type].shadowedImg,
                                                     movingPoint.X,
                                                     movingPoint.Y,
                                                     ChessBoard.CellWidth,
                                                     ChessBoard.CellHeight);
            }
        }

        public void draw()
        {
            pictureBox1.Invalidate();
        }

        public Colors FirstPlayerColor
        {
            set { lightSquare = value == Colors.white ? true : false; }
        }

        private class visualFigure
        {
            public Image image;
            public Bitmap bitmap;
            public Image shadowedImg;

            public visualFigure(string imgFileName, Types type, Colors color)
            {
                string pathPrefix = "../../chess figures/";
                
                image = Image.FromFile(pathPrefix + color.ToString() + type.ToString() + ".png");
                bitmap = new Bitmap(image);
                shadowedImg = Image.FromFile(pathPrefix + color.ToString() + type.ToString() + "Shaded.png");
            }
        }

        private bool testAlphaCollision(Figure figure, int top, int left, int mouseX, int mouseY)
        {
            int relativeX = mouseX - left * ChessBoard.CellWidth;
            int relativeY = mouseY - top * ChessBoard.CellHeight;

            if (relativeX >= 0 && relativeY >= 0 && 
                relativeX < possibleFigures[(int)figure.Color, (int)figure.Type].image.Width && 
                relativeY < possibleFigures[(int)figure.Color, (int)figure.Type].image.Height)
            {
                //handle transparency of the image for the figure
                if (possibleFigures[(int)figure.Color, (int)figure.Type].bitmap.GetPixel(relativeX, relativeY).A >= 8)
                {
                    return true;
                }
            }
            return false;
        }

    }
}

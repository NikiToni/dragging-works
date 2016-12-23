using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_drag_test
{
    class Figure
    {
        private Types figureType;
        private Colors figureColor;
        private bool onTable;

        public Figure(Types type, Colors color)
        {
            figureType = type;
            figureColor = color;
            onTable = true;
        }

        public Types Type
        {
            get { return figureType; }
        }

        public Colors Color
        {
            get { return figureColor; }
        }

        public bool OnTable
        {
            get { return onTable; }
        }
    }
}

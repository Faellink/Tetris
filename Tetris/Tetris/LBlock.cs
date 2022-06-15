using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class LBlock: BlockUserControl
    {
        public LBlock()
        {
            BlockWidth = 3;
            BlockHeight = 2;
            BlockDots = new int[,]
                    {
                       {0,0,1 },
                       {1,1,1 }
                    };
            BlockColor = new SolidBrush(Color.LightGreen);
        }
    }
}

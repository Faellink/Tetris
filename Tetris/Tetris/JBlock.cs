using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class JBlock: BlockUserControl
    {
        public JBlock()
        {
            BlockWidth = 3;
            BlockHeight = 2;
            BlockDots = new int[,]
                    {
                       {1,0,0 },
                       {1,1,1 }
                    };
            BlockID = 5;
            BlockColor = new SolidBrush(Color.DarkGreen);
        }
    }
}

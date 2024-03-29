﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class TBlock: BlockUserControl
    {
        public TBlock()
        {
            BlockWidth = 3;
            BlockHeight = 2;
            BlockDots = new int[,]
                    {
                        {0,1,0},
                        {1,1,1}
                    };
            BlockID = 3;
            BlockColor = new SolidBrush(Color.Purple);
        }
    }
}

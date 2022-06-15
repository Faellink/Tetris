using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class OBlock: BlockUserControl
    {
        public OBlock()
        {
            BlockWidth = 2;
            BlockHeight = 2;
            BlockDots = new int[,]
            {
                        {1,1},
                        {1,1}
            };
            BlockColor = new SolidBrush(Color.Orange)
        }
    }
}

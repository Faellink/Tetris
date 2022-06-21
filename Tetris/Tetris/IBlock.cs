using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class IBlock: BlockUserControl
    {
        public IBlock()
        {
            BlockWidth = 1;
            BlockHeight = 4;
            BlockDots = new int[,]
                    {
                        {1},
                        {1},
                        {1},
                        {1}
                    };
            BlockRotations = new int[][,]
{
                new int[,]
                    {
                        {1},
                        {1},
                        {1},
                        {1}
                    },
                new int[,]
                    {
                        {1,1,1,1}
                    },
                new int[,]
                    {
                        {1},
                        {1},
                        {1},
                        {1}
                    },
                new int[,]
                    {
                        {1,1,1,1}
                    }
};

            BlockColor = new SolidBrush(Color.Blue);
        }
    }
}

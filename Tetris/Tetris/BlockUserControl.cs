using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class BlockUserControl : UserControl
    {
        
        public int BlockWidth;
        public int BlockHeight;
        public int[,] BlockDots;

        public Brush BlockColor;

        public int BlockID;

        public int[,] roolbackDots;

        public BlockUserControl()
        {
            InitializeComponent();
        }

        public void RotateBlock()
        {
            roolbackDots = BlockDots;

            BlockDots = new int[BlockWidth, BlockHeight];
            for (int i = 0; i < BlockWidth; i++)
            {
                for (int j = 0; j < BlockHeight; j++)
                {
                    BlockDots[i, j] = roolbackDots[BlockHeight - 1 - j, i];
                }
            }

            var temp = BlockWidth;
            BlockWidth = BlockHeight;
            BlockHeight = temp;

        }

        public void RollbackBlock()
        {
            BlockDots = roolbackDots;

            var temp = BlockWidth;
            BlockWidth = BlockHeight;
            BlockHeight = temp;
        }
    }
}

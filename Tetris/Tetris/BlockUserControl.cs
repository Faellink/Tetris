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

        public int[][,] BlockRotations;

        public int[,] roolbackDots;

        public static int rotationState;

        public BlockUserControl()
        {
            InitializeComponent();
        }

        public void RotateBlock()
        {
            //if (rotationState<3)
            //{
            //    rotationState++;
            //}
            //else
            //{
            //    rotationState = 0;
            //}


            ////2x3
            //if (rotationState%2==0)
            //{
            //    //par
            //    BlockDots = new int[BlockWidth,BlockHeight];
            //}
            //else
            //{
            //    //impar
            //    BlockDots = new int[BlockHeight, BlockWidth];

            //}

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

            //BlockDots = new int[BlockWidth, BlockHeight];

            //BlockDots = BlockRotations[rotationState];

            //for (int i = 0; i < BlockDots.GetLength(0); i++)
            //{
            //    for (int j = 0; j < BlockDots.GetLength(1); j++)
            //    {
            //        BlockDots[i, j] = BlockRotations[rotationState][i, j];
            //    }
            //}

            //Console.WriteLine(BlockDots);

        }
    }
}

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

        public int[][,] BlockRotations;

        static int rotationState = 0;

        public BlockUserControl()
        {
            InitializeComponent();
        }

        public void RotateBlock()
        {

            if (rotationState < 3)
            {
                rotationState++;
            }
            else
            {
                rotationState = 0;
            }

            Console.WriteLine(rotationState);

            foreach (int i in BlockRotations[rotationState])
            {
                Console.Write(i.ToString());
            }

            var temp = BlockHeight;
            BlockHeight = BlockWidth;
            BlockWidth = temp;

            BlockDots = new int[BlockWidth, BlockHeight];

            BlockDots = BlockRotations[rotationState];
            
            Console.WriteLine();
            
        }
    }
}

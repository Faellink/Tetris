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

        public BlockUserControl()
        {
            InitializeComponent();
        }
    }
}

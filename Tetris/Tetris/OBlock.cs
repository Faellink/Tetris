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
    public partial class OBlock : TetrisUserControl
    {

        public int[,] oBlockSize = new int[2, 2] { {1, 1}, { 1, 1 } };

        public Color oBlockColor = Color.Yellow;

        public OBlock()
        {
            InitializeComponent();
            BackColor = Color.Yellow;
        }
    }
}

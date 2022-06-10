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
    public partial class Block : UserControl
    {

        public int[,] dots = new int[4, 4];
        public Color blockColor;
        public int xPos;
        public int yPos;
        public int blockID;


        public Block()
        {
            InitializeComponent();
        }
    }
}

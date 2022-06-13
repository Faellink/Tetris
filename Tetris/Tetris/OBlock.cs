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
    public partial class OBlock : Block
    {

        //public int[,] dots = new int[4, 4] { {0,0,0,0},{0,1,1,0 },{0,1,1,0},{0,0,0,0} };
        //public Color blockColor = Color.Yellow;
        //public int xPos;
        //public int yPos;
        //public int blockID = 1;


        public OBlock()
        {
            InitializeComponent();

            //dots = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 1, 1, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 } };
            //blockColor = Color.Yellow;
            //blockID = 1;

            BlockColor = Color.Yellow;

        }
    }
}

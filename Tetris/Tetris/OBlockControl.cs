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
    public partial class OBlockControl : TetrisUserControl
    {

        public int[,] blocks = new int[,] { { 0, 0, 0, 0 }, { 0, 2, 2, 0 }, { 0, 2, 2, 0 }, { 0, 0, 0, 0 } };


        public OBlockControl()
        {
            InitializeComponent();
            this.BackColor = Color.Red;
        }
    }
}

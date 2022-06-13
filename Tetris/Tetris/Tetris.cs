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
    public partial class TetrisForm : Form
    {

        public Block[,] grid = new Block[20, 10];

        //public Point

        public TetrisForm()
        {
            InitializeComponent();

            StartGrid();
        }

        public void StartGrid()
        {
            //20x10

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Block block = new Block();
                    grid[i, j] = block;
                    tableLayoutPanel1.Controls.Add(block,j,i);
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int spawnPoit = 4;

            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    OBlock oB = new OBlock();
                    //grid[i, j + spawnPoit].BlockColor = Color.Yellow;
                    var temp = grid[i, j + spawnPoit];
                    temp.BackColor = Color.Red;
                }
            }

            

            Console.WriteLine("done");

        }
    }
}

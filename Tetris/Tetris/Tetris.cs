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

        public int[,] grid = new int[20, 10];

        public TetrisForm()
        {
            InitializeComponent();

            BoardGrid();
        }

        private void BoardGrid()
        {
            //20x10

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    TetrisUserControl tetrisUC = new TetrisUserControl();
                    tableLayoutPanel1.Controls.Add(tetrisUC, j,i);
                }
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int spanwPoint = 4;

            //grid[0, randoNumber]

            OBlock oBlockT = new OBlock();

            int count = 0;

            for (int i = 0; i < oBlockT.oBlockSize.GetLength(0); i++)
            {
                for (int j = 0; j < oBlockT.oBlockSize.GetLength(1); j++)
                {
                    grid[i, j+spanwPoint] = oBlockT.oBlockSize[i, j];

                    //tableLayoutPanel1.Controls.Add(oBlockT, spanwPoint+j,i);

                    //tableLayoutPanel1.Controls.Remove(tableLayoutPanel1.GetControlFromPosition(j + spanwPoint, i));

                    tableLayoutPanel1.Controls.Add(oBlockT, i, j);

                    count++;
                }
            }

            Console.WriteLine($"Count O: {count}");

            int cCount = 0;

            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c == oBlockT)
                {
                    cCount++;
                    Console.WriteLine($"C count: {cCount}");
                }
            }

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i,j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }
    }
}

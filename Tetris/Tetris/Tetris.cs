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

        Control[,] grid = new Control[20,10];

        public TetrisForm()
        {
            InitializeComponent();

            //this.KeyDown += TetrisForm_KeyDown;



            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    TetrisUserControl tetricUC = new TetrisUserControl();
                    grid[i, j] = tetricUC; 
                    tableLayoutPanel1.Controls.Add(tetricUC,j,i);
                }
            }

            int count = 0;


            foreach (var c in grid)
            {
               
            }

            Console.WriteLine(count);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int spawnPoint = 4;

            

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    OBlock oBlockT = new OBlock();
                    grid[i, j + spawnPoint] = oBlockT;
                    tableLayoutPanel1.Controls.Remove(tableLayoutPanel1.GetControlFromPosition(j+spawnPoint,i));
                    tableLayoutPanel1.Controls.Add(oBlockT, j+ spawnPoint, i);
                }
            }
        }

        private void TetrisForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Right)
            {
                Console.WriteLine("right");
                return true;
            }
            if (keyData == Keys.Left)
            {
                Console.WriteLine("left");
                return true;
            }
            if (keyData == Keys.Up)
            {
                Console.WriteLine("up");
                return true;
            }
            if (keyData == Keys.Down)
            {
                Console.WriteLine("down");
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}

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

        UserControl[,] userTC = new UserControl[20, 10];

        int[,] boardGrid = new int[20, 10];

        public TetrisForm()
        {
            InitializeComponent();
            InitializeBoard();
        }

        void InitializeBoard()
        {
            //20x10 grid

            int countCell = 0;

            //UserControl[,] userTC = new UserControl[20, 10];

            for (int i = 0; i < userTC.GetLength(0); i++)
            {
                for (int j = 0; j < userTC.GetLength(1); j++)
                {
                    TetrisUserControl tetrisUC = new TetrisUserControl();

                    userTC[i, j] = tetrisUC;
                    tableLayoutPanel1.Controls.Add(tetrisUC,j,i);
                }
            }

            foreach (TetrisUserControl t in userTC)
            {
                //tableLayoutPanel1.Controls.Add(TetrisUserControl, );
                t.BackColor = Color.Black;
                countCell++;
            }

            Console.WriteLine(countCell);

            int zeroCell = 0;

            for (int i = 0; i < boardGrid.GetLength(0); i++)
            {
                for (int j = 0; j < boardGrid.GetLength(1); j++)
                {
                    boardGrid[i, j] = zeroCell;
                    Console.Write(boardGrid[i, j] + " ");
                }
                Console.WriteLine();
            }
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            //test block
            //[0 0 0 0]
            //[0 1 1 0]
            //[0 1 1 0]
            //[0 0 0 0]

            //int[,] testBlock = new int[4, 4] { {0,0,0,0 },{0,1,1,0},{0,1,1,0},{0,0,0,0 } };

            //for (int i = 0; i < testBlock.GetLength(0); i++)
            //{
            //    for (int j = 0; j < testBlock.GetLength(1); j++)
            //    {
            //        boardGrid[i, j] = testBlock[i, j];
            //    }
            //}

            //print board grid
            for (int i = 0; i < boardGrid.GetLength(0); i++)
            {
                for (int j = 0; j < boardGrid.GetLength(1); j++)
                {
                    Console.Write(boardGrid[i, j] + " ");
                }
                Console.WriteLine();
            }


            for (int i = 0; i < userTC.GetLength(0); i++)
            {
                for (int j = 0; j < userTC.GetLength(1); j++)
                {
                    
                }
            }

        }
    }
}

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

        public TetrisForm()
        {
            InitializeComponent();
            InitializeBoard();
        }

        void InitializeBoard()
        {
            //20x10 grid

            int countCell = 0;

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
                t.BackColor = Color.Black;
                countCell++;
            }

            Console.WriteLine(countCell);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            OBlockControl oBlock = new OBlockControl();

            Random random = new Random();
            int randomN = random.Next(5,7);

            userTC[0, randomN].BackColor = oBlock.BackColor;
        }
    }
}

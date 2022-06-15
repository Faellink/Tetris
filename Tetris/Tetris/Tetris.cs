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
        public TetrisForm()
        {
            InitializeComponent();
            LoadCanvas();
        }

        Bitmap bitmap;
        Graphics graphics;
        int gridWidth = 10;
        int gridHeight = 20;
        int[,] griDotArray;
        int dotSize = 20;

        private void LoadCanvas()
        {
            picTetris.Width = gridWidth * dotSize;
            picTetris.Height = gridHeight * dotSize;

            bitmap = new Bitmap(picTetris.Width, picTetris.Height);

            graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(Brushes.Black, 0,0,bitmap.Width, bitmap.Height);

            picTetris.Image = bitmap;

            griDotArray = new int[gridHeight,gridWidth];
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < griDotArray.GetLength(0); i++)
            {
                for (int j = 0; j < griDotArray.GetLength(1); j++)
                {
                    Console.Write(griDotArray[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("/////////");
        }
    }
}

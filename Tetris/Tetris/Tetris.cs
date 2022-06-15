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

        private static BlockUserControl[] blocks = new BlockUserControl[] { new OBlock() };

        BlockUserControl currentBlock;

        public TetrisForm()
        {
            InitializeComponent();
            LoadCanvas();

            currentBlock = GetRandomBlock();

            timer1.Tick += TimerTick;
            timer1.Interval = 500;
            timer1.Start();
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

            griDotArray = new int[gridWidth,gridHeight];
        }

        int currentX;
        int currentY;

        private BlockUserControl GetRandomBlock()
        {
            //var shape = ShapesHandler.GetRandomShape();

            //var block = blocks[new Random().Next(blocks.Length)];

            var block = blocks[0];

            // Calculate the x and y values as if the shape lies in the center
            currentX = 4;
            currentY = -block.BlockHeight;

            return block;
        }

        // returns if it reaches the bottom or touches any other blocks
        private bool MoveBlockIfPossible(int moveDown = 0, int moveSide = 0)
        {
            var newX = currentX + moveSide;
            var newY = currentY + moveDown;

            // check if it reaches the bottom or side bar
            if (newX < 0 || newX + currentBlock.BlockWidth > gridWidth || newY + currentBlock.BlockHeight > gridHeight)
                return false;

            // check if it touches any other blocks 
            for (int i = 0; i < currentBlock.BlockWidth; i++)
            {
                for (int j = 0; j < currentBlock.BlockHeight; j++)
                {
                    if (newY + j > 0 && griDotArray[newX + i, newY + j] == 1 && currentBlock.BlockDots[j, i] == 1)
                        return false;
                }
            }

            currentX = newX;
            currentY = newY;

            DrawBlock();

            return true;
        }

        Bitmap workingBitmap;
        Graphics workingGraphics;

        private void DrawBlock()
        {
            workingBitmap = new Bitmap(bitmap);
            workingGraphics = Graphics.FromImage(workingBitmap);

            for (int i = 0; i < currentBlock.BlockWidth; i++)
            {
                for (int j = 0; j < currentBlock.BlockHeight; j++)
                {
                    if (currentBlock.BlockDots[j, i] == 1)
                        workingGraphics.FillRectangle(currentBlock.BlockColor, (currentX + i) * dotSize, (currentY + j) * dotSize, dotSize, dotSize);
                }
            }

            picTetris.Image = workingBitmap;
        }

        private void UpdateGRidDotArrayWithCurrentBlock()
        {
            for (int i = 0; i < currentBlock.BlockWidth; i++)
            {
                for (int j = 0; j < currentBlock.BlockHeight; j++)
                {
                    if (currentBlock.BlockDots[j, i] == 1)
                    {
                        BoolGameOver();

                        griDotArray[currentX + i, currentY + j] = 1;
                    }
                }
            }
        }

        private void BoolGameOver()
        {
            if (currentY < 0)
            {
                timer1.Stop();
                MessageBox.Show("Game Over");
                Application.Restart();
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            var isMoveSuccess = MoveBlockIfPossible(moveDown: 1);

            // if shape reached bottom or touched any other shapes
            if (!isMoveSuccess)
            {
                // copy working image into canvas image
                bitmap = new Bitmap(workingBitmap);

                UpdateGRidDotArrayWithCurrentBlock();

                // get next shape
                currentBlock = GetRandomBlock();
            }
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

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

        private static BlockUserControl[] blocks = new BlockUserControl[] { new OBlock(), new IBlock(), new TBlock(), new LBlock(), new JBlock(), new SBlock(), new ZBlock() };

        //private static BlockUserControl[] blocks = new BlockUserControl[] { new TBlock()};


        private static BlockUserControl[] shuffledBlock = new BlockUserControl[7];

        BlockUserControl currentBlock;

        public TetrisForm()
        {
            InitializeComponent();
            LoadCanvas();

            ShuffleBlocksArray(blocks);
            currentBlock = GetRandomBlock();

            timer1.Tick += TimerTick;
            timer1.Interval = 500;
            timer1.Start();
        }


        Bitmap bitmap;
        Graphics graphics;
        int gridWidth = 10;
        int gridHeight = 20;
        int[,] gridDotArray;
        int dotSize = 20;

        private void LoadCanvas()
        {
            picTetris.Width = gridWidth * dotSize;
            picTetris.Height = gridHeight * dotSize;

            bitmap = new Bitmap(picTetris.Width, picTetris.Height);

            graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(Brushes.Black, 0,0,bitmap.Width, bitmap.Height);

            picTetris.Image = bitmap;

            gridDotArray = new int[gridHeight,gridWidth];
        }

        private BlockUserControl[] ShuffleBlocksArray(BlockUserControl[] blocks)
        {
            Random random = new Random();

            Array.Copy(blocks, shuffledBlock, blocks.Length);

            for (int i = shuffledBlock.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                var temp = shuffledBlock[i];
                shuffledBlock[i] = shuffledBlock[j];
                shuffledBlock[j] = temp;
            }

            foreach (BlockUserControl sBlock in shuffledBlock)
            {
                Console.WriteLine(sBlock.ToString());
            }
            Console.WriteLine("//////////////");

            return shuffledBlock;
        }

        int currentX;
        int currentY;
        int blocksGenerated = 0;

        private BlockUserControl GetRandomBlock()
        {
            //var shape = ShapesHandler.GetRandomShape();

            //var block = shuffledBlock[new Random().Next(shuffledBlock.Length)];

            var block = shuffledBlock[blocksGenerated];

            

            // Calculate the x and y values as if the shape lies in the center
            currentX = 4;
            currentY = -block.BlockHeight;

            blocksGenerated++;

            if (blocksGenerated==7)
            {
                ShuffleBlocksArray(shuffledBlock);
                blocksGenerated = 0;
            }

            //Console.WriteLine(shuffledBlock[blocksGenerated + 1]);


            block.ResetRotation();
            Console.WriteLine("Rotation State: "+ block.RotationState);
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
                    if (newY + j > 0 && gridDotArray[newY + j, newX + i] == 1 && currentBlock.BlockDots[j, i] == 1)
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

        private void UpdateGridDotArrayWithCurrentBlock()
        {
            for (int i = 0; i < currentBlock.BlockWidth; i++)
            {
                for (int j = 0; j < currentBlock.BlockHeight; j++)
                {
                    if (currentBlock.BlockDots[j, i] == 1)
                    {
                        BoolGameOver();

                        gridDotArray[currentY + j, currentX + i] = 1;
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
                //Application.Restart();
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

                UpdateGridDotArrayWithCurrentBlock();

                // get next shape
                currentBlock = GetRandomBlock();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridDotArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridDotArray.GetLength(1); j++)
                {
                    Console.Write(gridDotArray[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("/////////");
        }

        private void MoveLeftRight(int moveLeftRight)
        {
            MoveBlockIfPossible(moveSide: moveLeftRight);
        }

        private void MoveDown(int dropPiece)
        {
            MoveBlockIfPossible(moveDown: dropPiece);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Right)
            {
                //Console.WriteLine("right");
                MoveLeftRight(1);
                return true;
            }
            if (keyData == Keys.Left)
            {
                //Console.WriteLine("left");
                MoveLeftRight(-1);
                return true;
            }
            if (keyData == Keys.Up)
            {
                //Console.WriteLine("up");
                currentBlock.RotateBlock();
                return true;
            }
            if (keyData == Keys.Down)
            {
                //Console.WriteLine("down");
                MoveDown(1);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

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

        private static BlockUserControl[] shuffledBlock = new BlockUserControl[blocks.Length];

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
            var block = shuffledBlock[blocksGenerated];

            currentX = 4;
            currentY = -block.BlockHeight;

            blocksGenerated++;

            if (blocksGenerated==7)
            {
                ShuffleBlocksArray(shuffledBlock);
                blocksGenerated = 0;
            }

            return block;
        }

        private bool MoveBlockIfPossible(int moveDown = 0, int moveSide = 0)
        {
            var newX = currentX + moveSide;
            var newY = currentY + moveDown;

            if (newX < 0 || newX + currentBlock.BlockWidth > gridWidth || newY + currentBlock.BlockHeight > gridHeight)
                return false;

            for (int i = 0; i < currentBlock.BlockWidth; i++)
            {
                for (int j = 0; j < currentBlock.BlockHeight; j++)
                {
                    if (newY + j > 0 && gridDotArray[newY + j, newX + i] != 0 && currentBlock.BlockDots[j, i] != 0)
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

            for (int i = 0; i < currentBlock.BlockDots.GetLength(1); i++)
            {
                for (int j = 0; j < currentBlock.BlockDots.GetLength(0); j++)
                {
                    if (currentBlock.BlockDots[j, i] != 0)
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
                    if (currentBlock.BlockDots[j, i] != 0)
                    {
                        BoolGameOver();

                        if (gameover == false)
                        {
                            gridDotArray[currentY + j, currentX + i] = currentBlock.BlockID;
                        }
                    }
                }
            }
        }

        public void CheckRows()
        {
            int clearedRows = 0;

            for (int i = gridHeight-1; i >= 0 ; i--)
            {
                if (CheckRowFull(i))
                {
                    ClearRow(i);
                    clearedRows++;
                }else if (clearedRows>0)
                {
                    MoveRowDown(i, clearedRows);
                }
            }

            PrintGridDotArray();
            UpdateBitmap();
        }

        private void MoveRowDown(int i, int clearedRows)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                gridDotArray[i + clearedRows, j] = gridDotArray[i, j];
            }
        }

        private void ClearRow(int i)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                gridDotArray[i, j] = 0;
            }
        }

        private bool CheckRowFull(int i)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                if (gridDotArray[i,j] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void UpdateBitmap()
        {
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {

                    graphics = Graphics.FromImage(bitmap);

                    switch (gridDotArray[j,i])
                    {
                        case 1:
                            graphics.FillRectangle(Brushes.Blue,i * dotSize, j * dotSize, dotSize, dotSize);
                            break;
                        case 2:
                            graphics.FillRectangle(Brushes.Orange, i * dotSize, j * dotSize, dotSize, dotSize);
                            break;
                        case 3:
                            graphics.FillRectangle(Brushes.Purple, i * dotSize, j * dotSize, dotSize, dotSize);
                            break;
                        case 4:
                            graphics.FillRectangle(Brushes.LightGreen, i * dotSize, j * dotSize, dotSize, dotSize);
                            break;
                        case 5:
                            graphics.FillRectangle(Brushes.DarkGreen, i * dotSize, j * dotSize, dotSize, dotSize);
                            break;
                        case 6:
                            graphics.FillRectangle(Brushes.Red, i * dotSize, j * dotSize, dotSize, dotSize);
                            break;
                        case 7:
                            graphics.FillRectangle(Brushes.Salmon, i * dotSize, j * dotSize, dotSize, dotSize);
                            break;
                        default:
                            graphics.FillRectangle(Brushes.Black, i * dotSize, j * dotSize, dotSize, dotSize);
                            break;
                    }
                }
            }

            picTetris.Image = bitmap;
        }

        public bool gameover = false;
        private void BoolGameOver()
        {
            if (currentY < 0)
            {
                timer1.Stop();
                MessageBox.Show("Game Over");
                //Application.Restart();
                //Application.Exit();
                gameover = true;
            }

        }

        private void TimerTick(object sender, EventArgs e)
        {
            var isMoveSuccess = MoveBlockIfPossible(moveDown: 1);

            if (!isMoveSuccess)
            {
                bitmap = new Bitmap(workingBitmap);

                UpdateGridDotArrayWithCurrentBlock();

                currentBlock = GetRandomBlock();

                CheckRows();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            PrintGridDotArray();
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

            var verticalMove = 0;
            var horizontalMove = 0;

            switch (keyData)
            {
                case Keys.Left:
                    verticalMove--;
                    break;
                case Keys.Right:
                    verticalMove++;
                    break;
                case Keys.Down:
                    horizontalMove++;
                    break;
                case Keys.Up:
                    currentBlock.RotateBlock();
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

            var isMoveSuccess = MoveBlockIfPossible(horizontalMove, verticalMove);

            if (!isMoveSuccess && keyData == Keys.Up)
                currentBlock.RollbackBlock();


            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void PrintGridDotArray()
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
    }
}

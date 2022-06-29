using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class TetrisForm : Form
    {

        public enum GameState
        {
            Unpaused,
            Paused
        }

        

        //GameState gameState;

        private static BlockUserControl[] blocks = new BlockUserControl[] { new OBlock(), new IBlock(), new TBlock(), new LBlock(), new JBlock(), new SBlock(), new ZBlock() };

        private static BlockUserControl[] shuffledBlock = new BlockUserControl[blocks.Length];

        BlockUserControl currentBlock;

        public TetrisForm()
        {
            InitializeComponent();
            LoadCanvas();

            //gameState = GameState.Paused;
            GameStateMachine(GameState.Paused);

            btnRestart.Enabled = false;

            btnPause.Enabled = false;
            btnPause.Text = "PAUSE";

            //ShuffleBlocksArray(blocks);
            //currentBlock = GetRandomBlock();

            //timer1.Tick += TimerTick;
            //timer1.Interval = 500;
            //timer1.Start();
        }

        public void PreLoadGAme()
        {
            LoadCanvas();

            //gameState = GameState.Paused;
            GameStateMachine(GameState.Paused);

            btnRestart.Enabled = false;

            btnPause.Enabled = false;
            btnPause.Text = "PAUSE";

            //ShuffleBlocksArray(blocks);
            //currentBlock = GetRandomBlock();

            //timer1.Tick += TimerTick;
            //timer1.Interval = 500;
            //timer1.Start();

        }

        public int score = 0;

        public void StartGame()
        {
            //LoadCanvas();

            //gameState = GameState.Unpaused;

            

            btnRestart.Enabled = true;

            btnPause.Enabled = true;
            GameStateMachine(GameState.Unpaused);

            score = 0;
            UpdateScoreCounter(score);

            ShuffleBlocksArray(blocks);
            currentBlock = GetRandomBlock();

            btnStart.Enabled = false;

            timer1.Tick += TimerTick;
            timer1.Interval = 500;
            timer1.Start();
            
        }

        public void GameStateMachine(GameState game)
        {
            switch (game)
            {
                case GameState.Unpaused:
                    isPaused = false;
                    btnPause.Text = "PAUSE";
                    timer1.Start();
                    break;
                case GameState.Paused:
                    timer1.Stop();
                    btnPause.Text = "RESUME";
                    isPaused = true;
                    break;
            }
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
            if (!BoolGameOver())
            {
                for (int i = 0; i < currentBlock.BlockWidth; i++)
                {
                    for (int j = 0; j < currentBlock.BlockHeight; j++)
                    {
                        if (currentBlock.BlockDots[j, i] != 0)
                        {
                           gridDotArray[currentY + j, currentX + i] = currentBlock.BlockID;
                        }
                    }
                }
            }
            else
            {
                GameOver();
                MessageBox.Show("Game Over");
            }
        }

        int clearedRows = 0;

        public void CheckRows(int Row, int Column, int clearedRows)
        {

            if (0 <= Row && Row <= gridHeight-1 )
            {
                CheckIfRowIsFull(Row,Column);

                if (sum == 10)
                {
                    ClearRow(Row,Column);
                    clearedRows++;
                    
                    if (clearedRows==4)
                    {
                        UpdateScoreCounter(500);
                    }
                    else
                    {
                        UpdateScoreCounter(50);
                    }

                    Console.WriteLine(timer1.Interval.ToString());
                    timer1.Interval -= 100;

                }else if (clearedRows > 0 )
                {
                    MoveRowDown(Row,Column, clearedRows);
                }

                Row--;
                sum = 0;
                CheckRows(Row, Column, clearedRows);
            }
            else
            {
                //PrintGridDotArray();
                clearedRows = 0;
                UpdateBitmap();
                return;
            }
        }

        public int sum = 0;

        public void CheckIfRowIsFull(int Row, int Column)
        {
            if (0 <= Column && Column <= gridHeight - 1)
            {
                if (gridDotArray[Row, Column] != 0)
                {
                    sum++;
                }
                Column--;
                CheckIfRowIsFull(Row,Column);
            }
            else
            {
                return;
            }
        }

        private void MoveRowDown(int Row, int Column, int clearedRows)
        {
            if (0 <= Column && Column <= gridHeight - 1)
            {
                gridDotArray[Row + clearedRows, Column] = gridDotArray[Row, Column];
                Column--;
                MoveRowDown(Row, Column, clearedRows);
            }
            else
            {
                return;
            }
        }

        private void ClearRow(int Row, int Column)
        {
            if (0 <= Column && Column <= gridHeight - 1)
            {
                gridDotArray[Row, Column] = 0;
                Column--;
                ClearRow(Row, Column);
            }
            else
            {
                return;
            }
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
        private bool BoolGameOver()
        {
            if (currentY < 0)
            {
                Console.WriteLine("game over ");
                return true;
            }
            else
            {
                Console.WriteLine("continue ");
                return false;
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

                CheckRows(gridHeight - 1, gridWidth - 1, clearedRows);

            }
            Console.WriteLine($"tick {timer1.Interval.ToString()}");

        }

        private void UpdateScoreCounter(int bonus)
        {
            score += bonus;
            lblScore.Text = ($"SCORE: {score}");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGame();
            lblScore.Focus();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {

            ResetGridDotArray();

            currentBlock = GetRandomBlock();
            timer1.Interval = 500;
            score = 0;
            lblScore.Focus();
        }

        private void GameOver()
        {
            ResetGridDotArray();

            timer1.Stop();
            timer1.Tick -= TimerTick;
            timer1.Dispose();

            btnRestart.Enabled = false;

            btnPause.Enabled = false;
            btnPause.Text = "PAUSE";


            Console.WriteLine($"toc {timer1.Interval.ToString()}");
            btnStart.Enabled = true;
        }


        public bool isPaused; 

        private void btnPause_Click(object sender, EventArgs e)
        {
            //PrintGridDotArray();
            //gameState = GameState.Paused;
            if (isPaused == false)
            {
                GameStateMachine(GameState.Paused);
            }
            else
            {
                GameStateMachine(GameState.Unpaused);
            }
            
            lblScore.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (isPaused == false)
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
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);

            }
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

        private void ResetGridDotArray()
        {
            for (int i = 0; i < gridDotArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridDotArray.GetLength(1); j++)
                {
                    gridDotArray[i, j] = 0;
                }
            }
            Console.WriteLine("cleared");

            PrintGridDotArray();
            
            UpdateBitmap();
        }

    }
}

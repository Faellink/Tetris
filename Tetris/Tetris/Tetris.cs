using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class TetrisForm : Form
    {

        private static BlockUserControl[] blocksArray = new BlockUserControl[] { new OBlock(), new IBlock(), new TBlock(),
                                                                                 new LBlock(), new JBlock(), new SBlock(), 
                                                                                 new ZBlock() };

        private static BlockUserControl[] shuffledBlockArray = new BlockUserControl[blocksArray.Length];

        BlockUserControl currentBlock;

        Bitmap bitmap;
        Graphics graphics;

        Bitmap workingBitmap;
        Graphics workingGraphics;

        int gridWidth = 10;
        int gridHeight = 20;
        int[,] gridArray;
        int blockGraphicSize = 20;

        int currentPositionX;
        int currentPositionY;
        int blocksGenerated = 0;

        int gridArrayCellContaisBlock = 0;
        int clearedRows = 0;
        int score = 0;

        bool isPaused;

        public enum GameState
        {
            Unpaused,
            Paused
        }

        string[] boardGridStringArray;
        DateTime matchDate;

        int gameOverInt;

        public TetrisForm()
        {
            InitializeComponent();
            LoadGame();

            boardGridStringArray = new string[(gridHeight * gridWidth)];
            Console.WriteLine(boardGridStringArray.Length);
        }

        public void LoadGame()
        {
            LoadTetrisBoard();

            GameStateSwitch(GameState.Paused);

            btnSave.Enabled = false;
            btnRestart.Enabled = false;
            btnPause.Enabled = false;
            btnPause.Text = "PAUSE";
        }

        private void LoadTetrisBoard()
        {
            picTetris.Width = gridWidth * blockGraphicSize;
            picTetris.Height = gridHeight * blockGraphicSize;

            bitmap = new Bitmap(picTetris.Width, picTetris.Height);

            graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(Brushes.Black, 0, 0, bitmap.Width, bitmap.Height);

            picTetris.Image = bitmap;

            gridArray = new int[gridHeight, gridWidth];
        }

        public void GameStateSwitch(GameState state)
        {
            switch (state)
            {
                case GameState.Unpaused:
                    isPaused = false;
                    btnSave.Enabled = false;
                    btnLoad.Enabled = false;
                    txtIdGameToLoad.ReadOnly = true;
                    gameTimer.Start();
                    btnPause.Text = "PAUSE";
                    break;
                case GameState.Paused:
                    isPaused = true;
                    btnSave.Enabled = true;
                    btnLoad.Enabled = true;
                    txtIdGameToLoad.ReadOnly = false;
                    gameTimer.Stop();
                    btnPause.Text = "RESUME";
                    break;
            }
        }

        public void StartGame()
        {

            matchDate = DateTime.Now;

            Console.WriteLine(matchDate);

            btnRestart.Enabled = true;
            btnPause.Enabled = true;

            GameStateSwitch(GameState.Unpaused);

            score = 0;
            UpdateScoreCounter(score);

            ShuffleBlocksArray(blocksArray);
            currentBlock = GetRandomBlock();

            btnStart.Enabled = false;

            gameTimer.Tick += TimerTick;
            gameTimer.Interval = 500;
            gameTimer.Start();
        }

        private BlockUserControl[] ShuffleBlocksArray(BlockUserControl[] blocks)
        {
            Random random = new Random();

            Array.Copy(blocks, shuffledBlockArray, blocks.Length);

            for (int i = shuffledBlockArray.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                var temp = shuffledBlockArray[i];
                shuffledBlockArray[i] = shuffledBlockArray[j];
                shuffledBlockArray[j] = temp;
            }

            return shuffledBlockArray;
        }

        private BlockUserControl GetRandomBlock()
        {
            var block = shuffledBlockArray[blocksGenerated];

            currentPositionX = 4;
            currentPositionY = -block.BlockHeight;

            blocksGenerated++;

            if (blocksGenerated==7)
            {
                ShuffleBlocksArray(shuffledBlockArray);
                blocksGenerated = 0;
            }

            return block;
        }

        private bool MoveBlockIfPossible(int moveDown = 0, int moveSide = 0)
        {
            var newX = currentPositionX + moveSide;
            var newY = currentPositionY + moveDown;

            if (newX < 0 || newX + currentBlock.BlockWidth > gridWidth || newY + currentBlock.BlockHeight > gridHeight)
                return false;

            for (int i = 0; i < currentBlock.BlockWidth; i++)
            {
                for (int j = 0; j < currentBlock.BlockHeight; j++)
                {
                    if (newY + j > 0 && gridArray[newY + j, newX + i] != 0 && currentBlock.BlockDots[j, i] != 0)
                        return false;
                }
            }

            currentPositionX = newX;
            currentPositionY = newY;

            DrawBlock();

            return true;
        }


        private void DrawBlock()
        {
            workingBitmap = new Bitmap(bitmap);
            workingGraphics = Graphics.FromImage(workingBitmap);

            for (int i = 0; i < currentBlock.BlockDots.GetLength(1); i++)
            {
                for (int j = 0; j < currentBlock.BlockDots.GetLength(0); j++)
                {
                    if (currentBlock.BlockDots[j, i] != 0)
                        workingGraphics.FillRectangle(currentBlock.BlockColor, (currentPositionX + i) * blockGraphicSize, (currentPositionY + j) * blockGraphicSize, blockGraphicSize, blockGraphicSize);
                }
            }

            picTetris.Image = workingBitmap;
        }

        private void UpdateGridArrayWithCurrentBlock()
        {
            if (!CheckGameOver())
            {
                for (int i = 0; i < currentBlock.BlockWidth; i++)
                {
                    for (int j = 0; j < currentBlock.BlockHeight; j++)
                    {
                        if (currentBlock.BlockDots[j, i] != 0)
                        {
                           gridArray[currentPositionY + j, currentPositionX + i] = currentBlock.BlockID;
                        }
                    }
                }
            }
            else
            {
                GameOver();
                SendGameDataToDB(boardGridStringArray, score, matchDate, 1);
                ReceiveGameDataFromDB();
                MessageBox.Show("Game Over");
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            var isMoveSuccess = MoveBlockIfPossible(moveDown: 1);

            if (!isMoveSuccess)
            {
                bitmap = new Bitmap(workingBitmap);

                UpdateGridArrayWithCurrentBlock();

                currentBlock = GetRandomBlock();

                CheckRows(gridHeight - 1, gridWidth - 1, clearedRows);
            }
        }

        public void CheckRows(int Row, int Column, int clearedRows)
        {
            if (0 <= Row && Row <= gridHeight-1 )
            {
                IsRowFull(Row,Column);

                if (gridArrayCellContaisBlock == 10)
                {
                    ClearFullRow(Row,Column);
                    clearedRows++;
                    
                    if (clearedRows==4)
                    {
                        UpdateScoreCounter(500);
                    }
                    else
                    {
                        UpdateScoreCounter(50);
                    }

                    if (gameTimer.Interval <= 20)
                    {
                        gameTimer.Interval = 20;
                    }
                    else
                    {
                        gameTimer.Interval -= 5;
                    }

                }else if (clearedRows > 0 )
                {
                    MoveRowsDown(Row,Column, clearedRows);
                }

                Row--;
                gridArrayCellContaisBlock = 0;
                CheckRows(Row, Column, clearedRows);
            }
            else
            {
                clearedRows = 0;
                UpdateBitmap();
                return;
            }
        }

        public void IsRowFull(int Row, int Column)
        {
            if (0 <= Column && Column <= gridHeight - 1)
            {
                if (gridArray[Row, Column] != 0)
                {
                    gridArrayCellContaisBlock++;
                }
                Column--;
                IsRowFull(Row,Column);
            }
            else
            {
                return;
            }
        }

        private void MoveRowsDown(int Row, int Column, int clearedRows)
        {
            if (0 <= Column && Column <= gridHeight - 1)
            {
                gridArray[Row + clearedRows, Column] = gridArray[Row, Column];
                Column--;
                MoveRowsDown(Row, Column, clearedRows);
            }
            else
            {
                return;
            }
        }

        private void ClearFullRow(int Row, int Column)
        {
            if (0 <= Column && Column <= gridHeight - 1)
            {
                gridArray[Row, Column] = 0;
                Column--;
                ClearFullRow(Row, Column);
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

                    switch (gridArray[j,i])
                    {
                        case 1:
                            graphics.FillRectangle(Brushes.Blue,i * blockGraphicSize, j * blockGraphicSize, blockGraphicSize, blockGraphicSize);
                            break;
                        case 2:
                            graphics.FillRectangle(Brushes.Orange, i * blockGraphicSize, j * blockGraphicSize, blockGraphicSize, blockGraphicSize);
                            break;
                        case 3:
                            graphics.FillRectangle(Brushes.Purple, i * blockGraphicSize, j * blockGraphicSize, blockGraphicSize, blockGraphicSize);
                            break;
                        case 4:
                            graphics.FillRectangle(Brushes.LightGreen, i * blockGraphicSize, j * blockGraphicSize, blockGraphicSize, blockGraphicSize);
                            break;
                        case 5:
                            graphics.FillRectangle(Brushes.DarkGreen, i * blockGraphicSize, j * blockGraphicSize, blockGraphicSize, blockGraphicSize);
                            break;
                        case 6:
                            graphics.FillRectangle(Brushes.Red, i * blockGraphicSize, j * blockGraphicSize, blockGraphicSize, blockGraphicSize);
                            break;
                        case 7:
                            graphics.FillRectangle(Brushes.Salmon, i * blockGraphicSize, j * blockGraphicSize, blockGraphicSize, blockGraphicSize);
                            break;
                        default:
                            graphics.FillRectangle(Brushes.Black, i * blockGraphicSize, j * blockGraphicSize, blockGraphicSize, blockGraphicSize);
                            break;
                    }
                }
            }
            picTetris.Image = bitmap;
        }

        private bool CheckGameOver()
        {
            if (currentPositionY < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateScoreCounter(int bonus)
        {
            score += bonus;
            lblScore.Text = ($"SCORE: {score}");
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            StartGame();
            lblScore.Focus();
        }

        private void RestartButtonClick(object sender, EventArgs e)
        {
            ResetGridArray();

            currentBlock = GetRandomBlock();
            gameTimer.Interval = 500;
            score = 0;
            UpdateScoreCounter(score);
            btnSave.Enabled = false;
            lblScore.Focus();
        }

        private void GameOver()
        {

            ResetGridArray();

            gameTimer.Stop();
            gameTimer.Tick -= TimerTick;
            gameTimer.Dispose();

            btnRestart.Enabled = false;

            btnPause.Enabled = false;
            btnPause.Text = "PAUSE";

            btnStart.Enabled = true;
        }

        private void PauseButtonClick(object sender, EventArgs e)
        {
            if (isPaused == false)
            {
                GameStateSwitch(GameState.Paused);
            }
            else
            {
                GameStateSwitch(GameState.Unpaused);
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

        //public void PrintGridDotArray()
        //{
        //    for (int i = 0; i < gridArray.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < gridArray.GetLength(1); j++)
        //        {
        //            Console.Write(gridArray[i, j]);
        //        }
        //        Console.WriteLine();
        //    }
        //    Console.WriteLine("/////////");
        //}

        private void ResetGridArray()
        {
            for (int i = 0; i < gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridArray.GetLength(1); j++)
                {
                    gridArray[i, j] = 0;
                }
            }

            UpdateBitmap();
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            int boardGridIndex = 0;

            for (int i = 0; i < gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridArray.GetLength(1); j++)
                {
                    boardGridStringArray[boardGridIndex] += gridArray[i, j] + ","; 
                    boardGridIndex++;
                }
            }

            SendGameDataToDB(boardGridStringArray,score, matchDate,0);
            ReceiveGameDataFromDB();
        }

        private void SendGameDataToDB(string[] boardGridStringArray, int score, DateTime matchDate,int gameOverInt)
        {

            var stringBoard = String.Join("", boardGridStringArray);

            string connectionString = "Data Source=SQO-197;Initial Catalog=TetrisDB;Persist Security Info=True;User ID=sa;Password=sequor";

            SqlConnection connection = new SqlConnection(connectionString);

            string sql = "INSERT INTO TetrisGameResults(GameBoard, Score, MatchDate, GameOver)" + "VALUES (@gameBoard, @score, @matchDate, @gameOver)";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.Add(new SqlParameter("@gameBoard", stringBoard));
            command.Parameters.Add(new SqlParameter("@score", score));
            command.Parameters.Add(new SqlParameter("@matchDate", matchDate));
            command.Parameters.Add(new SqlParameter("@gameOver", gameOverInt));

            connection.Open();

            int result = command.ExecuteNonQuery();

            if (result < 0)
            {
                MessageBox.Show("Error inserting data into DataBase");
            }

            command.Dispose();
            connection.Close();
        }

        void ReceiveGameDataFromDB()
        {
            string connectionString = "Data Source=SQO-197;Initial Catalog=TetrisDB;Persist Security Info=True;User ID=sa;Password=sequor";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string sql = "SELECT TOP 1(GameID) FROM TetrisGameResults ORDER BY GameID DESC";
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader != null)
            {
                while (reader.Read())
                {
                    var tempGameId = reader.GetInt32(0).ToString();
                    MessageBox.Show($"Saved as GameID {tempGameId}.\nUse this ID to Load your game and resume playing!");
                    return;
                }
            }

            command.Dispose();
            connection.Close();
        }

        private void LoadButtonCLick(object sender, EventArgs e)
        {
            int gameID = int.Parse(txtIdGameToLoad.Text);
            Console.WriteLine($"Loading game {gameID}");
            CheckGameIdOnDB(gameID);
        }

        private void CheckGameIdOnDB(int gameID)
        {
            string connectionString = "Data Source=SQO-197;Initial Catalog=TetrisDB;Persist Security Info=True;User ID=sa;Password=sequor";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string sql = ($"SELECT COUNT(*) FROM TetrisGameResults WHERE GameID = {gameID}");
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader != null)
            {
                while (reader.Read())
                {
                    var tempGameId = reader.GetInt32(0);
                    if (tempGameId == 1)
                    {
                        reader.Close();
                        Console.WriteLine("game found");
                        LoadGame(connection,gameID);
                    }
                    else
                    {
                        Console.WriteLine("game not found");
                    }
                    return;
                }
            }

            command.Dispose();
            connection.Close();
        }

        private void LoadGame(SqlConnection connection,int gameID)
        {
            string sql = ($"SELECT * FROM TetrisGameResults WHERE GameID = { gameID}");
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader != null)
            {
                while (reader.Read())
                {
                    int tempGameOverInt = (int)(reader["GameOver"]);
                    if (tempGameOverInt == 0)
                    {
                        Console.WriteLine(tempGameOverInt);
                        int tempScore = (int)reader["Score"];
                        score = tempScore;
                        UpdateScoreCounter(score);
                        string tempGameBoard = reader["GameBoard"].ToString();
                        Console.WriteLine(tempGameBoard.ToString());
                        ConvertBoardFromDB(tempGameBoard);
                    }
                    else
                    {
                        MessageBox.Show("Can't load game");
                    }
                    return;
                }
            }
            command.Dispose();
            connection.Close();
        }

        private void ConvertBoardFromDB(string tempGameBoard)
        {
            boardGridStringArray = tempGameBoard.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int boardGridIndex = 0;

            for (int i = 0; i < gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < gridArray.GetLength(1); j++)
                {
                    gridArray[i, j] = int.Parse(boardGridStringArray[boardGridIndex]);
                    boardGridIndex++;
                }
            }

            UpdateBitmap();
        }
    }
}

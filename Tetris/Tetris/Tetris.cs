﻿using System;
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

        Block[,] currentBlock = new Block[4, 4];

        Block[] tempBlock = new Block[4];

        public Block[,] gridBlock = new Block[20, 10];

        //public Point

        public TetrisForm()
        {
            InitializeComponent();

            StartGrid();
        }

        public void StartGrid()
        {
            //20x10

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    //Block block = new Block();
                    //grid[i, j] = block;
                    //tableLayoutPanel1.Controls.Add(block, j, i);

                    grid[i, j] = 0;
                    Block block = new Block();
                    gridBlock[i, j] = block;
                    tableLayoutPanel1.Controls.Add(block, j, i);
                }
            }

            

            UpdateGrid();
        }

        private void UpdateGrid()
        {
            foreach (int i in grid)
            {
                if (i != 0)
                {
                    
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int spawnPoit = 4;

            int[,] block = new int[4, 4] { {0,0,0,0},
                                           {0,0,0,1},
                                           {0,1,1,1},
                                           {0,0,0,0}
                                          };

            for (int i = 0; i < block.GetLength(0); i++)
            {
                for (int j = 0; j < block.GetLength(1); j++)
                {
                    //OBlock oB = new OBlock();
                    //grid[i, j + spawnPoit].BlockColor = Color.Yellow;
                    //var temp = grid[i, j + spawnPoit];
                    //temp.BackColor = Color.Red;

                    if (block[i, j] == 1)
                    {
                        grid[i, j+ spawnPoit] = block[i, j];
                        //var test = grid[block[i, j], block[i, j] + spawnPoit];
                        //test.BackColor = Color.Yellow;
                        var temp = gridBlock[i, j + spawnPoit];
                        temp.BackColor = Color.Red;
                        //currentBlock[i, j] = grid[i, j+spawnPoit];
                    }

                }
            }



            Console.WriteLine("done");
            DebugGrid();

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (currentBlock[i,j] != null)
                    {
                        currentBlock[i, j].BackColor = Color.Aquamarine;
                    }
                }
            }
        }

        void DebugGrid()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i,j]+ " ");
                }
                Console.WriteLine();
            }
        }
    }
}


namespace Tetris
{
    partial class TetrisForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.lblScore = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picTetris = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblIdText = new System.Windows.Forms.Label();
            this.txtIdGameToLoad = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTetris)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(68, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.TabStop = false;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(68, 42);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(75, 23);
            this.btnRestart.TabIndex = 2;
            this.btnRestart.TabStop = false;
            this.btnRestart.Text = "RESTART";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.RestartButtonClick);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(68, 71);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 3;
            this.btnPause.TabStop = false;
            this.btnPause.Text = "PAUSE";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.PauseButtonClick);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(42, 16);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(44, 13);
            this.lblScore.TabIndex = 0;
            this.lblScore.Text = "SCORE";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picTetris);
            this.panel1.Location = new System.Drawing.Point(89, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 400);
            this.panel1.TabIndex = 5;
            // 
            // picTetris
            // 
            this.picTetris.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picTetris.Location = new System.Drawing.Point(0, 0);
            this.picTetris.Name = "picTetris";
            this.picTetris.Size = new System.Drawing.Size(200, 400);
            this.picTetris.TabIndex = 0;
            this.picTetris.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRestart);
            this.panel2.Controls.Add(this.btnStart);
            this.panel2.Controls.Add(this.btnPause);
            this.panel2.Location = new System.Drawing.Point(385, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(204, 111);
            this.panel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblScore);
            this.panel3.Location = new System.Drawing.Point(385, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(204, 44);
            this.panel3.TabIndex = 0;
            this.panel3.TabStop = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblIdText);
            this.panel4.Controls.Add(this.txtIdGameToLoad);
            this.panel4.Controls.Add(this.btnLoad);
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Location = new System.Drawing.Point(385, 195);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(204, 204);
            this.panel4.TabIndex = 7;
            // 
            // lblIdText
            // 
            this.lblIdText.AutoSize = true;
            this.lblIdText.Location = new System.Drawing.Point(42, 65);
            this.lblIdText.Name = "lblIdText";
            this.lblIdText.Size = new System.Drawing.Size(126, 13);
            this.lblIdText.TabIndex = 10;
            this.lblIdText.Text = "Input GAME ID to LOAD ";
            // 
            // txtIdGameToLoad
            // 
            this.txtIdGameToLoad.Location = new System.Drawing.Point(44, 90);
            this.txtIdGameToLoad.Name = "txtIdGameToLoad";
            this.txtIdGameToLoad.Size = new System.Drawing.Size(124, 20);
            this.txtIdGameToLoad.TabIndex = 9;
            this.txtIdGameToLoad.TabStop = false;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(67, 116);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 8;
            this.btnLoad.TabStop = false;
            this.btnLoad.Text = "LOAD";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.LoadButtonCLick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(68, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // TetrisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 448);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TetrisForm";
            this.Text = "TETRIS";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTetris)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picTetris;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblIdText;
        private System.Windows.Forms.TextBox txtIdGameToLoad;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
    }
}


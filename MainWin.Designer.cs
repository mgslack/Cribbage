namespace Cribbage
{
    partial class MainWin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
            this.pbUpCard = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblGW = new System.Windows.Forms.Label();
            this.lblCardsLeft = new System.Windows.Forms.Label();
            this.lblPlayedTally = new System.Windows.Forms.Label();
            this.lblCompScore = new System.Windows.Forms.Label();
            this.lblHumScore = new System.Windows.Forms.Label();
            this.lblCompWon = new System.Windows.Forms.Label();
            this.lblHumWon = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.lblCrib = new System.Windows.Forms.Label();
            this.pbCard1 = new System.Windows.Forms.PictureBox();
            this.pbCard2 = new System.Windows.Forms.PictureBox();
            this.pbCard3 = new System.Windows.Forms.PictureBox();
            this.pbCard4 = new System.Windows.Forms.PictureBox();
            this.pbCard5 = new System.Windows.Forms.PictureBox();
            this.pbCard6 = new System.Windows.Forms.PictureBox();
            this.btnCard1 = new System.Windows.Forms.Button();
            this.btnCard2 = new System.Windows.Forms.Button();
            this.btnCard3 = new System.Windows.Forms.Button();
            this.btnCard4 = new System.Windows.Forms.Button();
            this.btnCard5 = new System.Windows.Forms.Button();
            this.btnCard6 = new System.Windows.Forms.Button();
            this.lblWinPts = new System.Windows.Forms.Label();
            this.playedCanvas = new Cribbage.PlayedCanvas();
            this.cribBoard = new Cribbage.CribbageBoard();
            ((System.ComponentModel.ISupportInitialize)(this.pbUpCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard6)).BeginInit();
            this.SuspendLayout();
            // 
            // pbUpCard
            // 
            this.pbUpCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbUpCard.Location = new System.Drawing.Point(122, 183);
            this.pbUpCard.Name = "pbUpCard";
            this.pbUpCard.Size = new System.Drawing.Size(73, 98);
            this.pbUpCard.TabIndex = 1;
            this.pbUpCard.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cards Left (comps):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Played Cards Tally:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Current Scores:";
            // 
            // lblGW
            // 
            this.lblGW.AutoSize = true;
            this.lblGW.Location = new System.Drawing.Point(210, 249);
            this.lblGW.Name = "lblGW";
            this.lblGW.Size = new System.Drawing.Size(69, 13);
            this.lblGW.TabIndex = 5;
            this.lblGW.Text = "Games Won:";
            // 
            // lblCardsLeft
            // 
            this.lblCardsLeft.AutoSize = true;
            this.lblCardsLeft.Location = new System.Drawing.Point(309, 183);
            this.lblCardsLeft.Name = "lblCardsLeft";
            this.lblCardsLeft.Size = new System.Drawing.Size(13, 13);
            this.lblCardsLeft.TabIndex = 6;
            this.lblCardsLeft.Text = "0";
            // 
            // lblPlayedTally
            // 
            this.lblPlayedTally.AutoSize = true;
            this.lblPlayedTally.Location = new System.Drawing.Point(309, 205);
            this.lblPlayedTally.Name = "lblPlayedTally";
            this.lblPlayedTally.Size = new System.Drawing.Size(13, 13);
            this.lblPlayedTally.TabIndex = 7;
            this.lblPlayedTally.Text = "0";
            // 
            // lblCompScore
            // 
            this.lblCompScore.BackColor = System.Drawing.Color.Blue;
            this.lblCompScore.ForeColor = System.Drawing.Color.White;
            this.lblCompScore.Location = new System.Drawing.Point(312, 227);
            this.lblCompScore.Name = "lblCompScore";
            this.lblCompScore.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCompScore.Size = new System.Drawing.Size(25, 13);
            this.lblCompScore.TabIndex = 8;
            this.lblCompScore.Text = "0";
            // 
            // lblHumScore
            // 
            this.lblHumScore.BackColor = System.Drawing.Color.Red;
            this.lblHumScore.ForeColor = System.Drawing.Color.White;
            this.lblHumScore.Location = new System.Drawing.Point(343, 227);
            this.lblHumScore.Name = "lblHumScore";
            this.lblHumScore.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblHumScore.Size = new System.Drawing.Size(25, 13);
            this.lblHumScore.TabIndex = 9;
            this.lblHumScore.Text = "0";
            // 
            // lblCompWon
            // 
            this.lblCompWon.BackColor = System.Drawing.Color.Blue;
            this.lblCompWon.ForeColor = System.Drawing.Color.White;
            this.lblCompWon.Location = new System.Drawing.Point(312, 249);
            this.lblCompWon.Name = "lblCompWon";
            this.lblCompWon.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCompWon.Size = new System.Drawing.Size(25, 13);
            this.lblCompWon.TabIndex = 10;
            this.lblCompWon.Text = "0";
            // 
            // lblHumWon
            // 
            this.lblHumWon.BackColor = System.Drawing.Color.Red;
            this.lblHumWon.ForeColor = System.Drawing.Color.White;
            this.lblHumWon.Location = new System.Drawing.Point(343, 249);
            this.lblHumWon.Name = "lblHumWon";
            this.lblHumWon.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblHumWon.Size = new System.Drawing.Size(25, 13);
            this.lblHumWon.TabIndex = 11;
            this.lblHumWon.Text = "0";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(22, 183);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 12;
            this.btnNew.Text = "&New Game";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(22, 212);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(22, 241);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(75, 23);
            this.btnOptions.TabIndex = 14;
            this.btnOptions.Text = "&Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.Location = new System.Drawing.Point(213, 274);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 15;
            this.btnGo.Text = "&Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Enabled = false;
            this.btnContinue.Location = new System.Drawing.Point(294, 274);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 16;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // lblCrib
            // 
            this.lblCrib.AutoSize = true;
            this.lblCrib.BackColor = System.Drawing.Color.Gray;
            this.lblCrib.ForeColor = System.Drawing.Color.White;
            this.lblCrib.Location = new System.Drawing.Point(137, 284);
            this.lblCrib.Name = "lblCrib";
            this.lblCrib.Size = new System.Drawing.Size(44, 13);
            this.lblCrib.TabIndex = 17;
            this.lblCrib.Text = "[ CRIB ]";
            // 
            // pbCard1
            // 
            this.pbCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCard1.Location = new System.Drawing.Point(12, 317);
            this.pbCard1.Name = "pbCard1";
            this.pbCard1.Size = new System.Drawing.Size(73, 98);
            this.pbCard1.TabIndex = 18;
            this.pbCard1.TabStop = false;
            this.pbCard1.Tag = "0";
            // 
            // pbCard2
            // 
            this.pbCard2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCard2.Location = new System.Drawing.Point(91, 317);
            this.pbCard2.Name = "pbCard2";
            this.pbCard2.Size = new System.Drawing.Size(73, 98);
            this.pbCard2.TabIndex = 19;
            this.pbCard2.TabStop = false;
            this.pbCard2.Tag = "1";
            // 
            // pbCard3
            // 
            this.pbCard3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCard3.Location = new System.Drawing.Point(170, 317);
            this.pbCard3.Name = "pbCard3";
            this.pbCard3.Size = new System.Drawing.Size(73, 98);
            this.pbCard3.TabIndex = 20;
            this.pbCard3.TabStop = false;
            this.pbCard3.Tag = "2";
            // 
            // pbCard4
            // 
            this.pbCard4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCard4.Location = new System.Drawing.Point(249, 317);
            this.pbCard4.Name = "pbCard4";
            this.pbCard4.Size = new System.Drawing.Size(73, 98);
            this.pbCard4.TabIndex = 21;
            this.pbCard4.TabStop = false;
            this.pbCard4.Tag = "3";
            // 
            // pbCard5
            // 
            this.pbCard5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCard5.Location = new System.Drawing.Point(328, 317);
            this.pbCard5.Name = "pbCard5";
            this.pbCard5.Size = new System.Drawing.Size(73, 98);
            this.pbCard5.TabIndex = 22;
            this.pbCard5.TabStop = false;
            this.pbCard5.Tag = "4";
            // 
            // pbCard6
            // 
            this.pbCard6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCard6.Location = new System.Drawing.Point(407, 317);
            this.pbCard6.Name = "pbCard6";
            this.pbCard6.Size = new System.Drawing.Size(73, 98);
            this.pbCard6.TabIndex = 23;
            this.pbCard6.TabStop = false;
            this.pbCard6.Tag = "5";
            // 
            // btnCard1
            // 
            this.btnCard1.Location = new System.Drawing.Point(12, 421);
            this.btnCard1.Name = "btnCard1";
            this.btnCard1.Size = new System.Drawing.Size(73, 23);
            this.btnCard1.TabIndex = 24;
            this.btnCard1.Tag = "0";
            this.btnCard1.Text = "Play/Discard";
            this.btnCard1.UseVisualStyleBackColor = true;
            this.btnCard1.Visible = false;
            this.btnCard1.Click += new System.EventHandler(this.btnPlayDisX_Click);
            // 
            // btnCard2
            // 
            this.btnCard2.Location = new System.Drawing.Point(91, 421);
            this.btnCard2.Name = "btnCard2";
            this.btnCard2.Size = new System.Drawing.Size(73, 23);
            this.btnCard2.TabIndex = 25;
            this.btnCard2.Tag = "1";
            this.btnCard2.Text = "Play/Discard";
            this.btnCard2.UseVisualStyleBackColor = true;
            this.btnCard2.Visible = false;
            this.btnCard2.Click += new System.EventHandler(this.btnPlayDisX_Click);
            // 
            // btnCard3
            // 
            this.btnCard3.Location = new System.Drawing.Point(170, 421);
            this.btnCard3.Name = "btnCard3";
            this.btnCard3.Size = new System.Drawing.Size(73, 23);
            this.btnCard3.TabIndex = 26;
            this.btnCard3.Tag = "2";
            this.btnCard3.Text = "Play/Discard";
            this.btnCard3.UseVisualStyleBackColor = true;
            this.btnCard3.Visible = false;
            this.btnCard3.Click += new System.EventHandler(this.btnPlayDisX_Click);
            // 
            // btnCard4
            // 
            this.btnCard4.Location = new System.Drawing.Point(249, 421);
            this.btnCard4.Name = "btnCard4";
            this.btnCard4.Size = new System.Drawing.Size(73, 23);
            this.btnCard4.TabIndex = 27;
            this.btnCard4.Tag = "3";
            this.btnCard4.Text = "Play/Discard";
            this.btnCard4.UseVisualStyleBackColor = true;
            this.btnCard4.Visible = false;
            this.btnCard4.Click += new System.EventHandler(this.btnPlayDisX_Click);
            // 
            // btnCard5
            // 
            this.btnCard5.Location = new System.Drawing.Point(328, 421);
            this.btnCard5.Name = "btnCard5";
            this.btnCard5.Size = new System.Drawing.Size(73, 23);
            this.btnCard5.TabIndex = 28;
            this.btnCard5.Tag = "4";
            this.btnCard5.Text = "Play/Discard";
            this.btnCard5.UseVisualStyleBackColor = true;
            this.btnCard5.Visible = false;
            this.btnCard5.Click += new System.EventHandler(this.btnPlayDisX_Click);
            // 
            // btnCard6
            // 
            this.btnCard6.Location = new System.Drawing.Point(407, 421);
            this.btnCard6.Name = "btnCard6";
            this.btnCard6.Size = new System.Drawing.Size(73, 23);
            this.btnCard6.TabIndex = 29;
            this.btnCard6.Tag = "5";
            this.btnCard6.Text = "Play/Discard";
            this.btnCard6.UseVisualStyleBackColor = true;
            this.btnCard6.Visible = false;
            this.btnCard6.Click += new System.EventHandler(this.btnPlayDisX_Click);
            // 
            // lblWinPts
            // 
            this.lblWinPts.AutoSize = true;
            this.lblWinPts.Location = new System.Drawing.Point(376, 249);
            this.lblWinPts.Name = "lblWinPts";
            this.lblWinPts.Size = new System.Drawing.Size(19, 13);
            this.lblWinPts.TabIndex = 31;
            this.lblWinPts.Text = "(0)";
            this.lblWinPts.Visible = false;
            // 
            // playedCanvas
            // 
            this.playedCanvas.BackColor = System.Drawing.Color.DarkGreen;
            this.playedCanvas.Location = new System.Drawing.Point(457, 183);
            this.playedCanvas.Name = "playedCanvas";
            this.playedCanvas.Size = new System.Drawing.Size(184, 110);
            this.playedCanvas.TabIndex = 30;
            // 
            // cribBoard
            // 
            this.cribBoard.BackColor = System.Drawing.Color.Silver;
            this.cribBoard.Location = new System.Drawing.Point(12, 12);
            this.cribBoard.Name = "cribBoard";
            this.cribBoard.Size = new System.Drawing.Size(641, 156);
            this.cribBoard.SoundOn = false;
            this.cribBoard.TabIndex = 0;
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 453);
            this.Controls.Add(this.lblWinPts);
            this.Controls.Add(this.playedCanvas);
            this.Controls.Add(this.btnCard6);
            this.Controls.Add(this.btnCard5);
            this.Controls.Add(this.btnCard4);
            this.Controls.Add(this.btnCard3);
            this.Controls.Add(this.btnCard2);
            this.Controls.Add(this.btnCard1);
            this.Controls.Add(this.pbCard6);
            this.Controls.Add(this.pbCard5);
            this.Controls.Add(this.pbCard4);
            this.Controls.Add(this.pbCard3);
            this.Controls.Add(this.pbCard2);
            this.Controls.Add(this.pbCard1);
            this.Controls.Add(this.lblCrib);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.lblHumWon);
            this.Controls.Add(this.lblCompWon);
            this.Controls.Add(this.lblHumScore);
            this.Controls.Add(this.lblCompScore);
            this.Controls.Add(this.lblPlayedTally);
            this.Controls.Add(this.lblCardsLeft);
            this.Controls.Add(this.lblGW);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbUpCard);
            this.Controls.Add(this.cribBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cribbage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
            this.Load += new System.EventHandler(this.MainWin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbUpCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CribbageBoard cribBoard;
        private System.Windows.Forms.PictureBox pbUpCard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblGW;
        private System.Windows.Forms.Label lblCardsLeft;
        private System.Windows.Forms.Label lblPlayedTally;
        private System.Windows.Forms.Label lblCompScore;
        private System.Windows.Forms.Label lblHumScore;
        private System.Windows.Forms.Label lblCompWon;
        private System.Windows.Forms.Label lblHumWon;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Label lblCrib;
        private System.Windows.Forms.PictureBox pbCard1;
        private System.Windows.Forms.PictureBox pbCard2;
        private System.Windows.Forms.PictureBox pbCard3;
        private System.Windows.Forms.PictureBox pbCard4;
        private System.Windows.Forms.PictureBox pbCard5;
        private System.Windows.Forms.PictureBox pbCard6;
        private System.Windows.Forms.Button btnCard1;
        private System.Windows.Forms.Button btnCard2;
        private System.Windows.Forms.Button btnCard3;
        private System.Windows.Forms.Button btnCard4;
        private System.Windows.Forms.Button btnCard5;
        private System.Windows.Forms.Button btnCard6;
        private PlayedCanvas playedCanvas;
        private System.Windows.Forms.Label lblWinPts;
    }
}


namespace Cribbage
{
    partial class OptionsWin
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
            this.cbPrompts = new System.Windows.Forms.CheckBox();
            this.cbImage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbAlwaysStart = new System.Windows.Forms.CheckBox();
            this.cbSoundsOn = new System.Windows.Forms.CheckBox();
            this.cbMatchPoint = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.udWinPoints = new System.Windows.Forms.NumericUpDown();
            this.cbDTSkunk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWinPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // cbPrompts
            // 
            this.cbPrompts.AutoSize = true;
            this.cbPrompts.Location = new System.Drawing.Point(15, 12);
            this.cbPrompts.Name = "cbPrompts";
            this.cbPrompts.Size = new System.Drawing.Size(160, 17);
            this.cbPrompts.TabIndex = 0;
            this.cbPrompts.Text = "&Prompts On (new game/exit)";
            this.cbPrompts.UseVisualStyleBackColor = true;
            this.cbPrompts.CheckedChanged += new System.EventHandler(this.cbPrompts_CheckedChanged);
            // 
            // cbImage
            // 
            this.cbImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImage.FormattingEnabled = true;
            this.cbImage.Location = new System.Drawing.Point(12, 162);
            this.cbImage.Name = "cbImage";
            this.cbImage.Size = new System.Drawing.Size(140, 21);
            this.cbImage.TabIndex = 8;
            this.cbImage.SelectedIndexChanged += new System.EventHandler(this.cbImage_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Card &Back Image";
            // 
            // pbBack
            // 
            this.pbBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBack.Location = new System.Drawing.Point(181, 162);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(73, 98);
            this.pbBack.TabIndex = 3;
            this.pbBack.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(12, 268);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(93, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cbAlwaysStart
            // 
            this.cbAlwaysStart.AutoSize = true;
            this.cbAlwaysStart.Location = new System.Drawing.Point(15, 35);
            this.cbAlwaysStart.Name = "cbAlwaysStart";
            this.cbAlwaysStart.Size = new System.Drawing.Size(158, 17);
            this.cbAlwaysStart.TabIndex = 1;
            this.cbAlwaysStart.Text = "&Human Player Always Starts";
            this.cbAlwaysStart.UseVisualStyleBackColor = true;
            this.cbAlwaysStart.CheckStateChanged += new System.EventHandler(this.cbAlwaysStart_CheckedChanged);
            // 
            // cbSoundsOn
            // 
            this.cbSoundsOn.AutoSize = true;
            this.cbSoundsOn.Location = new System.Drawing.Point(15, 58);
            this.cbSoundsOn.Name = "cbSoundsOn";
            this.cbSoundsOn.Size = new System.Drawing.Size(155, 17);
            this.cbSoundsOn.TabIndex = 2;
            this.cbSoundsOn.Text = "Cribbage Board &Sounds On";
            this.cbSoundsOn.UseVisualStyleBackColor = true;
            this.cbSoundsOn.CheckStateChanged += new System.EventHandler(this.cbSoundsOn_CheckedChanged);
            // 
            // cbMatchPoint
            // 
            this.cbMatchPoint.AutoSize = true;
            this.cbMatchPoint.Location = new System.Drawing.Point(15, 81);
            this.cbMatchPoint.Name = "cbMatchPoint";
            this.cbMatchPoint.Size = new System.Drawing.Size(205, 17);
            this.cbMatchPoint.TabIndex = 3;
            this.cbMatchPoint.Text = "&Match Points (instead of Games Won)";
            this.cbMatchPoint.UseVisualStyleBackColor = true;
            this.cbMatchPoint.CheckedChanged += new System.EventHandler(this.cbMatchPoint_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Match Points for &Win (3-99):";
            // 
            // udWinPoints
            // 
            this.udWinPoints.Location = new System.Drawing.Point(176, 99);
            this.udWinPoints.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.udWinPoints.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.udWinPoints.Name = "udWinPoints";
            this.udWinPoints.Size = new System.Drawing.Size(38, 20);
            this.udWinPoints.TabIndex = 5;
            this.udWinPoints.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.udWinPoints.ValueChanged += new System.EventHandler(this.udWinPoints_ValueChanged);
            // 
            // cbDTSkunk
            // 
            this.cbDTSkunk.AutoSize = true;
            this.cbDTSkunk.Location = new System.Drawing.Point(34, 121);
            this.cbDTSkunk.Name = "cbDTSkunk";
            this.cbDTSkunk.Size = new System.Drawing.Size(162, 17);
            this.cbDTSkunk.TabIndex = 6;
            this.cbDTSkunk.Text = "&Double/Triple Skunk Scored";
            this.cbDTSkunk.UseVisualStyleBackColor = true;
            this.cbDTSkunk.CheckedChanged += new System.EventHandler(this.cbDTSkunk_CheckedChanged);
            // 
            // OptionsWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 302);
            this.Controls.Add(this.cbDTSkunk);
            this.Controls.Add(this.udWinPoints);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbMatchPoint);
            this.Controls.Add(this.cbSoundsOn);
            this.Controls.Add(this.cbAlwaysStart);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pbBack);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbImage);
            this.Controls.Add(this.cbPrompts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cribbage Options";
            this.Load += new System.EventHandler(this.OptionsWin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWinPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbPrompts;
        private System.Windows.Forms.ComboBox cbImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbBack;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbAlwaysStart;
        private System.Windows.Forms.CheckBox cbSoundsOn;
        private System.Windows.Forms.CheckBox cbMatchPoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udWinPoints;
        private System.Windows.Forms.CheckBox cbDTSkunk;
    }
}
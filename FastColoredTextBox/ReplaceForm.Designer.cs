namespace FastColoredTextBoxNS
{
    partial class ReplaceForm
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
            this.btClose = new System.Windows.Forms.Button();
            this.btFindNext = new System.Windows.Forms.Button();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.cbRegex = new System.Windows.Forms.CheckBox();
            this.cbMatchCase = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbWholeWord = new System.Windows.Forms.CheckBox();
            this.btReplace = new System.Windows.Forms.Button();
            this.btReplaceAll = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbReplace = new System.Windows.Forms.TextBox();
            this.btFindAll = new System.Windows.Forms.Button();
            this.btFindPrev = new System.Windows.Forms.Button();
            this.btReplacePrev = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(364, 224);
            this.btClose.Margin = new System.Windows.Forms.Padding(4);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(100, 28);
            this.btClose.TabIndex = 11;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btFindNext
            // 
            this.btFindNext.Location = new System.Drawing.Point(148, 153);
            this.btFindNext.Margin = new System.Windows.Forms.Padding(4);
            this.btFindNext.Name = "btFindNext";
            this.btFindNext.Size = new System.Drawing.Size(100, 28);
            this.btFindNext.TabIndex = 5;
            this.btFindNext.Text = "Find next";
            this.btFindNext.UseVisualStyleBackColor = true;
            this.btFindNext.Click += new System.EventHandler(this.btFindNext_Click);
            // 
            // tbFind
            // 
            this.tbFind.Location = new System.Drawing.Point(83, 15);
            this.tbFind.Margin = new System.Windows.Forms.Padding(4);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(380, 22);
            this.tbFind.TabIndex = 0;
            this.tbFind.TextChanged += new System.EventHandler(this.cbMatchCase_CheckedChanged);
            this.tbFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFind_KeyPress);
            // 
            // cbRegex
            // 
            this.cbRegex.AutoSize = true;
            this.cbRegex.Location = new System.Drawing.Point(364, 47);
            this.cbRegex.Margin = new System.Windows.Forms.Padding(4);
            this.cbRegex.Name = "cbRegex";
            this.cbRegex.Size = new System.Drawing.Size(69, 20);
            this.cbRegex.TabIndex = 3;
            this.cbRegex.Text = "Regex";
            this.cbRegex.UseVisualStyleBackColor = true;
            this.cbRegex.CheckedChanged += new System.EventHandler(this.cbMatchCase_CheckedChanged);
            // 
            // cbMatchCase
            // 
            this.cbMatchCase.AutoSize = true;
            this.cbMatchCase.Location = new System.Drawing.Point(88, 47);
            this.cbMatchCase.Margin = new System.Windows.Forms.Padding(4);
            this.cbMatchCase.Name = "cbMatchCase";
            this.cbMatchCase.Size = new System.Drawing.Size(98, 20);
            this.cbMatchCase.TabIndex = 1;
            this.cbMatchCase.Text = "Match case";
            this.cbMatchCase.UseVisualStyleBackColor = true;
            this.cbMatchCase.CheckedChanged += new System.EventHandler(this.cbMatchCase_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find: ";
            // 
            // cbWholeWord
            // 
            this.cbWholeWord.AutoSize = true;
            this.cbWholeWord.Location = new System.Drawing.Point(205, 47);
            this.cbWholeWord.Margin = new System.Windows.Forms.Padding(4);
            this.cbWholeWord.Name = "cbWholeWord";
            this.cbWholeWord.Size = new System.Drawing.Size(135, 20);
            this.cbWholeWord.TabIndex = 2;
            this.cbWholeWord.Text = "Match whole word";
            this.cbWholeWord.UseVisualStyleBackColor = true;
            this.cbWholeWord.CheckedChanged += new System.EventHandler(this.cbMatchCase_CheckedChanged);
            // 
            // btReplace
            // 
            this.btReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F);
            this.btReplace.Location = new System.Drawing.Point(148, 188);
            this.btReplace.Margin = new System.Windows.Forms.Padding(4);
            this.btReplace.Name = "btReplace";
            this.btReplace.Size = new System.Drawing.Size(100, 28);
            this.btReplace.TabIndex = 8;
            this.btReplace.Text = "Replace next";
            this.btReplace.UseVisualStyleBackColor = true;
            this.btReplace.Click += new System.EventHandler(this.btReplace_Click);
            // 
            // btReplaceAll
            // 
            this.btReplaceAll.Location = new System.Drawing.Point(364, 188);
            this.btReplaceAll.Margin = new System.Windows.Forms.Padding(4);
            this.btReplaceAll.Name = "btReplaceAll";
            this.btReplaceAll.Size = new System.Drawing.Size(100, 28);
            this.btReplaceAll.TabIndex = 10;
            this.btReplaceAll.Text = "Replace all";
            this.btReplaceAll.UseVisualStyleBackColor = true;
            this.btReplaceAll.Click += new System.EventHandler(this.btReplaceAll_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Replace:";
            // 
            // tbReplace
            // 
            this.tbReplace.Location = new System.Drawing.Point(83, 96);
            this.tbReplace.Margin = new System.Windows.Forms.Padding(4);
            this.tbReplace.Name = "tbReplace";
            this.tbReplace.Size = new System.Drawing.Size(380, 22);
            this.tbReplace.TabIndex = 4;
            this.tbReplace.TextChanged += new System.EventHandler(this.cbMatchCase_CheckedChanged);
            this.tbReplace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbReplace_KeyPress);
            // 
            // btFindAll
            // 
            this.btFindAll.Location = new System.Drawing.Point(364, 153);
            this.btFindAll.Margin = new System.Windows.Forms.Padding(4);
            this.btFindAll.Name = "btFindAll";
            this.btFindAll.Size = new System.Drawing.Size(100, 28);
            this.btFindAll.TabIndex = 7;
            this.btFindAll.Text = "Find all";
            this.btFindAll.UseVisualStyleBackColor = true;
            this.btFindAll.Click += new System.EventHandler(this.btFindAll_Click);
            // 
            // btFindPrev
            // 
            this.btFindPrev.Location = new System.Drawing.Point(256, 153);
            this.btFindPrev.Margin = new System.Windows.Forms.Padding(4);
            this.btFindPrev.Name = "btFindPrev";
            this.btFindPrev.Size = new System.Drawing.Size(100, 28);
            this.btFindPrev.TabIndex = 6;
            this.btFindPrev.Text = "Find prev";
            this.btFindPrev.UseVisualStyleBackColor = true;
            this.btFindPrev.Click += new System.EventHandler(this.btFindPrev_Click);
            // 
            // btReplacePrev
            // 
            this.btReplacePrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F);
            this.btReplacePrev.Location = new System.Drawing.Point(256, 188);
            this.btReplacePrev.Margin = new System.Windows.Forms.Padding(4);
            this.btReplacePrev.Name = "btReplacePrev";
            this.btReplacePrev.Size = new System.Drawing.Size(100, 28);
            this.btReplacePrev.TabIndex = 9;
            this.btReplacePrev.Text = "Replace prev";
            this.btReplacePrev.UseVisualStyleBackColor = true;
            this.btReplacePrev.Click += new System.EventHandler(this.btReplacePrev_Click);
            // 
            // ReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 270);
            this.Controls.Add(this.btFindAll);
            this.Controls.Add(this.btFindPrev);
            this.Controls.Add(this.btReplacePrev);
            this.Controls.Add(this.tbFind);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbReplace);
            this.Controls.Add(this.btReplaceAll);
            this.Controls.Add(this.btReplace);
            this.Controls.Add(this.cbWholeWord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbMatchCase);
            this.Controls.Add(this.cbRegex);
            this.Controls.Add(this.btFindNext);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplaceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find and replace";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReplaceForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btFindNext;
        private System.Windows.Forms.CheckBox cbRegex;
        private System.Windows.Forms.CheckBox cbMatchCase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbWholeWord;
        private System.Windows.Forms.Button btReplace;
        private System.Windows.Forms.Button btReplaceAll;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tbFind;
        public System.Windows.Forms.TextBox tbReplace;
        private System.Windows.Forms.Button btFindAll;
        private System.Windows.Forms.Button btFindPrev;
        private System.Windows.Forms.Button btReplacePrev;
    }
}
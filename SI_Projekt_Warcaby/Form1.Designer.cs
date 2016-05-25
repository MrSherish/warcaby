namespace Warcaby
{
    partial class Form1
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
            this.boardPanel = new System.Windows.Forms.Panel();
            this.boardSizeLabel = new System.Windows.Forms.Label();
            this.boardSizeSelector = new System.Windows.Forms.NumericUpDown();
            this.checkersRowCounterLabel = new System.Windows.Forms.Label();
            this.rowsOfCheckers = new System.Windows.Forms.NumericUpDown();
            this.isP1Human = new System.Windows.Forms.CheckBox();
            this.isP2Human = new System.Windows.Forms.CheckBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.comboBoxAlgP1 = new System.Windows.Forms.ComboBox();
            this.comboBoxAlgP2 = new System.Windows.Forms.ComboBox();
            this.treeDepth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.boardSizeSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsOfCheckers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDepth)).BeginInit();
            this.SuspendLayout();
            // 
            // boardPanel
            // 
            this.boardPanel.Location = new System.Drawing.Point(12, 146);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(560, 560);
            this.boardPanel.TabIndex = 0;
            // 
            // boardSizeLabel
            // 
            this.boardSizeLabel.AutoSize = true;
            this.boardSizeLabel.Location = new System.Drawing.Point(13, 13);
            this.boardSizeLabel.Name = "boardSizeLabel";
            this.boardSizeLabel.Size = new System.Drawing.Size(59, 13);
            this.boardSizeLabel.TabIndex = 1;
            this.boardSizeLabel.Text = "Board size:";
            this.boardSizeLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // boardSizeSelector
            // 
            this.boardSizeSelector.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.boardSizeSelector.Location = new System.Drawing.Point(16, 29);
            this.boardSizeSelector.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.boardSizeSelector.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.boardSizeSelector.Name = "boardSizeSelector";
            this.boardSizeSelector.Size = new System.Drawing.Size(56, 20);
            this.boardSizeSelector.TabIndex = 3;
            this.boardSizeSelector.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.boardSizeSelector.ValueChanged += new System.EventHandler(this.boardSizeSelector_ValueChanged);
            // 
            // checkersRowCounterLabel
            // 
            this.checkersRowCounterLabel.AutoSize = true;
            this.checkersRowCounterLabel.Location = new System.Drawing.Point(102, 13);
            this.checkersRowCounterLabel.Name = "checkersRowCounterLabel";
            this.checkersRowCounterLabel.Size = new System.Drawing.Size(143, 13);
            this.checkersRowCounterLabel.TabIndex = 4;
            this.checkersRowCounterLabel.Text = "Number of rows of checkers:";
            // 
            // rowsOfCheckers
            // 
            this.rowsOfCheckers.Location = new System.Drawing.Point(105, 29);
            this.rowsOfCheckers.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.rowsOfCheckers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rowsOfCheckers.Name = "rowsOfCheckers";
            this.rowsOfCheckers.Size = new System.Drawing.Size(56, 20);
            this.rowsOfCheckers.TabIndex = 5;
            this.rowsOfCheckers.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.rowsOfCheckers.ValueChanged += new System.EventHandler(this.rowsOfCheckers_ValueChanged);
            // 
            // isP1Human
            // 
            this.isP1Human.AutoSize = true;
            this.isP1Human.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.isP1Human.Location = new System.Drawing.Point(244, 13);
            this.isP1Human.Name = "isP1Human";
            this.isP1Human.Size = new System.Drawing.Size(101, 17);
            this.isP1Human.TabIndex = 7;
            this.isP1Human.Text = "Human Player 1";
            this.isP1Human.UseVisualStyleBackColor = true;
            this.isP1Human.CheckedChanged += new System.EventHandler(this.isP1Human_CheckedChanged);
            // 
            // isP2Human
            // 
            this.isP2Human.AutoSize = true;
            this.isP2Human.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.isP2Human.Location = new System.Drawing.Point(244, 55);
            this.isP2Human.Name = "isP2Human";
            this.isP2Human.Size = new System.Drawing.Size(101, 17);
            this.isP2Human.TabIndex = 8;
            this.isP2Human.Text = "Human Player 2";
            this.isP2Human.UseVisualStyleBackColor = true;
            this.isP2Human.CheckedChanged += new System.EventHandler(this.isP2Human_CheckedChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(16, 55);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(159, 40);
            this.buttonStart.TabIndex = 9;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPlayer1.Location = new System.Drawing.Point(246, 118);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(99, 25);
            this.labelPlayer1.TabIndex = 10;
            this.labelPlayer1.Text = "Player 1";
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPlayer2.Location = new System.Drawing.Point(246, 709);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(99, 25);
            this.labelPlayer2.TabIndex = 11;
            this.labelPlayer2.Text = "Player 2";
            // 
            // comboBoxAlgP1
            // 
            this.comboBoxAlgP1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboBoxAlgP1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxAlgP1.FormattingEnabled = true;
            this.comboBoxAlgP1.Items.AddRange(new object[] {
            "Minimax",
            "Alfa - Beta"});
            this.comboBoxAlgP1.Location = new System.Drawing.Point(351, 12);
            this.comboBoxAlgP1.Name = "comboBoxAlgP1";
            this.comboBoxAlgP1.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAlgP1.TabIndex = 12;
            this.comboBoxAlgP1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBoxAlgP2
            // 
            this.comboBoxAlgP2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboBoxAlgP2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxAlgP2.FormattingEnabled = true;
            this.comboBoxAlgP2.Items.AddRange(new object[] {
            "Minimax",
            "Alfa - Beta"});
            this.comboBoxAlgP2.Location = new System.Drawing.Point(351, 53);
            this.comboBoxAlgP2.Name = "comboBoxAlgP2";
            this.comboBoxAlgP2.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAlgP2.TabIndex = 13;
            // 
            // treeDepth
            // 
            this.treeDepth.Location = new System.Drawing.Point(313, 90);
            this.treeDepth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.treeDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.treeDepth.Name = "treeDepth";
            this.treeDepth.Size = new System.Drawing.Size(56, 20);
            this.treeDepth.TabIndex = 14;
            this.treeDepth.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Tree depth";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Location = new System.Drawing.Point(375, 93);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(135, 17);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "Observe Num. of Leafs";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 742);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeDepth);
            this.Controls.Add(this.comboBoxAlgP2);
            this.Controls.Add(this.comboBoxAlgP1);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.isP2Human);
            this.Controls.Add(this.isP1Human);
            this.Controls.Add(this.rowsOfCheckers);
            this.Controls.Add(this.checkersRowCounterLabel);
            this.Controls.Add(this.boardSizeSelector);
            this.Controls.Add(this.boardSizeLabel);
            this.Controls.Add(this.boardPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.boardSizeSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsOfCheckers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDepth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel boardPanel;
        private System.Windows.Forms.Label boardSizeLabel;
        internal System.Windows.Forms.NumericUpDown boardSizeSelector;
        private System.Windows.Forms.Label checkersRowCounterLabel;
        internal System.Windows.Forms.NumericUpDown rowsOfCheckers;
        private System.Windows.Forms.CheckBox isP1Human;
        private System.Windows.Forms.CheckBox isP2Human;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.ComboBox comboBoxAlgP1;
        private System.Windows.Forms.ComboBox comboBoxAlgP2;
        internal System.Windows.Forms.NumericUpDown treeDepth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}


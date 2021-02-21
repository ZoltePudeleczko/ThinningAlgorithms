namespace ThinningAlgorithms
{
	partial class MainWindow
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.goBtn = new System.Windows.Forms.Button();
			this.thinComboBox = new System.Windows.Forms.ComboBox();
			this.stopCheckBox = new System.Windows.Forms.CheckBox();
			this.saveCheckBox = new System.Windows.Forms.CheckBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.loadImageToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(734, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// loadImageToolStripMenuItem
			// 
			this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
			this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
			this.loadImageToolStripMenuItem.Text = "Load Image...";
			this.loadImageToolStripMenuItem.Click += new System.EventHandler(this.loadImageToolStripMenuItem_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(734, 437);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(728, 393);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.goBtn);
			this.flowLayoutPanel1.Controls.Add(this.thinComboBox);
			this.flowLayoutPanel1.Controls.Add(this.stopCheckBox);
			this.flowLayoutPanel1.Controls.Add(this.saveCheckBox);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 402);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(594, 32);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// goBtn
			// 
			this.goBtn.Location = new System.Drawing.Point(3, 3);
			this.goBtn.Name = "goBtn";
			this.goBtn.Size = new System.Drawing.Size(75, 23);
			this.goBtn.TabIndex = 0;
			this.goBtn.Text = "Go!";
			this.goBtn.UseVisualStyleBackColor = true;
			this.goBtn.Click += new System.EventHandler(this.goBtn_Click);
			// 
			// thinComboBox
			// 
			this.thinComboBox.FormattingEnabled = true;
			this.thinComboBox.Location = new System.Drawing.Point(84, 3);
			this.thinComboBox.Name = "thinComboBox";
			this.thinComboBox.Size = new System.Drawing.Size(171, 21);
			this.thinComboBox.TabIndex = 7;
			// 
			// stopCheckBox
			// 
			this.stopCheckBox.AutoSize = true;
			this.stopCheckBox.Location = new System.Drawing.Point(261, 3);
			this.stopCheckBox.Name = "stopCheckBox";
			this.stopCheckBox.Size = new System.Drawing.Size(119, 17);
			this.stopCheckBox.TabIndex = 4;
			this.stopCheckBox.Text = "Stop after operation";
			this.stopCheckBox.UseVisualStyleBackColor = true;
			// 
			// saveCheckBox
			// 
			this.saveCheckBox.AutoSize = true;
			this.saveCheckBox.Location = new System.Drawing.Point(386, 3);
			this.saveCheckBox.Name = "saveCheckBox";
			this.saveCheckBox.Size = new System.Drawing.Size(87, 17);
			this.saveCheckBox.TabIndex = 5;
			this.saveCheckBox.Text = "Save images";
			this.saveCheckBox.UseVisualStyleBackColor = true;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(734, 461);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(500, 350);
			this.Name = "MainWindow";
			this.Text = "Thinning Algorithms";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button goBtn;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.CheckBox stopCheckBox;
		private System.Windows.Forms.CheckBox saveCheckBox;
		private System.Windows.Forms.ComboBox thinComboBox;
	}
}


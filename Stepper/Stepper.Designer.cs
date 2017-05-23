namespace Stepper
{
    partial class Stepper
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
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.songInfo = new System.Windows.Forms.DataGridView();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.close = new System.Windows.Forms.Button();
			this.overwriteStepfiles = new System.Windows.Forms.Button();
			this.getInfo = new System.Windows.Forms.Button();
			this.folderChooser = new System.Windows.Forms.Button();
			this.currentFolder = new System.Windows.Forms.TextBox();
			this.button7 = new System.Windows.Forms.Button();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.songInfo)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// folderBrowserDialog1
			// 
			this.folderBrowserDialog1.ShowNewFolderButton = false;
			// 
			// tabPage4
			// 
			this.tabPage4.AutoScroll = true;
			this.tabPage4.Controls.Add(this.flowLayoutPanel1);
			this.tabPage4.Controls.Add(this.songInfo);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(1038, 672);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Write Stepfiles";
			this.tabPage4.UseVisualStyleBackColor = true;
			this.tabPage4.Resize += new System.EventHandler(this.tabPage4_Resize);
			// 
			// songInfo
			// 
			this.songInfo.AllowUserToAddRows = false;
			this.songInfo.AllowUserToDeleteRows = false;
			this.songInfo.AllowUserToResizeColumns = false;
			this.songInfo.AllowUserToResizeRows = false;
			this.songInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.songInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.songInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.songInfo.Location = new System.Drawing.Point(3, 3);
			this.songInfo.Name = "songInfo";
			this.songInfo.ReadOnly = true;
			this.songInfo.RowHeadersVisible = false;
			this.songInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.songInfo.ShowEditingIcon = false;
			this.songInfo.Size = new System.Drawing.Size(1032, 630);
			this.songInfo.TabIndex = 8;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.button7);
			this.flowLayoutPanel1.Controls.Add(this.currentFolder);
			this.flowLayoutPanel1.Controls.Add(this.folderChooser);
			this.flowLayoutPanel1.Controls.Add(this.getInfo);
			this.flowLayoutPanel1.Controls.Add(this.overwriteStepfiles);
			this.flowLayoutPanel1.Controls.Add(this.close);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 634);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(1032, 35);
			this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(200, 35);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(1032, 35);
			this.flowLayoutPanel1.TabIndex = 17;
			this.flowLayoutPanel1.WrapContents = false;
			this.flowLayoutPanel1.Resize += new System.EventHandler(this.flowLayoutPanel1_Resize);
			// 
			// close
			// 
			this.close.BackColor = System.Drawing.Color.Goldenrod;
			this.close.Location = new System.Drawing.Point(885, 3);
			this.close.Name = "close";
			this.close.Size = new System.Drawing.Size(142, 30);
			this.close.TabIndex = 6;
			this.close.Text = "Close";
			this.close.UseVisualStyleBackColor = false;
			this.close.Click += new System.EventHandler(this.close_Click);
			// 
			// overwriteStepfiles
			// 
			this.overwriteStepfiles.BackColor = System.Drawing.Color.Orange;
			this.overwriteStepfiles.Location = new System.Drawing.Point(726, 3);
			this.overwriteStepfiles.MinimumSize = new System.Drawing.Size(90, 30);
			this.overwriteStepfiles.Name = "overwriteStepfiles";
			this.overwriteStepfiles.Size = new System.Drawing.Size(153, 30);
			this.overwriteStepfiles.TabIndex = 5;
			this.overwriteStepfiles.Text = "Overwrite Stepfiles";
			this.overwriteStepfiles.UseVisualStyleBackColor = false;
			this.overwriteStepfiles.Click += new System.EventHandler(this.overwriteStepfiles_Click);
			// 
			// getInfo
			// 
			this.getInfo.BackColor = System.Drawing.Color.YellowGreen;
			this.getInfo.Location = new System.Drawing.Point(647, 3);
			this.getInfo.Name = "getInfo";
			this.getInfo.Size = new System.Drawing.Size(73, 30);
			this.getInfo.TabIndex = 2;
			this.getInfo.Text = "Get info";
			this.getInfo.UseVisualStyleBackColor = false;
			this.getInfo.Click += new System.EventHandler(this.getInfo_Click);
			// 
			// folderChooser
			// 
			this.folderChooser.BackColor = System.Drawing.Color.LightBlue;
			this.folderChooser.Location = new System.Drawing.Point(534, 3);
			this.folderChooser.Name = "folderChooser";
			this.folderChooser.Size = new System.Drawing.Size(107, 30);
			this.folderChooser.TabIndex = 0;
			this.folderChooser.Text = "Change folder";
			this.folderChooser.UseVisualStyleBackColor = false;
			this.folderChooser.Click += new System.EventHandler(this.selectFolder_Click);
			// 
			// currentFolder
			// 
			this.currentFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.currentFolder.Location = new System.Drawing.Point(225, 3);
			this.currentFolder.Name = "currentFolder";
			this.currentFolder.Size = new System.Drawing.Size(303, 26);
			this.currentFolder.TabIndex = 1;
			this.currentFolder.Text = "C:\\Games\\StepMania 5\\Songs\\Test";
			this.currentFolder.TextChanged += new System.EventHandler(this.currentFolder_TextChanged);
			// 
			// button7
			// 
			this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.button7.Location = new System.Drawing.Point(3, 3);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(216, 30);
			this.button7.TabIndex = 0;
			this.button7.Text = "Back to Pump Single";
			this.button7.UseVisualStyleBackColor = false;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.textBox1);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1038, 672);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Instructions";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.tabPage1.Resize += new System.EventHandler(this.tabPage1_Resize);
			// 
			// button1
			// 
			this.button1.AutoSize = true;
			this.button1.BackColor = System.Drawing.Color.YellowGreen;
			this.button1.CausesValidation = false;
			this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button1.Location = new System.Drawing.Point(3, 639);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(1032, 30);
			this.button1.TabIndex = 4;
			this.button1.Text = "Continue";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.textBox1.Location = new System.Drawing.Point(3, 3);
			this.textBox1.MaximumSize = new System.Drawing.Size(2000, 1000);
			this.textBox1.MinimumSize = new System.Drawing.Size(100, 100);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(1032, 630);
			this.textBox1.TabIndex = 5;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1046, 698);
			this.tabControl1.TabIndex = 16;
			// 
			// Stepper
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1046, 698);
			this.Controls.Add(this.tabControl1);
			this.MaximumSize = new System.Drawing.Size(1062, 736);
			this.Name = "Stepper";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Stepper v2.0";
			this.tabPage4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.songInfo)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.TextBox currentFolder;
		private System.Windows.Forms.Button folderChooser;
		private System.Windows.Forms.Button getInfo;
		private System.Windows.Forms.Button overwriteStepfiles;
		private System.Windows.Forms.Button close;
		private System.Windows.Forms.DataGridView songInfo;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TabControl tabControl1;
		//       private System.ComponentModel.BackgroundWorker backgroundWorker1;

    }
}


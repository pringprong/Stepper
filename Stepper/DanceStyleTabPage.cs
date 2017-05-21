using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Stepper
{
	public /*partial*/ class DanceStyleTabPage : TabPage
	{
		private System.ComponentModel.IContainer components = null;
		FlowLayoutPanel flowLayoutPanel2;
		Panel panel29;
		Button button2;
		Button button3;
		private int instructionsTextboxGap = 40;
		List<NotesetPanel> nsp_list;

//		int beats_per_measure;
//		int measures_per_sample;
//		private Pen blackpen;
//		private Pen redpen;
//		private Pen bluepen;
//		Random r;

		public DanceStyleTabPage()
		{

		}
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		public DanceStyleTabPage(string dance_style, int beats, int measures, Pen black, Pen red, Pen blue, Random random)
		{
			InitializeComponent();
			nsp_list = new List<NotesetPanel>();
			foreach (string level in StepDeets.getLevels())
			{
				nsp_list.Add(new NotesetPanel(dance_style, level, beats, measures, black, red, blue, random));
			}
			button2 = new Button();
			flowLayoutPanel2 = new FlowLayoutPanel();
			panel29 = new Panel();
			button3 = new Button();


			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.AutoScroll = true;
			this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel2.Size = new System.Drawing.Size(1032, 630);
			this.flowLayoutPanel2.TabIndex = 20;
			this.flowLayoutPanel2.WrapContents = false;

			// 
			// panel29
			// 
			this.panel29.AutoSize = true;
			this.panel29.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel29.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel29.Location = new System.Drawing.Point(3, 639);
			this.panel29.MinimumSize = new System.Drawing.Size(100, 30);
			this.panel29.Size = new System.Drawing.Size(1032, 30);
			this.panel29.TabIndex = 14;
			this.panel29.Resize += new System.EventHandler(this.panel29_Resize);

			this.Location = new System.Drawing.Point(4, 22);
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Size = new System.Drawing.Size(1038, 672);
			this.TabIndex = 1;
			this.Text = StepDeets.stepTitle(dance_style);
			this.UseVisualStyleBackColor = true;
			this.Resize += new System.EventHandler(this.tabPage2_Resize);

			// 
			// button3
			// 
			this.button3.BackColor = System.Drawing.Color.YellowGreen;
			this.button3.Dock = System.Windows.Forms.DockStyle.Right;
			this.button3.Location = new System.Drawing.Point(518, 0);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(514, 30);
			this.button3.TabIndex = 1;
			this.button3.Text = "Continue to Dance Solo";
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.button2.Dock = System.Windows.Forms.DockStyle.Left;
			this.button2.Location = new System.Drawing.Point(0, 0);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(514, 30);
			this.button2.TabIndex = 0;
			this.button2.Text = "Back to Instructions";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);

			// put it all together
			this.panel29.Controls.Add(this.button3);
			this.panel29.Controls.Add(this.button2);
			foreach(NotesetPanel nsp in nsp_list)
			{
				this.flowLayoutPanel2.Controls.Add(nsp);
			}
			this.flowLayoutPanel2.Controls.Add(this.panel29);
			this.Controls.Add(this.flowLayoutPanel2);

		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}

		private void tabPage2_Resize(object sender, EventArgs e)
		{
			flowLayoutPanel2.Height = this.Height - instructionsTextboxGap;
		}

		private void panel29_Resize(object sender, EventArgs e)
		{
			button2.Width = panel29.Width / 2 - 5;
			button3.Width = panel29.Width / 2 - 5;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			//tabControl1.SelectedTab = tabPage1;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			//tabControl1.SelectedTab = tabPage5;
		}

	}
}

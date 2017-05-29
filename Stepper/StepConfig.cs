using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Stepper
{
	class StepConfig : Form
	{

		private FlowLayoutPanel topdown_flp;
		private FlowLayoutPanel leftright_flp;
		private TabControl step_subsets_tc;
		private List<StepConfigTabPage> sctpl;

		private Button ok_button;
		private Button reset_button;
		private Button cancel_button;
		private String dance_style;

		private int max_rows = 0;
		private int max_columns = 0;

		public StepConfig(String dc)
		{
			dance_style = dc;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			topdown_flp = new FlowLayoutPanel();
			leftright_flp = new FlowLayoutPanel();
			ok_button = new Button();
			cancel_button = new Button();
			reset_button = new Button();
			step_subsets_tc = new TabControl();
			sctpl = new List<StepConfigTabPage>();

			//
			// sctpl
			//
			foreach (string type in StepDeets.StepTypes)
			{
				Dictionary<string, string[]> sg = StepDeets.getStepGrid(dance_style, type);
				int rows = sg.Keys.Count() + 1;
				if (rows > max_rows)
				{
					max_rows = rows;
				}
				int columns = sg[StepDeets.Base].Count() + 1;
				if (columns > max_columns)
				{
					max_columns = columns;
				}
			}

			this.Width = max_columns * 50;
			this.Height = max_rows * 30 + 100;

			foreach (string type in StepDeets.StepTypes)
			{
				Dictionary<string, string[]> sg = StepDeets.getStepGrid(dance_style, type);
				StepConfigTabPage sctp = new StepConfigTabPage(dance_style, type, sg, this.Width - 70, this.Height - 100);
				sctpl.Add(sctp);
			}


			// 
			// ok_button
			// 
			this.ok_button.BackColor = System.Drawing.Color.YellowGreen;
			this.ok_button.Size = new System.Drawing.Size(50, 28);
			this.ok_button.TabIndex = 1;
			this.ok_button.Text = "OK";
			this.ok_button.UseVisualStyleBackColor = false;
			this.ok_button.Click += new System.EventHandler(this.ok_button_Click);


			// 
			// cancel_button
			// 
			this.cancel_button.BackColor = System.Drawing.Color.YellowGreen;
			this.cancel_button.Size = new System.Drawing.Size(50, 28);
			this.cancel_button.TabIndex = 1;
			this.cancel_button.Text = "Cancel";
			this.cancel_button.UseVisualStyleBackColor = false;
			this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);


			// 
			// reset_button
			// 
			this.reset_button.BackColor = System.Drawing.Color.YellowGreen;
			this.reset_button.Size = new System.Drawing.Size(100, 28);
			this.reset_button.TabIndex = 1;
			this.reset_button.Text = "Reset Defaults";
			this.reset_button.UseVisualStyleBackColor = false;
			this.reset_button.Click += new System.EventHandler(this.reset_button_Click);

			//
			// topdown_flp
			// 
	//		this.topdown_flp.AutoSize = true;
			this.topdown_flp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.topdown_flp.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
	//		this.topdown_flp.Dock = System.Windows.Forms.DockStyle.Top;
			this.topdown_flp.Location = new System.Drawing.Point(3, 3);
			this.topdown_flp.Margin = new System.Windows.Forms.Padding(0);
		//	this.topdown_flp.MinimumSize = new System.Drawing.Size(100, 30);
		//	this.topdown_flp.MaximumSize = new System.Drawing.Size(1032, 30);
			this.topdown_flp.Size = new System.Drawing.Size(this.Width - 30, this.Height -50);

			//
			// leftright_flp
			// 
	//		this.leftright_flp.AutoSize = true;
			this.leftright_flp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
	//		this.leftright_flp.Dock = System.Windows.Forms.DockStyle.Bottom;
	//		this.leftright_flp.Location = new System.Drawing.Point(3, 639);
			this.leftright_flp.Margin = new System.Windows.Forms.Padding(0);
		//	this.leftright_flp.MinimumSize = new System.Drawing.Size(100, 30);
		//	this.leftright_flp.MaximumSize = new System.Drawing.Size(1032, 30);
			this.leftright_flp.Size = new System.Drawing.Size(this.Width - 35, 30);

			// 
			// tabControl1
			// 
	//		this.step_subsets_tc.Dock = System.Windows.Forms.DockStyle.Top;
			this.step_subsets_tc.Location = new System.Drawing.Point(3, 3);
			this.step_subsets_tc.SelectedIndex = 0;
			this.step_subsets_tc.Size = new System.Drawing.Size(this.Width-35, this.Height - 90);
	//		this.step_subsets_tc.TabIndex = 16;


			// 
			// StepConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
	//		this.ClientSize = new System.Drawing.Size(498, 785);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = StepDeets.stepTitle(dance_style) + " Step Configuration";
			this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

	//		this.ResumeLayout(false);

			leftright_flp.Controls.Add(this.ok_button);
			leftright_flp.Controls.Add(this.reset_button);
			leftright_flp.Controls.Add(this.cancel_button);
			foreach (StepConfigTabPage sctp in sctpl)
			{
				step_subsets_tc.Controls.Add(sctp);
			}
			topdown_flp.Controls.Add(this.step_subsets_tc);
			topdown_flp.Controls.Add(this.leftright_flp);
			this.Controls.Add(this.topdown_flp);
		}


		private void ok_button_Click(object sender, EventArgs e)
		{
			saveSettings();
			this.Close();
		}

		private void reset_button_Click(object sender, EventArgs e)
		{
			resetSettings();
		}

		private void cancel_button_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void saveSettings()
		{
			foreach (StepConfigTabPage sctp in sctpl)
			{
				sctp.saveSettings();
			}
		}

		private void resetSettings()
		{
			foreach (StepConfigTabPage sctp in sctpl)
			{
				sctp.resetSettings();
			}
		}
	}
}

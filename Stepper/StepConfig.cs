﻿using System;
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
		private Panel topdown_flp;
		private FlowLayoutPanel leftright_flp;
		private TabControl step_subsets_tc;
		private List<StepConfigTabPage> sctpl;
		private Button ok_button;
		private Button reset_button;
		private Button cancel_button;
		private String dance_style;
		private Pen blackpen;
		private Pen graypen;
		private Pen lightgraypen;
		private SolidBrush blackbrush;
		private SolidBrush graybrush;
		private SolidBrush lightgraybrush;
		private SolidBrush whitebrush;
		private ConfigSettings config;

		public StepConfig(String dc, ConfigSettings c)
		{
			dance_style = dc;
			config = c;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			topdown_flp = new Panel();
			leftright_flp = new FlowLayoutPanel();
			ok_button = new Button();
			cancel_button = new Button();
			reset_button = new Button();
			step_subsets_tc = new TabControl();
			sctpl = new List<StepConfigTabPage>();

			blackpen = new Pen(Color.Blue, 1);
			blackpen.Alignment = PenAlignment.Inset;
			graypen = new Pen(Color.Black, 1);
			graypen.Alignment = PenAlignment.Inset;
			lightgraypen = new Pen(Color.LightGray, 1);
			lightgraypen.Alignment = PenAlignment.Inset;

			blackbrush = new SolidBrush(Color.Blue);
			graybrush = new SolidBrush(Color.Black);
			whitebrush = new SolidBrush(Color.White);
			lightgraybrush = new SolidBrush(Color.LightGray);

			// 
			// StepConfig
			// 
			this.BackColor = Color.White;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Text = StepDeets.stepTitle(dance_style) + " Step Configuration: Double-click to toggle";
			this.TransparencyKey = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.AutoScroll = true;
			this.MinimumSize = new Size(450, 300);
			this.Size = new Size(900, 600);

			//
			// topdown_flp
			// 
			this.topdown_flp.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.topdown_flp.BorderStyle = BorderStyle.None;
			this.topdown_flp.Location = new Point(3, 3);
			this.topdown_flp.Size = new Size(this.Width-25, this.Height-45);

			//
			// leftright_flp
			// 
			this.leftright_flp.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.leftright_flp.BorderStyle = BorderStyle.None;
			this.leftright_flp.Size = new System.Drawing.Size(topdown_flp.Width - 10, 40);
			this.leftright_flp.Location = new Point(3, (topdown_flp.Height - (leftright_flp.Height + 5)));

			// 
			// ok_button
			// 
			this.ok_button.BackColor = Color.LightGreen;
			this.ok_button.Text = "OK";
			this.ok_button.Size = new Size(75, leftright_flp.Height -10);
			this.ok_button.Click += new System.EventHandler(this.ok_button_Click);

			// 
			// reset_button
			// 
			this.reset_button.BackColor = Color.LightBlue;
			this.reset_button.Text = "Reset " + StepDeets.stepTitle(dance_style) + " Defaults";
			this.reset_button.Size = new Size(200, leftright_flp.Height - 10);
			this.reset_button.Click += new System.EventHandler(this.reset_button_Click);

			// 
			// cancel_button
			// 
			this.cancel_button.BackColor = Color.LightPink;
			this.cancel_button.Text = "Cancel";
			this.cancel_button.Size = new Size(75, leftright_flp.Height - 10);
			this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);

			// 
			// tabControl1
			// 
			this.step_subsets_tc.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.step_subsets_tc.Location = new System.Drawing.Point(3, 3);
			this.step_subsets_tc.Size = new System.Drawing.Size(topdown_flp.Width - 10, topdown_flp.Height - (leftright_flp.Height + 10));
	

			// 
			// tab pages
			//
			foreach (string type in StepDeets.StepTypes)
			{
				//Dictionary<string, string[]> sg = StepDeets.getTempStepGrid(dance_style, type);
				Dictionary<string, string[]> sg = config.getTempStepGrid(dance_style, type);
				StepConfigTabPage sctp = new StepConfigTabPage(dance_style, type, sg, step_subsets_tc.Width, step_subsets_tc.Height, 
					blackpen, graypen, lightgraypen, blackbrush, graybrush, lightgraybrush, whitebrush, config);
				sctpl.Add(sctp);
				step_subsets_tc.Controls.Add(sctp);
			}

			// put it all together
			leftright_flp.Controls.Add(this.ok_button);
			leftright_flp.Controls.Add(this.reset_button);
			leftright_flp.Controls.Add(this.cancel_button);
			topdown_flp.Controls.Add(this.leftright_flp);
			topdown_flp.Controls.Add(this.step_subsets_tc);
			this.Controls.Add(this.topdown_flp);
		}


		private void ok_button_Click(object sender, EventArgs e)
		{
			foreach (StepConfigTabPage sctp in sctpl)
			{
				sctp.saveSettings();
			}
			this.Close();
		}

		private void reset_button_Click(object sender, EventArgs e)
		{
			foreach (StepConfigTabPage sctp in sctpl)
			{
				sctp.resetSettings();
			}
		}

		private void cancel_button_Click(object sender, EventArgs e)
		{
			foreach (StepConfigTabPage sctp in sctpl)
			{
				sctp.cancelSettings();
			} 
			this.Close();
		}

		protected override void Dispose(bool disposing)
		{
			blackpen.Dispose();
			graypen.Dispose();
			lightgraypen.Dispose();
			blackbrush.Dispose();
			graybrush.Dispose();
			lightgraybrush.Dispose();
			whitebrush.Dispose();
			base.Dispose(disposing);
		}

		public void set_config(ConfigSettings c)
		{
			config = c;
			foreach (StepConfigTabPage sctp in sctpl)
			{
				sctp.set_config(c);
			}
		}
	}
}

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
	public class DanceStyleTabPage : TabPage
	{
		FlowLayoutPanel nsp_panel;
		FlowLayoutPanel bottom_panel;
		Button back_button;
		Button forward_button;
		Button config_button;
		private int instructionsTextboxGap = 40;
		List<NotesetPanel> nsp_list;
		private TabPage prevTabPage;
		private TabPage nextTabPage;
		private TabControl parentControl;
		private StepConfig stepconfig;
		private ConfigSettings config;
		public string ds { get; private set; }

		public DanceStyleTabPage(TabControl parent, string dance_style, int beats, int measures, Pen black, Pen red, Pen blue, Random random, ConfigSettings c)
		{
			config = c;
			parentControl = parent;
			nsp_list = new List<NotesetPanel>();
			foreach (string level in StepDeets.Levels)
			{
				nsp_list.Add(new NotesetPanel(dance_style, level, beats, measures, black, red, blue, random, config));
			}
			back_button = new Button();
			nsp_panel = new FlowLayoutPanel();
			bottom_panel = new FlowLayoutPanel();
			forward_button = new Button();
			config_button = new Button();
			stepconfig = new StepConfig(dance_style, config);
			ds = dance_style;

			// 
			// nsp_panel
			// 
			this.nsp_panel.AutoScroll = true;
			this.nsp_panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.nsp_panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nsp_panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.nsp_panel.Location = new System.Drawing.Point(3, 3);
			this.nsp_panel.Margin = new System.Windows.Forms.Padding(0);
			this.nsp_panel.Size = new System.Drawing.Size(1032, 630);
			this.nsp_panel.TabIndex = 20;
			this.nsp_panel.WrapContents = false;

			// 
			// bottomPanel
			// 
			this.bottom_panel.AutoSize = true;
			this.bottom_panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.bottom_panel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottom_panel.Location = new System.Drawing.Point(3, 639);
			this.bottom_panel.Margin = new System.Windows.Forms.Padding(0);
			this.bottom_panel.MinimumSize = new System.Drawing.Size(100, 30);
			this.bottom_panel.MaximumSize = new System.Drawing.Size(1032, 30);
			this.bottom_panel.Size = new System.Drawing.Size(1032, 30);

			// this
			this.Location = new System.Drawing.Point(4, 22);
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Size = new System.Drawing.Size(1038, 672);
			this.TabIndex = 1;
			this.Text = StepDeets.stepTitle(dance_style);
			this.UseVisualStyleBackColor = true;
			this.Resize += new System.EventHandler(this.tabPage2_Resize);

			// 
			// forward_button
			// 
			this.forward_button.BackColor = System.Drawing.Color.YellowGreen;
			this.forward_button.Size = new System.Drawing.Size(333, 28);
			this.forward_button.TabIndex = 1;
			this.forward_button.Text = "Continue to Dance Solo";
			this.forward_button.UseVisualStyleBackColor = false;
			this.forward_button.Click += new System.EventHandler(this.forward_button_Click);
			// 
			// config_button
			// 
			this.config_button.BackColor = System.Drawing.Color.LightGray;
			this.config_button.Size = new System.Drawing.Size(333, 28);
			this.config_button.TabIndex = 1;
			this.config_button.Text = StepDeets.stepTitle(dance_style) + " Options";
			this.config_button.UseVisualStyleBackColor = false;
			this.config_button.Click += new System.EventHandler(this.config_button_Click);
			// 
			// back_button
			// 
			this.back_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.back_button.Size = new System.Drawing.Size(333, 28);
			this.back_button.TabIndex = 0;
			this.back_button.Text = "Back to Instructions";
			this.back_button.UseVisualStyleBackColor = false;
			this.back_button.Click += new System.EventHandler(this.back_button_Click);

			// put it all together
			this.bottom_panel.Controls.Add(this.back_button);
			this.bottom_panel.Controls.Add(this.config_button);
			this.bottom_panel.Controls.Add(this.forward_button);
			foreach (NotesetPanel nsp in nsp_list)
			{
				this.nsp_panel.Controls.Add(nsp);
			}
			this.nsp_panel.Controls.Add(this.bottom_panel);
			this.Controls.Add(this.nsp_panel);
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}

		private void tabPage2_Resize(object sender, EventArgs e)
		{
			nsp_panel.Height = this.Height - instructionsTextboxGap;
		}

		private void bottom_panel_Resize(object sender, EventArgs e)
		{
			back_button.Width = bottom_panel.Width / 2 - 5;
			forward_button.Width = bottom_panel.Width / 2 - 5;
		}

		private void back_button_Click(object sender, EventArgs e)
		{
			if (prevTabPage  != null) {
				parentControl.SelectedTab = prevTabPage;
			}
		}

		private void forward_button_Click(object sender, EventArgs e)
		{
			if (nextTabPage != null)
			{
				parentControl.SelectedTab = nextTabPage;
			}
		}

		public void set_config(ConfigSettings c)
		{
			config = c;
			stepconfig.set_config(c);
			foreach (NotesetPanel np in nsp_list)
			{
				np.setConfig(c);
			}
		}

		private void config_button_Click(object sender, EventArgs e)
		{
			stepconfig.ShowDialog();
		}

		public NotesetParameters[] setNoteSetParametersList()
		{
			List<NotesetParameters> l = new List<NotesetParameters>();
			foreach (NotesetPanel np in nsp_list) {
				l.Add(np.getNotesetParameters());
			}
			return l.ToArray();
		}

		public void getNoteSetParametersList(Dictionary<string,NotesetParameters> npd)
		{
			int i = 0;
			foreach (NotesetPanel np in nsp_list)
			{
				string level = np.getLevel();
				NotesetParameters nsp = npd[level];
				np.setNotesetParameters(nsp);
				i++;
			}
		}

		public Dictionary<string, NotesetParameters> setNoteSetParametersDictionary()
		{
			Dictionary<string, NotesetParameters> this_page_params = new Dictionary<string, NotesetParameters>();
			foreach (NotesetPanel np in nsp_list)
			{
				this_page_params.Add(np.getLevel(), np.getNotesetParameters());
			}
			return this_page_params;
		}

		public void setPrev(string label, TabPage tp)
		{
			back_button.Text = "Go back to " + label;
			prevTabPage = tp;
		}
		public void setNext(string label, TabPage tp)
		{
			forward_button.Text = "Continue to " + label;
			nextTabPage = tp;
		}
	}
}

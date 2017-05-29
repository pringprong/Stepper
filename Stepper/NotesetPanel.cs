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
	public class NotesetPanel : Panel
	{
		ToolTip toolTip1;
		SampleWindow sw;
		Button sample1;
		Label level_label;

		Label every_2nd_label;
		NumericUpDown step_fill_nud;
		TrackBar step_fill_trackbar;
		Label no_arrow_label;
		Label arrow_label;

		Panel on_beat_panel;
		NumericUpDown on_beat_nud;
		TrackBar on_beat_trackbar;
		Label on_beat_plus_half_beat_label;
		Label on_beat_only_label;

		Panel jumps_panel;
		NumericUpDown jumps_nud;
		TrackBar jumps_trackbar;
		Label on_beat_label;
		Label jumps_slider_label;
		Label single_foot_slider_label;

		Panel halfbeat_panel;
		NumericUpDown half_beat_nud;
		TrackBar half_beat_trackbar;
		Label half_beat_label;
		Label quintuples_slider_label;
		Label triples_slider_label;

		Panel triple_type_panel;
		NumericUpDown triple_type_nud;
		TrackBar triple_type_trackbar;
		Label triples_label;
		Label ABC_label;
		Label ABA_label;

		Panel quintuple_type_panel;
		NumericUpDown quintuple_type_nud;
		TrackBar quintuple_type_trackbar;
		Label quintuples_label;
		Label ABCDE_label;
		Label ABABA_label;
		Label ABABC_label;

		Panel checkbox_panel;
		CheckBox alternate_foot;
		CheckBox arrow_repeat;
		CheckBox triples_on_1_only;
		CheckBox quintuples_on_1_only;
		CheckBox full8thStream;

		string dance_style;
		string sdlevel;
		int beats_per_measure;
		int measures_per_sample;
		private Pen blackpen;
		private Pen redpen;
		private Pen bluepen;
		Random r;

		public NotesetPanel(NotesetParameters np, int b, int m, Pen bla, Pen re, Pen blu, Random ra)
		{
			dance_style = np.dance_style;
			sdlevel = np.dance_level;
			beats_per_measure = b;
			measures_per_sample = m;
			blackpen = bla;
			redpen = re;
			bluepen = blu;
			r = ra;
			initialize();

			alternate_foot.Checked = np.alternating_foot;
			arrow_repeat.Checked = np.repeat_arrows;
			step_fill_trackbar.Value = np.percent_stepfill;
			on_beat_trackbar.Value = np.percent_onbeat;
			jumps_trackbar.Value = np.percent_jumps;
			triples_on_1_only.Checked = np.triples_on_1_only;
			quintuples_on_1_only.Checked = np.quintuples_on_1_only;
			triple_type_trackbar.Value = np.triple_type;
			quintuple_type_trackbar.Value = np.quintuple_type;
			full8thStream.Checked = np.full8th;
		}
		
		public NotesetPanel(string ds, string StepDeetslevel, int beats, int measures, Pen black, Pen red, Pen blue, Random random)
		{
			dance_style = ds;
			sdlevel = StepDeetslevel;
			beats_per_measure = beats;
			measures_per_sample = measures;
			blackpen = black;
			redpen = red;
			bluepen = blue;
			r = random;
			initialize();
		}

		public string getLevel()
		{
			return sdlevel;
		}

		public void setNotesetParameters(NotesetParameters np) {
			dance_style = np.dance_style;
			sdlevel = np.dance_level;
			alternate_foot.Checked = np.alternating_foot;
			arrow_repeat.Checked = np.repeat_arrows;
			step_fill_trackbar.Value = np.percent_stepfill;
			on_beat_trackbar.Value = np.percent_onbeat;
			jumps_trackbar.Value = np.percent_jumps;
			triples_on_1_only.Checked = np.triples_on_1_only;
			quintuples_on_1_only.Checked = np.quintuples_on_1_only;
			triple_type_trackbar.Value = np.triple_type;
			quintuple_type_trackbar.Value = np.quintuple_type;
			full8thStream.Checked = np.full8th;
		}

		public NotesetParameters getNotesetParameters()
		{
			return new NotesetParameters(dance_style, sdlevel, alternate_foot.Checked, arrow_repeat.Checked, step_fill_trackbar.Value,
				on_beat_trackbar.Value, jumps_trackbar.Value, half_beat_trackbar.Value, triples_on_1_only.Checked, 
				quintuples_on_1_only.Checked, triple_type_trackbar.Value, quintuple_type_trackbar.Value, full8thStream.Checked);
		}

		private void initialize()  {
			
			// Constructors
			sample1 = new Button();
			quintuple_type_panel = new Panel();
			triple_type_panel = new Panel();
			every_2nd_label = new Label();
			step_fill_nud = new NumericUpDown();
			on_beat_panel = new Panel();
			checkbox_panel = new Panel();
			no_arrow_label = new Label();
			arrow_label = new Label();
			step_fill_trackbar = new TrackBar();
			level_label = new Label();
			quintuple_type_nud = new NumericUpDown();
			quintuples_label = new Label();
			ABCDE_label = new Label();
			ABABA_label = new Label();
			ABABC_label = new Label();
			quintuple_type_trackbar = new TrackBar();
			triple_type_nud = new NumericUpDown();
			triples_label = new Label();
			ABC_label = new Label();
			ABA_label = new Label();
			triple_type_trackbar = new TrackBar();
			full8thStream = new CheckBox();
			quintuples_on_1_only = new CheckBox();
			triples_on_1_only = new CheckBox();
			arrow_repeat = new CheckBox();
			alternate_foot = new CheckBox();
			on_beat_nud = new NumericUpDown();
			on_beat_plus_half_beat_label = new Label();
			on_beat_only_label = new Label();
			halfbeat_panel = new Panel();
			jumps_panel = new Panel();
			on_beat_trackbar = new TrackBar();
			half_beat_label = new Label();
			quintuples_slider_label = new Label();
			triples_slider_label = new Label();
			half_beat_trackbar = new TrackBar();
			half_beat_nud = new NumericUpDown();
			on_beat_label = new Label();
			jumps_slider_label = new Label();
			single_foot_slider_label = new Label();
			jumps_trackbar = new TrackBar();
			jumps_nud = new NumericUpDown();
			toolTip1 = new ToolTip();

			// Initialize this
		//	InitializeComponent();
			this.Location = new System.Drawing.Point(12, 275);
			this.Size = new System.Drawing.Size(575, 248);
			this.BackColor = StepDeets.levelColor(sdlevel);
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Location = new System.Drawing.Point(3, 3);
			this.Size = new System.Drawing.Size(1020, 120);
			this.TabIndex = 5;

			// 
			// sample1
			// 
			this.sample1.Location = new System.Drawing.Point(66, 3);
			this.sample1.Size = new System.Drawing.Size(57, 22);
			this.sample1.TabIndex = 20;
			this.sample1.Text = "Sample";
			this.sample1.UseVisualStyleBackColor = true;
			this.sample1.Click += new System.EventHandler(this.sample1_Click);
			// 
			// quintuple_type_panel
			// 
			this.quintuple_type_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.quintuple_type_panel.Location = new System.Drawing.Point(705, 3);
			this.quintuple_type_panel.Size = new System.Drawing.Size(141, 110);
			this.quintuple_type_panel.TabIndex = 19;
			// 
			// triple_type_panel
			// 
			this.triple_type_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.triple_type_panel.Location = new System.Drawing.Point(594, 3);
			this.triple_type_panel.Size = new System.Drawing.Size(107, 110);
			this.triple_type_panel.TabIndex = 18;
			// 
			// every_2nd_label
			// 
			this.every_2nd_label.AutoSize = true;
			this.every_2nd_label.Location = new System.Drawing.Point(35, 58);
			this.every_2nd_label.Size = new System.Drawing.Size(55, 13);
			this.every_2nd_label.TabIndex = 11;
			this.every_2nd_label.Text = "Every 2nd";
			// 
			// step_fill_nud
			// 
			this.step_fill_nud.Location = new System.Drawing.Point(75, 36);
			this.step_fill_nud.Size = new System.Drawing.Size(42, 20);
			this.step_fill_nud.TabIndex = 5;
			this.step_fill_nud.Value = 60;
			this.step_fill_nud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.step_fill_nud.Value = new decimal(new int[] { 60, 0, 0, 0 });
			this.step_fill_nud.ValueChanged += new System.EventHandler(this.step_fill_nud_ValueChanged);
			// 
			// checkbox_panel
			// 
			this.checkbox_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.checkbox_panel.Location = new System.Drawing.Point(849, 3);
			this.checkbox_panel.Size = new System.Drawing.Size(162, 110);
			this.checkbox_panel.TabIndex = 9;
			// 
			// on_beat_panel
			// 
			this.on_beat_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.on_beat_panel.Location = new System.Drawing.Point(129, 3);
			this.on_beat_panel.Size = new System.Drawing.Size(459, 110);
			this.on_beat_panel.TabIndex = 10;
			// 
			// no_arrow_label
			// 
			this.no_arrow_label.AutoSize = true;
			this.no_arrow_label.Location = new System.Drawing.Point(35, 95);
			this.no_arrow_label.Size = new System.Drawing.Size(50, 13);
			this.no_arrow_label.TabIndex = 8;
			this.no_arrow_label.Text = "No arrow";
			// 
			// arrow_label
			// 
			this.arrow_label.AutoSize = true;
			this.arrow_label.Location = new System.Drawing.Point(35, 25);
			this.arrow_label.Size = new System.Drawing.Size(34, 13);
			this.arrow_label.TabIndex = 7;
			this.arrow_label.Text = "Arrow";
			// 
			// step_fill_trackbar
			// 
			this.step_fill_trackbar.Location = new System.Drawing.Point(5, 20);
			this.step_fill_trackbar.Maximum = 100;
			this.step_fill_trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.step_fill_trackbar.Size = new System.Drawing.Size(45, 90);
			this.step_fill_trackbar.TabIndex = 1;
			this.step_fill_trackbar.TickFrequency = 10;
			this.step_fill_trackbar.Value = 60;
			this.step_fill_trackbar.ValueChanged += new System.EventHandler(this.step_fill_trackbar_ValueChanged);
			// 
			// level_label
			// 
			this.level_label.AutoSize = true;
			this.level_label.BackColor = StepDeets.labelColor(sdlevel);
			this.level_label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.level_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.level_label.Location = new System.Drawing.Point(2, 2);
			this.level_label.Size = new System.Drawing.Size(53, 19);
			this.level_label.TabIndex = 0;
			this.level_label.Text = StepDeets.levelTitle(sdlevel);
			// 
			// quintuple_type_nud
			// 
			this.quintuple_type_nud.Location = new System.Drawing.Point(10, 42);
			this.quintuple_type_nud.Size = new System.Drawing.Size(42, 20);
			this.quintuple_type_nud.TabIndex = 8;
			this.quintuple_type_nud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.quintuple_type_nud.ValueChanged += new System.EventHandler(this.quintuple_type_nud_ValueChanged);
			// 
			// quintuples_label
			// 
			this.quintuples_label.AutoSize = true;
			this.quintuples_label.Location = new System.Drawing.Point(1, 8);
			this.quintuples_label.Size = new System.Drawing.Size(60, 13);
			this.quintuples_label.TabIndex = 16;
			this.quintuples_label.Text = "Quintuples:";
			// 
			// ABCDE_label
			// 
			this.ABCDE_label.AutoSize = true;
			this.ABCDE_label.Location = new System.Drawing.Point(89, 88);
			this.ABCDE_label.Size = new System.Drawing.Size(43, 13);
			this.ABCDE_label.TabIndex = 13;
			this.ABCDE_label.Text = "ABCDE";
			// 
			// ABABA_label
			// 
			this.ABABA_label.AutoSize = true;
			this.ABABA_label.Location = new System.Drawing.Point(89, 7);
			this.ABABA_label.Size = new System.Drawing.Size(42, 13);
			this.ABABA_label.TabIndex = 15;
			this.ABABA_label.Text = "ABABA";
			// 
			// ABABC_label
			// 
			this.ABABC_label.AutoSize = true;
			this.ABABC_label.Location = new System.Drawing.Point(89, 47);
			this.ABABC_label.Size = new System.Drawing.Size(42, 13);
			this.ABABC_label.TabIndex = 15;
			this.ABABC_label.Text = "ABABC";
			// 
			// quintuple_type_trackbar
			// 
			this.quintuple_type_trackbar.Location = new System.Drawing.Point(63, 3);
			this.quintuple_type_trackbar.Maximum = 100;
			this.quintuple_type_trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.quintuple_type_trackbar.RightToLeftLayout = true;
			this.quintuple_type_trackbar.Size = new System.Drawing.Size(45, 105);
			this.quintuple_type_trackbar.TabIndex = 14;
			this.quintuple_type_trackbar.TickFrequency = 10;
			this.quintuple_type_trackbar.ValueChanged += new System.EventHandler(this.quintuple_type_trackbar_ValueChanged);
			// 
			// triple_type_nud
			// 
			this.triple_type_nud.Location = new System.Drawing.Point(10, 42);
			this.triple_type_nud.Size = new System.Drawing.Size(42, 20);
			this.triple_type_nud.TabIndex = 8;
			this.triple_type_nud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.triple_type_nud.ValueChanged += new System.EventHandler(this.triple_type_nud_ValueChanged);
			// 
			// triples_label
			// 
			this.triples_label.AutoSize = true;
			this.triples_label.Location = new System.Drawing.Point(1, 8);
			this.triples_label.Size = new System.Drawing.Size(41, 13);
			this.triples_label.TabIndex = 16;
			this.triples_label.Text = "Triples:";
			// 
			// ABC_label
			// 
			this.ABC_label.AutoSize = true;
			this.ABC_label.Location = new System.Drawing.Point(70, 88);
			this.ABC_label.Size = new System.Drawing.Size(28, 13);
			this.ABC_label.TabIndex = 13;
			this.ABC_label.Text = "ABC";
			// 
			// ABA_label
			// 
			this.ABA_label.AutoSize = true;
			this.ABA_label.Location = new System.Drawing.Point(70, 7);
			this.ABA_label.Size = new System.Drawing.Size(28, 13);
			this.ABA_label.TabIndex = 15;
			this.ABA_label.Text = "ABA";
			// 
			// triple_type_trackbar
			// 
			this.triple_type_trackbar.Location = new System.Drawing.Point(50, 3);
			this.triple_type_trackbar.Maximum = 100;
			this.triple_type_trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.triple_type_trackbar.RightToLeftLayout = true;
			this.triple_type_trackbar.Size = new System.Drawing.Size(45, 105);
			this.triple_type_trackbar.TabIndex = 14;
			this.triple_type_trackbar.TickFrequency = 10;
			this.triple_type_trackbar.ValueChanged += new System.EventHandler(this.triple_type_trackbar_ValueChanged);

			// 
			// full8thStream
			// 
			this.full8thStream.AutoSize = true;
			this.full8thStream.Location = new System.Drawing.Point(5, 85);
			this.full8thStream.Size = new System.Drawing.Size(94, 17);
			this.full8thStream.TabIndex = 7;
			this.full8thStream.Text = "Full 8th stream";
			this.full8thStream.UseVisualStyleBackColor = true;
			this.full8thStream.CheckedChanged += new System.EventHandler(this.full8thStream_CheckedChanged);
			// 
			// quintuples_on_1_or_2
			// 
			this.quintuples_on_1_only.AutoSize = true;
			this.quintuples_on_1_only.Location = new System.Drawing.Point(5, 65);
			this.quintuples_on_1_only.Size = new System.Drawing.Size(150, 17);
			this.quintuples_on_1_only.TabIndex = 6;
			this.quintuples_on_1_only.Text = "Quintuples only on first beat";
			this.quintuples_on_1_only.UseVisualStyleBackColor = true;
			// 
			// triples_on_1_and_3
			// 
			this.triples_on_1_only.AutoSize = true;
			this.triples_on_1_only.Location = new System.Drawing.Point(5, 45);
			this.triples_on_1_only.Size = new System.Drawing.Size(135, 17);
			this.triples_on_1_only.TabIndex = 5;
			this.triples_on_1_only.Text = "Triples only on first beat";
			this.triples_on_1_only.UseVisualStyleBackColor = true;
			// 
			// arrow_repeat
			// 
			this.arrow_repeat.AutoSize = true;
			this.arrow_repeat.Checked = true;
			this.arrow_repeat.CheckState = System.Windows.Forms.CheckState.Checked;
			this.arrow_repeat.Location = new System.Drawing.Point(5, 25);
			this.arrow_repeat.Size = new System.Drawing.Size(86, 17);
			this.arrow_repeat.TabIndex = 4;
			this.arrow_repeat.Text = "Arrow repeat";
			this.arrow_repeat.UseVisualStyleBackColor = true;
			// 
			// alternate_foot
			// 
			this.alternate_foot.AutoSize = true;
			this.alternate_foot.Checked = true;
			this.alternate_foot.CheckState = System.Windows.Forms.CheckState.Checked;
			this.alternate_foot.Location = new System.Drawing.Point(5, 5);
			this.alternate_foot.Size = new System.Drawing.Size(100, 17);
			this.alternate_foot.TabIndex = 3;
			this.alternate_foot.Text = "Alternating feet ";
			this.alternate_foot.UseVisualStyleBackColor = true;
			// 
			// on_beat_nud
			// 
			this.on_beat_nud.Location = new System.Drawing.Point(62, 44);
			this.on_beat_nud.Size = new System.Drawing.Size(42, 20);
			this.on_beat_nud.TabIndex = 11;
			this.on_beat_nud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.on_beat_nud.Value = new decimal(new int[] {100,0,0,0});
			this.on_beat_nud.ValueChanged += new System.EventHandler(this.on_beat_nud_ValueChanged);
			// 
			// on_beat_plus_half_beat_label
			// 
			this.on_beat_plus_half_beat_label.AutoSize = true;
			this.on_beat_plus_half_beat_label.Location = new System.Drawing.Point(6, 87);
			this.on_beat_plus_half_beat_label.Size = new System.Drawing.Size(98, 13);
			this.on_beat_plus_half_beat_label.TabIndex = 4;
			this.on_beat_plus_half_beat_label.Text = "On beat + half beat";
			// 
			// on_beat_only_label
			// 
			this.on_beat_only_label.AutoSize = true;
			this.on_beat_only_label.Location = new System.Drawing.Point(37, 11);
			this.on_beat_only_label.Size = new System.Drawing.Size(67, 13);
			this.on_beat_only_label.TabIndex = 3;
			this.on_beat_only_label.Text = "On beat only";
			// 
			// halfbeat_panel
			// 
			this.halfbeat_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.halfbeat_panel.Location = new System.Drawing.Point(141, 55);
			this.halfbeat_panel.Size = new System.Drawing.Size(310, 50);
			this.halfbeat_panel.TabIndex = 16;
			// 
			// jumps_panel
			// 
			this.jumps_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.jumps_panel.Location = new System.Drawing.Point(141, 2);
			this.jumps_panel.Size = new System.Drawing.Size(310, 50);
			this.jumps_panel.TabIndex = 12;
			// 
			// on_beat_trackbar
			// 
			this.on_beat_trackbar.Location = new System.Drawing.Point(89, 3);
			this.on_beat_trackbar.Maximum = 100;
			this.on_beat_trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.on_beat_trackbar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.on_beat_trackbar.RightToLeftLayout = true;
			this.on_beat_trackbar.Size = new System.Drawing.Size(45, 102);
			this.on_beat_trackbar.TabIndex = 2;
			this.on_beat_trackbar.TickFrequency = 10;
			this.on_beat_trackbar.Value = 100;
			this.on_beat_trackbar.ValueChanged += new System.EventHandler(this.on_beat_trackbar_ValueChanged);
			// 
			// on_beat_label
			// 
			this.on_beat_label.AutoSize = true;
			this.on_beat_label.Location = new System.Drawing.Point(8, 7);
			this.on_beat_label.Size = new System.Drawing.Size(48, 13);
			this.on_beat_label.TabIndex = 17;
			this.on_beat_label.Text = "On beat:";
			// 
			// half_beat_label
			// 
			this.half_beat_label.AutoSize = true;
			this.half_beat_label.Location = new System.Drawing.Point(9, 8);
			this.half_beat_label.Size = new System.Drawing.Size(53, 13);
			this.half_beat_label.TabIndex = 16;
			this.half_beat_label.Text = "Half-beat:";
			// 
			// quintuples_slider_label
			// 
			this.quintuples_slider_label.AutoSize = true;
			this.quintuples_slider_label.Location = new System.Drawing.Point(214, 28);
			this.quintuples_slider_label.Size = new System.Drawing.Size(57, 13);
			this.quintuples_slider_label.TabIndex = 13;
			this.quintuples_slider_label.Text = "Quintuples";
			// 
			// triples_slider_label
			// 
			this.triples_slider_label.AutoSize = true;
			this.triples_slider_label.Location = new System.Drawing.Point(69, 28);
			this.triples_slider_label.Size = new System.Drawing.Size(38, 13);
			this.triples_slider_label.TabIndex = 15;
			this.triples_slider_label.Text = "Triples";
			// 
			// half_beat_trackbar
			// 
			this.half_beat_trackbar.Location = new System.Drawing.Point(67, 3);
			this.half_beat_trackbar.Maximum = 100;
			this.half_beat_trackbar.RightToLeftLayout = true;
			this.half_beat_trackbar.Size = new System.Drawing.Size(177, 45);
			this.half_beat_trackbar.TabIndex = 14;
			this.half_beat_trackbar.TickFrequency = 10;
			this.half_beat_trackbar.ValueChanged += new System.EventHandler(this.half_beat_trackbar_ValueChanged);
			// 
			// half_beat_nud
			// 
			this.half_beat_nud.Location = new System.Drawing.Point(257, 6);
			this.half_beat_nud.Size = new System.Drawing.Size(42, 20);
			this.half_beat_nud.TabIndex = 8;
			this.half_beat_nud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.half_beat_nud.ValueChanged += new System.EventHandler(this.half_beat_nud_ValueChanged);
			// 
			// jumps_slider_label
			// 
			this.jumps_slider_label.AutoSize = true;
			this.jumps_slider_label.Location = new System.Drawing.Point(214, 28);
			this.jumps_slider_label.Size = new System.Drawing.Size(37, 13);
			this.jumps_slider_label.TabIndex = 13;
			this.jumps_slider_label.Text = "Jumps";
			// 
			// single_foot_slider_label
			// 
			this.single_foot_slider_label.AutoSize = true;
			this.single_foot_slider_label.Location = new System.Drawing.Point(69, 28);
			this.single_foot_slider_label.Size = new System.Drawing.Size(57, 13);
			this.single_foot_slider_label.TabIndex = 15;
			this.single_foot_slider_label.Text = "Single foot";
			// 
			// jumps_trackbar
			// 
			this.jumps_trackbar.Location = new System.Drawing.Point(67, 3);
			this.jumps_trackbar.Maximum = 100;
			this.jumps_trackbar.RightToLeftLayout = true;
			this.jumps_trackbar.Size = new System.Drawing.Size(177, 45);
			this.jumps_trackbar.TabIndex = 14;
			this.jumps_trackbar.TickFrequency = 10;
			this.jumps_trackbar.ValueChanged += new System.EventHandler(this.jumps_trackbar_ValueChanged);
			// 
			// jumps_nud
			// 
			this.jumps_nud.Location = new System.Drawing.Point(257, 6);
			this.jumps_nud.Size = new System.Drawing.Size(42, 20);
			this.jumps_nud.TabIndex = 8;
			this.jumps_nud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.jumps_nud.ValueChanged += new System.EventHandler(this.jumps_nud_ValueChanged);

			// Set up the delays for the ToolTip.
			toolTip1.AutoPopDelay = 5000;
			toolTip1.InitialDelay = 200;
			toolTip1.ReshowDelay = 200;
			// Force the ToolTip text to be displayed whether or not the form is active.
			toolTip1.ShowAlways = true;

			toolTip1.SetToolTip(this.level_label, "Settings for the \"" +  StepDeets.stepTitle(dance_style) + " - " + StepDeets.levelTitle(sdlevel) + "\" level in Stepmania");
			toolTip1.SetToolTip(this.sample1, "Show an example of what kind of arrows will be generated with current settings");

			toolTip1.SetToolTip(this.step_fill_trackbar, "Percentage of beats that should have an arrow");
			toolTip1.SetToolTip(this.step_fill_nud, "Percentage of beats that should have an arrow");
			toolTip1.SetToolTip(this.every_2nd_label, "Set the slider here to generate an arrow for every second beat");
			toolTip1.SetToolTip(this.no_arrow_label, "Set the slider down here to generate very few arrows");
			toolTip1.SetToolTip(this.arrow_label, "Set the slider up here to generate many arrows");

			toolTip1.SetToolTip(this.on_beat_nud, "Beats with arrows: percentage that should have only on-beat arrows, no half-beat arrows");
			toolTip1.SetToolTip(this.on_beat_trackbar, "Beats with arrows: percentage that should have only on-beat arrows, no half-beat arrows");
			toolTip1.SetToolTip(this.on_beat_only_label, "Set the slider here to generate only arrows on the beat (easier)");
			toolTip1.SetToolTip(this.on_beat_plus_half_beat_label, "Set the slider here to generate triples and quintuples in addition to on-beat arrows");

			toolTip1.SetToolTip(this.jumps_nud, "Beats with on-beat arrows: percentage of jumps (both feet) compared to single-foot arrows");
			toolTip1.SetToolTip(this.jumps_trackbar, "Beats with on-beat arrows: percentage of jumps (both feet) compared to single-foot arrows");
			toolTip1.SetToolTip(this.on_beat_label, "Beats with on-beat arrows: percentage of jumps (both feet) compared to single-foot arrows");
			toolTip1.SetToolTip(this.single_foot_slider_label, "Set the slider here to generate fewer jumps (easier)");
			toolTip1.SetToolTip(this.jumps_slider_label, "Set the slider here to generate more jumps (challenging)");

			toolTip1.SetToolTip(this.half_beat_nud, "Beats with half-beat arrows: percentage of quintuples vs triples");
			toolTip1.SetToolTip(this.half_beat_trackbar, "Beats with half-beat arrows: percentage of quintuples vs triples");
			toolTip1.SetToolTip(this.half_beat_label, "Beats with half-beat arrows: percentage of quintuples vs triples");
			toolTip1.SetToolTip(this.triples_slider_label, "Set the slider here to generate mainly triples (easier)");
			toolTip1.SetToolTip(this.quintuples_slider_label, "Set the slider here to generate mainly quintuples (challenging)");

			toolTip1.SetToolTip(this.triple_type_nud, "Triples: Percentage of triples in the easier ABA form compared to the challenging ABC form");
			toolTip1.SetToolTip(this.triple_type_trackbar, "Triples: Percentage of triples in the easier ABA form compared to the challenging ABC form");
			toolTip1.SetToolTip(this.triples_label, "Triples: Percentage of triples in the easier ABA form compared to the challenging ABC form");
			toolTip1.SetToolTip(this.ABA_label, "Set the slider here to generate easier ABA triples");
			toolTip1.SetToolTip(this.ABC_label, "Set the slider here to generate challenging ABC triples");

			toolTip1.SetToolTip(this.quintuple_type_nud, "Percentage of quintuples in the easier ABABA form compared to the challenging ABCDE form");
			toolTip1.SetToolTip(this.quintuples_label, "Percentage of quintuples in the easier ABABA form compared to the challenging ABCDE form");
			toolTip1.SetToolTip(this.quintuple_type_trackbar, "Percentage of quintuples in the easier ABABA form compared to the challenging ABCDE form");
			toolTip1.SetToolTip(this.ABABA_label, "Set the slider here to generate easier ABABA quintuples");
			toolTip1.SetToolTip(this.ABABC_label, "Set the slider here to generate moderate ABABC quintuples");
			toolTip1.SetToolTip(this.ABCDE_label, "Set the slider here to generate challenging ABCDE quintuples");

			toolTip1.SetToolTip(this.alternate_foot, "Check if you want single steps to always alternate between left and right foot");
			toolTip1.SetToolTip(this.arrow_repeat, "Allow the same arrow twice in a row");
			toolTip1.SetToolTip(this.triples_on_1_only, "Allow triples on 1st beat only. Uncheck for triples on 2nd and 3rd beats");
			toolTip1.SetToolTip(this.quintuples_on_1_only, "Allow quintuples 1st beat only. Uncheck for quintuples on 2nd beat as well");
			toolTip1.SetToolTip(this.full8thStream, "Check if you want a full set of 8 arrows per measure and ignore settings for triples and quintuples");

			// put it all together
			this.quintuple_type_panel.Controls.Add(this.quintuples_label);
			this.quintuple_type_panel.Controls.Add(this.quintuple_type_nud);
			this.quintuple_type_panel.Controls.Add(this.ABCDE_label);
			this.quintuple_type_panel.Controls.Add(this.ABABA_label);
			this.quintuple_type_panel.Controls.Add(this.ABABC_label);
			this.quintuple_type_panel.Controls.Add(this.quintuple_type_trackbar);
			this.triple_type_panel.Controls.Add(this.triple_type_nud);
			this.triple_type_panel.Controls.Add(this.triples_label);
			this.triple_type_panel.Controls.Add(this.ABC_label);
			this.triple_type_panel.Controls.Add(this.ABA_label);
			this.triple_type_panel.Controls.Add(this.triple_type_trackbar);
			this.checkbox_panel.Controls.Add(this.full8thStream);
			this.checkbox_panel.Controls.Add(this.quintuples_on_1_only);
			this.checkbox_panel.Controls.Add(this.triples_on_1_only);
			this.checkbox_panel.Controls.Add(this.arrow_repeat);
			this.checkbox_panel.Controls.Add(this.alternate_foot);
			this.on_beat_panel.Controls.Add(this.on_beat_nud);
			this.on_beat_panel.Controls.Add(this.on_beat_plus_half_beat_label);
			this.on_beat_panel.Controls.Add(this.on_beat_only_label);
			this.on_beat_panel.Controls.Add(this.halfbeat_panel);
			this.on_beat_panel.Controls.Add(this.jumps_panel);
			this.on_beat_panel.Controls.Add(this.on_beat_trackbar);
			this.Controls.Add(this.sample1);
			this.Controls.Add(this.quintuple_type_panel);
			this.Controls.Add(this.triple_type_panel);
			this.Controls.Add(this.every_2nd_label);
			this.Controls.Add(this.step_fill_nud);
			this.Controls.Add(this.on_beat_panel);
			this.Controls.Add(this.checkbox_panel);
			this.Controls.Add(this.no_arrow_label);
			this.Controls.Add(this.arrow_label);
			this.Controls.Add(this.step_fill_trackbar);
			this.Controls.Add(this.level_label);
			this.halfbeat_panel.Controls.Add(this.half_beat_label);
			this.halfbeat_panel.Controls.Add(this.quintuples_slider_label);
			this.halfbeat_panel.Controls.Add(this.triples_slider_label);
			this.halfbeat_panel.Controls.Add(this.half_beat_trackbar);
			this.halfbeat_panel.Controls.Add(this.half_beat_nud);
			this.jumps_panel.Controls.Add(this.on_beat_label);
			this.jumps_panel.Controls.Add(this.jumps_slider_label);
			this.jumps_panel.Controls.Add(this.single_foot_slider_label);
			this.jumps_panel.Controls.Add(this.jumps_trackbar);
			this.jumps_panel.Controls.Add(this.jumps_nud);
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}

		private void sample1_Click(object sender, EventArgs e)
		{
			NotesetParameters np = new NotesetParameters(dance_style, sdlevel, alternate_foot.Checked, arrow_repeat.Checked, (int)step_fill_nud.Value,
				(int)on_beat_nud.Value, (int)jumps_nud.Value, (int)half_beat_nud.Value, triples_on_1_only.Checked, quintuples_on_1_only.Checked, 
				triple_type_trackbar.Value, quintuple_type_trackbar.Value, full8thStream.Checked);
			Noteset sample_noteset = new Noteset(np, StepDeets.SM, measures_per_sample, r);
			sample_noteset.generateSteps();
			char[] f = sample_noteset.getFeet();
			string[] s = sample_noteset.getSteps();
			sw = new SampleWindow(dance_style, StepDeets.levelTitle(sdlevel), measures_per_sample, f, s, blackpen, redpen, bluepen);
			sw.Show();
		}
		private void step_fill_nud_ValueChanged(object sender, EventArgs e)
		{
			step_fill_trackbar.Value = Convert.ToInt32(step_fill_nud.Value);
		}

		private void step_fill_trackbar_ValueChanged(object sender, EventArgs e)
		{
			step_fill_nud.Value = step_fill_trackbar.Value;
		}

		private void on_beat_trackbar_ValueChanged(object sender, EventArgs e)
		{
			on_beat_nud.Value = on_beat_trackbar.Value;
		}

		private void on_beat_nud_ValueChanged(object sender, EventArgs e)
		{
			on_beat_trackbar.Value = Convert.ToInt32(on_beat_nud.Value);
		}

		private void jumps_trackbar_ValueChanged(object sender, EventArgs e)
		{
			jumps_nud.Value = jumps_trackbar.Value;
		}

		private void jumps_nud_ValueChanged(object sender, EventArgs e)
		{
			jumps_trackbar.Value = Convert.ToInt32(jumps_nud.Value);
		}

		private void half_beat_trackbar_ValueChanged(object sender, EventArgs e)
		{
			half_beat_nud.Value = half_beat_trackbar.Value;
		}

		private void half_beat_nud_ValueChanged(object sender, EventArgs e)
		{
			half_beat_trackbar.Value = Convert.ToInt32(half_beat_nud.Value);
		}

		private void full8thStream_CheckedChanged(object sender, EventArgs e)
		{
			if (full8thStream.Checked)
			{
				halfbeat_panel.Enabled = false;
				triple_type_panel.Enabled = false;
				quintuple_type_panel.Enabled = false;
				triples_on_1_only.Enabled = false;
				quintuples_on_1_only.Enabled = false;
				on_beat_nud.Enabled = false;
				on_beat_trackbar.Enabled = false;
				on_beat_plus_half_beat_label.Enabled = false;
				on_beat_only_label.Enabled = false;
				full8thStream.Font = new Font(full8thStream.Font, FontStyle.Bold);
			}
			else
			{
				halfbeat_panel.Enabled = true;
				triple_type_panel.Enabled = true;
				quintuple_type_panel.Enabled = true;
				triples_on_1_only.Enabled = true;
				quintuples_on_1_only.Enabled = true;
				on_beat_nud.Enabled = true;
				on_beat_trackbar.Enabled = true;
				on_beat_plus_half_beat_label.Enabled = true;
				on_beat_only_label.Enabled = true;
				full8thStream.Font = new Font(full8thStream.Font, FontStyle.Regular);
			}
		}
		private void triple_type_trackbar_ValueChanged(object sender, EventArgs e)
		{
			triple_type_nud.Value = triple_type_trackbar.Value;
		}

		private void triple_type_nud_ValueChanged(object sender, EventArgs e)
		{
			triple_type_trackbar.Value = Convert.ToInt32(triple_type_nud.Value);
		}

		private void quintuple_type_trackbar_ValueChanged(object sender, EventArgs e)
		{
			quintuple_type_nud.Value = quintuple_type_trackbar.Value;
		}

		private void quintuple_type_nud_ValueChanged(object sender, EventArgs e)
		{
			quintuple_type_trackbar.Value = Convert.ToInt32(quintuple_type_nud.Value);
		}
	}
}

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
		private System.ComponentModel.IContainer components = null;
		Button sample1;
		Panel panel28;
		Panel panel16;
		Label label11;
		NumericUpDown stepFill;
		Panel panel3;
		Panel panel2;
		Label label18;
		Label label17;
		TrackBar stepFill_trackbar;
		Label level;
		NumericUpDown quintupleType;
		Label label59;
		Label label60;
		Label label61;
		TrackBar quintupleTypetrackbar;
		NumericUpDown tripleType;
		Label label56;
		Label label57;
		Label label58;
		TrackBar tripleTypetrackbar;
		CheckBox full8thStream;
		CheckBox quintuples_on_1_or_2;
		CheckBox triples_on_1_and_3;
		CheckBox arrow_repeat;
		CheckBox alternate_foot;
		NumericUpDown onBeat;
		Label label20;
		Label label19;
		Panel panel5;
		Panel panel4;
		TrackBar onBeatTrackbar;
		Label label25;
		Label label23;
		Label label24;
		TrackBar quintuplesTrackbar;
		NumericUpDown quintuples;
		Label label26;
		Label label22;
		Label label21;
		TrackBar jumpsTrackbar;
		NumericUpDown jumps;
		ToolTip toolTip1;
		SampleWindow sw;

		string dance_style;
		string sdlevel;
		int beats_per_measure;
		int measures_per_sample;
		private Pen blackpen;
		private Pen redpen;
		private Pen bluepen;
		Random r;

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
			
			// Constructors
			sample1 = new Button();
			panel28 = new Panel();
			panel16 = new Panel();
			label11 = new Label();
			stepFill = new NumericUpDown();
			panel3 = new Panel();
			panel2 = new Panel();
			label18 = new Label();
			label17 = new Label();
			stepFill_trackbar = new TrackBar();
			level = new Label();
			quintupleType = new NumericUpDown();
			label59 = new Label();
			label60 = new Label();
			label61 = new Label();
			quintupleTypetrackbar = new TrackBar();
			tripleType = new NumericUpDown();
			label56 = new Label();
			label57 = new Label();
			label58 = new Label();
			tripleTypetrackbar = new TrackBar();
			full8thStream = new CheckBox();
			quintuples_on_1_or_2 = new CheckBox();
			triples_on_1_and_3 = new CheckBox();
			arrow_repeat = new CheckBox();
			alternate_foot = new CheckBox();
			onBeat = new NumericUpDown();
			label20 = new Label();
			label19 = new Label();
			panel5 = new Panel();
			panel4 = new Panel();
			onBeatTrackbar = new TrackBar();
			label25 = new Label();
			label23 = new Label();
			label24 = new Label();
			quintuplesTrackbar = new TrackBar();
			quintuples = new NumericUpDown();
			label26 = new Label();
			label22 = new Label();
			label21 = new Label();
			jumpsTrackbar = new TrackBar();
			jumps = new NumericUpDown();
			toolTip1 = new ToolTip();

			// Initialize this
			InitializeComponent();
			this.Location = new System.Drawing.Point(12, 275);
			this.Size = new System.Drawing.Size(575, 248);
			this.BackColor = StepDeets.levelColor(StepDeetslevel);
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Location = new System.Drawing.Point(3, 3);
			this.Size = new System.Drawing.Size(1020, 120);
			this.TabIndex = 5;

			// 
			// sample1
			// 
			this.sample1.Location = new System.Drawing.Point(66, 3);
			this.sample1.Name = "sample1";
			this.sample1.Size = new System.Drawing.Size(57, 22);
			this.sample1.TabIndex = 20;
			this.sample1.Text = "Sample";
			this.sample1.UseVisualStyleBackColor = true;
			this.sample1.Click += new System.EventHandler(this.sample1_Click);
			// 
			// panel28
			// 
			this.panel28.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel28.Enabled = false;
			this.panel28.Location = new System.Drawing.Point(715, 3);
			this.panel28.Size = new System.Drawing.Size(131, 110);
			this.panel28.TabIndex = 19;
			// 
			// panel16
			// 
			this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel16.Enabled = false;
			this.panel16.Location = new System.Drawing.Point(594, 3);
			this.panel16.Size = new System.Drawing.Size(117, 112);
			this.panel16.TabIndex = 18;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(35, 58);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(55, 13);
			this.label11.TabIndex = 11;
			this.label11.Text = "Every 2nd";
			// 
			// stepFill
			// 
			this.stepFill.Location = new System.Drawing.Point(75, 36);
			this.stepFill.Name = "stepFill";
			this.stepFill.Size = new System.Drawing.Size(42, 20);
			this.stepFill.TabIndex = 5;
			this.stepFill.Value = 60;
			this.stepFill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.stepFill.Value = new decimal(new int[] { 60, 0, 0, 0 });
			this.stepFill.ValueChanged += new System.EventHandler(this.stepFill_ValueChanged);
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel2.Location = new System.Drawing.Point(849, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(157, 110);
			this.panel2.TabIndex = 9;
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel3.Location = new System.Drawing.Point(129, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(459, 110);
			this.panel3.TabIndex = 10;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(35, 95);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(50, 13);
			this.label18.TabIndex = 8;
			this.label18.Text = "No arrow";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(35, 25);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(34, 13);
			this.label17.TabIndex = 7;
			this.label17.Text = "Arrow";
			// 
			// stepFill_trackbar
			// 
			this.stepFill_trackbar.Location = new System.Drawing.Point(5, 20);
			this.stepFill_trackbar.Maximum = 100;
			this.stepFill_trackbar.Name = "stepFill_trackbar";
			this.stepFill_trackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.stepFill_trackbar.Size = new System.Drawing.Size(45, 90);
			this.stepFill_trackbar.TabIndex = 1;
			this.stepFill_trackbar.TickFrequency = 10;
			this.stepFill_trackbar.Value = 60;
			this.stepFill_trackbar.ValueChanged += new System.EventHandler(this.stepFill_trackbar_ValueChanged);
			// 
			// level
			// 
			this.level.AutoSize = true;
			this.level.BackColor = StepDeets.labelColor(StepDeetslevel);
			this.level.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.level.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.level.Location = new System.Drawing.Point(2, 2);
			this.level.Name = "level";
			this.level.Size = new System.Drawing.Size(53, 19);
			this.level.TabIndex = 0;
			this.level.Text = StepDeets.levelTitle(StepDeetslevel);
			// 
			// quintupleType
			// 
			this.quintupleType.Location = new System.Drawing.Point(82, 42);
			this.quintupleType.Name = "quintupleType";
			this.quintupleType.Size = new System.Drawing.Size(42, 20);
			this.quintupleType.TabIndex = 8;
			this.quintupleType.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.quintupleType.ValueChanged += new System.EventHandler(this.quintupleType_ValueChanged);
			// 
			// label59
			// 
			this.label59.AutoSize = true;
			this.label59.Location = new System.Drawing.Point(1, 8);
			this.label59.Name = "label59";
			this.label59.Size = new System.Drawing.Size(60, 13);
			this.label59.TabIndex = 16;
			this.label59.Text = "Quintuples:";
			// 
			// label60
			// 
			this.label60.AutoSize = true;
			this.label60.Location = new System.Drawing.Point(79, 88);
			this.label60.Name = "label60";
			this.label60.Size = new System.Drawing.Size(43, 13);
			this.label60.TabIndex = 13;
			this.label60.Text = "ABCDE";
			// 
			// label61
			// 
			this.label61.AutoSize = true;
			this.label61.Location = new System.Drawing.Point(79, 7);
			this.label61.Name = "label61";
			this.label61.Size = new System.Drawing.Size(42, 13);
			this.label61.TabIndex = 15;
			this.label61.Text = "ABABA";
			// 
			// quintupleTypetrackbar
			// 
			this.quintupleTypetrackbar.Location = new System.Drawing.Point(53, 3);
			this.quintupleTypetrackbar.Maximum = 100;
			this.quintupleTypetrackbar.Name = "quintupleTypetrackbar";
			this.quintupleTypetrackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.quintupleTypetrackbar.RightToLeftLayout = true;
			this.quintupleTypetrackbar.Size = new System.Drawing.Size(45, 105);
			this.quintupleTypetrackbar.TabIndex = 14;
			this.quintupleTypetrackbar.TickFrequency = 10;
			this.quintupleTypetrackbar.ValueChanged += new System.EventHandler(this.quintupleTypetrackbar_ValueChanged);
			// 
			// tripleType
			// 
			this.tripleType.Location = new System.Drawing.Point(65, 42);
			this.tripleType.Name = "tripleType";
			this.tripleType.Size = new System.Drawing.Size(42, 20);
			this.tripleType.TabIndex = 8;
			this.tripleType.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tripleType.ValueChanged += new System.EventHandler(this.tripleType_ValueChanged);
			// 
			// label56
			// 
			this.label56.AutoSize = true;
			this.label56.Location = new System.Drawing.Point(1, 8);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(41, 13);
			this.label56.TabIndex = 16;
			this.label56.Text = "Triples:";
			// 
			// label57
			// 
			this.label57.AutoSize = true;
			this.label57.Location = new System.Drawing.Point(62, 88);
			this.label57.Name = "label57";
			this.label57.Size = new System.Drawing.Size(28, 13);
			this.label57.TabIndex = 13;
			this.label57.Text = "ABC";
			// 
			// label58
			// 
			this.label58.AutoSize = true;
			this.label58.Location = new System.Drawing.Point(62, 7);
			this.label58.Name = "label58";
			this.label58.Size = new System.Drawing.Size(28, 13);
			this.label58.TabIndex = 15;
			this.label58.Text = "ABA";
			// 
			// tripleTypetrackbar
			// 
			this.tripleTypetrackbar.Location = new System.Drawing.Point(40, 1);
			this.tripleTypetrackbar.Maximum = 100;
			this.tripleTypetrackbar.Name = "tripleTypetrackbar";
			this.tripleTypetrackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.tripleTypetrackbar.RightToLeftLayout = true;
			this.tripleTypetrackbar.Size = new System.Drawing.Size(45, 105);
			this.tripleTypetrackbar.TabIndex = 14;
			this.tripleTypetrackbar.TickFrequency = 10;
			this.tripleTypetrackbar.ValueChanged += new System.EventHandler(this.tripleTypetrackbar_ValueChanged);

			// 
			// full8thStream
			// 
			this.full8thStream.AutoSize = true;
			this.full8thStream.Enabled = false;
			this.full8thStream.Location = new System.Drawing.Point(5, 85);
			this.full8thStream.Name = "full8thStream";
			this.full8thStream.Size = new System.Drawing.Size(94, 17);
			this.full8thStream.TabIndex = 7;
			this.full8thStream.Text = "Full 8th stream";
			this.full8thStream.UseVisualStyleBackColor = true;
			this.full8thStream.CheckedChanged += new System.EventHandler(this.full8thStream_CheckedChanged);
			// 
			// quintuples_on_1_or_2
			// 
			this.quintuples_on_1_or_2.AutoSize = true;
			this.quintuples_on_1_or_2.Location = new System.Drawing.Point(5, 65);
			this.quintuples_on_1_or_2.Name = "quintuples_on_1_or_2";
			this.quintuples_on_1_or_2.Size = new System.Drawing.Size(150, 17);
			this.quintuples_on_1_or_2.TabIndex = 6;
			this.quintuples_on_1_or_2.Text = "Quintuples on either 1 or 2";
			this.quintuples_on_1_or_2.UseVisualStyleBackColor = true;
			// 
			// triples_on_1_and_3
			// 
			this.triples_on_1_and_3.AutoSize = true;
			this.triples_on_1_and_3.Location = new System.Drawing.Point(5, 45);
			this.triples_on_1_and_3.Name = "triples_on_1_and_3";
			this.triples_on_1_and_3.Size = new System.Drawing.Size(135, 17);
			this.triples_on_1_and_3.TabIndex = 5;
			this.triples_on_1_and_3.Text = "Triples on both 1 and 3";
			this.triples_on_1_and_3.UseVisualStyleBackColor = true;
			// 
			// arrow_repeat
			// 
			this.arrow_repeat.AutoSize = true;
			this.arrow_repeat.Checked = true;
			this.arrow_repeat.CheckState = System.Windows.Forms.CheckState.Checked;
			this.arrow_repeat.Location = new System.Drawing.Point(5, 25);
			this.arrow_repeat.Name = "arrow_repeat";
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
			this.alternate_foot.Name = "alternate_foot";
			this.alternate_foot.Size = new System.Drawing.Size(100, 17);
			this.alternate_foot.TabIndex = 3;
			this.alternate_foot.Text = "Alternating feet ";
			this.alternate_foot.UseVisualStyleBackColor = true;
			// 
			// onBeat
			// 
			this.onBeat.Location = new System.Drawing.Point(62, 44);
			this.onBeat.Name = "onBeat";
			this.onBeat.Size = new System.Drawing.Size(42, 20);
			this.onBeat.TabIndex = 11;
			this.onBeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.onBeat.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.onBeat.ValueChanged += new System.EventHandler(this.onBeat_ValueChanged);
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(6, 87);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(98, 13);
			this.label20.TabIndex = 4;
			this.label20.Text = "On beat + half beat";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(37, 11);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(67, 13);
			this.label19.TabIndex = 3;
			this.label19.Text = "On beat only";
			// 
			// panel5
			// 
			this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel5.Location = new System.Drawing.Point(141, 55);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(310, 50);
			this.panel5.TabIndex = 16;
			// 
			// panel4
			// 
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel4.Location = new System.Drawing.Point(141, 2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(310, 50);
			this.panel4.TabIndex = 12;
			// 
			// onBeatTrackbar
			// 
			this.onBeatTrackbar.Location = new System.Drawing.Point(89, 3);
			this.onBeatTrackbar.Maximum = 100;
			this.onBeatTrackbar.Name = "onBeatTrackbar";
			this.onBeatTrackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.onBeatTrackbar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.onBeatTrackbar.RightToLeftLayout = true;
			this.onBeatTrackbar.Size = new System.Drawing.Size(45, 102);
			this.onBeatTrackbar.TabIndex = 2;
			this.onBeatTrackbar.TickFrequency = 10;
			this.onBeatTrackbar.Value = 100;
			this.onBeatTrackbar.ValueChanged += new System.EventHandler(this.onBeatTrackbar_ValueChanged);
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(9, 8);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(53, 13);
			this.label25.TabIndex = 16;
			this.label25.Text = "Half-beat:";
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(214, 28);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(57, 13);
			this.label23.TabIndex = 13;
			this.label23.Text = "Quintuples";
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(69, 28);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(38, 13);
			this.label24.TabIndex = 15;
			this.label24.Text = "Triples";
			// 
			// quintuplesTrackbar
			// 
			this.quintuplesTrackbar.Location = new System.Drawing.Point(67, 3);
			this.quintuplesTrackbar.Maximum = 100;
			this.quintuplesTrackbar.Name = "quintuplesTrackbar";
			this.quintuplesTrackbar.RightToLeftLayout = true;
			this.quintuplesTrackbar.Size = new System.Drawing.Size(177, 45);
			this.quintuplesTrackbar.TabIndex = 14;
			this.quintuplesTrackbar.TickFrequency = 10;
			this.quintuplesTrackbar.ValueChanged += new System.EventHandler(this.quintuplesTrackbar_ValueChanged);
			// 
			// quintuples
			// 
			this.quintuples.Location = new System.Drawing.Point(257, 6);
			this.quintuples.Name = "quintuples";
			this.quintuples.Size = new System.Drawing.Size(42, 20);
			this.quintuples.TabIndex = 8;
			this.quintuples.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.quintuples.ValueChanged += new System.EventHandler(this.quintuples_ValueChanged);
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(8, 7);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(48, 13);
			this.label26.TabIndex = 17;
			this.label26.Text = "On beat:";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(214, 28);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(37, 13);
			this.label22.TabIndex = 13;
			this.label22.Text = "Jumps";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(69, 28);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(57, 13);
			this.label21.TabIndex = 15;
			this.label21.Text = "Single foot";
			// 
			// jumpsTrackbar
			// 
			this.jumpsTrackbar.Location = new System.Drawing.Point(67, 3);
			this.jumpsTrackbar.Maximum = 100;
			this.jumpsTrackbar.Name = "jumpsTrackbar";
			this.jumpsTrackbar.RightToLeftLayout = true;
			this.jumpsTrackbar.Size = new System.Drawing.Size(177, 45);
			this.jumpsTrackbar.TabIndex = 14;
			this.jumpsTrackbar.TickFrequency = 10;
			this.jumpsTrackbar.ValueChanged += new System.EventHandler(this.jumpsTrackbar_ValueChanged);
			// 
			// jumps
			// 
			this.jumps.Location = new System.Drawing.Point(257, 6);
			this.jumps.Name = "jumps";
			this.jumps.Size = new System.Drawing.Size(42, 20);
			this.jumps.TabIndex = 8;
			this.jumps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.jumps.ValueChanged += new System.EventHandler(this.jumps_ValueChanged);

			// Set up the delays for the ToolTip.
			toolTip1.AutoPopDelay = 5000;
			toolTip1.InitialDelay = 200;
			toolTip1.ReshowDelay = 200;
			// Force the ToolTip text to be displayed whether or not the form is active.
			toolTip1.ShowAlways = true;

			toolTip1.SetToolTip(this.level, "Settings for the \"Novice\" level in Stepmania");
			toolTip1.SetToolTip(this.alternate_foot, "Check if you want single steps to always alternate between left and right foot");
			toolTip1.SetToolTip(this.stepFill_trackbar, "Percentage of beats that should have an arrow");
			toolTip1.SetToolTip(this.stepFill, "Percentage of beats that should have an arrow");
			toolTip1.SetToolTip(this.onBeat, "Of beats with arrows, percentage of beats that should have only on-beat arrows, no half-beat arrows");
			toolTip1.SetToolTip(this.onBeatTrackbar, "Of beats with arrows, percentage of beats that should have only on-beat arrows, no half-beat arrows");
			toolTip1.SetToolTip(this.jumps, "Of beats with on-beat arrows, percentage of jumps (both feet) compared to single-foot arrows");
			toolTip1.SetToolTip(this.jumpsTrackbar, "Of beats with on-beat arrows, percentage of jumps (both feet) compared to single-foot arrows");
			toolTip1.SetToolTip(this.quintuples, "Of beats with half-beat arrows, percentage of quintuples vs triples");
			toolTip1.SetToolTip(this.quintuplesTrackbar, "Of beats with half-beat arrows, percentage of quintuples vs triples");
			toolTip1.SetToolTip(this.arrow_repeat, "Allow the same arrow twice in a row");
			toolTip1.SetToolTip(this.triples_on_1_and_3, "Allow triples on both the 1st and 3rd beat of a 4-beat measure. Uncheck for 1st beat only");
			toolTip1.SetToolTip(this.quintuples_on_1_or_2, "Allow quintuples on either the 1st or 2nd beat of a 4-beat measure. Uncheck for 1st beat only");

			// put it all together
			this.panel28.Controls.Add(this.quintupleType);
			this.panel28.Controls.Add(this.label59);
			this.panel28.Controls.Add(this.label60);
			this.panel28.Controls.Add(this.label61);
			this.panel28.Controls.Add(this.quintupleTypetrackbar);
			this.panel16.Controls.Add(this.tripleType);
			this.panel16.Controls.Add(this.label56);
			this.panel16.Controls.Add(this.label57);
			this.panel16.Controls.Add(this.label58);
			this.panel16.Controls.Add(this.tripleTypetrackbar);
			this.panel2.Controls.Add(this.full8thStream);
			this.panel2.Controls.Add(this.quintuples_on_1_or_2);
			this.panel2.Controls.Add(this.triples_on_1_and_3);
			this.panel2.Controls.Add(this.arrow_repeat);
			this.panel2.Controls.Add(this.alternate_foot);
			this.panel3.Controls.Add(this.onBeat);
			this.panel3.Controls.Add(this.label20);
			this.panel3.Controls.Add(this.label19);
			this.panel3.Controls.Add(this.panel5);
			this.panel3.Controls.Add(this.panel4);
			this.panel3.Controls.Add(this.onBeatTrackbar);
			this.Controls.Add(this.sample1);
			this.Controls.Add(this.panel28);
			this.Controls.Add(this.panel16);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.stepFill);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.stepFill_trackbar);
			this.Controls.Add(this.level);
			this.panel5.Controls.Add(this.label25);
			this.panel5.Controls.Add(this.label23);
			this.panel5.Controls.Add(this.label24);
			this.panel5.Controls.Add(this.quintuplesTrackbar);
			this.panel5.Controls.Add(this.quintuples);
			this.panel4.Controls.Add(this.label26);
			this.panel4.Controls.Add(this.label22);
			this.panel4.Controls.Add(this.label21);
			this.panel4.Controls.Add(this.jumpsTrackbar);
			this.panel4.Controls.Add(this.jumps);

		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}

		private void sample1_Click(object sender, EventArgs e)
		{
			Noteset sample_noteset = new Noteset(dance_style, StepDeets.SM, measures_per_sample, sdlevel, beats_per_measure,
						alternate_foot.Checked, arrow_repeat.Checked, (int)stepFill.Value, (int)onBeat.Value, (int)jumps.Value, r,
						(int)quintuples.Value, triples_on_1_and_3.Checked, quintuples_on_1_or_2.Checked);
			sample_noteset.generateSteps();
			char[] f = sample_noteset.getFeet();
			string[] s = sample_noteset.getSteps();
			sw = new SampleWindow(dance_style, StepDeets.levelTitle(sdlevel), measures_per_sample, f, s, blackpen, redpen, bluepen);
			sw.Show();
		}
		private void stepFill_ValueChanged(object sender, EventArgs e)
		{
			stepFill_trackbar.Value = Convert.ToInt32(stepFill.Value);
		}

		private void stepFill_trackbar_ValueChanged(object sender, EventArgs e)
		{
			stepFill.Value = stepFill_trackbar.Value;
		}

		private void onBeatTrackbar_ValueChanged(object sender, EventArgs e)
		{
			onBeat.Value = onBeatTrackbar.Value;
		}

		private void onBeat_ValueChanged(object sender, EventArgs e)
		{
			onBeatTrackbar.Value = Convert.ToInt32(onBeat.Value);
		}

		private void jumpsTrackbar_ValueChanged(object sender, EventArgs e)
		{
			jumps.Value = jumpsTrackbar.Value;
		}

		private void jumps_ValueChanged(object sender, EventArgs e)
		{
			jumpsTrackbar.Value = Convert.ToInt32(jumps.Value);
		}

		private void quintuplesTrackbar_ValueChanged(object sender, EventArgs e)
		{
			quintuples.Value = quintuplesTrackbar.Value;
		}

		private void quintuples_ValueChanged(object sender, EventArgs e)
		{
			quintuplesTrackbar.Value = Convert.ToInt32(quintuples.Value);
		}

		private void full8thStream_CheckedChanged(object sender, EventArgs e)
		{
			if (full8thStream.Checked)
			{
				triples_on_1_and_3.Enabled = false;
				quintuplesTrackbar.Enabled = false;
				quintuples_on_1_or_2.Enabled = false;
				quintuples.Enabled = false;
				label25.Enabled = false;
			}
			else
			{
				triples_on_1_and_3.Enabled = true;
				quintuplesTrackbar.Enabled = true;
				quintuples_on_1_or_2.Enabled = true;
				quintuples.Enabled = true;
				label25.Enabled = true;
			}
		}
		private void tripleTypetrackbar_ValueChanged(object sender, EventArgs e)
		{
			tripleType.Value = tripleTypetrackbar.Value;
		}

		private void tripleType_ValueChanged(object sender, EventArgs e)
		{
			tripleTypetrackbar.Value = Convert.ToInt32(tripleType.Value);
		}
		private void quintupleTypetrackbar_ValueChanged(object sender, EventArgs e)
		{
			quintupleType.Value = quintupleTypetrackbar.Value;
		}

		private void quintupleType_ValueChanged(object sender, EventArgs e)
		{
			quintupleTypetrackbar.Value = Convert.ToInt32(quintupleType.Value);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

	}
}

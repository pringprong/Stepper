﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Stepper
{
    public partial class Stepper : Form
    {
        private List<Song> songs;
        private int beats_per_measure = 4;
        Random r;
        private bool folderTextChanged = false;
        private ToolTip toolTip1;

        private int instructionsTextboxGap = 40;

        public Stepper()
        {
            InitializeComponent(); 
            textBox1.Text = @"Stepper overwrites existing Stepmania .ssc (or .sm) stepfiles with automatically generated steps. 

Instructions:
1. Before running Stepper, open C:\Games\StepMania 5\Songs and create a new song group folder such as 'Cardio'
2. Copy songs (entire folders containing .mp3, .ssc, etc.) from your other song group folders into the new one.
3. Run Stepper and change the settings for the 5 difficulty levels of each set of steps 'Dance Single', 'Pump Single' etc.
4. Move to the 'Write Stepfiles' tab. Browse to the new folder and click 'Get Info'. The table will show the songs, max and min beats per minute, number of stops, etc. in the selected song group folder
5. Click 'Overwrite stepfiles' to overwrite the existing stepfiles and create dance steps for each song according to the settings.
6. Open Stepmania and try out your new steps!

Warnings: 
1. Although Stepper will create a backup of the old stepfile, there is no quick way to restore from backup. It's better to copy the song folders to a new song group folder before you start instead of changing the old .sm files in place
2. Stepmania stores information about each song in a cache. If Stepmania doesn't display all 5 song levels created by Stepper, go to C:\Users\<your username>\AppData\Roaming\StepMania 5\Cache and delete everything, then restart Stepmania to refresh the cache
3. Some songs use a .dwi file instead of a .sm or .ssc file to store step information. Stepper does not work for .dwi format stepfiles."; 
           
            songs = new List<Song>();
            r = new Random();
            toolTip1 = new ToolTip();
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 200;
            toolTip1.ReshowDelay = 200;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.folderChooser, "Open folder chooser");
            toolTip1.SetToolTip(this.getInfo, "Click here to get some information about the song files in the selected folder");
            toolTip1.SetToolTip(this.overwriteStepfiles, "Overwrite the stepfiles in the selected folder according to the settings below");
            toolTip1.SetToolTip(this.close, "Close this application");
            toolTip1.SetToolTip(this.currentFolder, "The currently selected folder");

            toolTip1.SetToolTip(this.level, "Settings for the \"Novice\" level in Stepmania");
            toolTip1.SetToolTip(this.level2, "Settings for the \"Easy\" level in Stepmania");
            toolTip1.SetToolTip(this.level3, "Settings for the \"Medium\" level in Stepmania");
            toolTip1.SetToolTip(this.level4, "Settings for the \"Hard\" level in Stepmania");
            toolTip1.SetToolTip(this.level5, "Settings for the \"Expert\" level in Stepmania");

            toolTip1.SetToolTip(this.alternate_foot, "Check if you want single steps to always alternate between left and right foot");
            toolTip1.SetToolTip(this.alternate_foot2, "Check if you want single steps to always alternate between left and right foot");
            toolTip1.SetToolTip(this.alternate_foot3, "Check if you want single steps to always alternate between left and right foot");
            toolTip1.SetToolTip(this.alternate_foot4, "Check if you want single steps to always alternate between left and right foot");
            toolTip1.SetToolTip(this.alternate_foot5, "Check if you want single steps to always alternate between left and right foot");

            toolTip1.SetToolTip(this.stepFill_trackbar, "Percentage of beats that should have an arrow");
            toolTip1.SetToolTip(this.stepFill, "Percentage of beats that should have an arrow");
            toolTip1.SetToolTip(this.stepFill_trackbar2, "Percentage of beats that should have an arrow");
            toolTip1.SetToolTip(this.stepFill2, "Percentage of beats that should have an arrow");
            toolTip1.SetToolTip(this.stepFill_trackbar3, "Percentage of beats that should have an arrow");
            toolTip1.SetToolTip(this.stepFill3, "Percentage of beats that should have an arrow");
            toolTip1.SetToolTip(this.stepFill_trackbar4, "Percentage of beats that should have an arrow");
            toolTip1.SetToolTip(this.stepFill4, "Percentage of beats that should have an arrow");
            toolTip1.SetToolTip(this.stepFill_trackbar5, "Percentage of beats that should have an arrow");
            toolTip1.SetToolTip(this.stepFill5, "Percentage of beats that should have an arrow");

            toolTip1.SetToolTip(this.onBeat, "Of beats with arrows, percentage of beats that should have only on-beat arrows, no half-beat arrows");
            toolTip1.SetToolTip(this.onBeatTrackbar, "Of beats with arrows, percentage of beats that should have only on-beat arrows, no half-beat arrows");
            toolTip1.SetToolTip(this.onBeat2, "Of beats with arrows, percentage of beats that should have only on-beat arrows, no half-beat arrows");
            toolTip1.SetToolTip(this.onBeatTrackbar2, "Of beats with arrows, percentage of beats that should have only on-beat arrows, no half-beat arrows");
            toolTip1.SetToolTip(this.onBeat3, "Of beats with arrows, percentage of beats that should have only on-beat arrows, no half-beat arrows");
            toolTip1.SetToolTip(this.onBeatTrackbar3, "Of beats with arrows, percentage of beats that should have only on-beat arrows, no half-beat arrows");
            toolTip1.SetToolTip(this.onBeat4, "Of beats with arrows, percentage of beats that should have only on-beat arrows, no half-beat arrows");
            toolTip1.SetToolTip(this.onBeatTrackbar4, "Of beats with arrows, percentage of beats that should have only on-beat arrows, no half-beat arrows");
            toolTip1.SetToolTip(this.onBeat5, "Of beats with arrows, percentage of beats that should have only on-beat arrows, no half-beat arrows");
            toolTip1.SetToolTip(this.onBeatTrackbar5, "Of beats with arrows, percentage of beats that should have only on-beat arrows, no half-beat arrows");

            toolTip1.SetToolTip(this.jumps, "Of beats with on-beat arrows, percentage of jumps (both feet) compared to single-foot arrows");
            toolTip1.SetToolTip(this.jumpsTrackbar, "Of beats with on-beat arrows, percentage of jumps (both feet) compared to single-foot arrows");
            toolTip1.SetToolTip(this.jumps2, "Of beats with on-beat arrows, percentage of jumps (both feet) compared to single-foot arrows");
            toolTip1.SetToolTip(this.jumpsTrackbar2, "Of beats with on-beat arrows, percentage of jumps (both feet) compared to single-foot arrows");
            toolTip1.SetToolTip(this.jumps3, "Of beats with on-beat arrows, percentage of jumps (both feet) compared to single-foot arrows");
            toolTip1.SetToolTip(this.jumpsTrackbar3, "Of beats with on-beat arrows, percentage of jumps (both feet) compared to single-foot arrows");
            toolTip1.SetToolTip(this.jumps4, "Of beats with on-beat arrows, percentage of jumps (both feet) compared to single-foot arrows");
            toolTip1.SetToolTip(this.jumpsTrackbar4, "Of beats with on-beat arrows, percentage of jumps (both feet) compared to single-foot arrows");
            toolTip1.SetToolTip(this.jumps5, "Of beats with on-beat arrows, percentage of jumps (both feet) compared to single-foot arrows");
            toolTip1.SetToolTip(this.jumpsTrackbar5, "Of beats with on-beat arrows, percentage of jumps (both feet) compared to single-foot arrows");

            toolTip1.SetToolTip(this.quintuples, "Of beats with half-beat arrows, percentage of quintuples vs triples");
            toolTip1.SetToolTip(this.quintuplesTrackbar, "Of beats with half-beat arrows, percentage of quintuples vs triples");
            toolTip1.SetToolTip(this.quintuples2, "Of beats with half-beat arrows, percentage of quintuples vs triples");
            toolTip1.SetToolTip(this.quintuplesTrackbar2, "Of beats with half-beat arrows, percentage of quintuples vs triples");
            toolTip1.SetToolTip(this.quintuples3, "Of beats with half-beat arrows, percentage of quintuples vs triples");
            toolTip1.SetToolTip(this.quintuplesTrackbar3, "Of beats with half-beat arrows, percentage of quintuples vs triples");
            toolTip1.SetToolTip(this.quintuples4, "Of beats with half-beat arrows, percentage of quintuples vs triples");
            toolTip1.SetToolTip(this.quintuplesTrackbar4, "Of beats with half-beat arrows, percentage of quintuples vs triples");
            toolTip1.SetToolTip(this.quintuples5, "Of beats with half-beat arrows, percentage of quintuples vs triples");
            toolTip1.SetToolTip(this.quintuplesTrackbar5, "Of beats with half-beat arrows, percentage of quintuples vs triples");

            toolTip1.SetToolTip(this.arrow_repeat, "Allow the same arrow twice in a row");
            toolTip1.SetToolTip(this.arrow_repeat2, "Allow the same arrow twice in a row");
            toolTip1.SetToolTip(this.arrow_repeat3, "Allow the same arrow twice in a row");
            toolTip1.SetToolTip(this.arrow_repeat4, "Allow the same arrow twice in a row");
            toolTip1.SetToolTip(this.arrow_repeat5, "Allow the same arrow twice in a row");

            toolTip1.SetToolTip(this.triples_on_1_and_3, "Allow triples on both the 1st and 3rd beat of a 4-beat measure. Uncheck for 1st beat only");
            toolTip1.SetToolTip(this.triples_on_1_and_32, "Allow triples on both the 1st and 3rd beat of a 4-beat measure. Uncheck for 1st beat only");
            toolTip1.SetToolTip(this.triples_on_1_and_33, "Allow triples on both the 1st and 3rd beat of a 4-beat measure. Uncheck for 1st beat only");
            toolTip1.SetToolTip(this.triples_on_1_and_34, "Allow triples on both the 1st and 3rd beat of a 4-beat measure. Uncheck for 1st beat only");
            toolTip1.SetToolTip(this.triples_on_1_and_35, "Allow triples on both the 1st and 3rd beat of a 4-beat measure. Uncheck for 1st beat only");

            toolTip1.SetToolTip(this.quintuples_on_1_or_2, "Allow quintuples on either the 1st or 2nd beat of a 4-beat measure. Uncheck for 1st beat only");
            toolTip1.SetToolTip(this.quintuples_on_1_or_22, "Allow quintuples on either the 1st or 2nd beat of a 4-beat measure. Uncheck for 1st beat only");
            toolTip1.SetToolTip(this.quintuples_on_1_or_23, "Allow quintuples on either the 1st or 2nd beat of a 4-beat measure. Uncheck for 1st beat only");
            toolTip1.SetToolTip(this.quintuples_on_1_or_24, "Allow quintuples on either the 1st or 2nd beat of a 4-beat measure. Uncheck for 1st beat only");
            toolTip1.SetToolTip(this.quintuples_on_1_or_25, "Allow quintuples on either the 1st or 2nd beat of a 4-beat measure. Uncheck for 1st beat only");
            
        }

        private void selectFolder_Click(object sender, EventArgs e)
        {
            string default_folder = "C:\\Games\\StepMania 5\\Songs";
            if ((Directory.Exists(currentFolder.Text)))
            {
                folderBrowserDialog1.SelectedPath = currentFolder.Text;
            }
            else if ((Directory.Exists(default_folder)))
            {
                folderBrowserDialog1.SelectedPath = default_folder;
            }
            else
            {
                folderBrowserDialog1.SelectedPath = "";
            }
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                currentFolder.Text = folderBrowserDialog1.SelectedPath;
                folderTextChanged = true;
            }
        }

        private void getInfo_Click(object sender, EventArgs e)
        {
            songs.Clear();
            if (!(Directory.Exists(currentFolder.Text)))
            {
                MessageBox.Show("Please choose a valid Stepmania song group folder", "Folder name invalid");
                return;
            }

            string[] dirs = Directory.GetDirectories(currentFolder.Text);
            var fileCount = dirs.Count();
            songInfo.ColumnCount = 8;
            songInfo.Columns[0].HeaderText = "Song Path";
            songInfo.Columns[1].HeaderText = "Min BPM";
            songInfo.Columns[2].HeaderText = "Max BPM";
            songInfo.Columns[3].HeaderText = "# BPM Changes";
            songInfo.Columns[4].HeaderText = "# Stops";
            songInfo.Columns[5].HeaderText = "# Step Sets";
            songInfo.Columns[6].HeaderText = "# Measures";
            songInfo.Columns[7].HeaderText = "Type";
            songInfo.Columns[0].Width = 420;
            songInfo.Columns[1].Width = 90;
            songInfo.Columns[2].Width = 90;
            songInfo.Columns[3].Width = 110;
            songInfo.Columns[4].Width = 70;
            songInfo.Columns[5].Width = 90;
            songInfo.Columns[6].Width = 90;
            songInfo.Columns[7].Width = 50;
            songInfo.Columns[0].ToolTipText = "Complete path name of each Stepmania song folder";
            songInfo.Columns[1].ToolTipText = "Minimum beats per minute of each song";
            songInfo.Columns[2].ToolTipText = "Maximum beats per minute of each song";
            songInfo.Columns[3].ToolTipText = "Number of times the beats per minute changes during each song";
            songInfo.Columns[4].ToolTipText = "Number of stops during each song";
            songInfo.Columns[5].ToolTipText = "Number of sets of arrows for each song, including Single, Double, and Solo. This program will produce 5 Single sets";
            songInfo.Columns[6].ToolTipText = "Range of number of measures found in each set of arrows. There are 4 beats per measure.";
            songInfo.Columns[7].ToolTipText = "Stepfile type: .ssc, .sm, or .dwi";

            songInfo.RowCount = fileCount;
            DataGridViewColumnCollection coll = songInfo.Columns;
            DataGridViewColumn c = coll[0];
            for (int i = 0; i < fileCount; i++)
            {
                songInfo[0, i].Value = dirs[i];
                if (Directory.GetFiles(dirs[i], "*.ssc").Count() > 0)
                {
                    //parse .ssc file
                    string[] files = Directory.GetFiles(dirs[i], "*.ssc");
                    Song s = parseSSCFile(files[0]);
                    if (!s.Equals(null))
                    {
                        songs.Add(s);
                        songInfo[1, i].Value = s.getMinBPM();
                        songInfo[2, i].Value = s.getMaxBPM();
                        songInfo[3, i].Value = s.getBPMChanges();
                        songInfo[4, i].Value = s.getNumStops();
                        songInfo[5, i].Value = s.getNumNotesets();
                        if (s.getMinMeasures() == s.getMaxMeasures())
                        {
                            songInfo[6, i].Value = s.getMinMeasures();
                        }
                        else
                        {
                            songInfo[6, i].Value = s.getMinMeasures() + "-" + s.getMaxMeasures();
                        }
                        songInfo[7, i].Value = s.getType();
                        songInfo.ClearSelection();
                    }
                }
                else if (Directory.GetFiles(dirs[i], "*.sm").Count() > 0)
                {
                    // parse .sm file
                    string[] files = Directory.GetFiles(dirs[i], "*.sm");
                    Song s = parseSMFile(files[0]);
                    if (!s.Equals(null))
                    {
                        songs.Add(s);
                        songInfo[1, i].Value = s.getMinBPM();
                        songInfo[2, i].Value = s.getMaxBPM();
                        songInfo[3, i].Value = s.getBPMChanges();
                        songInfo[4, i].Value = s.getNumStops();
                        songInfo[5, i].Value = s.getNumNotesets();
                        if (s.getMinMeasures() == s.getMaxMeasures())
                        {
                            songInfo[6, i].Value = s.getMinMeasures();
                        }
                        else
                        {
                            songInfo[6, i].Value = s.getMinMeasures() + "-" + s.getMaxMeasures();
                        }
                        songInfo[7, i].Value = s.getType();
                        songInfo.ClearSelection();
                    }
                }
                else if (Directory.GetFiles(dirs[i], "*.dwi").Count() > 0)
                {
                    songInfo[1, i].Value = "";
                    songInfo[2, i].Value = "";
                    songInfo[3, i].Value = "";
                    songInfo[4, i].Value = "";
                    songInfo[5, i].Value = "";
                    songInfo[6, i].Value = "";
                    songInfo[7, i].Value = "DWI";
                    songInfo.ClearSelection();
                }
                else
                {
                    songInfo[1, i].Value = "";
                    songInfo[2, i].Value = "";
                    songInfo[3, i].Value = "";
                    songInfo[4, i].Value = "";
                    songInfo[5, i].Value = "";
                    songInfo[6, i].Value = "";
                    songInfo[7, i].Value = "NONE";
                    songInfo.ClearSelection();
                }
            } // end for (int i = 0; i < fileCount; i++) 
            folderTextChanged = false;
            if (songs.Count == 0)
            {
                MessageBox.Show("Please choose a valid Stepmania song group folder", "Folder name invalid");
                folderTextChanged = true;
                return;
            }
        }

        private void overwriteStepfiles_Click(object sender, EventArgs e)
        {
            if (songs.Count == 0)
            {
                MessageBox.Show("Please choose a Stepmania song group folder and click \"Get info\"", "Click Get info");
                return;
            }
            if (folderTextChanged)
            {
                MessageBox.Show("The folder name has changed or is invalid. Please choose a Stepmania song group folder and click \"Get info\" to update the song info table", "Click Get info");
                return;
            }
            DialogResult result1 = MessageBox.Show("Are you sure you want to overwrite the stepfiles in " + currentFolder.Text + " ? This action cannot be undone.",
                "Warning: Overwriting stepfiles",
                MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                songs.ForEach(delegate(Song s)
                {
                    Noteset note1 = new Noteset("dance-single", s.getType(), s.getNumMeasures(), level.Text, beats_per_measure,
                        alternate_foot.Checked, arrow_repeat.Checked, (int)stepFill.Value, (int)onBeat.Value, (int)jumps.Value, r,
                        (int)quintuples.Value, triples_on_1_and_3.Checked, quintuples_on_1_or_2.Checked);
                    note1.generateSteps();

                    Noteset note2 = new Noteset("dance-single", s.getType(), s.getNumMeasures(), level2.Text, beats_per_measure,
                        alternate_foot2.Checked, arrow_repeat2.Checked, (int)stepFill2.Value, (int)onBeat2.Value, (int)jumps2.Value, r,
                        (int)quintuples2.Value, triples_on_1_and_32.Checked, quintuples_on_1_or_22.Checked);
                    note2.generateSteps();

                    Noteset note3 = new Noteset("dance-single", s.getType(), s.getNumMeasures(), level3.Text, beats_per_measure,
                       alternate_foot3.Checked, arrow_repeat3.Checked, (int)stepFill3.Value, (int)onBeat3.Value, (int)jumps3.Value, r,
                       (int)quintuples3.Value, triples_on_1_and_33.Checked, quintuples_on_1_or_23.Checked);
                    note3.generateSteps();

                    Noteset note4 = new Noteset("dance-single", s.getType(), s.getNumMeasures(), level4.Text, beats_per_measure,
                        alternate_foot4.Checked, arrow_repeat4.Checked, (int)stepFill4.Value, (int)onBeat4.Value, (int)jumps4.Value, r,
                        (int)quintuples4.Value, triples_on_1_and_34.Checked, quintuples_on_1_or_24.Checked);
                    note4.generateSteps();

                    Noteset note5 = new Noteset("dance-single", s.getType(), s.getNumMeasures(), level5.Text, beats_per_measure,
                        alternate_foot5.Checked, arrow_repeat5.Checked, (int)stepFill5.Value, (int)onBeat5.Value, (int)jumps5.Value, r,
                        (int)quintuples5.Value, triples_on_1_and_35.Checked, quintuples_on_1_or_25.Checked);
                    note5.generateSteps();

                    Noteset pump_single1 = new Noteset("pump-single", s.getType(), s.getNumMeasures(), level.Text, beats_per_measure,
                        alternate_foot.Checked, arrow_repeat.Checked, (int)stepFill.Value, (int)onBeat.Value, (int)jumps.Value, r,
                        (int)quintuples.Value, triples_on_1_and_3.Checked, quintuples_on_1_or_2.Checked);
                    pump_single1.generateSteps();

                    Noteset pump_single2 = new Noteset("pump-single", s.getType(), s.getNumMeasures(), level2.Text, beats_per_measure,
                        alternate_foot2.Checked, arrow_repeat2.Checked, (int)stepFill2.Value, (int)onBeat2.Value, (int)jumps2.Value, r,
                        (int)quintuples2.Value, triples_on_1_and_32.Checked, quintuples_on_1_or_22.Checked);
                    pump_single2.generateSteps();

                    Noteset pump_single3 = new Noteset("pump-single", s.getType(), s.getNumMeasures(), level3.Text, beats_per_measure,
                       alternate_foot3.Checked, arrow_repeat3.Checked, (int)stepFill3.Value, (int)onBeat3.Value, (int)jumps3.Value, r,
                       (int)quintuples3.Value, triples_on_1_and_33.Checked, quintuples_on_1_or_23.Checked);
                    pump_single3.generateSteps();

                    Noteset pump_single4 = new Noteset("pump-single", s.getType(), s.getNumMeasures(), level4.Text, beats_per_measure,
                        alternate_foot4.Checked, arrow_repeat4.Checked, (int)stepFill4.Value, (int)onBeat4.Value, (int)jumps4.Value, r,
                        (int)quintuples4.Value, triples_on_1_and_34.Checked, quintuples_on_1_or_24.Checked);
                    pump_single4.generateSteps();

                    Noteset pump_single5 = new Noteset("pump-single", s.getType(), s.getNumMeasures(), level5.Text, beats_per_measure,
                        alternate_foot5.Checked, arrow_repeat5.Checked, (int)stepFill5.Value, (int)onBeat5.Value, (int)jumps5.Value, r,
                        (int)quintuples5.Value, triples_on_1_and_35.Checked, quintuples_on_1_or_25.Checked);
                    pump_single5.generateSteps();

                    if (s.getType().Equals("SSC"))
                    {
                        // no ssc file, so backup the old .sm file and then overwrite it
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmm");
                        string old_path = s.getPath();
                        Regex alter_extension = new Regex("\\.ssc");
                        string backup_path = alter_extension.Replace(old_path, ".ssc." + timestamp + ".bak");
                        if (!File.Exists(backup_path))
                        {
                            System.IO.File.Move(old_path, backup_path);
                        }
                        System.IO.StreamWriter file = new System.IO.StreamWriter(old_path);
                        List<string> header_lines = s.getHeader();
                        header_lines.ForEach(delegate(string header_line)
                        {
                            file.WriteLine(header_line);
                        });
                        note1.writeSSCSteps(file);
                        note2.writeSSCSteps(file);
                        note3.writeSSCSteps(file);
                        note4.writeSSCSteps(file);
                        note5.writeSSCSteps(file);
                        pump_single1.writeSSCSteps(file);
                        pump_single2.writeSSCSteps(file);
                        pump_single3.writeSSCSteps(file);
                        pump_single4.writeSSCSteps(file);
                        pump_single5.writeSSCSteps(file);

                        file.Close();
                    }
                    else if (s.getType().Equals("SM"))
                    {
                        // no ssc file, so backup the old .sm file and then overwrite it
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmm");
                        string old_path = s.getPath();
                        Regex alter_extension = new Regex("\\.sm");
                        string backup_path = alter_extension.Replace(old_path, ".sm." + timestamp + ".bak");
                        if (!File.Exists(backup_path))
                        {
                            System.IO.File.Move(old_path, backup_path);
                        }
                        System.IO.StreamWriter file = new System.IO.StreamWriter(old_path);
                        List<string> header_lines = s.getHeader();
                        header_lines.ForEach(delegate(string header_line)
                        {
                            file.WriteLine(header_line);
                        });
                        note1.writeSMSteps(file);
                        note2.writeSMSteps(file);
                        note3.writeSMSteps(file);
                        note4.writeSMSteps(file);
                        note5.writeSMSteps(file);
                        pump_single1.writeSMSteps(file);
                        pump_single2.writeSMSteps(file);
                        pump_single3.writeSMSteps(file);
                        pump_single4.writeSMSteps(file);
                        pump_single5.writeSMSteps(file);

                        file.Close();
                    }
                });
            }
        }

        private Song parseSSCFile(string file)
        {
            Regex bpms = new Regex("^\\s*#BPMS:\\s*");
            Regex notedata = new Regex("^\\s*#NOTEDATA:\\s*");
            Regex notes = new Regex("^\\s*#NOTES:\\s*");
            Regex stops = new Regex("^\\s*#STOPS:\\s*");
            Regex commas = new Regex("^\\s*,\\s*(//)*");  // line contains nothing but a single comma, maybe whitespace, and maybe a comment
            Regex final_comma = new Regex("\\s*,\\s*(//)*$");  // maybe whitespace, definite comma, maybe whitespace, maybe comment, end of line
            Regex semicolon = new Regex("^\\s*[0-9]*\\s*,*\\s*;"); // line contains a semicolon, possibly preceded by a digits and/or a comma
            Regex comment = new Regex("^\\s*(//)+"); // comments
            Regex blank = new Regex("^\\s*$"); // blank lines
            Regex semicolon_only = new Regex(";");
            Regex leading_beat_count = new Regex("[0-9]+\\.[0-9]*=");
            Regex floor = new Regex("\\.[0-9]+");

            int note_sets = 0;
            bool in_a_note_set = false;
            bool in_a_note_set_header = false;
            int count_measures = 0;
            List<int> measures_list = new List<int>();
            List<string> header_text = new List<string>();
            bool is_header = true;
            List<int> trimmed;
            int trimmed_count = -1;
            int trimmed_min = -1;
            int trimmed_max = -1;
            int num_stops = -1;
            string[] bpms_array;

            using (Stream fileStream = File.Open(file, FileMode.Open))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                note_sets = 0;
                in_a_note_set = false;
                in_a_note_set_header = false;
                string line = null;
                measures_list = new List<int>();
                is_header = true;
                header_text = new List<string>();
                do
                {
                    line = reader.ReadLine(); // get the next line from the file
                    if (line == null)
                    {
                        // there are no more lines; break out of the loop
                        break;
                    }
                    if (blank.Match(line).Success || comment.Match(line).Success)
                    {
                        continue;
                    }
                    if (bpms.Match(line).Success && !in_a_note_set && !in_a_note_set_header)
                    {
                        string current = bpms.Replace(line, ""); // trim the line type indicator  #BPMS:
                        current = semicolon_only.Replace(current, ""); // trim the trailing semicolon
                        current = final_comma.Replace(current, "");// trim trailing whitespace, comma, comment
                        bpms_array = current.Split(','); // split the list on comma
                        trimmed = new List<int>();
                        foreach (string b in bpms_array)
                        {
                            if (b != "")
                            {
                                string a = leading_beat_count.Replace(b, "");
                                a = floor.Replace(a, "");
                                trimmed.Add(Convert.ToInt32(a));
                            }
                        }
                        trimmed_count = trimmed.Count() - 1;
                        trimmed_min = trimmed.Min();
                        trimmed_max = trimmed.Max();
                    }
                    if (stops.Match(line).Success && !in_a_note_set && !in_a_note_set_header)
                    {
                        string song_name = file;
                        // split the line on each semicolon character
                        //  string[] parts = line.Split(';');
                        string complete_stops = "";
                        // STOPS might be spread out over several lines, so first we will concatenate them 
                        while (!semicolon_only.Match(line).Success)
                        {
                            header_text.Add(line);
                            complete_stops += line;
                            line = reader.ReadLine();
                        }
                        complete_stops += line;
                        complete_stops = stops.Replace(complete_stops, ""); // trim the line type indicator  #STOPS:
                        complete_stops = semicolon_only.Replace(complete_stops, ""); // trim the trailing semicolon
                        string[] stops_array = complete_stops.Split(','); // split the list on comma
                        string all_stops = string.Join(" ", stops_array);
                        num_stops = stops_array.Count();
                        if (complete_stops.Equals(""))
                        {
                            num_stops = 0;
                        }
                    }
                    if (is_header && !notedata.Match(line).Success)
                    {
                        header_text.Add(line);
                    }
                    if (in_a_note_set)
                    {
                        if (commas.Match(line).Success)
                        {
                            count_measures++;
                        }
                    }
                    if (semicolon.Match(line).Success && in_a_note_set)
                    {
                        // end of note set
                        if (count_measures > 0)
                        {
                            measures_list.Add(count_measures);
                        }
                        note_sets++;
                        count_measures = 0;
                        in_a_note_set = false;
                        in_a_note_set_header = false;
                    }
                    if (notedata.Match(line).Success)
                    {
                        in_a_note_set_header = true;
                        is_header = false;
                        in_a_note_set = false;
                    }
                    if (notes.Match(line).Success)
                    {
                        in_a_note_set_header = false;
                        is_header = false;
                        in_a_note_set = true;
                    }
                } while (true);
            } // end using StreamReader; end of song file
            var groups = measures_list.GroupBy(v => v);
            int maxCount = groups.Max(g => g.Count());
            int mode = groups.First(g => g.Count() == maxCount).Key;
            Song s = new Song("SSC", file, header_text, mode, trimmed_min, trimmed_max, trimmed_count, num_stops, note_sets, measures_list.Min(), measures_list.Max());
            return s;
        }

        private Song parseSMFile(string file)
        {
            Regex bpms = new Regex("^\\s*#BPMS:\\s*");
            Regex notes = new Regex("^\\s*#NOTES\\s*");
            Regex stops = new Regex("^\\s*#STOPS:\\s*");
            Regex commas = new Regex("^\\s*,\\s*(//)*");  // line contains nothing but a single comma, maybe whitespace, and maybe a comment
            Regex semicolon = new Regex("^\\s*[0-9]*\\s*,*\\s*;"); // line contains a semicolon, possibly preceded by a digits and/or a comma
            Regex comment = new Regex("^\\s*(//)+"); // comments
            Regex blank = new Regex("^\\s*$"); // blank lines

            int note_sets = 0;
            bool in_a_note_set = false;
            int count_measures = 0;
            List<int> measures_list = new List<int>();
            List<string> header_text = new List<string>();
            bool is_header = true;
            List<int> trimmed;
            int trimmed_count = 0;
            int trimmed_min = 0;
            int trimmed_max = 0;
            int num_stops = 0;

            using (Stream fileStream = File.Open(file, FileMode.Open))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                note_sets = 0;
                in_a_note_set = false;
                string line = null;
                measures_list = new List<int>();
                is_header = true;
                header_text = new List<string>();
                do
                {
                    // get the next line from the file
                    line = reader.ReadLine();
                    if (line == null)
                    {
                        // there are no more lines; break out of the loop
                        break;
                    }

                    if (blank.Match(line).Success || comment.Match(line).Success)
                    {
                        continue;
                    }

                    if (bpms.Match(line).Success)
                    {
                        string current = bpms.Replace(line, ""); // trim the line type indicator  #BPMS:
                        Regex r = new Regex(";");
                        current = r.Replace(current, ""); // trim the trailing semicolon
                        string[] bpms_array = current.Split(','); // split the list on comma
                        trimmed = new List<int>();
                        Regex leading_beat_count = new Regex("[0-9]+\\.[0-9]*=");
                        Regex floor = new Regex("\\.[0-9]+");
                        foreach (string b in bpms_array)
                        {
                            string a = leading_beat_count.Replace(b, "");
                            a = floor.Replace(a, "");
                            trimmed.Add(Convert.ToInt32(a));
                        }
                        trimmed_count = trimmed.Count() - 1;
                        trimmed_min = trimmed.Min();
                        trimmed_max = trimmed.Max();
                    }

                    if (stops.Match(line).Success)
                    {
                        string song_name = file;
                        // split the line on each semicolon character
                        //  string[] parts = line.Split(';');
                        string complete_stops = "";
                        Regex r = new Regex(";");
                        // STOPS might be spread out over several lines, so first we will concatenate them 
                        while (!r.Match(line).Success)
                        {
                            header_text.Add(line);
                            complete_stops += line;
                            line = reader.ReadLine();
                        }
                        complete_stops += line;
                        complete_stops = stops.Replace(complete_stops, ""); // trim the line type indicator  #STOPS:
                        complete_stops = r.Replace(complete_stops, ""); // trim the trailing semicolon
                        string[] stops_array = complete_stops.Split(','); // split the list on comma
                        string all_stops = string.Join(" ", stops_array);
                        num_stops = stops_array.Count();
                        if (complete_stops.Equals(""))
                        {
                            num_stops = 0;
                        }
                    }

                    if (notes.Match(line).Success)
                    {
                        in_a_note_set = true;
                        is_header = false;
                    }

                    if (is_header)
                    {
                        header_text.Add(line);
                    }

                    if (in_a_note_set)
                    {
                        if (commas.Match(line).Success)
                        {
                            count_measures++;
                        }
                    }
                    if (semicolon.Match(line).Success && in_a_note_set)
                    {
                        // end of note set
                        if (count_measures > 0)
                        {
                            measures_list.Add(count_measures);
                        }
                        note_sets++;
                        count_measures = 0;
                        in_a_note_set = false;
                    }

                } while (true);
            } // end using StreamReader; end of song file
            Song s = new Song("SM", file, header_text, (int)measures_list.Max(), trimmed_min, trimmed_max, trimmed_count, num_stops, note_sets, measures_list.Min(), measures_list.Max());
            return s;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void stepFill2_ValueChanged(object sender, EventArgs e)
        {
            stepFill_trackbar2.Value = Convert.ToInt32(stepFill2.Value);
        }

        private void stepFill2_trackbar_ValueChanged(object sender, EventArgs e)
        {
            stepFill2.Value = stepFill_trackbar2.Value;
        }

        private void onBeatTrackbar2_ValueChanged(object sender, EventArgs e)
        {
            onBeat2.Value = onBeatTrackbar2.Value;
        }

        private void onBeat2_ValueChanged(object sender, EventArgs e)
        {
            onBeatTrackbar2.Value = Convert.ToInt32(onBeat2.Value);
        }

        private void jumpsTrackbar2_ValueChanged(object sender, EventArgs e)
        {
            jumps2.Value = jumpsTrackbar2.Value;
        }

        private void jumps2_ValueChanged(object sender, EventArgs e)
        {
            jumpsTrackbar2.Value = Convert.ToInt32(jumps2.Value);
        }

        private void quintuplesTrackbar2_ValueChanged(object sender, EventArgs e)
        {
            quintuples2.Value = quintuplesTrackbar2.Value;
        }

        private void quintuples2_ValueChanged(object sender, EventArgs e)
        {
            quintuplesTrackbar2.Value = Convert.ToInt32(quintuples2.Value);
        }

        private void stepFill3_ValueChanged(object sender, EventArgs e)
        {
            stepFill_trackbar3.Value = Convert.ToInt32(stepFill3.Value);
        }

        private void stepFill_trackbar3_ValueChanged(object sender, EventArgs e)
        {
            stepFill3.Value = stepFill_trackbar3.Value;
        }

        private void onBeatTrackbar3_ValueChanged(object sender, EventArgs e)
        {
            onBeat3.Value = onBeatTrackbar3.Value;
        }

        private void onBeat3_ValueChanged(object sender, EventArgs e)
        {
            onBeatTrackbar3.Value = Convert.ToInt32(onBeat3.Value);
        }

        private void jumpsTrackbar3_ValueChanged(object sender, EventArgs e)
        {
            jumps3.Value = jumpsTrackbar3.Value;
        }

        private void jumps3_ValueChanged(object sender, EventArgs e)
        {
            jumpsTrackbar3.Value = Convert.ToInt32(jumps3.Value);
        }

        private void quintuplesTrackbar3_ValueChanged(object sender, EventArgs e)
        {
            quintuples3.Value = quintuplesTrackbar3.Value;
        }

        private void quintuples3_ValueChanged(object sender, EventArgs e)
        {
            quintuplesTrackbar3.Value = Convert.ToInt32(quintuples3.Value);
        }

        private void stepFill4_ValueChanged(object sender, EventArgs e)
        {
            stepFill_trackbar4.Value = Convert.ToInt32(stepFill4.Value);
        }

        private void stepFill_trackbar4_ValueChanged(object sender, EventArgs e)
        {
            stepFill4.Value = stepFill_trackbar4.Value;
        }

        private void onBeatTrackbar4_ValueChanged(object sender, EventArgs e)
        {
            onBeat4.Value = onBeatTrackbar4.Value;
        }

        private void onBeat4_ValueChanged(object sender, EventArgs e)
        {
            onBeatTrackbar4.Value = Convert.ToInt32(onBeat4.Value);
        }

        private void jumpsTrackbar4_ValueChanged(object sender, EventArgs e)
        {
            jumps4.Value = jumpsTrackbar4.Value;
        }

        private void jumps4_ValueChanged(object sender, EventArgs e)
        {
            jumpsTrackbar4.Value = Convert.ToInt32(jumps4.Value);
        }

        private void quintuplesTrackbar4_ValueChanged(object sender, EventArgs e)
        {
            quintuples4.Value = quintuplesTrackbar4.Value;
        }

        private void quintuples4_ValueChanged(object sender, EventArgs e)
        {
            quintuplesTrackbar4.Value = Convert.ToInt32(quintuples4.Value);
        }

        private void stepFill5_ValueChanged(object sender, EventArgs e)
        {
            stepFill_trackbar5.Value = Convert.ToInt32(stepFill5.Value);
        }

        private void stepFill_trackbar5_ValueChanged(object sender, EventArgs e)
        {
            stepFill5.Value = stepFill_trackbar5.Value;
        }

        private void onBeatTrackbar5_ValueChanged(object sender, EventArgs e)
        {
            onBeat5.Value = onBeatTrackbar5.Value;
        }

        private void onBeat5_ValueChanged(object sender, EventArgs e)
        {
            onBeatTrackbar5.Value = Convert.ToInt32(onBeat5.Value);
        }

        private void jumpsTrackbar5_ValueChanged(object sender, EventArgs e)
        {
            jumps5.Value = jumpsTrackbar5.Value;
        }

        private void jumps5_ValueChanged(object sender, EventArgs e)
        {
            jumpsTrackbar5.Value = Convert.ToInt32(jumps5.Value);
        }

        private void quintuplesTrackbar5_ValueChanged(object sender, EventArgs e)
        {
            quintuples5.Value = quintuplesTrackbar5.Value;
        }

        private void quintuples5_ValueChanged(object sender, EventArgs e)
        {
            quintuplesTrackbar5.Value = Convert.ToInt32(quintuples5.Value);
        }

        private void currentFolder_TextChanged(object sender, EventArgs e)
        {
            folderTextChanged = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void panel30_Resize(object sender, EventArgs e)
        {
            button4.Width = panel30.Width / 2 - 5;
            button5.Width = panel30.Width / 2 - 5;
        }

        private void tabPage1_Resize(object sender, EventArgs e)
        {
            textBox1.Height = tabPage1.Height - instructionsTextboxGap;
        }

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            button7.Width = Math.Max(Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * Convert.ToDouble(0.15)), button7.MinimumSize.Width);
            currentFolder.Width = Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * Convert.ToDouble(0.25));
            folderChooser.Width = Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * Convert.ToDouble(0.15));
            getInfo.Width = Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * Convert.ToDouble(0.15));
            overwriteStepfiles.Width = Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * Convert.ToDouble(0.15));
            close.Width = Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * Convert.ToDouble(0.1));
        }

        private void tabPage4_Resize(object sender, EventArgs e)
        {
            songInfo.Height = tabPage4.Height - instructionsTextboxGap;
        }

        private void panel29_Resize(object sender, EventArgs e)
        {
            button2.Width = panel29.Width / 2 - 5;
            button3.Width = panel29.Width / 2 - 5;
        }

        private void tabPage2_Resize(object sender, EventArgs e)
        {
            flowLayoutPanel2.Height = tabPage2.Height - instructionsTextboxGap;
        }

        private void tripleTypetrackbar_ValueChanged(object sender, EventArgs e)
        {
            tripleType.Value = tripleTypetrackbar.Value;
        }

        private void tripleType_ValueChanged(object sender, EventArgs e)
        {
            tripleTypetrackbar.Value = Convert.ToInt32(tripleType.Value);
        }

        private void tripleTypetrackbar2_ValueChanged(object sender, EventArgs e)
        {
            tripleType2.Value = tripleTypetrackbar2.Value;
        }

        private void tripleType2_ValueChanged(object sender, EventArgs e)
        {
            tripleTypetrackbar2.Value = Convert.ToInt32(tripleType2.Value);
        }

        private void tripleTypetrackbar3_ValueChanged(object sender, EventArgs e)
        {
            tripleType3.Value = tripleTypetrackbar3.Value;
        }

        private void tripleType3_ValueChanged(object sender, EventArgs e)
        {
            tripleTypetrackbar3.Value = Convert.ToInt32(tripleType3.Value);
        }

        private void tripleTypetrackbar4_ValueChanged(object sender, EventArgs e)
        {
            tripleType4.Value = tripleTypetrackbar4.Value;
        }

        private void tripleType4_ValueChanged(object sender, EventArgs e)
        {
            tripleTypetrackbar4.Value = Convert.ToInt32(tripleType4.Value);
        }

        private void tripleTypetrackbar5_ValueChanged(object sender, EventArgs e)
        {
            tripleType5.Value = tripleTypetrackbar5.Value;
        }

        private void tripleType5_ValueChanged(object sender, EventArgs e)
        {
            tripleTypetrackbar5.Value = Convert.ToInt32(tripleType5.Value);
        }

        private void quintupleTypetrackbar_ValueChanged(object sender, EventArgs e)
        {
            quintupleType.Value = quintupleTypetrackbar.Value;
        }

        private void quintupleType_ValueChanged(object sender, EventArgs e)
        {
            quintupleTypetrackbar.Value = Convert.ToInt32(quintupleType.Value);
        }

        private void quintupleTypetrackbar2_ValueChanged(object sender, EventArgs e)
        {
            quintupleType2.Value = quintupleTypetrackbar2.Value;
        }

        private void quintupleType2_ValueChanged(object sender, EventArgs e)
        {
            quintupleTypetrackbar2.Value = Convert.ToInt32(quintupleType2.Value);
        }

        private void quintupleTypetrackbar3_ValueChanged(object sender, EventArgs e)
        {
            quintupleType3.Value = quintupleTypetrackbar3.Value;
        }

        private void quintupleType3_ValueChanged(object sender, EventArgs e)
        {
            quintupleTypetrackbar3.Value = Convert.ToInt32(quintupleType3.Value);
        }

        private void quintupleTypetrackbar4_ValueChanged(object sender, EventArgs e)
        {
            quintupleType4.Value = quintupleTypetrackbar4.Value;
        }

        private void quintupleType4_ValueChanged(object sender, EventArgs e)
        {
            quintupleTypetrackbar4.Value = Convert.ToInt32(quintupleType4.Value);
        }

        private void quintupleTypetrackbar5_ValueChanged(object sender, EventArgs e)
        {
            quintupleType5.Value = quintupleTypetrackbar5.Value;
        }

        private void quintupleType5_ValueChanged(object sender, EventArgs e)
        {
            quintupleTypetrackbar5.Value = Convert.ToInt32(quintupleType5.Value);
        }
    }
}

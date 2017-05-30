using System;
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
using System.Drawing.Drawing2D;


namespace Stepper
{
    public class Stepper : Form
    {
        private List<Song> songs;
        private int beats_per_measure = 4;
        Random r;
        private bool folderTextChanged = false;
        private AdjustableArrowCap cap;
        private Pen blackpen;
        private Pen redpen;
        private Pen bluepen;
        private int measures_per_sample = 4;
        private int instructionsTextboxGap = 40;
		private DanceStyleTabPage[] dstp;
		private TabPage first_dance_style;
		private TabPage last_dance_style;
		private string last_dance_style_name = "";

		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TabPage tabPage_write_stepfiles;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button button_write_stepfiles_back;
		private System.Windows.Forms.TextBox currentFolder;
		private System.Windows.Forms.Button folderChooser;
		private System.Windows.Forms.Button getInfo;
		private System.Windows.Forms.Button overwriteStepfiles;
		private System.Windows.Forms.Button close;
		private System.Windows.Forms.DataGridView songInfo;
		private System.Windows.Forms.TabPage tabPage_instructions;
		private System.Windows.Forms.TextBox textBox_intro;
		private System.Windows.Forms.Button button_intro_continue;
		private System.Windows.Forms.TabControl tabControl1;

        public Stepper()
        {
            InitializeComponent();
            textBox_intro.Text = @"Stepper overwrites existing Stepmania .ssc (or .sm) stepfiles with automatically generated steps. 

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
			int i = 0;
			foreach (string style in StepDeets.DanceStyles)
			{
				dstp[i] = new DanceStyleTabPage(tabControl1, style, beats_per_measure, measures_per_sample, blackpen, redpen, bluepen, r);
				dstp[i].setNoteSetParametersList(StepDeets.default_params[style]);
				tabControl1.Controls.Add(dstp[i]);
				if (i == 0)
				{
					// first dance_style: set instructions tab pointer to this one, set back pointer to instructions tab
					first_dance_style = dstp[i];
					dstp[i].setPrev("Instructions", tabPage_instructions);
				}
				else // 
				{
					// middle dance styles: set next pointer of prev tab to this tabpage, set back pointer of this one to prev tabpage
					dstp[i - 1].setNext(StepDeets.stepTitle(style), dstp[i]);
					dstp[i].setPrev(StepDeets.stepTitle(StepDeets.DanceStyles[i-1]), dstp[i-1]);
				}
				if (i == (StepDeets.DanceStyles.Count() -1)) // last dance style: set next pointer of this tab to "write stepfiles" page 
				{
					dstp[i].setNext("Write Stepfiles", tabPage_write_stepfiles);
					last_dance_style = dstp[i];
					last_dance_style_name = StepDeets.stepTitle(style);
				}
				i++;
			}
			tabControl1.Controls.Add(this.tabPage_write_stepfiles);
			button_write_stepfiles_back.Text = "Back to " + this.last_dance_style_name;

        }

        private void selectFolder_Click(object sender, EventArgs e)
        {
            string default_folder = "C:\\Games\\StepMania 5\\Test";
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
			Cursor.Current = Cursors.WaitCursor;
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
                        songInfo[1, i].Value = s.min_BPM;
                        songInfo[2, i].Value = s.max_BPM;
                        songInfo[3, i].Value = s.BPM_changes;
                        songInfo[4, i].Value = s.num_stops;
						songInfo[5, i].Value = s.num_notesets;
                        if (s.min_measures == s.max_measures)
                        {
                            songInfo[6, i].Value = s.min_measures;
                        }
                        else
                        {
                            songInfo[6, i].Value = s.min_measures + "-" + s.max_measures;
                        }
						songInfo[7, i].Value = s.type;
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
                        songInfo[1, i].Value = s.min_BPM;
                        songInfo[2, i].Value = s.max_BPM;
						songInfo[3, i].Value = s.BPM_changes;
						songInfo[4, i].Value = s.num_stops;
						songInfo[5, i].Value = s.num_notesets;
                        if (s.min_measures == s.max_measures)
                        {
                            songInfo[6, i].Value = s.min_measures;
                        }
                        else
                        {
                            songInfo[6, i].Value = s.min_measures + "-" + s.max_measures;
                        }
                        songInfo[7, i].Value = s.type;
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
			Cursor.Current = Cursors.Default;
            if (songs.Count == 0)
            {
                MessageBox.Show("Please choose a valid Stepmania song group folder", "Folder name invalid");
                folderTextChanged = true;
                return;
            }
        }

        private void overwriteStepfiles_Click(object sender, EventArgs e)
        {
			List<Noteset> noteset_list = new List<Noteset>();
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
				Cursor.Current = Cursors.WaitCursor;
				foreach(Song s in songs)
                {
					noteset_list.Clear();
					foreach (DanceStyleTabPage page in dstp)
					{
						foreach (NotesetParameters n in page.getNoteSetParametersList() )
						{
							Noteset note = new Noteset(n, s.type, s.num_measures, r);
							note.generateSteps();
							noteset_list.Add(note);
						}
					}

					if (s.type.Equals(StepDeets.SSC))
                    {
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmm");
                        string old_path = s.path;
                        Regex alter_extension = new Regex("\\.ssc");
                        string backup_path = alter_extension.Replace(old_path, ".ssc." + timestamp + ".bak");
                        if (!File.Exists(backup_path))
                        {
                            System.IO.File.Move(old_path, backup_path);
                        }
                        System.IO.StreamWriter file = new System.IO.StreamWriter(old_path);
                        List<string> header_lines = s.header;
                        header_lines.ForEach(delegate(string header_line)
                        {
                            file.WriteLine(header_line);
                        });
						foreach (Noteset ns in noteset_list)
						{
							ns.writeSteps(StepDeets.SSC, file);
						}
                        file.Close();
                    }
					else if (s.type.Equals(StepDeets.SM))
                    {
                        // no ssc file, so backup the old .sm file and then overwrite it
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmm");
                        string old_path = s.path;
                        Regex alter_extension = new Regex("\\.sm");
                        string backup_path = alter_extension.Replace(old_path, ".sm." + timestamp + ".bak");
                        if (!File.Exists(backup_path))
                        {
                            System.IO.File.Move(old_path, backup_path);
                        }
                        System.IO.StreamWriter file = new System.IO.StreamWriter(old_path);
                        List<string> header_lines = s.header;
                        header_lines.ForEach(delegate(string header_line)
                        {
                            file.WriteLine(header_line);
                        });

						foreach (Noteset ns in noteset_list)
						{
							ns.writeSteps(StepDeets.SM, file);
						}
                        file.Close();
                    }
                }
				Cursor.Current = Cursors.Default; 
				MessageBox.Show("Step generation succeeded!\n\nDon't forget to clear the cache in \n\nC:\\Users\\<your username>\\AppData\\Roaming\\StepMania 5\\Cache\n\n if this is your first time running Stepper on this folder.");
                return;
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
            Song s = new Song(StepDeets.SSC, file, header_text, mode, trimmed_min, trimmed_max, trimmed_count, num_stops, note_sets, measures_list.Min(), measures_list.Max());
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
            Song s = new Song(StepDeets.SM, file, header_text, (int)measures_list.Max(), trimmed_min, trimmed_max, trimmed_count, num_stops, note_sets, measures_list.Min(), measures_list.Max());
            return s;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void currentFolder_TextChanged(object sender, EventArgs e)
        {
            folderTextChanged = true;
        }

        private void button_intro_continue_Click(object sender, EventArgs e)
        {
			if (first_dance_style != null)
			{
				tabControl1.SelectedTab = first_dance_style;
			}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage_instructions;
        }

		private void button4_Click(object sender, EventArgs e)
		{
			tabControl1.SelectedTab = tabPage_write_stepfiles;
		}

		private void button_write_stepfiles_back_Click(object sender, EventArgs e)
		{
			if (last_dance_style != null)
			{
				tabControl1.SelectedTab = last_dance_style;
			}
		}

		private void tabPage_instructions_Resize(object sender, EventArgs e)
        {
            textBox_intro.Height = tabPage_instructions.Height - instructionsTextboxGap;
        }

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            button_write_stepfiles_back.Width = Math.Max(Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * Convert.ToDouble(0.15)), button_write_stepfiles_back.MinimumSize.Width);
            currentFolder.Width = Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * Convert.ToDouble(0.25));
            folderChooser.Width = Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * Convert.ToDouble(0.15));
            getInfo.Width = Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * Convert.ToDouble(0.15));
            overwriteStepfiles.Width = Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * Convert.ToDouble(0.15));
            close.Width = Convert.ToInt32(Convert.ToDouble(flowLayoutPanel1.Width) * Convert.ToDouble(0.1));
        }

        private void tabPage_write_stepfiles_Resize(object sender, EventArgs e)
        {
            songInfo.Height = tabPage_write_stepfiles.Height - instructionsTextboxGap;
        }

		private void InitializeComponent()
		{
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.tabPage_write_stepfiles = new System.Windows.Forms.TabPage();
			this.songInfo = new System.Windows.Forms.DataGridView();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.close = new System.Windows.Forms.Button();
			this.overwriteStepfiles = new System.Windows.Forms.Button();
			this.getInfo = new System.Windows.Forms.Button();
			this.folderChooser = new System.Windows.Forms.Button();
			this.currentFolder = new System.Windows.Forms.TextBox();
			this.button_write_stepfiles_back = new System.Windows.Forms.Button();
			this.tabPage_instructions = new System.Windows.Forms.TabPage();
			this.button_intro_continue = new System.Windows.Forms.Button();
			this.textBox_intro = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage_write_stepfiles.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.songInfo)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.tabPage_instructions.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// folderBrowserDialog1
			// 
			this.folderBrowserDialog1.ShowNewFolderButton = false;
			// 
			// tabPage_write_stepfiles
			// 
			this.tabPage_write_stepfiles.AutoScroll = true;
			this.tabPage_write_stepfiles.Controls.Add(this.flowLayoutPanel1);
			this.tabPage_write_stepfiles.Controls.Add(this.songInfo);
			this.tabPage_write_stepfiles.Location = new System.Drawing.Point(4, 22);
			this.tabPage_write_stepfiles.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_write_stepfiles.Size = new System.Drawing.Size(1038, 672);
			this.tabPage_write_stepfiles.TabIndex = 3;
			this.tabPage_write_stepfiles.Text = "Write Stepfiles";
			this.tabPage_write_stepfiles.UseVisualStyleBackColor = true;
			this.tabPage_write_stepfiles.Resize += new System.EventHandler(this.tabPage_write_stepfiles_Resize);
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
			this.flowLayoutPanel1.Controls.Add(this.button_write_stepfiles_back);
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
			// button_write_stepfiles_back
			// 
			this.button_write_stepfiles_back.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.button_write_stepfiles_back.Location = new System.Drawing.Point(3, 3);
			this.button_write_stepfiles_back.Size = new System.Drawing.Size(216, 30);
			this.button_write_stepfiles_back.TabIndex = 0;
			this.button_write_stepfiles_back.Text = "Back to " + this.last_dance_style_name;
			this.button_write_stepfiles_back.UseVisualStyleBackColor = false;
			this.button_write_stepfiles_back.Click += new System.EventHandler(this.button_write_stepfiles_back_Click);
			// 
			// tabPage_instructions
			// 
			this.tabPage_instructions.Controls.Add(this.textBox_intro);
			this.tabPage_instructions.Controls.Add(this.button_intro_continue);
			this.tabPage_instructions.Location = new System.Drawing.Point(4, 22);
			this.tabPage_instructions.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_instructions.Size = new System.Drawing.Size(1038, 672);
			this.tabPage_instructions.TabIndex = 0;
			this.tabPage_instructions.Text = "Instructions";
			this.tabPage_instructions.UseVisualStyleBackColor = true;
			this.tabPage_instructions.Resize += new System.EventHandler(this.tabPage_instructions_Resize);
			// 
			// button_intro_continue
			// 
			this.button_intro_continue.AutoSize = true;
			this.button_intro_continue.BackColor = System.Drawing.Color.YellowGreen;
			this.button_intro_continue.CausesValidation = false;
			this.button_intro_continue.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.button_intro_continue.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button_intro_continue.Location = new System.Drawing.Point(3, 639);
			this.button_intro_continue.Name = "button1";
			this.button_intro_continue.Size = new System.Drawing.Size(1032, 30);
			this.button_intro_continue.TabIndex = 4;
			this.button_intro_continue.Text = "Continue";
			this.button_intro_continue.UseVisualStyleBackColor = false;
			this.button_intro_continue.Click += new System.EventHandler(this.button_intro_continue_Click);
			// 
			// textBox_intro
			// 
			this.textBox_intro.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox_intro.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.textBox_intro.Location = new System.Drawing.Point(3, 3);
			this.textBox_intro.MaximumSize = new System.Drawing.Size(2000, 1000);
			this.textBox_intro.MinimumSize = new System.Drawing.Size(100, 100);
			this.textBox_intro.Multiline = true;
			this.textBox_intro.ReadOnly = true;
			this.textBox_intro.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_intro.Size = new System.Drawing.Size(1032, 630);
			this.textBox_intro.TabIndex = 5;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage_instructions);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
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
			this.tabPage_write_stepfiles.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.songInfo)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.tabPage_instructions.ResumeLayout(false);
			this.tabPage_instructions.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);

			r = new Random();
			dstp = new DanceStyleTabPage[StepDeets.DanceStyles.Count()];

			// create arrows and pens for the Sample windows
			cap = new AdjustableArrowCap(2, 1);
			cap.WidthScale = 1;
			cap.BaseCap = LineCap.Square;
			cap.Height = 1;
			blackpen = new Pen(Color.Black, 10);
			blackpen.CustomEndCap = cap;
			redpen = new Pen(Color.Red, 10);
			redpen.CustomEndCap = cap;
			bluepen = new Pen(Color.Blue, 10);
			bluepen.CustomEndCap = cap;



		}

	/*	private System.ComponentModel.IContainer components = null;

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
		} */
     }
}

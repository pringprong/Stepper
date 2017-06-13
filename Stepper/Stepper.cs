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
		private TabControl tabControl1;
		private TabPage instructions_tabPage;
		private TextBox intro_textBox;
		private Button intro_continue_button;

		private DanceStyleTabPage[] dstp;
		private TabPage first_dance_style;
		private TabPage last_dance_style;
		private string last_dance_style_name = "";
		private Random r;
		private AdjustableArrowCap cap;
		private Pen blackpen;
		private Pen redpen;
		private Pen bluepen;
		private int measures_per_sample = 4;

		private List<Song> source_songs;
		private TabPage source_folder_tabPage;
		private FolderBrowserDialog select_source_folderBrowserDialog;
		private bool source_folder_text_changed = true;
		private FlowLayoutPanel source_flowLayoutPanel;
		private TextBox source_folder_TextBox;
		private Button source_folder_chooser_button;
		private Button source_get_info_button;
		private DataGridView source_song_info_dgv;
		private Button source_folder_back_button;
		private FlowLayoutPanel source_prev_next_flowLayoutPanel;
		private Button source_next_button;
		private Label source_Label;

		private List<Song> destination_songs;
		private TabPage destination_tabPage;
		private FolderBrowserDialog select_destination_folderBrowserDialog;
		private bool destination_folder_text_changed = false;
		private FlowLayoutPanel destination_flowLayoutPanel;
		private TextBox destination_folder_TextBox;
		private Button destination_folder_chooser_button;
		private Button destination_get_info_button;
		private CheckBox destination_folder_same_checkbox;
		private DataGridView destination_song_info_dgv;
		private Button destination_back_button;
		private Label destination_Label;
		private FlowLayoutPanel destination_prev_next_flowLayoutPanel;
		private Button overwrite_stepfiles_button;
		private Button save_config_button;
		private Button close_button;

		public Stepper()
		{
			InitializeComponent();
			int i = 0;
			foreach (string style in StepDeets.DanceStyles)
			{
				dstp[i] = new DanceStyleTabPage(tabControl1, style, StepDeets.beats_per_measure, measures_per_sample, blackpen, redpen, bluepen, r);
				dstp[i].setNoteSetParametersList(StepDeets.default_params[style]);
				tabControl1.Controls.Add(dstp[i]);
				if (i == 0)
				{
					// first dance_style: set instructions tab pointer to this one, set back pointer to instructions tab
					first_dance_style = dstp[i];
					dstp[i].setPrev("Instructions", instructions_tabPage);
				}
				else // 
				{
					// middle dance styles: set next pointer of prev tab to this tabpage, set back pointer of this one to prev tabpage
					dstp[i - 1].setNext(StepDeets.stepTitle(style), dstp[i]);
					dstp[i].setPrev(StepDeets.stepTitle(StepDeets.DanceStyles[i - 1]), dstp[i - 1]);
				}
				if (i == (StepDeets.DanceStyles.Count() - 1)) // last dance style: set next pointer of this tab to "write stepfiles" page 
				{
					dstp[i].setNext("Choose Source Folder", source_folder_tabPage);
					last_dance_style = dstp[i];
					last_dance_style_name = StepDeets.stepTitle(style);
				}
				i++;
			}
			tabControl1.Controls.Add(this.source_folder_tabPage);
			source_folder_back_button.Text = "Back to " + this.last_dance_style_name;
			tabControl1.Controls.Add(this.destination_tabPage);
		}

		private void source_folder_chooser_button_Click(object sender, EventArgs e)
		{
			string default_folder = StepDeets.DefaultSourceFolder;
			if ((Directory.Exists(source_folder_TextBox.Text)))
			{
				select_source_folderBrowserDialog.SelectedPath = source_folder_TextBox.Text;
			}
			else if ((Directory.Exists(default_folder)))
			{
				select_source_folderBrowserDialog.SelectedPath = default_folder;
			}
			else
			{
				select_source_folderBrowserDialog.SelectedPath = "";
			}
			var result = select_source_folderBrowserDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				source_folder_TextBox.Text = select_source_folderBrowserDialog.SelectedPath;
				source_folder_text_changed = true;
			}
		}

		private void destination_folder_chooser_button_Click(object sender, EventArgs e)
		{
			string default_folder = StepDeets.DefaultDestinationFolder;
			if ((Directory.Exists(destination_folder_TextBox.Text)))
			{
				select_destination_folderBrowserDialog.SelectedPath = destination_folder_TextBox.Text;
			}
			else if ((Directory.Exists(default_folder)))
			{
				select_destination_folderBrowserDialog.SelectedPath = default_folder;
			}
			else
			{
				select_destination_folderBrowserDialog.SelectedPath = "";
			}
			var result = select_destination_folderBrowserDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				destination_folder_TextBox.Text = select_destination_folderBrowserDialog.SelectedPath;
				destination_folder_text_changed = true;
			}
		}

		private void source_get_info_button_Click(object sender, EventArgs e)
		{
			bool success = getInfo(source_songs, source_song_info_dgv, source_folder_TextBox.Text);
			if (source_songs.Count == 0)
			{
				MessageBox.Show("Please choose a valid Stepmania song group folder", "Folder name invalid");
				source_folder_text_changed = true;
				return;
			}
			if (success)
			{
				source_folder_text_changed = false;
			}
		}

		private void destination_get_info_button_Click(object sender, EventArgs e)
		{
			bool success = getInfo(destination_songs, destination_song_info_dgv, destination_folder_TextBox.Text);
			if (success)
			{
				destination_folder_text_changed = false;
			}
		}

		private bool getInfo(List<Song> song_list, DataGridView song_info_dgv, string folder)
		{
			song_list.Clear();
			if (!(Directory.Exists(folder)))
			{
				MessageBox.Show("Please choose a valid Stepmania song group folder", "Folder name invalid");
				return false;
			}
			Cursor.Current = Cursors.WaitCursor;
			string[] dirs = Directory.GetDirectories(folder);
			var fileCount = dirs.Count();
			song_info_dgv.ColumnCount = 8;
			song_info_dgv.Columns[0].HeaderText = "Song Path";
			song_info_dgv.Columns[1].HeaderText = "Min BPM";
			song_info_dgv.Columns[2].HeaderText = "Max BPM";
			song_info_dgv.Columns[3].HeaderText = "# BPM Changes";
			song_info_dgv.Columns[4].HeaderText = "# Stops";
			song_info_dgv.Columns[5].HeaderText = "# Step Sets";
			song_info_dgv.Columns[6].HeaderText = "# Measures";
			song_info_dgv.Columns[7].HeaderText = "Type";
			song_info_dgv.Columns[0].Width = 415;
			song_info_dgv.Columns[1].Width = 90;
			song_info_dgv.Columns[2].Width = 90;
			song_info_dgv.Columns[3].Width = 110;
			song_info_dgv.Columns[4].Width = 70;
			song_info_dgv.Columns[5].Width = 90;
			song_info_dgv.Columns[6].Width = 90;
			song_info_dgv.Columns[7].Width = 50;
			song_info_dgv.Columns[0].ToolTipText = "Complete path name of each Stepmania song folder";
			song_info_dgv.Columns[1].ToolTipText = "Minimum beats per minute of each song";
			song_info_dgv.Columns[2].ToolTipText = "Maximum beats per minute of each song";
			song_info_dgv.Columns[3].ToolTipText = "Number of times the beats per minute changes during each song";
			song_info_dgv.Columns[4].ToolTipText = "Number of stops during each song";
			song_info_dgv.Columns[5].ToolTipText = "Number of sets of arrows for each song, including Single, Double, and Solo. This program will produce 5 Single sets";
			song_info_dgv.Columns[6].ToolTipText = "Range of number of measures found in each set of arrows. There are 4 beats per measure.";
			song_info_dgv.Columns[7].ToolTipText = "Stepfile type: .ssc, .sm, or .dwi";

			song_info_dgv.RowCount = fileCount;
			DataGridViewColumnCollection coll = song_info_dgv.Columns;
			DataGridViewColumn c = coll[0];
			for (int i = 0; i < fileCount; i++)
			{
				song_info_dgv[0, i].Value = dirs[i];
				if (Directory.GetFiles(dirs[i], "*.ssc").Count() > 0)
				{
					//parse .ssc file
					string[] files = Directory.GetFiles(dirs[i], "*.ssc");
					Song s = parseSSCFile(files[0]);
					if (!s.Equals(null))
					{
						song_list.Add(s);
						song_info_dgv[1, i].Value = s.min_BPM;
						song_info_dgv[2, i].Value = s.max_BPM;
						song_info_dgv[3, i].Value = s.BPM_changes;
						song_info_dgv[4, i].Value = s.num_stops;
						song_info_dgv[5, i].Value = s.num_notesets;
						if (s.min_measures == s.max_measures)
						{
							song_info_dgv[6, i].Value = s.min_measures;
						}
						else
						{
							song_info_dgv[6, i].Value = s.min_measures + "-" + s.max_measures;
						}
						song_info_dgv[7, i].Value = s.type;
						song_info_dgv.ClearSelection();
					}
				}
				else if (Directory.GetFiles(dirs[i], "*.sm").Count() > 0)
				{
					// parse .sm file
					string[] files = Directory.GetFiles(dirs[i], "*.sm");
					Song s = parseSMFile(files[0]);
					if (!s.Equals(null))
					{
						song_list.Add(s);
						song_info_dgv[1, i].Value = s.min_BPM;
						song_info_dgv[2, i].Value = s.max_BPM;
						song_info_dgv[3, i].Value = s.BPM_changes;
						song_info_dgv[4, i].Value = s.num_stops;
						song_info_dgv[5, i].Value = s.num_notesets;
						if (s.min_measures == s.max_measures)
						{
							song_info_dgv[6, i].Value = s.min_measures;
						}
						else
						{
							song_info_dgv[6, i].Value = s.min_measures + "-" + s.max_measures;
						}
						song_info_dgv[7, i].Value = s.type;
						song_info_dgv.ClearSelection();
					}
				}
				else if (Directory.GetFiles(dirs[i], "*.dwi").Count() > 0)
				{
					song_info_dgv[1, i].Value = "";
					song_info_dgv[2, i].Value = "";
					song_info_dgv[3, i].Value = "";
					song_info_dgv[4, i].Value = "";
					song_info_dgv[5, i].Value = "";
					song_info_dgv[6, i].Value = "";
					song_info_dgv[7, i].Value = "DWI";
					song_info_dgv.ClearSelection();
				}
				else
				{
					song_info_dgv[1, i].Value = "";
					song_info_dgv[2, i].Value = "";
					song_info_dgv[3, i].Value = "";
					song_info_dgv[4, i].Value = "";
					song_info_dgv[5, i].Value = "";
					song_info_dgv[6, i].Value = "";
					song_info_dgv[7, i].Value = "NONE";
					song_info_dgv.ClearSelection();
				}
			} // end for (int i = 0; i < fileCount; i++) 
			Cursor.Current = Cursors.Default;
			return true;
		}

		private void overwrite_stepfiles_button_Click(object sender, EventArgs e)
		{
			List<Noteset> noteset_list = new List<Noteset>();
			if (source_songs.Count == 0)
			{
				MessageBox.Show("Please choose a Stepmania song group folder on the Choose Source Folder page and click \"Get info\"", "Click Get info");
				return;
			}
			if (source_folder_text_changed || destination_folder_text_changed)
			{
				MessageBox.Show("The destination folder name has changed or is invalid. Please choose a Stepmania song group folder and click \"Get info\" to update the song info table", "Click Get info");
				return;
			}
			DialogResult result1 = MessageBox.Show("Are you sure you want to overwrite the stepfiles in " + source_folder_TextBox.Text + " ? This action cannot be undone.",
				"Warning: Overwriting stepfiles",
				MessageBoxButtons.YesNo);
			if (result1 == DialogResult.Yes)
			{
				Cursor.Current = Cursors.WaitCursor;
				foreach (Song s in source_songs)
				{
					noteset_list.Clear();
					foreach (DanceStyleTabPage page in dstp)
					{
						foreach (NotesetParameters n in page.getNoteSetParametersList())
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

		private void close_button_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void source_folder_TextBox_TextChanged(object sender, EventArgs e)
		{
			source_folder_text_changed = true;
		}

		private void destination_folder_TextBox_TextChanged(object sender, EventArgs e)
		{
			destination_folder_text_changed = true;
		}
		private void intro_continue_button_Click(object sender, EventArgs e)
		{
			if (first_dance_style != null)
			{
				tabControl1.SelectedTab = first_dance_style;
			}
		}

		private void destination_back_button_Click(object sender, EventArgs e)
		{
			tabControl1.SelectedTab = source_folder_tabPage;
		}

		private void source_folder_back_button_Click(object sender, EventArgs e)
		{
			if (last_dance_style != null)
			{
				tabControl1.SelectedTab = last_dance_style;
			}
		}

		private void source_next_button_Click(object sender, EventArgs e)
		{
			if (source_folder_text_changed)
			{
				MessageBox.Show("The folder name has changed or is invalid. Please choose a Stepmania song group folder and click \"Get info\" to update the song info table", "Click Get info");
				return;
			}
			tabControl1.SelectedTab = destination_tabPage;
		}

		private void destination_tabPage_GotFocus(object sender, EventArgs e)
		{
			if (source_folder_text_changed && tabControl1.SelectedTab == destination_tabPage)
			{
				MessageBox.Show("The folder name has changed or is invalid. Please choose a Stepmania song group folder and click \"Get info\" to update the song info table", "Click Get info");
				tabControl1.SelectedTab = this.source_folder_tabPage;
			}			
		}

		private void destination_folder_same_checkbox_CheckedChanged(object sender, EventArgs e)
		{
			if (destination_folder_same_checkbox.Checked)
			{
				destination_folder_TextBox.Text = source_folder_TextBox.Text;
				bool success = getInfo(destination_songs, destination_song_info_dgv, destination_folder_TextBox.Text);
				if (success)
				{
					destination_folder_text_changed = false;
				}
				destination_flowLayoutPanel.Enabled = false;
			}
			else
			{
				destination_flowLayoutPanel.Enabled = false;
				destination_folder_text_changed = true;
			}
		}

		private void InitializeComponent()
		{
			this.tabControl1 = new TabControl();
			this.instructions_tabPage = new TabPage();
			this.intro_textBox = new TextBox();
			this.intro_continue_button = new Button();

			this.source_folder_tabPage = new TabPage();
			this.select_source_folderBrowserDialog = new FolderBrowserDialog();
			this.source_flowLayoutPanel = new FlowLayoutPanel();
			this.source_folder_TextBox = new TextBox();
			this.source_folder_chooser_button = new Button();
			this.source_get_info_button = new Button();
			this.source_song_info_dgv = new DataGridView();
			this.source_folder_back_button = new Button();
			this.source_prev_next_flowLayoutPanel = new FlowLayoutPanel();
			this.source_next_button = new Button();
			this.source_Label = new Label();
			this.source_songs = new List<Song>();


			this.destination_tabPage = new TabPage();
			this.select_destination_folderBrowserDialog = new FolderBrowserDialog();
			this.destination_flowLayoutPanel = new FlowLayoutPanel();
			this.destination_folder_TextBox = new TextBox();
			this.destination_folder_chooser_button = new Button();
			this.destination_get_info_button = new Button();
			this.destination_folder_same_checkbox = new CheckBox();
			this.destination_song_info_dgv = new DataGridView();
			this.destination_back_button = new Button();
			this.destination_Label = new Label();
			this.destination_prev_next_flowLayoutPanel = new FlowLayoutPanel();
			this.overwrite_stepfiles_button = new Button();
			this.save_config_button = new Button();
			this.close_button = new Button();
			this.destination_songs = new List<Song>();
			// 
			// Stepper
			// 
			this.ClientSize = new Size(1060, 735);
			this.MaximumSize = this.ClientSize;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.AutoScroll = true;
			this.Text = "Stepper v2.0";
			// 
			// tabControl1
			// 
			this.tabControl1.Location = new Point(0, 0);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(this.Width - 16, this.Height - 41);
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.destination_tabPage_GotFocus);
			// 
			// instructions_tabPage
			// 
			this.instructions_tabPage.Text = "Instructions";
			this.instructions_tabPage.Size = tabControl1.Size;
			// 
			// intro_continue_button
			// 
			this.intro_continue_button.BackColor = Color.YellowGreen;
			this.intro_continue_button.Location = new Point(3, 639);
			this.intro_continue_button.Size = new Size(instructions_tabPage.Width - 20, 30);
			this.intro_continue_button.Text = "Continue";
			this.intro_continue_button.Click += new System.EventHandler(this.intro_continue_button_Click);
			// 
			// textBox_intro
			// 
			this.intro_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.intro_textBox.Location = new System.Drawing.Point(3, 3);
			this.intro_textBox.Multiline = true;
			this.intro_textBox.ReadOnly = true;
			//this.textBox_intro.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.intro_textBox.Size = new System.Drawing.Size(instructions_tabPage.Width - 20, instructions_tabPage.Height - 60);
			// 
			// source_folder_tabPage
			// 
			this.source_folder_tabPage.Text = "Choose Source Folder";
			this.source_folder_tabPage.Size = tabControl1.Size;
			this.source_folder_tabPage.Padding = new Padding(3);
			// 
			// source_flowLayoutPanel
			// 
			this.source_flowLayoutPanel.Location = new Point(3, 3);
			this.source_flowLayoutPanel.Margin = new Padding(3);
			this.source_flowLayoutPanel.Size = new Size(source_folder_tabPage.Width - 20, 35);
			//
			// source_Label
			//
			this.source_Label.Font = new Font("Microsoft Sans Serif", 12F);
			this.source_Label.Text = "Choose a source folder and click Get Info:";
			this.source_Label.Size = new Size(340, 30);
			this.source_Label.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// source_get_info_button
			// 
			this.source_get_info_button.BackColor = Color.YellowGreen;
			this.source_get_info_button.Size = new Size(source_flowLayoutPanel.Width / 6, 30);
			this.source_get_info_button.Text = "Get Info";
			this.source_get_info_button.Click += new System.EventHandler(this.source_get_info_button_Click);
			// 
			// source_folder_chooser_button
			// 
			this.source_folder_chooser_button.BackColor = Color.LightBlue;
			this.source_folder_chooser_button.Size = new Size(source_flowLayoutPanel.Width / 6, 30);
			this.source_folder_chooser_button.Text = "Change folder";
			this.source_folder_chooser_button.Click += new System.EventHandler(this.source_folder_chooser_button_Click);
			// 
			// source_folder_TextBox
			// 
			this.source_folder_TextBox.Font = new Font("Microsoft Sans Serif", 12F);
			this.source_folder_TextBox.Size = new Size(303, 30);
			this.source_folder_TextBox.Text = "C:\\Games\\StepMania 5\\Songs\\Test";
			this.source_folder_TextBox.TextChanged += new System.EventHandler(this.source_folder_TextBox_TextChanged);
			// 
			// source_song_info_dgv
			// 
			this.source_song_info_dgv.AllowUserToAddRows = false;
			this.source_song_info_dgv.AllowUserToDeleteRows = false;
			this.source_song_info_dgv.AllowUserToResizeColumns = false;
			this.source_song_info_dgv.AllowUserToResizeRows = false;
			this.source_song_info_dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
			this.source_song_info_dgv.Location = new Point(3, 40);
			this.source_song_info_dgv.ReadOnly = true;
			this.source_song_info_dgv.RowHeadersVisible = false;
			this.source_song_info_dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.source_song_info_dgv.ShowEditingIcon = false;
			this.source_song_info_dgv.Size = new Size(source_folder_tabPage.Width - 18, source_folder_tabPage.Height - 100);
			//
			// source_prev_next_flowLayoutPanel
			//
			this.source_prev_next_flowLayoutPanel.Size = new Size(source_folder_tabPage.Width - 20, 35);
			this.source_prev_next_flowLayoutPanel.Location = new Point(3, source_folder_tabPage.Height - 60);
			// 
			// source_folder_back_button
			// 
			this.source_folder_back_button.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.source_folder_back_button.Size = new Size(source_prev_next_flowLayoutPanel.Width / 2 - 8, 30);
			this.source_folder_back_button.Text = "Back to " + this.last_dance_style_name;
			this.source_folder_back_button.Click += new System.EventHandler(this.source_folder_back_button_Click);
			//
			// source_next_button
			//
			this.source_next_button.Size = source_folder_back_button.Size;
			this.source_next_button.Text = "Continue to Write Stepfiles";
			this.source_next_button.BackColor = Color.YellowGreen;
			this.source_next_button.Click += new System.EventHandler(this.source_next_button_Click);
			// 
			// select_source_folderBrowserDialog
			// 
			this.select_source_folderBrowserDialog.ShowNewFolderButton = false;
			//
			// destination_tabPage
			//
			this.destination_tabPage.Padding = new Padding(3);
			this.destination_tabPage.Size = tabControl1.Size;
			this.destination_tabPage.Text = "Write Stepfiles";
		//	this.destination_tabPage += 
			//
			// destination_folder_same_checkbox
			//
			this.destination_folder_same_checkbox.Text = "Destination folder same as source folder";
			this.destination_folder_same_checkbox.Size = new Size(400, 20);
			this.destination_folder_same_checkbox.Location = new Point(400, 3);
			this.destination_folder_same_checkbox.Font = new Font("Microsoft Sans Serif", 12F);
			this.destination_folder_same_checkbox.CheckedChanged += new System.EventHandler(this.destination_folder_same_checkbox_CheckedChanged);
			//
			// destination_flowLayoutPanel
			//
			this.destination_flowLayoutPanel.Location = new Point(3, 30);
			this.destination_flowLayoutPanel.Margin = new Padding(3);
			this.destination_flowLayoutPanel.Size = new Size(destination_tabPage.Width - 20, 35);
			//
			// destination_Label
			//
			this.destination_Label.Font = new Font("Microsoft Sans Serif", 12F);
			this.destination_Label.Text = "Choose a destination folder and click Get Info:";
			this.destination_Label.Size = new Size(340, 30);
			this.destination_Label.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// destination_get_info_button
			// 
			this.destination_get_info_button.BackColor = Color.YellowGreen;
			this.destination_get_info_button.Size = new Size(source_flowLayoutPanel.Width / 9, 30);
			this.destination_get_info_button.Text = "Get Info";
			this.destination_get_info_button.Click += new System.EventHandler(this.destination_get_info_button_Click);
			// 
			// destination_folder_chooser_button
			// 
			this.destination_folder_chooser_button.BackColor = Color.LightBlue;
			this.destination_folder_chooser_button.Size = new Size(destination_flowLayoutPanel.Width / 9, 30);
			this.destination_folder_chooser_button.Text = "Change folder";
			this.destination_folder_chooser_button.Click += new System.EventHandler(this.destination_folder_chooser_button_Click);
			// 
			// destination_folder_TextBox
			// 
			this.destination_folder_TextBox.Font = new Font("Microsoft Sans Serif", 12F);
			this.destination_folder_TextBox.Size = new Size(303, 30);
			this.destination_folder_TextBox.Text = "C:\\Games\\StepMania 5\\Songs\\Test";
			this.destination_folder_TextBox.TextChanged += new System.EventHandler(this.destination_folder_TextBox_TextChanged);
			// 
			// overwrite_stepfiles_button
			// 
			this.overwrite_stepfiles_button.BackColor = System.Drawing.Color.Orange;
			this.overwrite_stepfiles_button.Size = new System.Drawing.Size(destination_flowLayoutPanel.Width / 9, 30);
			this.overwrite_stepfiles_button.Location = new Point(400, destination_tabPage.Height - 90);
			this.overwrite_stepfiles_button.Text = "Overwrite Stepfiles";
			this.overwrite_stepfiles_button.Click += new System.EventHandler(this.overwrite_stepfiles_button_Click);
			// 
			// destination_song_info_dgv
			// 
			this.destination_song_info_dgv.AllowUserToAddRows = false;
			this.destination_song_info_dgv.AllowUserToDeleteRows = false;
			this.destination_song_info_dgv.AllowUserToResizeColumns = false;
			this.destination_song_info_dgv.AllowUserToResizeRows = false;
			this.destination_song_info_dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
			this.destination_song_info_dgv.Location = new Point(3, 70);
			this.destination_song_info_dgv.ReadOnly = true;
			this.destination_song_info_dgv.RowHeadersVisible = false;
			this.destination_song_info_dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.destination_song_info_dgv.ShowEditingIcon = false;
			this.destination_song_info_dgv.Size = new Size(destination_tabPage.Width - 18, destination_tabPage.Height - 160);
			//
			// destination_prev_next_flowLayoutPanel
			//
			this.destination_prev_next_flowLayoutPanel.Size = new Size(destination_tabPage.Width - 20, 35);
			this.destination_prev_next_flowLayoutPanel.Location = new Point(3, destination_tabPage.Height - 60);
			// 
			// destination_back_button
			// 
			this.destination_back_button.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.destination_back_button.Size = new Size(destination_prev_next_flowLayoutPanel.Width / 3 - 8, 30);
			this.destination_back_button.Text = "Back to Choose Source Folder";
			this.destination_back_button.Click += new System.EventHandler(this.destination_back_button_Click);
			//
			// save_config_button
			//
			this.close_button.BackColor = System.Drawing.Color.LightGray;
			this.save_config_button.Size = new Size(destination_prev_next_flowLayoutPanel.Width / 3 - 8, 30);
			this.save_config_button.Text = "Save Configuration";
			this.save_config_button.Enabled = false;
			// 
			// close_button
			// 
			this.close_button.BackColor = System.Drawing.Color.Goldenrod;
			this.close_button.Size = new Size(destination_prev_next_flowLayoutPanel.Width / 3 - 8, 30);
			this.close_button.Text = "Close";
			this.close_button.Click += new System.EventHandler(this.close_button_Click);

			// put it all together
			this.source_folder_tabPage.Controls.Add(this.source_flowLayoutPanel);
			this.source_flowLayoutPanel.Controls.Add(this.source_Label);
			this.source_flowLayoutPanel.Controls.Add(this.source_folder_TextBox);
			this.source_flowLayoutPanel.Controls.Add(this.source_folder_chooser_button);
			this.source_flowLayoutPanel.Controls.Add(this.source_get_info_button);
			this.source_folder_tabPage.Controls.Add(this.source_song_info_dgv);
			this.source_prev_next_flowLayoutPanel.Controls.Add(this.source_folder_back_button);
			this.source_prev_next_flowLayoutPanel.Controls.Add(this.source_next_button);
			this.source_folder_tabPage.Controls.Add(this.source_prev_next_flowLayoutPanel);

			this.destination_flowLayoutPanel.Controls.Add(this.destination_Label);
			this.destination_flowLayoutPanel.Controls.Add(this.destination_folder_TextBox);
			this.destination_flowLayoutPanel.Controls.Add(this.destination_folder_chooser_button);
			this.destination_flowLayoutPanel.Controls.Add(this.destination_get_info_button);
			this.destination_tabPage.Controls.Add(this.destination_flowLayoutPanel);
			this.destination_tabPage.Controls.Add(this.destination_folder_same_checkbox);
			this.destination_tabPage.Controls.Add(this.destination_song_info_dgv);
			this.destination_tabPage.Controls.Add(this.overwrite_stepfiles_button);
			this.destination_prev_next_flowLayoutPanel.Controls.Add(this.destination_back_button);
			this.destination_prev_next_flowLayoutPanel.Controls.Add(this.save_config_button);
			this.destination_prev_next_flowLayoutPanel.Controls.Add(this.close_button);
			this.destination_tabPage.Controls.Add(this.destination_prev_next_flowLayoutPanel);

			this.instructions_tabPage.Controls.Add(this.intro_textBox);
			this.instructions_tabPage.Controls.Add(this.intro_continue_button);
			this.tabControl1.Controls.Add(this.instructions_tabPage);
			this.Controls.Add(this.tabControl1);


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

			intro_textBox.Text = @"Stepper overwrites existing Stepmania .ssc (or .sm) stepfiles with automatically generated steps. 

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

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

namespace Stepper
{
    public partial class Stepper : Form
    {

        private List<Song> songs;
        private int beats_per_measure = 4;
        Random r;
        Instructions i;
        public Stepper()
        {
            InitializeComponent();
            songs = new List<Song>();
            r = new Random();
            i = new Instructions();
            i.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string default_folder = "C:\\Games\\StepMania 5\\Songs";
            if ((Directory.Exists(textBox1.Text)))
            {
                folderBrowserDialog1.SelectedPath = textBox1.Text;
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
                textBox1.Text = folderBrowserDialog1.SelectedPath;
             }
        }

        private void button2_Click(object sender, EventArgs e)
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

            string[] dirs = Directory.GetDirectories(@textBox1.Text);
            var fileCount = dirs.Count();
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].HeaderText = "Song Path";
            dataGridView1.Columns[1].HeaderText = "Min BPM";
            dataGridView1.Columns[2].HeaderText = "Max BPM";
            dataGridView1.Columns[3].HeaderText = "# BPM Changes";
            dataGridView1.Columns[4].HeaderText = "# Stops";
            dataGridView1.Columns[5].HeaderText = "# Note Sets";
            dataGridView1.Columns[6].HeaderText = "# Measures";
            dataGridView1.RowCount = fileCount;
            DataGridViewColumnCollection coll = dataGridView1.Columns;
            DataGridViewColumn c = coll[0];
            for (int i = 0; i < fileCount; i++) 
            {
                note_sets = 0;
                dataGridView1[0, i].Value = dirs[i];
                string[] files = Directory.GetFiles(dirs[i], "*.sm");
                if (files.Count() > 0)
                {
                    using (Stream fileStream = File.Open(files[0], FileMode.Open))
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

                            if (blank.Match(line).Success || comment.Match(line).Success) {
                                continue;
                            }

                            if (bpms.Match(line).Success)
                            {
                                string current = bpms.Replace(line, ""); // trim the line type indicator  #BPMS:
                                Regex r = new Regex(";");
                                current = r.Replace(current, ""); // trim the trailing semicolon
                                string[] bpms_array = current.Split(','); // split the list on comma
                                List<string> trimmed = new List<string>();
                                Regex leading_beat_count = new Regex("[0-9]+\\.[0-9]*=");
                                Regex floor = new Regex("\\.[0-9]+");
                                foreach (string b in bpms_array)
                                {
                                    string a = leading_beat_count.Replace(b, "");
                                    a = floor.Replace(a, "");
                                    trimmed.Add(a);
                                }
                                int num = trimmed.Count();
                                num--;

                                dataGridView1[1, i].Value = trimmed.Min();
                                dataGridView1[2, i].Value = trimmed.Max();
                                dataGridView1[3, i].Value = num;
                            }

                            if (stops.Match(line).Success)
                            {
                                string song_name = files[0];
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
                                int num = stops_array.Count();
                                if (complete_stops.Equals(""))
                                {
                                    num = 0;
                                }
                                dataGridView1[4, i].Value = num;
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
                     dataGridView1[5, i].Value = note_sets;
                     if (measures_list.Min() != measures_list.Max())
                    {
                        dataGridView1[6, i].Value = measures_list.Min() + "-" + measures_list.Max();
                    } else {
                        dataGridView1[6, i].Value = measures_list.Min();
                    }
                    dataGridView1.ClearSelection();
                    Song s = new Song(files[0], header_text, measures_list.Max());
                    songs.Add(s);
                }


            } // end for (int i = 0; i < fileCount; i++) 

        }

        private void button3_Click(object sender, EventArgs e)
        {
             songs.ForEach(delegate(Song s)
            {
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

                Noteset note1 = new Noteset(s.getNumMeasures(), level.Text, beats_per_measure,
                    alternate_foot.Checked, arrow_repeat.Checked, (int)stepFill.Value, (int)onBeat.Value, (int)jumps.Value, r,
                    (int)quintuples.Value, triples_on_1_and_3.Checked, quintuples_on_1_or_2.Checked);
                note1.generateSteps();
                note1.writeSteps(file);

                Noteset note2 = new Noteset(s.getNumMeasures(), level2.Text, beats_per_measure,
                    alternate_foot2.Checked, arrow_repeat2.Checked, (int)stepFill2.Value, (int)onBeat2.Value, (int)jumps2.Value, r,
                    (int)quintuples2.Value, triples_on_1_and_32.Checked, quintuples_on_1_or_22.Checked);
                note2.generateSteps();
                note2.writeSteps(file);

                Noteset note3 = new Noteset(s.getNumMeasures(), level3.Text, beats_per_measure,
                   alternate_foot3.Checked, arrow_repeat3.Checked, (int)stepFill3.Value, (int)onBeat3.Value, (int)jumps3.Value, r,
                   (int)quintuples3.Value, triples_on_1_and_33.Checked, quintuples_on_1_or_23.Checked);
                note3.generateSteps();
                note3.writeSteps(file);

                Noteset note4 = new Noteset(s.getNumMeasures(), level4.Text, beats_per_measure,
                    alternate_foot4.Checked, arrow_repeat4.Checked, (int)stepFill4.Value, (int)onBeat4.Value, (int)jumps4.Value, r,
                    (int)quintuples4.Value, triples_on_1_and_34.Checked, quintuples_on_1_or_24.Checked);
                note4.generateSteps();
                note4.writeSteps(file);

                Noteset note5 = new Noteset(s.getNumMeasures(), level5.Text, beats_per_measure,
                    alternate_foot5.Checked, arrow_repeat5.Checked, (int)stepFill5.Value, (int)onBeat5.Value, (int)jumps5.Value, r,
                    (int)quintuples5.Value, triples_on_1_and_35.Checked, quintuples_on_1_or_25.Checked);
                note5.generateSteps();
                note5.writeSteps(file);


                file.Close();
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            i.Show();
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

     }
}

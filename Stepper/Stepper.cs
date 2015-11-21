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
            comboBox1.SelectedIndex = 0;
            comboBox1.Refresh();
            comboBox2.SelectedIndex = 1;
            comboBox2.Refresh();
            comboBox3.SelectedIndex = 2;
            comboBox3.Refresh();
            comboBox4.SelectedIndex = 3;
            comboBox4.Refresh();
            comboBox5.SelectedIndex = 4;
            comboBox5.Refresh();
            r = new Random();
            i = new Instructions();
            i.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

                Noteset note1 = new Noteset(s.getNumMeasures(), (System.String)comboBox1.SelectedItem, beats_per_measure,
                    alternate_foot.Checked, arrow_repeat.Checked, (int)stepFill.Value, (int)jumps.Value, r, (int)triples.Value);
                note1.generateSteps();
                note1.writeSteps(file);

                Noteset note2 = new Noteset(s.getNumMeasures(), (System.String)comboBox2.SelectedItem, beats_per_measure,
                    alternate_foot2.Checked, arrow_repeat2.Checked, (int)stepFill2.Value, (int)jumps2.Value, r, (int)triples2.Value);
                note2.generateSteps();
                note2.writeSteps(file);

                Noteset note3 = new Noteset(s.getNumMeasures(), (System.String)comboBox3.SelectedItem, beats_per_measure,
                    alternate_foot3.Checked, arrow_repeat3.Checked, (int)stepFill3.Value, (int)jumps3.Value, r, (int)triples3.Value);
                note3.generateSteps();
                note3.writeSteps(file);

                Noteset note4 = new Noteset(s.getNumMeasures(), (System.String)comboBox4.SelectedItem, beats_per_measure,
                    alternate_foot4.Checked, arrow_repeat4.Checked, (int)stepFill4.Value, (int)jumps4.Value, r, (int)triples4.Value);
                note4.generateSteps();
                note4.writeSteps(file);

                Noteset note5 = new Noteset(s.getNumMeasures(), (System.String)comboBox5.SelectedItem, beats_per_measure,
                    alternate_foot5.Checked, arrow_repeat5.Checked, (int)stepFill5.Value, (int)jumps5.Value, r, (int)triples5.Value);
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
   
     }
}

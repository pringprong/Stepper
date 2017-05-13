using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepper
{
    class Noteset
    {
        string dance_style;
        string file_type;
        int num_measures;  // number of measures in this noteset
        string interface_level; // the level on the public interface: Novice, Easy, Medium, Hard, Expert
        string note_level; // the level inside the stepfile: Beginner, Easy Medium, Hard, Expert
        int difficulty; // difficulty number, which is just hardcoded here
        int beats_per_measure; // the number of beats, we assume 4 beats per measure
        int numBeats;
        bool alternate_foot; // whether the notes should alternate between left and right foot, not counting repeats and jumps
        bool repeat_arrow; // whether repeats of exactly the same note are allowed
        int stepfill; // the percentage of notes that are not "0000" (which is no arrow at all"
        int jumps; // the percentage of notes that are jumps
        int onBeat; // the percentage of notes that are on the beat only, with no half-beat
        Random r;
        Measure[] measures;
        int quintuples; // the percentage of half-beat steps that in a quintuple pattern rather than a triple
        bool triples_on_both_1_and_3;
        bool quintuples_either_on_1_or_2;
        char[] feet;
        string[] steps;
        string[] foot_laststep;

        public Noteset()
        {

        }

        public Noteset(string interface_style, string type, int measure, string interface_lvl, int beats_p_measure, bool alt_foot, bool repeat_arrows,
            int percent_stepfill, int percent_onbeat, int percent_jumps, Random random, int percent_quintuples,
            bool triples_on_1_and_3, bool quintuples_on_1_or_2)
        {
            dance_style = interface_style;
            file_type = type;
            num_measures = measure;
            interface_level = interface_lvl;
            beats_per_measure = beats_p_measure;
            numBeats = num_measures * beats_per_measure;
            alternate_foot = alt_foot;
            repeat_arrow = repeat_arrows;
            stepfill = percent_stepfill;
            jumps = percent_jumps;
            r = random;
            quintuples = percent_quintuples;
            onBeat = percent_onbeat;
            triples_on_both_1_and_3 = triples_on_1_and_3;
            quintuples_either_on_1_or_2 = quintuples_on_1_or_2;

            difficulty = 1;
            if (interface_level.Equals("Novice"))
            {
                note_level = "Beginner";
            }
            else
            {
                note_level = interface_level;
            }
            switch (note_level)
            {
                case ("Easy"):
                    difficulty = 4;
                    break;
                case ("Medium"):
                    difficulty = 6;
                    break;
                case ("Hard"):
                    difficulty = 8;
                    break;
                case ("Expert"):
                    difficulty = 10;
                    break;
            }
            measures = new Measure[num_measures];
            if (file_type == "SSC" && interface_level == "Expert")
            {
                note_level = "Challenge";
            }
            int numfeet = num_measures * numBeats * 2;
            feet = new char[numfeet];
            steps = new string[numfeet];

            if (dance_style == "dance-single")
            {
                foot_laststep = new string[] { "left", "0000" };
            }
            else if (dance_style == "dance-solo")
            {
                foot_laststep = new string[] { "left", "000000" };
            }
            else if (dance_style == "dance-double")
            {
                foot_laststep = new string[] { "left", "00000000" };
            }
            else if (dance_style == "pump-single")
            {
                foot_laststep = new string[] { "left", "00000" };
            }
        }

        public void writeSMSteps(System.IO.StreamWriter file)
        {
            file.WriteLine("#NOTES:");
            file.WriteLine("     " + dance_style + ":");
            file.WriteLine("     :");
            file.WriteLine("     " + note_level + ":");
            file.WriteLine("     " + difficulty + ":");
            file.WriteLine("     0.1,0.1,0.1,0.1,0.1:");
            for (int i = 0; i < num_measures; i++)
            {
                measures[i].writeSteps(file);
            }
            file.WriteLine(";");
        }

        public void writeSSCSteps(System.IO.StreamWriter file)
        {
            file.WriteLine("#NOTEDATA:;");
            file.WriteLine("#CHARTNAME:;");
            file.WriteLine("#STEPSTYPE:" + dance_style + ";");
            file.WriteLine("#DESCRIPTION:;");
            file.WriteLine("#CHARTSTYLE:;");
            file.WriteLine("#DIFFICULTY:" + note_level + ";");
            file.WriteLine("#METER:" + difficulty + ";");
            file.WriteLine("#RADARVALUES:0.1,0.1,0.1,0.1,0.1;");
            file.WriteLine("#CREDIT:Automatically generated by Stepper;");
            file.WriteLine("#NOTES:");
            for (int i = 0; i < num_measures; i++)
            {
                measures[i].writeSteps(file);
            }
            file.WriteLine(";");
        }

        public void generateSteps()
        {
            for (int i = 0; i < num_measures; i++)
            {
                Measure m = new Measure(dance_style, beats_per_measure, alternate_foot, repeat_arrow, stepfill, onBeat, jumps, r, quintuples,
                    triples_on_both_1_and_3, quintuples_either_on_1_or_2);
                foot_laststep = m.generateSteps(foot_laststep);
                measures[i] = m;
                char[] thisfoot = m.getFeet();
                for (int index = 0; index < 8; index++)
                {
                    feet[i * beats_per_measure * 2 + index] = thisfoot[index];
                }
                string[] thesesteps = m.getSteps();
                for (int index = 0; index < 8; index++)
                {
                    steps[i * beats_per_measure * 2 + index] = thesesteps[index];
                }
            }
        }

        public char[] getFeet()
        {
            return feet;
        }

        public string[] getSteps()
        {
            return steps;
        }
    }
}

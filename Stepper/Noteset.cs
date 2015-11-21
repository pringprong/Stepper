using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepper
{
    class Noteset
    {
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
        Random r;
        Measure[] measures;
        int triples;

        public Noteset()
        {

        }
        
        public Noteset(int measure, string interface_lvl, int beats_p_measure, bool alt_foot, bool repeat_arrows, 
            int percent_stepfill, int percent_jumps, Random random, int percent_triples) {
            num_measures = measure;
            interface_level = interface_lvl;
            beats_per_measure = beats_p_measure;
            numBeats = num_measures * beats_per_measure;
            alternate_foot = alt_foot;
            repeat_arrow = repeat_arrows;
            stepfill = percent_stepfill;
            jumps = percent_jumps;
            r = random;
            triples = percent_triples;

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
        }

        public void writeSteps(System.IO.StreamWriter file)
        {
            file.WriteLine("#NOTES:");
            file.WriteLine("     dance-single:");
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

        public void generateSteps()
        {
            string[] foot_laststep = new string[] { "left", "0000" };
            for (int i = 0; i < num_measures; i++)
            {
                Measure m = new Measure(beats_per_measure, alternate_foot, repeat_arrow, stepfill, jumps, r, triples);
                foot_laststep = m.generateSteps(foot_laststep);
                measures[i] = m;
            }
        }
    }
}

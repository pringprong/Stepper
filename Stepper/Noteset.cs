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
        string[] steps; // set of notes in the form "0010" 
        int beats_per_measure; // the number of beats, we assume 4 beats per measure
        int numBeats;
        bool alternate_foot; // whether the notes should alternate between left and right foot, not counting repeats and jumps
        bool repeat_arrow; // whether repeats of exactly the same note are allowed
        int stepfill; // the percentage of notes that are not "0000" (which is no arrow at all"
        int jumps; // the percentage of notes that are jumps
        Random r;

        public Noteset()
        {

        }
        
        public Noteset(int measure, string interface_lvl, int beats_p_measure, bool alt_foot, bool repeat_arrows, int percent_stepfill, int percent_jumps, Random random) {
            num_measures = measure;
            interface_level = interface_lvl;
            beats_per_measure = beats_p_measure;
            numBeats = num_measures * beats_per_measure;
            alternate_foot = alt_foot;
            repeat_arrow = repeat_arrows;
            stepfill = percent_stepfill;
            jumps = percent_jumps;
            r = random;

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
        }

        public void writeSteps(System.IO.StreamWriter file)
        {
            file.WriteLine("#NOTES:");
            file.WriteLine("     dance-single:");
            file.WriteLine("     :");
            file.WriteLine("     " + note_level + ":");
            file.WriteLine("     " + difficulty + ":");
            file.WriteLine("     0.1,0.1,0.1,0.1,0.1:");
            for (int i = 0; i < numBeats; i++)
            {
                file.WriteLine(steps[i]);
                if (((i + 1) % beats_per_measure) == 0)
                {
                    file.WriteLine(",");
                }
            }
            file.WriteLine(";");

        }

        public void generateSteps()
        {

            steps = new string[numBeats];
            string[] stepsfour = new string[] { "1000", "0100", "0010", "0001"};
            string[] jumpsteps = new string[] { "0011", "0110", "0101", "1100", "1001", "1010" };
            string[] leftsteps = new string[] { "1000", "0100", "0010"};
            string[] rightsteps = new string[] { "0100", "0010", "0001" };
            string laststep = "0000";
            string foot = "left";
            for (int i = 0; i < numBeats; i++)
            {
                int rInt = r.Next(0, 100);
                if (rInt < stepfill)
                {
                    if (!alternate_foot && repeat_arrow) // repeats allowed, no alternate foot constraint, so choose randomly
                    {
                        rInt = r.Next(0, 100);
                        if (rInt < jumps) // first decide if it's going to be a jump or not
                        {
                            steps[i] = jumpsteps[r.Next(0, 6)];
                        }
                        else
                        {
                            steps[i] = stepsfour[r.Next(0, 4)];
                        }
                    }
                    else if (!alternate_foot && !repeat_arrow) // repeats not allowed, so make sure the step isn't the same as laststep
                    {
                        rInt = r.Next(0, 100);
                        if (rInt < jumps)
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = jumpsteps[r.Next(0, 6)];
                            }
                            steps[i] = step;
                        }
                        else
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = stepsfour[r.Next(0, 4)];
                            }
                            steps[i] = step;
                        }
                    }
                    else if (alternate_foot && !repeat_arrow) // strict alternate foot, no repeats
                    {
                        rInt = r.Next(0, 100);
                        if (rInt < jumps)
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = jumpsteps[r.Next(0, 6)];
                            }
                            steps[i] = step;
                            // change feet for next
                            if (foot.Equals("left"))
                            {
                                foot = "right";
                            }
                            else {
                                foot = "left";
                            }
                        }
                        else
                        {
                            if (foot.Equals("left"))
                            {
                                string step = laststep;
                                while (step.Equals(laststep))
                                {
                                    step = rightsteps[r.Next(0, 3)];
                                }
                                steps[i] = step;
                                foot = "right";
                            }
                            else
                            {
                                string step = laststep;
                                while (step.Equals(laststep))
                                {
                                    step = leftsteps[r.Next(0, 3)];
                                }
                                steps[i] = step;
                                foot = "left";
                            }
                        }

                    }
                    else //if (alternate_foot && repeat_arrow)
                    {
                        rInt = r.Next(0, 100);
                        if (rInt < jumps)
                        {
                            steps[i] = jumpsteps[r.Next(0, 6)];
                            // change feet for next
                            if (foot.Equals("left"))
                            {
                                foot = "right";
                            }
                            else
                            {
                                foot = "left";
                            }
                        }
                        else
                        {
                            if (foot.Equals("left"))
                            {
                                string step = rightsteps[r.Next(0, 3)];
                                if (!step.Equals(laststep))
                                {
                                    foot = "right";
                                }
                                steps[i] = step;
                            }
                            else
                            {
                                string step = leftsteps[r.Next(0, 3)];
                                if (!step.Equals(laststep))
                                {
                                    foot = "left";
                                }
                                steps[i] = step;
                            }
                        }

                    }
                    laststep = steps[i];
                }
                else   // just put an empty step here
                {
                    steps[i] = "0000";
                }
            }
        }

       
    }
}

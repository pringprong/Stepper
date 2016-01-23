using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepper
{
    class Measure
    {
        string[] steps; // set of notes in the form "0010" 
        int arrows_per_measure; // the number of beats, we assume 4 beats per measure
        int beats_per_measure;
        bool alternate_foot; // whether the notes should alternate between left and right foot, not counting repeats and jumps
        bool repeat_arrow; // whether repeats of exactly the same note are allowed
        int stepfill; // the percentage of notes that are not "0000" (which is no arrow at all)
        int onBeat; // the percentage of notes that are only on the beat, with no half-beat 
        int jumps; // the percentage of notes that are jumps
        Random r;
        int quintuples; // the percentage of half-beat steps that in a quintuple pattern rather than a triple
        bool triples_on_both_1_and_3;
        bool quintuples_either_on_1_or_2; 

        public Measure()
        {

        }

         public Measure(int beats_p_measure, bool alt_foot, bool repeat_arrows, int percent_stepfill, int percent_onbeat,
            int percent_jumps, Random random, int percent_quintuples, bool triples_on_1_and_3, bool quintuples_on_1_or_2)
        {
            beats_per_measure = beats_p_measure;
            alternate_foot = alt_foot;
            repeat_arrow = repeat_arrows;
            stepfill = percent_stepfill;
            jumps = percent_jumps;
            r = random;
            quintuples = percent_quintuples;
            onBeat = percent_onbeat;
            triples_on_both_1_and_3 = triples_on_1_and_3;
            quintuples_either_on_1_or_2 = quintuples_on_1_or_2;
         }

        public void writeSteps(System.IO.StreamWriter file)
        {
            for (int i = 0; i < arrows_per_measure; i++)
            {
                file.WriteLine(steps[i]);
            }
            file.WriteLine(",");
        }

        public string[] generateSteps(string[] foot_laststep)
        {
            string foot = foot_laststep[0];
            string laststep = foot_laststep[1];
            string[] stepsfour = new string[] { "1000", "0100", "0010", "0001" };
            string[] jumpsteps = new string[] { "0011", "0110", "0101", "1100", "1001", "1010" };
            string[] leftsteps = new string[] { "1000", "0100", "0010" };
            string[] rightsteps = new string[] { "0100", "0010", "0001" };

            arrows_per_measure = beats_per_measure * 2;
            steps = new string[] { "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000" };

            for (int i = 0; i < arrows_per_measure; i++)
            {
                if (r.Next(0, 100) < stepfill) // decide if there will be an arrow here at all
                {
                    int rOnBeat = r.Next(0, 100); // random number to choose between on-beat and half-beat
                    int rTripQuint = r.Next(0, 100); // random number to choose between triples and quintuples
                    if (rOnBeat >= onBeat && rTripQuint < quintuples && ((i == 0) || (i == 2) && quintuples_either_on_1_or_2))
                    { // insert a quintuple
                        if (foot.Equals("left"))
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, 3)];
                            }
                            steps[i] = step;
                            steps[i + 2] = step;

                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, 3)];
                            }
                            steps[i + 1] = step;
                            steps[i + 3] = step;
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, 3)];
                            }
                            steps[i + 4] = step;
                            laststep = step;
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
                            steps[i + 2] = step;
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, 3)];
                            }
                            steps[i + 1] = step;
                            steps[i + 3] = step;
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, 3)];
                            }
                            steps[i + 4] = step;
                            laststep = step;
                            foot = "left";
                        }
                        i = i + 5;
                    }
                    else if (rOnBeat >= onBeat && rTripQuint >= quintuples && ((i <= 2) || (i == 4) && triples_on_both_1_and_3))
                    { // insert a triple
                        if (foot.Equals("left"))
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, 3)];
                            }
                            steps[i] = step;
                            steps[i + 2] = step;
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, 3)];
                            }
                            steps[i + 1] = step;
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
                            steps[i + 2] = step;
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, 3)];
                            }
                            steps[i + 1] = step;
                            foot = "left";
                        }
                        i = i + 3;
                    }
                    else
                    { // no half-beat arrows
                        if (!alternate_foot && repeat_arrow) // repeats allowed, no alternate foot constraint, so choose randomly
                        {
                            if (r.Next(0, 100) < jumps) // first decide if it's going to be a jump or not
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
                            if (r.Next(0, 100) < jumps)
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
                            if (r.Next(0, 100) < jumps)
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
                                else
                                {
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
                            if (r.Next(0, 100) < jumps)
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
                        i++; // skip the half-beat arrow, which is prefilled with "0000"
                    } // end else: no half-beat arrow 
                }  // end if (rInt < stepfill)
                else
                {
                    i++; // no arrow so skip the half-beat arrow, which is prefilled with "0000"
                }
            } // end for loop
            string[] finish_foot_laststep = new string[2];
            finish_foot_laststep[0] = foot;
            finish_foot_laststep[1] = laststep;
            return finish_foot_laststep;
        } // end method string generateSteps(string)
    }
}

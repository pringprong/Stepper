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
        char[] feet; // set of letters 'L', 'R', 'E' (either), and 'J' (jumps) representing which feet are stepping on the arrows

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

         public char[] getFeet()
         {
             return feet;
         }

         public string[] getSteps()
         {
             return steps;
         }

        public void writeSteps(System.IO.StreamWriter file)
        {
            for (int i = 0; i < arrows_per_measure; i++)
            {
                file.WriteLine(steps[i]);
            }
            file.WriteLine(",");
        }

        public string[] generateDanceSingleSteps(string[] foot_laststep)
        {
            string foot = foot_laststep[0];
            string laststep = foot_laststep[1];
            arrows_per_measure = beats_per_measure * 2;
            steps = new string[] { "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000" };
            feet = new char[arrows_per_measure];
            for (int i = 0; i < arrows_per_measure; i++)
            {
                // set initial step options
                string[] singlesteps = new string[] { "1000", "0100", "0010", "0001" };
                string[] jumpsteps = new string[] { "0011", "0110", "0101", "1100", "1001", "1010" };
                string[] leftsteps = new string[] { "1000", "0100", "0010" };
                string[] rightsteps = new string[] { "0100", "0010", "0001" };
      
                int rStepFill = r.Next(0, 100);
                if ((((i == 0) || (i == 4)) && (stepfill >= 50)) // for stepfill > 50, the 1st and 3rd beats always have arrows
                    || ((stepfill >= 50) && (rStepFill < ((stepfill - 50) * 2))) // for stepfill > 50, the 2nd and 4th beats are randomly chosen
                    || (((i == 0) || (i == 4)) && (rStepFill < (stepfill * 2))) // for stepfill < 50, the 2nd and 4th beats are always empty,  1st and 3rd beat are randomly chosen
                    ) // decide if there will be an arrow here at all
                {
                    int rOnBeat = r.Next(0, 100); // random number to choose between on-beat and half-beat
                    int rTripQuint = r.Next(0, 100); // random number to choose between triples and quintuples
                    if (rOnBeat >= onBeat && rTripQuint < quintuples && ((i == 0) || (i == 2) && quintuples_either_on_1_or_2))
                    { // insert a quintuple
                        bool fromJump = jumpsteps.Contains(laststep);
                        if (fromJump)
                        {
                            if (laststep == "0011") // top and right jump
                            {
                                rightsteps = new string[] { "0100", "0001" }; // don't try to put the right foot onto the top arrow, because the left foot is on it right now
                            }
                            else if (laststep == "0101") // bottom and right jump
                            {
                                rightsteps = new string[] { "0010", "0001" }; // don't try to put the right foot onto the bottom arrow, because the left foot is on it right now
                            }
                            else if (laststep == "1010") // top and left jump
                            {
                                leftsteps = new string[] { "1000", "0100" }; // don't try to put the left foot onto the top arrow, because the right foot is on it right now
                            }
                            else if (laststep == "1100") // bottom and left jump
                            {
                                leftsteps = new string[] { "1000", "0010" }; // don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
                            }
                        }
                        if (foot.Equals("left"))
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i] = step;
                            feet[i] = 'R';
                            steps[i + 2] = step;
                            feet[i + 2] = 'R';

                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i + 1] = step;
                            feet[i+1] = 'L';
                            steps[i + 3] = step;
                            feet[i+3] = 'L';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i + 4] = step;
                            feet[i+4] = 'R';

                            // prevent up-down-up-down-side type quintuples right after jumps, because it's too likely to start them on the wrong foot
                            if (fromJump && (
                                ((steps[i] == "0100") && (steps[i + 1] == "0010")) || 
                                ((steps[i] == "0010") && (steps[i + 1] == "0100")) ))
                            {
                                steps[i + 4] = steps[i+2];
                            }
                            laststep = steps[i + 4];
                            foot = "right";
                        }
                        else
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i] = step;
                            feet[i] = 'L';
                            steps[i + 2] = step;
                            feet[i+2] = 'L';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i + 1] = step;
                            feet[i+1] = 'R';
                            steps[i + 3] = step;
                            feet[i+3] = 'R';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i + 4] = step;
                            feet[i+4] = 'L';
                            // prevent up-down-up-down-side type quintuples right after jumps, because it's too likely to start them on the wrong foot
                            if (fromJump && (
                                ((steps[i] == "0100") && (steps[i + 1] == "0010")) || 
                                ((steps[i] == "0010") && (steps[i + 1] == "0100")) ))
                            {
                                steps[i + 4] = steps[i + 2];
                            }
                            laststep = steps[i + 4];
                            foot = "left";
                        }
                        i = i + 5;
                    }
                    else if (rOnBeat >= onBeat && rTripQuint >= quintuples && ((i <= 2) || (i == 4) && triples_on_both_1_and_3))
                    { // insert a triple
                        if (laststep == "0011") // top and right jump
                        {
                            rightsteps = new string[] { "0100", "0001" }; // don't try to put the right foot onto the top arrow, because the left foot is on it right now
                        }
                        else if (laststep == "0101") // bottom and right jump
                        {
                            rightsteps = new string[] { "0010", "0001" }; // don't try to put the right foot onto the bottom arrow, because the left foot is on it right now
                        }
                        else if (laststep == "1010") // top and left jump
                        {
                            leftsteps = new string[] { "1000", "0100" }; // don't try to put the left foot onto the top arrow, because the right foot is on it right now
                        }
                        else if (laststep == "1100") // bottom and left jump
                        {
                            leftsteps = new string[] { "1000", "0010" }; // don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
                        }
                        if (foot.Equals("left"))
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i] = step;
                            feet[i] = 'R';
                            steps[i + 2] = step;
                            feet[i + 2] = 'R';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i + 1] = step;
                            feet[i+1] = 'L';
                            foot = "right";
                        }
                        else
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i] = step;
                            feet[i] = 'L';
                            steps[i + 2] = step;
                            feet[i+2] = 'L';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i + 1] = step;
                            feet[i + 1] = 'R';
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
                                steps[i] = jumpsteps[r.Next(0, jumpsteps.Count())];
                                feet[i] = 'J';
                            }
                            else
                            {
                                steps[i] = singlesteps[r.Next(0, singlesteps.Count())];
                                feet[i] = 'E';
                            }
                            // change feet for next (this will impact future triples and quintuples)
                            if (foot.Equals("left"))
                            {
                                foot = "right";
                            }
                            else
                            {
                                foot = "left";
                            }
                        }
                        else if (!alternate_foot && !repeat_arrow) // repeats not allowed, so make sure the step isn't the same as laststep
                        {
                            if (r.Next(0, 100) < jumps)
                            {
                                string step = laststep;
                                while (step.Equals(laststep))
                                {
                                    step = jumpsteps[r.Next(0, jumpsteps.Count())];
                                }
                                steps[i] = step;
                                feet[i] = 'J';
                            }
                            else
                            {
                                string step = laststep;
                                while (step.Equals(laststep))
                                {
                                    step = singlesteps[r.Next(0, singlesteps.Count())];
                                }
                                steps[i] = step;
                                feet[i] = 'E';
                            }
                            // change feet for next (this will impact future triples and quintuples)
                            if (foot.Equals("left"))
                            {
                                foot = "right";
                            }
                            else
                            {
                                foot = "left";
                            }
                        }
                        else if (alternate_foot && !repeat_arrow) // strict alternate foot, no repeats
                        {
                            if (r.Next(0, 100) < jumps)
                            {
                                string step = laststep;
                                while (step.Equals(laststep))
                                {
                                    step = jumpsteps[r.Next(0, jumpsteps.Count())];
                                }
                                steps[i] = step;
                                feet[i] = 'J';
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
                                        step = rightsteps[r.Next(0, rightsteps.Count())];
                                    }
                                    steps[i] = step;
                                    feet[i] = 'R';
                                    foot = "right";
                                }
                                else
                                {
                                    string step = laststep;
                                    while (step.Equals(laststep))
                                    {
                                        step = leftsteps[r.Next(0, leftsteps.Count())];
                                    }
                                    steps[i] = step;
                                    feet[i] = 'L';
                                    foot = "left";
                                }
                            }
                        }
                        else //if (alternate_foot && repeat_arrow)
                        {
                            if (r.Next(0, 100) < jumps) // insert a jump
                            {
                                steps[i] = jumpsteps[r.Next(0, jumpsteps.Count())];
                                feet[i] = 'J';

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
                            else // insert a single-foot step
                            {
                                if (foot.Equals("left")) // insert a right foot step or a repeat if the previous step was a single-foot step
                                {
                                    string step;
                                    if (singlesteps.Contains(laststep) && !rightsteps.Contains(laststep))
                                    {
                                        // the previous step was a single step, so add it to the list of options
                                        string[] with_repeat = new string[rightsteps.Count() + 1];
                                        for (int j = 0; j < rightsteps.Count(); j++)
                                        {
                                            with_repeat[j] = rightsteps[j];
                                        }
                                        with_repeat[rightsteps.Count()] = laststep;
                                        step = with_repeat[r.Next(0, with_repeat.Count())];
                                    }
                                    else
                                    {
                                        step = rightsteps[r.Next(0, rightsteps.Count())];
                                    }
                                    if (!step.Equals(laststep))
                                    {
                                        foot = "right";
                                        feet[i] = 'R';
                                    }
                                    else
                                    {
                                        feet[i] = 'L';
                                    }
                                    steps[i] = step;
                                }
                                else // foot equals right, so insert a left-foot step or a repeat if the previous step was a single-foot step
                                {
                                    string step;
                                    if (singlesteps.Contains(laststep) && !leftsteps.Contains(laststep))
                                    {
                                        // the previous step was a single step, so add it to the list of options
                                        string[] with_repeat = new string[leftsteps.Count() + 1];
                                        for (int j = 0; j < leftsteps.Count(); j++)
                                        {
                                            with_repeat[j] = leftsteps[j];
                                        }
                                        with_repeat[leftsteps.Count()] = laststep;
                                        step = with_repeat[r.Next(0, with_repeat.Count())];
                                    }
                                    else
                                    {
                                        step = leftsteps[r.Next(0, leftsteps.Count())];
                                    }
                                    if (!step.Equals(laststep)) 
                                    {
                                        foot = "left";
                                        feet[i] = 'L';
                                    }
                                    else
                                    {
                                        feet[i] = 'R';
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
        } // end method string generateDanceSingleSteps(string[])

        public string[] generateDanceSoloSteps(string[] foot_laststep)
        {
            string foot = foot_laststep[0];
            string laststep = foot_laststep[1];
            arrows_per_measure = beats_per_measure * 2;
            steps = new string[] { "000000", "000000", "000000", "000000", "000000", "000000", "000000", "000000" };
            feet = new char[arrows_per_measure];

            for (int i = 0; i < arrows_per_measure; i++)
            {
                // set initial step options
                string[] singlesteps = new string[] { "100000", "010000", "001000", "000100", "000010", "000001" };
                string[] jumpsteps = new string[] { "110000", // left and topleft  c
                                                    "101000", // left and bottom    c
                                                    "100100", // left and top     c
                                                    "100010", // left and topright  c
                                                    "100001", // left and right    c
                                                    "011000", // topleft and bottom  c
                                                    "010100", // topleft and top  c
                                                    "010010", // topleft and topright  c
                                                    "010001", // topleft and right   c
                                                    "001100", // bottom and top  c
                                                    "001010", // bottom and topright c
                                                    "001001", // bottom and right c
                                                    "000110", // top and topright
                                                    "000101", // top and right
                                                    "000011" // topright and right
                                                   };
                string[] leftsteps = new string[] { "100000", "010000", "001000", "000100" };
                string[] rightsteps = new string[] { "001000", "000100", "000010", "000001" };
                bool fromJump = jumpsteps.Contains(laststep);
                if (fromJump)
                {
                    if (laststep == "101000")// left and bottom
                    {
                        leftsteps = new string[] { "100000", "010000", "000100" };
                        // don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
                    }
                    else if (laststep == "100100")  // left and top
                    {
                        leftsteps = new string[] { "100000", "010000", "001000" };
                        // don't try to put the left foot onto the top arrow, because the right foot is on it right now
                    }
                    else if (laststep == "011000") // topleft and bottom
                    {
                        leftsteps = new string[] { "100000", "010000", "000100" };
                        // don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
                    }
                    else if (laststep == "010100")  // topleft and top
                    {
                        leftsteps = new string[] { "100000", "010000", "001000" };
                        // don't try to put the left foot onto the top arrow, because the right foot is on it right now
                    }
                    else if (laststep == "001010") // bottom and topright
                    {
                        rightsteps = new string[] { "000100", "000010", "000001" };
                        // don't try to put the right foot onto the bottom arrow, because the left foot is on it right now
                    }
                    else if (laststep == "001001") // bottom and right
                    {
                        rightsteps = new string[] { "000100", "000010", "000001" };
                        // don't try to put the right foot onto the bottom arrow, because the left foot is on it right now
                    }
                    else if (laststep == "000110") // top and topright
                    {
                        rightsteps = new string[] { "001000", "000010", "000001" };
                        // don't try to put the right foot onto the top arrow, because the left foot is on it right now
                    }
                    else if (laststep == "000101") // top and right
                    {
                        rightsteps = new string[] { "001000", "000010", "000001" };
                        // don't try to put the right foot onto the top arrow, because the left foot is on it right now
                    }
                }

                int rStepFill = r.Next(0, 100);
                if ((((i == 0) || (i == 4)) && (stepfill >= 50)) // for stepfill > 50, the 1st and 3rd beats always have arrows
                    || ((stepfill >= 50) && (rStepFill < ((stepfill - 50) * 2))) // for stepfill > 50, the 2nd and 4th beats are randomly chosen
                    || (((i == 0) || (i == 4)) && (rStepFill < (stepfill * 2))) // for stepfill < 50, the 2nd and 4th beats are always empty,  1st and 3rd beat are randomly chosen
                    ) // decide if there will be an arrow here at all
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
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i] = step;
                            feet[i] = 'R';
                            steps[i + 2] = step;
                            feet[i + 2] = 'R';

                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i + 1] = step;
                            feet[i+1] = 'L';
                            steps[i + 3] = step;
                            feet[i+3] = 'L';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i + 4] = step;
                            feet[i+4] = 'R';

                            // prevent up-down-up-down-side type quintuples right after jumps, because it's too likely to start them on the wrong foot
                            if (fromJump && (
                                ((steps[i] == "001000") && (steps[i + 1] == "000100")) || 
                                ((steps[i] == "000100") && (steps[i + 1] == "001000")) ))
                            {
                                steps[i + 4] = steps[i+2];
                            }
                            laststep = steps[i + 4];
                            foot = "right";
                        }
                        else
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i] = step;
                            feet[i] = 'L';
                            steps[i + 2] = step;
                            feet[i+2] = 'L';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i + 1] = step;
                            feet[i+1] = 'R';
                            steps[i + 3] = step;
                            feet[i+3] = 'R';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i + 4] = step;
                            feet[i+4] = 'L';
                            // prevent up-down-up-down-side type quintuples right after jumps, because it's too likely to start them on the wrong foot
                            if (fromJump && (
                                ((steps[i] == "001000") && (steps[i + 1] == "000100")) ||
                                ((steps[i] == "000100") && (steps[i + 1] == "001000"))))
                            {
                                steps[i + 4] = steps[i + 2];
                            }
                            laststep = steps[i + 4];
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
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i] = step;
                            feet[i] = 'R';
                            steps[i + 2] = step;
                            feet[i + 2] = 'R';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i + 1] = step;
                            feet[i+1] = 'L';
                            foot = "right";
                        }
                        else
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i] = step;
                            feet[i] = 'L';
                            steps[i + 2] = step;
                            feet[i+2] = 'L';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i + 1] = step;
                            feet[i + 1] = 'R';
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
                                steps[i] = jumpsteps[r.Next(0, jumpsteps.Count())];
                                feet[i] = 'J';
                            }
                            else
                            {
                                steps[i] = singlesteps[r.Next(0, singlesteps.Count())];
                                feet[i] = 'E';
                            }
                            // change feet for next (this will impact future triples and quintuples)
                            if (foot.Equals("left"))
                            {
                                foot = "right";
                            }
                            else
                            {
                                foot = "left";
                            }
                        }
                        else if (!alternate_foot && !repeat_arrow) // repeats not allowed, so make sure the step isn't the same as laststep
                        {
                            if (r.Next(0, 100) < jumps)
                            {
                                string step = laststep;
                                while (step.Equals(laststep))
                                {
                                    step = jumpsteps[r.Next(0, jumpsteps.Count())];
                                }
                                steps[i] = step;
                                feet[i] = 'J';
                            }
                            else
                            {
                                string step = laststep;
                                while (step.Equals(laststep))
                                {
                                    step = singlesteps[r.Next(0, singlesteps.Count())];
                                }
                                steps[i] = step;
                                feet[i] = 'E';
                            }
                            // change feet for next (this will impact future triples and quintuples)
                            if (foot.Equals("left"))
                            {
                                foot = "right";
                            }
                            else
                            {
                                foot = "left";
                            }
                        }
                        else if (alternate_foot && !repeat_arrow) // strict alternate foot, no repeats
                        {
                            if (r.Next(0, 100) < jumps)
                            {
                                string step = laststep;
                                while (step.Equals(laststep))
                                {
                                    step = jumpsteps[r.Next(0, jumpsteps.Count())];
                                }
                                steps[i] = step;
                                feet[i] = 'J';
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
                                        step = rightsteps[r.Next(0, rightsteps.Count())];
                                    }
                                    steps[i] = step;
                                    feet[i] = 'R';
                                    foot = "right";
                                }
                                else
                                {
                                    string step = laststep;
                                    while (step.Equals(laststep))
                                    {
                                        step = leftsteps[r.Next(0, leftsteps.Count())];
                                    }
                                    steps[i] = step;
                                    feet[i] = 'L';
                                    foot = "left";
                                }
                            }
                        }
                        else //if (alternate_foot && repeat_arrow)
                        {
                            if (r.Next(0, 100) < jumps) // insert a jump
                            {
                                steps[i] = jumpsteps[r.Next(0, jumpsteps.Count())];
                                feet[i] = 'J';

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
                            else // insert a single-foot step
                            {
                                if (foot.Equals("left")) // insert a right foot step or a repeat if the previous step was a single-foot step
                                {
                                    string step;
                                    if (singlesteps.Contains(laststep) && !rightsteps.Contains(laststep))
                                    {
                                        // the previous step was a single step, so add it to the list of options
                                        string[] with_repeat = new string[rightsteps.Count() + 1];
                                        for (int j = 0; j < rightsteps.Count(); j++)
                                        {
                                            with_repeat[j] = rightsteps[j];
                                        }
                                        with_repeat[rightsteps.Count()] = laststep;
                                        step = with_repeat[r.Next(0, with_repeat.Count())];
                                    }
                                    else
                                    {
                                        step = rightsteps[r.Next(0, rightsteps.Count())];
                                    }
                                    if (!step.Equals(laststep))
                                    {
                                        foot = "right";
                                        feet[i] = 'R';
                                    }
                                    else
                                    {
                                        feet[i] = 'L';
                                    }
                                    steps[i] = step;
                                }
                                else // foot equals right, so insert a left-foot step or a repeat if the previous step was a single-foot step
                                {
                                    string step;
                                    if (singlesteps.Contains(laststep) && !leftsteps.Contains(laststep))
                                    {
                                        // the previous step was a single step, so add it to the list of options
                                        string[] with_repeat = new string[leftsteps.Count() + 1];
                                        for (int j = 0; j < leftsteps.Count(); j++)
                                        {
                                            with_repeat[j] = leftsteps[j];
                                        }
                                        with_repeat[leftsteps.Count()] = laststep;
                                        step = with_repeat[r.Next(0, with_repeat.Count())];
                                    }
                                    else
                                    {
                                        step = leftsteps[r.Next(0, leftsteps.Count())];
                                    }
                                    if (!step.Equals(laststep)) 
                                    {
                                        foot = "left";
                                        feet[i] = 'L';
                                    }
                                    else
                                    {
                                        feet[i] = 'R';
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
        }// end method string generateDanceSoloSteps(string[])

        public string[] generateDanceDoubleSteps(string[] foot_laststep)
        {
            string foot = foot_laststep[0];
            string laststep = foot_laststep[1];
            arrows_per_measure = beats_per_measure * 2;
            steps = new string[] { "00000000", "00000000", "00000000", "00000000", "00000000", "00000000", "00000000", "00000000" };
            feet = new char[arrows_per_measure];

            for (int i = 0; i < arrows_per_measure; i++)
            {
                // set initial step options
                string[] singlesteps = new string[] { "10000000", "01000000", "00100000", "00010000", "00001000", "00000100", "00000010", "00000001" };
                int rStepFill = r.Next(0, 100);
                if ((((i == 0) || (i == 4)) && (stepfill >= 50)) // for stepfill > 50, the 1st and 3rd beats always have arrows
                    || ((stepfill >= 50) && (rStepFill < ((stepfill - 50) * 2))) // for stepfill > 50, the 2nd and 4th beats are randomly chosen
                    || (((i == 0) || (i == 4)) && (rStepFill < (stepfill * 2))) // for stepfill < 50, the 2nd and 4th beats are always empty,  1st and 3rd beat are randomly chosen
                    ) // decide if there will be an arrow here at all
                {
                    laststep = singlesteps[r.Next(0, singlesteps.Count())];
                    steps[i] = laststep;
                    feet[i] = 'L';
                    i++;
                } // end if
                else
                {
                    i++;
                    i++;// no arrow so skip the half-beat arrow, which is prefilled with "0000"
                }

            } // end for loop
            string[] finish_foot_laststep = new string[2];
            finish_foot_laststep[0] = foot;
            finish_foot_laststep[1] = laststep;
            return finish_foot_laststep;
        }// end method string generateDanceDoubleSteps(string[])

        public string[] generatePumpSingleSteps(string[] foot_laststep)
        {
            string foot = foot_laststep[0];
            string laststep = foot_laststep[1];
            arrows_per_measure = beats_per_measure * 2;
            steps = new string[] { "00000", "00000", "00000", "00000", "00000", "00000", "00000", "00000" };
            feet = new char[arrows_per_measure];

            for (int i = 0; i < arrows_per_measure; i++)
            {
                string[] singlesteps = new string[] { "10000", "01000", "00100", "00010", "00001" };
                string[] jumpsteps = new string[] { "00011", "00110", "00101", "01100", "01001", "01010", "10001", "10010", "10100", "11000" };
                string[] leftsteps = new string[] { "10000", "01000", "00100", "00010", "00001" };
                string[] rightsteps = new string[] { "10000", "01000", "00100", "00010", "00001" };
                
                int rStepFill = r.Next(0, 100);
                if ((((i == 0) || (i == 4)) && (stepfill >= 50)) // for stepfill > 50, the 1st and 3rd beats always have arrows
                    || ((stepfill >= 50) && (rStepFill < ((stepfill - 50) * 2))) // for stepfill > 50, the 2nd and 4th beats are randomly chosen
                    || (((i == 0) || (i == 4)) && (rStepFill < (stepfill * 2))) // for stepfill < 50, the 2nd and 4th beats are always empty,  1st and 3rd beat are randomly chosen
                    ) // decide if there will be an arrow here at all
                {
                    int rOnBeat = r.Next(0, 100); // random number to choose between on-beat and half-beat
                    int rTripQuint = r.Next(0, 100); // random number to choose between triples and quintuples
                    if (rOnBeat >= onBeat && rTripQuint < quintuples && ((i == 0) || (i == 2) && quintuples_either_on_1_or_2))
                    { // insert a quintuple
                        bool fromJump = ((laststep == "00011") // both right; footing unknown
                            || (laststep == "00110") // center and top right
                            || (laststep == "00101") // center and bottom right
                            || (laststep == "01100") // top left and center
                            || (laststep == "01001") // top left and bottom right
                            || (laststep == "01010") // both top
                            || (laststep == "10001") // both bottom
                            || (laststep == "10010") // bottom left and top right
                            || (laststep == "10100") // bottom left and center
                            || (laststep == "11000") // both left; footing unknown
                            );
                        if (fromJump)
                        {
                            if (laststep == "01010") // both top arrows
                            {
                                leftsteps = new string[] { "10000", "01000", "00100", "00001" }; // left foot can't go on top right
                                rightsteps = new string[] { "10000", "00100", "00010", "00001" }; // right foot can't go on top left
                            }
                            else if (laststep == "10001") // both bottom arrows
                            {
                                leftsteps = new string[] { "10000", "01000", "00100", "00010" }; // left foot can't go on bottom right
                                rightsteps = new string[] { "01000", "00100", "00010", "00001" }; // right foot can't go on bottom left
                            }
                            else if (laststep == "01100") // top left and center
                            {
                                rightsteps = new string[] { "01000", "00100", "00010", "00001" }; // right foot can't go on top left
                            }
                            else if (laststep == "10100") // bottom left and center
                            {
                                rightsteps = new string[] { "10000", "00100", "00010", "00001" }; // right foot can't go on bottom left
                            }
                            else if (laststep == "00110") // center and top right
                            {
                                leftsteps = new string[] { "10000", "01000", "00100", "00001" }; // left foot can't go on top right
                            }
                            else if (laststep == "00101") // center and bottom right
                            {
                                leftsteps = new string[] { "10000", "01000", "00100", "00010" }; // left foot can't go on bottom right
                            }
                        }
                        if (foot.Equals("left"))
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i] = step;
                            steps[i + 2] = step;
                            feet[i] = 'R';
                            feet[i + 2] = 'R';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i + 1] = step;
                            steps[i + 3] = step;
                            feet[i + 1] = 'L';
                            feet[i + 3] = 'L';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i + 4] = step;
                            feet[i + 4] = 'R';
                            // prevent up-down-up-down-side type quintuples right after jumps, because it's too likely to start them on the wrong foot
                            if (fromJump && (
                                ((steps[i] == "00001") && (steps[i + 1] == "00010")) ||
                                ((steps[i] == "00010") && (steps[i + 1] == "00001")) ||
                                ((steps[i] == "10000") && (steps[i + 1] == "01000")) ||
                                ((steps[i] == "01000") && (steps[i + 1] == "10000"))
                                ))
                            {
                                steps[i + 4] = steps[i + 2];
                            } 
                            laststep = steps[i + 4];
                            foot = "right";
                        }
                        else
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i] = step;
                            steps[i + 2] = step;
                            feet[i] = 'L';
                            feet[i + 2] = 'L';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i + 1] = step;
                            steps[i + 3] = step;
                            feet[i + 1] = 'R';
                            feet[i + 3] = 'R';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i + 4] = step;
                            feet[i + 4] = 'L';
                            // prevent up-down-up-down-side type quintuples right after jumps, because it's too likely to start them on the wrong foot
                            if (fromJump && (
                                ((steps[i] == "00001") && (steps[i + 1] == "00010")) ||
                                ((steps[i] == "00010") && (steps[i + 1] == "00001")) ||
                                ((steps[i] == "10000") && (steps[i + 1] == "01000")) ||
                                ((steps[i] == "01000") && (steps[i + 1] == "10000"))
                                ))
                            {
                                steps[i + 4] = steps[i + 2];
                            }
                            laststep = steps[i + 4];
                            foot = "left";
                        }
                        i = i + 5;
                    }
                    else if (rOnBeat >= onBeat && rTripQuint >= quintuples && ((i <= 2) || (i == 4) && triples_on_both_1_and_3))
                    { // insert a triple
                        bool fromJump = ((laststep == "00011") // both right; footing unknown
                            || (laststep == "00110") // center and top right
                            || (laststep == "00101") // center and bottom right
                            || (laststep == "01100") // top left and center
                            || (laststep == "01001") // top left and bottom right
                            || (laststep == "01010") // both top
                            || (laststep == "10001") // both bottom
                            || (laststep == "10010") // bottom left and top right
                            || (laststep == "10100") // bottom left and center
                            || (laststep == "11000") // both left; footing unknown
                            );
                       if (fromJump)
                        {
                            if (laststep == "01010") // both top arrows
                            {
                                leftsteps = new string[] { "10000", "01000", "00100", "00001" }; // left foot can't go on top right
                                rightsteps = new string[] { "10000", "00100", "00010", "00001" }; // right foot can't go on top left
                            }
                            else if (laststep == "10001") // both bottom arrows
                            {
                                leftsteps = new string[] { "10000", "01000", "00100", "00010" }; // left foot can't go on bottom right
                                rightsteps = new string[] { "01000", "00100", "00010", "00001" }; // right foot can't go on bottom left
                            }
                            else if (laststep == "01100") // top left and center
                            {
                                rightsteps = new string[] { "01000", "00100", "00010", "00001" }; // right foot can't go on top left
                            }
                            else if (laststep == "10100") // bottom left and center
                            {
                                rightsteps = new string[] { "10000", "00100", "00010", "00001" }; // right foot can't go on bottom left
                            }
                            else if (laststep == "00110") // center and top right
                            {
                                leftsteps = new string[] { "10000", "01000", "00100", "00001" }; // left foot can't go on top right
                            }
                            else if (laststep == "00101") // center and bottom right
                            {
                                leftsteps = new string[] { "10000", "01000", "00100", "00010" }; // left foot can't go on bottom right
                            }
                        }
                        if (foot.Equals("left"))
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i] = step;
                            steps[i + 2] = step;
                            feet[i] = 'R';
                            feet[i + 2] = 'R';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i + 1] = step;
                            feet[i + 1] = 'L';
                            foot = "right";
                        }
                        else
                        {
                            string step = laststep;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i] = step;
                            steps[i + 2] = step;
                            feet[i] = 'L';
                            feet[i + 2] = 'L';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i + 1] = step;
                            feet[i + 1] = 'R';
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
                                steps[i] = jumpsteps[r.Next(0, jumpsteps.Count())];
                            }
                            else
                            {
                                steps[i] = singlesteps[r.Next(0, singlesteps.Count())];
                            }
                            feet[i] = 'E';
                            // change feet for next (this will impact future triples and quintuples)
                            if (foot.Equals("left"))
                            {
                                foot = "right";
                            }
                            else
                            {
                                foot = "left";
                            }
                        }
                        else if (!alternate_foot && !repeat_arrow) // repeats not allowed, so make sure the step isn't the same as laststep
                        {
                            if (r.Next(0, 100) < jumps)
                            {
                                string step = laststep;
                                while (step.Equals(laststep))
                                {
                                    step = jumpsteps[r.Next(0, jumpsteps.Count())];
                                }
                                steps[i] = step;
                                feet[i] = 'B';
                            }
                            else
                            {
                                string step = laststep;
                                while (step.Equals(laststep))
                                {
                                    step = singlesteps[r.Next(0, singlesteps.Count())];
                                }
                                steps[i] = step;
                                feet[i] = 'E';
                            }
                            // change feet for next (this will impact future triples and quintuples)
                            if (foot.Equals("left"))
                            {
                                foot = "right";
                            }
                            else
                            {
                                foot = "left";
                            }
                        }
                        else if (alternate_foot && !repeat_arrow) // strict alternate foot, no repeats
                        {
                            if (r.Next(0, 100) < jumps)
                            {
                                string step = laststep;
                                while (step.Equals(laststep))
                                {
                                    step = jumpsteps[r.Next(0, jumpsteps.Count())];
                                }
                                steps[i] = step;
                                feet[i] = 'B';
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
                                        step = rightsteps[r.Next(0, rightsteps.Count())];
                                    }
                                    steps[i] = step;
                                    feet[i] = 'R';
                                    foot = "right";
                                }
                                else
                                {
                                    string step = laststep;
                                    while (step.Equals(laststep))
                                    {
                                        step = leftsteps[r.Next(0, leftsteps.Count())];
                                    }
                                    steps[i] = step;
                                    feet[i] = 'L';
                                    foot = "left";
                                }
                            }
                        }
                        else //if (alternate_foot && repeat_arrow)
                        {
                            if (r.Next(0, 100) < jumps) // insert a jump
                            {
                                steps[i] = jumpsteps[r.Next(0, jumpsteps.Count())];
                                feet[i] = 'B';
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
                            else // insert a single-foot step
                            {
                                if (foot.Equals("left")) // insert a right foot step or a repeat if the previous step was a single-foot step
                                {
                                    string step;
                                    if (singlesteps.Contains(laststep) && !rightsteps.Contains(laststep))
                                    {
                                        // the previous step was a single step, so add it to the list of options
                                        string[] with_repeat = new string[rightsteps.Count() + 1];
                                        for (int j = 0; j < rightsteps.Count(); j++)
                                        {
                                            with_repeat[j] = rightsteps[j];
                                        }
                                        with_repeat[rightsteps.Count()] = laststep;
                                        step = with_repeat[r.Next(0, with_repeat.Count())];
                                    }
                                    else
                                    {
                                        step = rightsteps[r.Next(0, rightsteps.Count())];
                                    }
                                    if (!step.Equals(laststep))
                                    {
                                        foot = "right";
                                        feet[i] = 'L';
                                    }
                                    else
                                    {
                                        feet[i] = 'R';
                                    }
                                    steps[i] = step;
                                }
                                else // foot equals right, so insert a left-foot step or a repeat if the previous step was a single-foot step
                                {
                                    string step;
                                    if (singlesteps.Contains(laststep) && !leftsteps.Contains(laststep))
                                    {
                                        // the previous step was a single step, so add it to the list of options
                                        string[] with_repeat = new string[leftsteps.Count() + 1];
                                        for (int j = 0; j < leftsteps.Count(); j++)
                                        {
                                            with_repeat[j] = leftsteps[j];
                                        }
                                        with_repeat[leftsteps.Count()] = laststep;
                                        step = with_repeat[r.Next(0, with_repeat.Count())];
                                    }
                                    else
                                    {
                                        step = leftsteps[r.Next(0, leftsteps.Count())];
                                    }
                                    if (!step.Equals(laststep))
                                    {
                                        foot = "left";
                                        feet[i] = 'R';
                                    }
                                    else
                                    {
                                        feet[i] = 'L';
                                    }
                                    steps[i] = step;
                                }
                            } 
                        } 
                        laststep = steps[i];
                        i++; // skip the half-beat arrow, which is prefilled with "00000"
                    } // end else: no half-beat arrow 
                }  // end if (rInt < stepfill)
                else
                {
                    i++; // no arrow so skip the half-beat arrow, which is prefilled with "00000"
                }
            } // end for loop
            string[] finish_foot_laststep = new string[2];
            finish_foot_laststep[0] = foot;
            finish_foot_laststep[1] = laststep;
            return finish_foot_laststep;
        } // end method string generatePumpSingleSteps(string[])
    }
}

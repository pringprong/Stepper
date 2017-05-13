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
        string dance_style;
        string[] singlesteps;
        string[] jumpsteps;
        string[] leftsteps;
        string[] rightsteps = new string[] { "0100", "0010", "0001" };
        bool fromJump;


        public Measure()
        {

        }

        public Measure(string dancestyle, int beats_p_measure, bool alt_foot, bool repeat_arrows, int percent_stepfill, int percent_onbeat,
           int percent_jumps, Random random, int percent_quintuples, bool triples_on_1_and_3, bool quintuples_on_1_or_2)
        {
            dance_style = dancestyle;
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

        private bool isUDUDSQuintuple(string steps_i, string steps_i_plus_one)
        {
            if (dance_style.Equals("dance-single"))
            {
                if (((steps_i == "0100") && (steps_i_plus_one == "0010")) ||
                    ((steps_i == "0010") && (steps_i_plus_one == "0100")))
                {
                    return true;
                }
            }
            else if (dance_style.Equals("dance-solo"))
            {
                if (((steps_i == "001000") && (steps_i_plus_one == "000100")) ||
                    ((steps_i == "000100") && (steps_i_plus_one == "001000")))
                {
                    return true;
                }
            }
            else if (dance_style.Equals("dance-double"))
            {
                if (((steps_i == "01000000") && (steps_i_plus_one == "00100000")) ||
                    ((steps_i == "00100000") && (steps_i_plus_one == "01000000")) ||
                    ((steps_i == "00000100") && (steps_i_plus_one == "00000010")) ||
                    ((steps_i == "00000010") && (steps_i_plus_one == "00000100")))
                {
                    return true;
                }
            }
            else if (dance_style.Equals("pump-single"))
            {
                if (((steps_i == "00001") && (steps_i_plus_one == "00010")) ||
                    ((steps_i == "00010") && (steps_i_plus_one == "00001")) ||
                    ((steps_i == "10000") && (steps_i_plus_one == "01000")) ||
                    ((steps_i == "01000") && (steps_i_plus_one == "10000")))
                {
                    return true;
                }
            }
            return false;
        }

        private void createEmpty()
        {
            if (dance_style.Equals("dance-single"))
            {
                steps = new string[] { "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000" };
            }
            else if (dance_style.Equals("dance-solo"))
            {
                steps = new string[] { "000000", "000000", "000000", "000000", "000000", "000000", "000000", "000000" };
            }
            else if (dance_style.Equals("dance-double"))
            {
                steps = new string[] { "00000000", "00000000", "00000000", "00000000", "00000000", "00000000", "00000000", "00000000" };
            }
            else if (dance_style.Equals("pump-single"))
            {
                steps = new string[] { "00000", "00000", "00000", "00000", "00000", "00000", "00000", "00000" };
            }
        }

        private void initiateStep(string laststep)
        {
            if (dance_style.Equals("dance-single"))
            {
                singlesteps = new string[] { "1000", "0100", "0010", "0001" };
                jumpsteps = new string[] { "0011", "0110", "0101", "1100", "1001", "1010" };
                leftsteps = new string[] { "1000", "0100", "0010" };
                rightsteps = new string[] { "0100", "0010", "0001" };
                fromJump = jumpsteps.Contains(laststep);
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
            }
            else if (dance_style.Equals("dance-solo"))
            {
                singlesteps = new string[] { "100000", "010000", "001000", "000100", "000010", "000001" };
                jumpsteps = new string[] { "110000", // left and topleft  
                                                    "101000", // left and bottom    
                                                    "100100", // left and top     
                                                    "100010", // left and topright  
                                                    "100001", // left and right    
                                                    "011000", // topleft and bottom  
                                                    "010100", // topleft and top  
                                                    "010010", // topleft and topright  
                                                    "010001", // topleft and right   
                                                    "001100", // bottom and top  
                                                    "001010", // bottom and topright 
                                                    "001001", // bottom and right 
                                                    "000110", // top and topright
                                                    "000101", // top and right
                                                    "000011" // topright and right
                                                   };
                leftsteps = new string[] { "100000", "010000", "001000", "000100" };
                rightsteps = new string[] { "001000", "000100", "000010", "000001" };
                fromJump = jumpsteps.Contains(laststep);
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
            }
            else if (dance_style.Equals("dance-double"))
            {
                singlesteps = new string[] { "10000000", "01000000", "00100000", "00010000", "00001000", "00000100", "00000010", "00000001" };
                jumpsteps = new string[] { 
                     "00110000", "01100000", "01010000", "11000000", "10010000", "10100000",  // jumps on the left-hand side
                      "00000011", "00000110", "00000101", "00001100", "00001001", "00001010",  // jumps on the right-hand side
                      "10001000", // both left arrows
                      "01000100", "01001000", // left bottom and right-left or right bottom
                      "00100010", "00101000", // left top and rightleft or right top
                      "00011000", "00010100", "00010010", "00010001"   // left right and right left, right top, right bottom or right right
                };
                rightsteps = new string[] { "10000000", "01000000", "00100000", "00010000", "00001000", "00000100", "00000010", "00000001" };
                leftsteps = new string[] { "10000000", "01000000", "00100000", "00010000", "00001000", "00000100", "00000010", "00000001" };
                fromJump = jumpsteps.Contains(laststep);
                if (fromJump)
                {
                }
            }
            else if (dance_style.Equals("pump-single"))
            {
                singlesteps = new string[] { "10000", "01000", "00100", "00010", "00001" };
                jumpsteps = new string[] { "00011", "00110", "00101", "01100", "01001", "01010", "10001", "10010", "10100", "11000" };
                leftsteps = new string[] { "10000", "01000", "00100", "00010", "00001" };
                rightsteps = new string[] { "10000", "01000", "00100", "00010", "00001" };
                fromJump = jumpsteps.Contains(laststep);
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
            }
        }

        public string[] generateSteps(string[] foot_laststep)
        {
            string foot = foot_laststep[0];
            string laststep = foot_laststep[1];
            arrows_per_measure = beats_per_measure * 2;
            createEmpty();
            feet = new char[arrows_per_measure];

            for (int i = 0; i < arrows_per_measure; i++)
            {
                // set initial options for this step
                initiateStep(laststep);

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
                            feet[i + 1] = 'L';
                            steps[i + 3] = step;
                            feet[i + 3] = 'L';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i + 4] = step;
                            feet[i + 4] = 'R';

                            // prevent up-down-up-down-side type quintuples right after jumps, because it's too likely to start them on the wrong foot
                            if (fromJump && isUDUDSQuintuple(steps[i], steps[i + 1]))
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
                            feet[i] = 'L';
                            steps[i + 2] = step;
                            feet[i + 2] = 'L';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = rightsteps[r.Next(0, rightsteps.Count())];
                            }
                            steps[i + 1] = step;
                            feet[i + 1] = 'R';
                            steps[i + 3] = step;
                            feet[i + 3] = 'R';
                            laststep = step;
                            while (step.Equals(laststep))
                            {
                                step = leftsteps[r.Next(0, leftsteps.Count())];
                            }
                            steps[i + 4] = step;
                            feet[i + 4] = 'L';
                            // prevent up-down-up-down-side type quintuples right after jumps, because it's too likely to start them on the wrong foot
                            if (fromJump && isUDUDSQuintuple(steps[i], steps[i + 1]))
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
                            feet[i] = 'L';
                            steps[i + 2] = step;
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
                        i++; // skip the half-beat arrow, which is prefilled with an empty step
                    } // end else: no half-beat arrow 
                }  // end if (rInt < stepfill)
                else
                {
                    i++; // no arrow so skip the half-beat arrow, which is prefilled with an empty step
                }
            } // end for loop
            string[] finish_foot_laststep = new string[2];
            finish_foot_laststep[0] = foot;
            finish_foot_laststep[1] = laststep;
            return finish_foot_laststep;
        } // end method string generateSteps(string[])
    }
}

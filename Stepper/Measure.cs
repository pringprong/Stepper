﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepper
{
	class Measure
	{
		string[] steps; // set of notes in the form "0010" 
		int arrows_per_measure;
		Random r;
		char[] feet; // set of letters StepDeets.L 'L', StepDeets.R 'R', StepDeets.E 'E' (either), and StepDeets.J 'J' (jumps) representing which feet are stepping on the arrows
		NotesetParameters np;
		string[] singlesteps;
		string[] jumpsteps;
		string[] leftsteps;
		string[] rightsteps;
		bool fromJump;
		ConfigSettings config;

		public Measure()
		{

		}

		public Measure(NotesetParameters notesetparams, Random random, ConfigSettings c)
		{
			np = notesetparams;
			r = random;
			config = c;
			arrows_per_measure = StepDeets.beats_per_measure * 2;
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

		private void createEmpty()
		{
			steps = new string[arrows_per_measure];
			for (int i = 0; i < arrows_per_measure; i++)
			{
				steps[i] = StepDeets.emptyStep(np.dance_style);
			}
		}

		public string[] generateSteps(string[] foot_laststep)
		{
			string foot = foot_laststep[0];
			string laststep = foot_laststep[1];
			arrows_per_measure = StepDeets.beats_per_measure * 2;
			createEmpty();
			feet = new char[arrows_per_measure];

			for (int i = 0; i < arrows_per_measure; i++)
			{
				// set initial options for this step
				leftsteps = config.getStepList(np.dance_style, StepDeets.Left, laststep);
				rightsteps = config.getStepList(np.dance_style, StepDeets.Right, laststep);
				jumpsteps = config.getStepList(np.dance_style, StepDeets.Jump, laststep);
				singlesteps = config.getStepList(np.dance_style, StepDeets.Single, laststep);
				fromJump = config.fromJump(np.dance_style, laststep);

				int rStepFill = r.Next(0, 100);
				if ( // decide if there will be an arrow here at all
					// if full 8th stream is not checked
					(!np.full8th &&((((i == 0) || (i == 4)) && (np.percent_stepfill >= 50)) // for stepfill > 50, the 1st and 3rd beats always have arrows
					|| ((np.percent_stepfill >= 50) && (rStepFill < ((np.percent_stepfill - 50) * 2))) // for stepfill > 50, the 2nd and 4th beats are randomly chosen
					|| (((i == 0) || (i == 4)) && (rStepFill < (np.percent_stepfill * 2))))) // for stepfill < 50, the 2nd and 4th beats are always empty,  1st and 3rd beat are randomly chosen
					// if full 8th stream is checked
					|| (np.full8th && ((((i == 0) || (i == 4) || (i == 2) || (i == 6)) && (np.percent_stepfill >= 50)) // for stepfill > 50, the on-beats always have arrows
					|| ((np.percent_stepfill >= 50) && (rStepFill < ((np.percent_stepfill - 50) * 2))) // for stepfill > 50, the 2nd and 4th beats are randomly chosen
					|| (((i == 0) || (i == 4) || (i == 2) || (i == 6)) && (rStepFill < (np.percent_stepfill * 2)))))) 
				{
					int rOnBeat = r.Next(0, 100); // random number to choose between on-beat and half-beat
					int rTripQuint = r.Next(0, 100); // random number to choose between triples and quintuples
					if (!np.full8th // 8th stream is not checked
						&& rOnBeat >= np.percent_onbeat  // AND random number is on correct side of on-beat slider
						&& rTripQuint < np.percent_quintuples  // AND random number is on correct side of triples-vs-quintuples slider
						&& ((i == 0) || ((i == 2) &&! np.quintuples_on_1_only))) // AND (first beat OR second beat and quintuples-on-1st-beat-only is unchecked
					{ // insert a quintuple
						int rQuintType = r.Next(0, 50); // random number to choose what kind of quintuple
						if (np.quintuple_type >= 50 && (rQuintType + 50) < np.quintuple_type)
						{ // insert ABABA quintuple
							if (foot.Equals(StepDeets.Left))
							{
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.R;
								steps[i + 2] = step;
								feet[i + 2] = StepDeets.R;
								steps[i + 4] = step;
								feet[i + 4] = StepDeets.R;
								// reset leftsteps because the set of appropriate options might have changed (for Dance Double)
								leftsteps = config.getStepList(np.dance_style, StepDeets.Left, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i + 1] = step;
								feet[i + 1] = StepDeets.L;
								steps[i + 3] = step;
								feet[i + 3] = StepDeets.L;
								laststep = step;
								foot = StepDeets.Right;
							}
							else
							{
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.L;
								steps[i + 2] = step;
								feet[i + 2] = StepDeets.L;
								steps[i + 4] = step;
								feet[i + 4] = StepDeets.L;
								// reset rightsteps because the set of appropriate options might have changed (for Dance Double)
								rightsteps = config.getStepList(np.dance_style, StepDeets.Right, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i + 1] = step;
								feet[i + 1] = StepDeets.R;
								steps[i + 3] = step;
								feet[i + 3] = StepDeets.R;
								laststep = steps[i + 4];
								foot = StepDeets.Left;
							}
						}
						else if ((np.quintuple_type < 50 && rQuintType < np.quintuple_type) || (np.quintuple_type >= 50 && (rQuintType+50) >= np.quintuple_type))
						{ // insert ABABC quintuple
							if (foot.Equals(StepDeets.Left))
							{
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.R;
								steps[i + 2] = step;
								feet[i + 2] = StepDeets.R;
								// reset leftsteps because the set of appropriate options might have changed (for Dance Double)
								leftsteps = config.getStepList(np.dance_style, StepDeets.Left, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i + 1] = step;
								feet[i + 1] = StepDeets.L;
								steps[i + 3] = step;
								feet[i + 3] = StepDeets.L;
								// reset rightsteps because the set of appropriate options might have changed (for Dance Double)
								rightsteps = config.getStepList(np.dance_style, StepDeets.Right, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i + 4] = step;
								feet[i + 4] = StepDeets.R;

								// prevent up-down-up-down-side type quintuples right after jumps, because it's too likely to start them on the wrong foot
								if (fromJump && StepDeets.isUDUDSQuintuple(np.dance_style, steps[i], steps[i + 1]))
								{
									steps[i + 4] = steps[i + 2];
								}
								laststep = steps[i + 4];
								foot = StepDeets.Right;
							}
							else
							{ 
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.L;
								steps[i + 2] = step;
								feet[i + 2] = StepDeets.L;
								// reset rightsteps because the set of appropriate options might have changed (for Dance Double)
								rightsteps = config.getStepList(np.dance_style, StepDeets.Right, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i + 1] = step;
								feet[i + 1] = StepDeets.R;
								steps[i + 3] = step;
								feet[i + 3] = StepDeets.R;
								// reset leftsteps because the set of appropriate options might have changed (for Dance Double)
								leftsteps = config.getStepList(np.dance_style, StepDeets.Left, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i + 4] = step;
								feet[i + 4] = StepDeets.L;
								// prevent up-down-up-down-side type quintuples right after jumps, because it's too likely to start them on the wrong foot
								if (fromJump && StepDeets.isUDUDSQuintuple(np.dance_style, steps[i], steps[i + 1]))
								{
									steps[i + 4] = steps[i + 2];
								}
								laststep = steps[i + 4];
								foot = StepDeets.Left;
							}
						}
						else // insert ABCDE quintuple
						{
							if (foot.Equals(StepDeets.Left))
							{
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.R;

								// reset leftsteps because the set of appropriate options might have changed (for Dance Double)
								leftsteps = config.getStepList(np.dance_style, StepDeets.Left, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i + 1] = step;
								feet[i + 1] = StepDeets.L;

								// reset rightsteps because the set of appropriate options might have changed (for Dance Double)
								rightsteps = config.getStepList(np.dance_style, StepDeets.Right, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i + 2] = step;
								feet[i + 2] = StepDeets.R;

								// reset leftsteps because the set of appropriate options might have changed (for Dance Double)
								leftsteps = config.getStepList(np.dance_style, StepDeets.Left, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i + 3] = step;
								feet[i + 3] = StepDeets.L;

								// reset rightsteps because the set of appropriate options might have changed (for Dance Double)
								rightsteps = config.getStepList(np.dance_style, StepDeets.Right, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i + 4] = step;
								feet[i + 4] = StepDeets.R;

								laststep = steps[i + 4];
								foot = StepDeets.Right;
							}
							else
							{
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.L;

								// reset rightsteps because the set of appropriate options might have changed (for Dance Double)
								rightsteps = config.getStepList(np.dance_style, StepDeets.Right, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i + 1] = step;
								feet[i + 1] = StepDeets.R;

								// reset leftsteps because the set of appropriate options might have changed (for Dance Double)
								leftsteps = config.getStepList(np.dance_style, StepDeets.Left, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i + 2] = step;
								feet[i + 2] = StepDeets.L;

								// reset rightsteps because the set of appropriate options might have changed (for Dance Double)
								rightsteps = config.getStepList(np.dance_style, StepDeets.Right, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i + 3] = step;
								feet[i + 3] = StepDeets.R;

								// reset leftsteps because the set of appropriate options might have changed (for Dance Double)
								leftsteps = config.getStepList(np.dance_style, StepDeets.Left, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i + 4] = step;
								feet[i + 4] = StepDeets.L;

								laststep = steps[i + 4];
								foot = StepDeets.Right;
							}
						}
						i = i + 5;
					}
					else if (!np.full8th   // full 8th stream is not checked
						&& (rOnBeat >= np.percent_onbeat)   // AND random number is on the correct side of the onbeat slider
						&& (rTripQuint >= np.percent_quintuples)  // AND random number is on the correct side of the triples-vs-quintuples slider
						&& ((i == 0)  // AND ( first beat of measure
							|| ((i == 2) || (i == 4)) && !np.triples_on_1_only))  // OR second or third beat of measure, and "triples on 1st beat only" is unchecked)
					{ // insert a triple
						if (r.Next(0, 100) < np.triple_type)  // random number to choose what kind of triple
						{  // insert an ABA triple
							if (foot.Equals(StepDeets.Left))
							{
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.R;
								steps[i + 2] = step;
								feet[i + 2] = StepDeets.R;
								// reset leftsteps because the set of appropriate options might have changed (for Dance Double)
								leftsteps = config.getStepList(np.dance_style, StepDeets.Left, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i + 1] = step;
								feet[i + 1] = StepDeets.L;
								foot = StepDeets.Right;
							}
							else
							{
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.L;
								steps[i + 2] = step;
								feet[i + 2] = StepDeets.L;
								// reset rightsteps because the set of appropriate options might have changed (for Dance Double)
								rightsteps = config.getStepList(np.dance_style, StepDeets.Right, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i + 1] = step;
								feet[i + 1] = StepDeets.R;
								foot = StepDeets.Left;
							}
						}
						else  // insert an ABC triple
						{
							if (foot.Equals(StepDeets.Left))
							{
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.R;

								// reset leftsteps because the set of appropriate options might have changed (for Dance Double)
								leftsteps = config.getStepList(np.dance_style, StepDeets.Left, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i + 1] = step;
								feet[i + 1] = StepDeets.L;

								// reset rightsteps because the set of appropriate options might have changed (for Dance Double)
								rightsteps = config.getStepList(np.dance_style, StepDeets.Right, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i + 2] = step;
								feet[i + 2] = StepDeets.R;

								foot = StepDeets.Right;
								laststep = step;
							}
							else
							{
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.L;

								// reset rightsteps because the set of appropriate options might have changed (for Dance Double)
								rightsteps = config.getStepList(np.dance_style, StepDeets.Right, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = rightsteps[r.Next(0, rightsteps.Count())];
								}
								steps[i + 1] = step;
								feet[i + 1] = StepDeets.R;

								// reset leftsteps because the set of appropriate options might have changed (for Dance Double)
								leftsteps = config.getStepList(np.dance_style, StepDeets.Left, step);
								laststep = step;
								while (step.Equals(laststep))
								{
									step = leftsteps[r.Next(0, leftsteps.Count())];
								}
								steps[i + 2] = step;
								feet[i + 2] = StepDeets.L;

								foot = StepDeets.Left;
								laststep = step;
							}
						}
						i = i + 3;
					}
					else
					{ // no half-beat arrows
						if (!np.alternating_foot && np.repeat_arrows) // repeats allowed, no alternate foot constraint, so choose randomly
						{
							if (r.Next(0, 100) < np.percent_jumps && ((i % 2) == 0)) // insert a jump if it's not a half-beat
							{
								steps[i] = jumpsteps[r.Next(0, jumpsteps.Count())];
								feet[i] = StepDeets.J;
							}
							else
							{
								steps[i] = singlesteps[r.Next(0, singlesteps.Count())];
								feet[i] = StepDeets.E;
							}
						}
						else if (!np.alternating_foot && !np.repeat_arrows) // repeats not allowed, so make sure the step isn't the same as laststep
						{
							if (r.Next(0, 100) < np.percent_jumps && ((i % 2) == 0)) // insert a jump if it's not a half-beat
							{
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = jumpsteps[r.Next(0, jumpsteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.J;
							}
							else
							{
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = singlesteps[r.Next(0, singlesteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.E;
							}
						}
						else if (np.alternating_foot && !np.repeat_arrows) // strict alternate foot, no repeats
						{
							if (r.Next(0, 100) < np.percent_jumps && ((i % 2) == 0)) // insert a jump if it's not a half-beat
							{
								string step = laststep;
								while (step.Equals(laststep))
								{
									step = jumpsteps[r.Next(0, jumpsteps.Count())];
								}
								steps[i] = step;
								feet[i] = StepDeets.J;
							}
							else
							{
								if (foot.Equals(StepDeets.Left))
								{
									string step = laststep;
									while (step.Equals(laststep))
									{
										step = rightsteps[r.Next(0, rightsteps.Count())];
									}
									steps[i] = step;
									feet[i] = StepDeets.R;
									foot = StepDeets.Right;
								}
								else
								{
									string step = laststep;
									while (step.Equals(laststep))
									{
										step = leftsteps[r.Next(0, leftsteps.Count())];
									}
									steps[i] = step;
									feet[i] = StepDeets.L;
									foot = StepDeets.Left;
								}
							}
						}
						else //if (alternate_foot && repeat_arrow)
						{
							if ((r.Next(0, 100) < np.percent_jumps) && ((i % 2) == 0)) // insert a jump if it's not a half-beat
							{
								steps[i] = jumpsteps[r.Next(0, jumpsteps.Count())];
								feet[i] = StepDeets.J;
							}
							else // insert a single-foot step
							{
								if (foot.Equals(StepDeets.Left)) // insert a right foot step or a repeat if the previous step was a single-foot step
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
										foot = StepDeets.Right;
										feet[i] = StepDeets.R;
									}
									else
									{
										feet[i] = StepDeets.L;
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
										foot = StepDeets.Left;
										feet[i] = StepDeets.L;
									}
									else
									{
										feet[i] = StepDeets.R;
									}
									steps[i] = step;
								}
							}
						}
						laststep = steps[i];
						if (!np.full8th) { i++; }  // skip the half-beat arrow, which is prefilled with an empty step
					} // end else: no half-beat arrow 
				}  // end if (rInt < stepfill)
				else
				{
					if(!np.full8th) {i++;} // no arrow so skip the half-beat arrow, which is prefilled with an empty step
				}
			} // end for loop
			string[] finish_foot_laststep = new string[2];
			finish_foot_laststep[0] = foot;
			finish_foot_laststep[1] = laststep;
			return finish_foot_laststep;
		} // end method string generateSteps(string[])
	}
}

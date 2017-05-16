using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stepper
{
	static class StepDictionary
	{
		private static Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> 
			steps_3d_dictionary	= new Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> {
				{ "dance-single", new Dictionary<string, Dictionary<string, string[]>> {
					{ "left", new Dictionary<string, string[]>() { 
						// single steps reachable with the left foot while the right foot is on the key step (or both feet for jumps)
						{ "base", new string[] {"1000", "0100", "0010"} },
						{ "1000", new string[] {"1", "1", "1"} },
						{ "0100", new string[] {"1", "1", "1"} },
						{ "0010", new string[] {"1", "1", "1"} },
						{ "0001", new string[] {"1", "1", "1"} },
						{ "0011", new string[] {"1", "1", "1"} },
						{ "0101", new string[] {"1", "1", "1"} },
						{ "1001", new string[] {"1", "1", "1"} },
						{ "0110", new string[] {"1", "1", "1"} },
						{ "1010", new string[] {"1", "1", "X"} }, // top and left jump; don't try to put the left foot onto the top arrow, because the right foot is on it
						{ "1100", new string[] {"1", "X", "1"} }, // bottom and left jump don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
					} },
					{ "right", new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ "base", new string[] {"0100", "0010", "0001"} },
						{ "1000", new string[] {"1", "1", "1"} },
						{ "0100", new string[] {"1", "1", "1"} },
						{ "0010", new string[] {"1", "1", "1"} },
						{ "0001", new string[] {"1", "1", "1"} },
						{ "0011", new string[] {"1", "X", "1"} }, // top and right
						{ "0101", new string[] {"X", "1", "1"} }, // bottom and right
						{ "1001", new string[] {"1", "1", "1"} },
						{ "0110", new string[] {"1", "1", "1"} },
						{ "1010", new string[] {"1", "1", "1"} }, 
						{ "1100", new string[] {"1", "1", "1"} }, 
					} },
					{ "jump", new Dictionary<string, string[]>() { 
						// jump steps reachable from the key step: all reachable
						{ "base", new string[] {"0011", "0110", "0101", "1100", "1001", "1010"} },
						{ "1000", new string[] {"1", "1", "1", "1", "1", "1"} },
						{ "0100", new string[] {"1", "1", "1", "1", "1", "1"} },
						{ "0010", new string[] {"1", "1", "1", "1", "1", "1"} },
						{ "0001", new string[] {"1", "1", "1", "1", "1", "1"} },
						{ "0011", new string[] {"1", "1", "1", "1", "1", "1"} }, 
						{ "0101", new string[] {"1", "1", "1", "1", "1", "1"} }, 
						{ "1001", new string[] {"1", "1", "1", "1", "1", "1"} },
						{ "0110", new string[] {"1", "1", "1", "1", "1", "1"} },
						{ "1010", new string[] {"1", "1", "1", "1", "1", "1"} }, 
						{ "1100", new string[] {"1", "1", "1", "1", "1", "1"} }, 
					} },
				} },
				{ "dance-solo", new Dictionary<string, Dictionary<string, string[]>> {
					{ "left", new Dictionary<string, string[]>() { 
						// single steps reachable with the left foot while the right foot is on the key step (or both feet for jumps)
						{ "base", new string[] {"100000" /*left*/, "010000" /*topleft*/, "001000"/*bottom*/, "000100"/*top*/} },
						{ "100000", /* left                */ new string[] {"1", "1", "1", "1"} },
						{ "010000", /* topleft             */ new string[] {"1", "1", "1", "1"} },
						{ "001000", /* bottom              */ new string[] {"1", "1", "1", "1"} },
						{ "000100", /* top                 */ new string[] {"1", "1", "1", "1"} },
						{ "000010", /* topright            */ new string[] {"1", "1", "1", "1"} },
						{ "000001", /* right               */ new string[] {"1", "1", "1", "1"} },
						{ "110000", /* left and topleft    */ new string[] {"1", "1", "1", "1"} },
						{ "101000", /* left and bottom     */ new string[] {"1", "1", "X", "1"} },// don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
						{ "100100", /* left and top        */ new string[] {"1", "1", "1", "X"} },// don't try to put the left foot onto the top arrow, because the right foot is on it right now
						{ "100010", /* left and topright   */ new string[] {"1", "1", "1", "1"} },
						{ "100001", /* left and right      */ new string[] {"1", "1", "1", "1"} },
						{ "011000", /* topleft and bottom  */ new string[] {"1", "1", "X", "1"} },// don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
						{ "010100", /* topleft and top     */ new string[] {"1", "1", "1", "X"} },// don't try to put the left foot onto the top arrow, because the right foot is on it right now
						{ "010010", /* topleft and topright*/ new string[] {"1", "1", "1", "1"} },
						{ "010001", /* topleft and right   */ new string[] {"1", "1", "1", "1"} },
						{ "001100", /* bottom and top      */ new string[] {"1", "1", "1", "1"} }, 
						{ "001010", /* bottom and topright */ new string[] {"1", "1", "1", "1"} }, 
						{ "001001", /* bottom and right    */ new string[] {"1", "1", "1", "1"} },
						{ "000110", /* top and topright    */ new string[] {"1", "1", "1", "1"} },
						{ "000101", /* top and right       */ new string[] {"1", "1", "1", "1"} }, 
						{ "000011", /* topright and right  */ new string[] {"1", "1", "1", "1"} }, 
					} },
					{ "right", new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ "base", new string[] {"001000"/*bottom*/, "000100"/*top*/, "000010"/*topright*/, "000001"/*right*/} },
						{ "100000", /* left                */ new string[] {"1", "1", "1", "1"} },
						{ "010000", /* topleft             */ new string[] {"1", "1", "1", "1"} },
						{ "001000", /* bottom              */ new string[] {"1", "1", "1", "1"} },
						{ "000100", /* top                 */ new string[] {"1", "1", "1", "1"} },
						{ "000010", /* topright            */ new string[] {"1", "1", "1", "1"} },
						{ "000001", /* right               */ new string[] {"1", "1", "1", "1"} },
						{ "110000", /* left and topleft    */ new string[] {"1", "1", "1", "1"} },
						{ "101000", /* left and bottom     */ new string[] {"1", "1", "1", "1"} },
						{ "100100", /* left and top        */ new string[] {"1", "1", "1", "1"} },
						{ "100010", /* left and topright   */ new string[] {"1", "1", "1", "1"} },
						{ "100001", /* left and right      */ new string[] {"1", "1", "1", "1"} },
						{ "011000", /* topleft and bottom  */ new string[] {"1", "1", "1", "1"} },
						{ "010100", /* topleft and top     */ new string[] {"1", "1", "1", "1"} },
						{ "010010", /* topleft and topright*/ new string[] {"1", "1", "1", "1"} },
						{ "010001", /* topleft and right   */ new string[] {"1", "1", "1", "1"} },
						{ "001100", /* bottom and top      */ new string[] {"1", "1", "1", "1"} }, 
						{ "001010", /* bottom and topright */ new string[] {"X", "1", "1", "1"} },// don't try to put the right foot onto the bottom arrow, because the left foot is on it right now 
						{ "001001", /* bottom and right    */ new string[] {"X", "1", "1", "1"} },// don't try to put the right foot onto the bottom arrow, because the left foot is on it right now
						{ "000110", /* top and topright    */ new string[] {"1", "X", "1", "1"} },// don't try to put the right foot onto the top arrow, because the left foot is on it right now
						{ "000101", /* top and right       */ new string[] {"1", "X", "1", "1"} },// don't try to put the right foot onto the top arrow, because the left foot is on it right now 
						{ "000011", /* topright and right  */ new string[] {"1", "1", "1", "1"} }, 
					} },
					{ "jump", new Dictionary<string, string[]>() { 
						// jump steps reachable from the key step
						// all reachable
						{ "base", new string[] { "110000", /* left and topleft */  
                                                    "101000", /* left and bottom */  
                                                    "100100", /* left and top */  
                                                    "100010", /* left and topright*/  
                                                    "100001", /* left and right*/    
                                                    "011000", /* topleft and bottom*/  
                                                    "010100", /* topleft and top */ 
                                                    "010010", /* topleft and topright*/  
                                                    "010001", /* topleft and right*/   
                                                    "001100", /* bottom and top */ 
                                                    "001010", /* bottom and topright*/ 
                                                    "001001", /* bottom and right*/ 
                                                    "000110", /* top and topright*/
                                                    "000101", /* top and right*/
                                                    "000011", /* topright and right*/
                                                   } },
						{ "100000", /* left                */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "010000", /* topleft             */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "001000", /* bottom              */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "000100", /* top                 */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "000010", /* topright            */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "000001", /* right               */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "110000", /* left and topleft    */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "101000", /* left and bottom     */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "100100", /* left and top        */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "100010", /* left and topright   */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "100001", /* left and right      */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "011000", /* topleft and bottom  */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "010100", /* topleft and top     */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "010010", /* topleft and topright*/ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "010001", /* topleft and right   */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "001100", /* bottom and top      */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } }, 
						{ "001010", /* bottom and topright */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } }, 
						{ "001001", /* bottom and right    */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "000110", /* top and topright    */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } },
						{ "000101", /* top and right       */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } }, 
						{ "000011", /* topright and right  */ new string[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", } }, 
					} },
				} },
			};

		public static bool fromJump(string dance_style, string laststep)
		{
			string[] jumpsteps = steps_3d_dictionary[dance_style]["jump"]["base"];
			if (jumpsteps.Contains(laststep)) {
				return true;
			}
			else {
				return false;
			}
		}

		public static string[] getStepList(string dance_style, string foottype, string laststep)
		{
			List<string> temp1 = new List<string>();
			List<string> temp2 = new List<string>();
			if (steps_3d_dictionary[dance_style].Keys.Contains(foottype))
			{
				for (int b = 0; b < steps_3d_dictionary[dance_style][foottype]["base"].Count(); b++)
				{
					if (steps_3d_dictionary[dance_style][foottype][laststep][b].Equals("1"))
					// look up the previous step in the dictionary and get the list of steps that we can go to from here
					{
						temp1.Add(steps_3d_dictionary[dance_style][foottype]["base"][b]);
					}
				}
				return temp1.ToArray();
			}
			else // if the request is not for left, right, or jump, then we return the union of left and right as the set of single steps
			{
				for (int b = 0; b < steps_3d_dictionary[dance_style]["left"]["base"].Count(); b++)
				{
					if (steps_3d_dictionary[dance_style]["left"][laststep][b].Equals("1"))
					// look up the previous step in the dictionary and get the list of steps that we can go to from here
					{
						temp1.Add(steps_3d_dictionary[dance_style]["left"]["base"][b]);
					}
				}
				for (int b = 0; b < steps_3d_dictionary[dance_style]["right"]["base"].Count(); b++)
				{
					if (steps_3d_dictionary[dance_style]["right"][laststep][b].Equals("1"))
					// look up the previous step in the dictionary and get the list of steps that we can go to from here
					{
						temp2.Add(steps_3d_dictionary[dance_style]["right"]["base"][b]);
					}
				}
				temp1.AddRange(temp2);
				return temp1.ToArray();
			}
		}

		public static bool isUDUDSQuintuple(string dance_style, string steps_i, string steps_i_plus_one)
		// up-down-up-down-side quintuples are not allowed after jumps, because they are too easy to start on the wrong foot
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
				if (((steps_i == "001000") && (steps_i_plus_one == "000100")) || //bottom and top
					((steps_i == "000100") && (steps_i_plus_one == "001000")) || //top and bottom
					((steps_i == "100000") && (steps_i_plus_one == "010000")) || //left and topleft  // i guess these will never happen, but who knows
					((steps_i == "010000") && (steps_i_plus_one == "100000")) || // topleft and left
					((steps_i == "000001") && (steps_i_plus_one == "000010")) || // right and topright
					((steps_i == "000010") && (steps_i_plus_one == "000001"))) // topright and right
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
	}
}

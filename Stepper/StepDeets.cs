using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stepper
{
	static class StepDeets
	{
		public const string DanceSingle = "dance-single";
		public const string DanceSolo = "dance-solo";
		public const string DanceDouble = "dance-double";
		public const string PumpSingle = "pump-single";
		public const string Left = "left";
		public const string Right = "right";
		public const string Jump = "jump";
		public const string Base = "base";
		public const string Single = "single";
		public const string T = "1";
		public const string F = "0";
		public const char R = 'R'; // right foot
		public const char L = 'L'; // left foot
		public const char J = 'J';  // jump
		public const char E = 'E';  //either foot
		
		private static Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> 
			steps_3d_dictionary	= new Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> {
				{ StepDeets.DanceSingle, new Dictionary<string, Dictionary<string, string[]>> {
					{ StepDeets.Left, new Dictionary<string, string[]>() { 
						// single steps reachable with the left foot while the right foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"1000", "0100", "0010"} },
						{ "1000", new string[] {T, T, T} },
						{ "0100", new string[] {T, T, T} },
						{ "0010", new string[] {T, T, T} },
						{ "0001", new string[] {T, T, T} },
						{ "0011", new string[] {T, T, T} },
						{ "0101", new string[] {T, T, T} },
						{ "1001", new string[] {T, T, T} },
						{ "0110", new string[] {T, T, T} },
						{ "1010", new string[] {T, T, F} }, // top and left jump; don't try to put the left foot onto the top arrow, because the right foot is on it
						{ "1100", new string[] {T, F, T} }, // bottom and left jump don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"0100", "0010", "0001"} },
						{ "1000", new string[] {T, T, T} },
						{ "0100", new string[] {T, T, T} },
						{ "0010", new string[] {T, T, T} },
						{ "0001", new string[] {T, T, T} },
						{ "0011", new string[] {T, F, T} }, // top and right
						{ "0101", new string[] {F, T, T} }, // bottom and right
						{ "1001", new string[] {T, T, T} },
						{ "0110", new string[] {T, T, T} },
						{ "1010", new string[] {T, T, T} }, 
						{ "1100", new string[] {T, T, T} }, 
					} },
					{ StepDeets.Jump, new Dictionary<string, string[]>() { 
						// jump steps reachable from the key step: all reachable
						{ StepDeets.Base, new string[] {"0011", "0110", "0101", "1100", "1001", "1010"} },
						{ "1000", new string[] {T, T, T, T, T, T} },
						{ "0100", new string[] {T, T, T, T, T, T} },
						{ "0010", new string[] {T, T, T, T, T, T} },
						{ "0001", new string[] {T, T, T, T, T, T} },
						{ "0011", new string[] {T, T, T, T, T, T} }, 
						{ "0101", new string[] {T, T, T, T, T, T} }, 
						{ "1001", new string[] {T, T, T, T, T, T} },
						{ "0110", new string[] {T, T, T, T, T, T} },
						{ "1010", new string[] {T, T, T, T, T, T} }, 
						{ "1100", new string[] {T, T, T, T, T, T} }, 
					} },
				} },
				{ StepDeets.DanceSolo, new Dictionary<string, Dictionary<string, string[]>> {
					{ StepDeets.Left, new Dictionary<string, string[]>() { 
						// single steps reachable with the left foot while the right foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"100000" /*left*/, "010000" /*topleft*/, "001000"/*bottom*/, "000100"/*top*/} },
						{ "100000", /* left                */ new string[] {T, T, T, T} },
						{ "010000", /* topleft             */ new string[] {T, T, T, T} },
						{ "001000", /* bottom              */ new string[] {T, T, T, T} },
						{ "000100", /* top                 */ new string[] {T, T, T, T} },
						{ "000010", /* topright            */ new string[] {T, T, T, T} },
						{ "000001", /* right               */ new string[] {T, T, T, T} },
						{ "110000", /* left and topleft    */ new string[] {T, T, T, T} },
						{ "101000", /* left and bottom     */ new string[] {T, T, F, T} },// don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
						{ "100100", /* left and top        */ new string[] {T, T, T, F} },// don't try to put the left foot onto the top arrow, because the right foot is on it right now
						{ "100010", /* left and topright   */ new string[] {T, T, T, T} },
						{ "100001", /* left and right      */ new string[] {T, T, T, T} },
						{ "011000", /* topleft and bottom  */ new string[] {T, T, F, T} },// don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
						{ "010100", /* topleft and top     */ new string[] {T, T, T, F} },// don't try to put the left foot onto the top arrow, because the right foot is on it right now
						{ "010010", /* topleft and topright*/ new string[] {T, T, T, T} },
						{ "010001", /* topleft and right   */ new string[] {T, T, T, T} },
						{ "001100", /* bottom and top      */ new string[] {T, T, T, T} }, 
						{ "001010", /* bottom and topright */ new string[] {T, T, T, T} }, 
						{ "001001", /* bottom and right    */ new string[] {T, T, T, T} },
						{ "000110", /* top and topright    */ new string[] {T, T, T, T} },
						{ "000101", /* top and right       */ new string[] {T, T, T, T} }, 
						{ "000011", /* topright and right  */ new string[] {T, T, T, T} }, 
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"001000"/*bottom*/, "000100"/*top*/, "000010"/*topright*/, "000001"/*right*/} },
						{ "100000", /* left                */ new string[] {T, T, T, T} },
						{ "010000", /* topleft             */ new string[] {T, T, T, T} },
						{ "001000", /* bottom              */ new string[] {T, T, T, T} },
						{ "000100", /* top                 */ new string[] {T, T, T, T} },
						{ "000010", /* topright            */ new string[] {T, T, T, T} },
						{ "000001", /* right               */ new string[] {T, T, T, T} },
						{ "110000", /* left and topleft    */ new string[] {T, T, T, T} },
						{ "101000", /* left and bottom     */ new string[] {T, T, T, T} },
						{ "100100", /* left and top        */ new string[] {T, T, T, T} },
						{ "100010", /* left and topright   */ new string[] {T, T, T, T} },
						{ "100001", /* left and right      */ new string[] {T, T, T, T} },
						{ "011000", /* topleft and bottom  */ new string[] {T, T, T, T} },
						{ "010100", /* topleft and top     */ new string[] {T, T, T, T} },
						{ "010010", /* topleft and topright*/ new string[] {T, T, T, T} },
						{ "010001", /* topleft and right   */ new string[] {T, T, T, T} },
						{ "001100", /* bottom and top      */ new string[] {T, T, T, T} }, 
						{ "001010", /* bottom and topright */ new string[] {F, T, T, T} },// don't try to put the right foot onto the bottom arrow, because the left foot is on it right now 
						{ "001001", /* bottom and right    */ new string[] {F, T, T, T} },// don't try to put the right foot onto the bottom arrow, because the left foot is on it right now
						{ "000110", /* top and topright    */ new string[] {T, F, T, T} },// don't try to put the right foot onto the top arrow, because the left foot is on it right now
						{ "000101", /* top and right       */ new string[] {T, F, T, T} },// don't try to put the right foot onto the top arrow, because the left foot is on it right now 
						{ "000011", /* topright and right  */ new string[] {T, T, T, T} }, 
					} },
					{ StepDeets.Jump, new Dictionary<string, string[]>() { 
						// jump steps reachable from the key step
						// all reachable
						{ StepDeets.Base, new string[] { "110000", /* left and topleft */  
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
						{ "100000", /* left                */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "010000", /* topleft             */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "001000", /* bottom              */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "000100", /* top                 */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "000010", /* topright            */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "000001", /* right               */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "110000", /* left and topleft    */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "101000", /* left and bottom     */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "100100", /* left and top        */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "100010", /* left and topright   */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "100001", /* left and right      */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "011000", /* topleft and bottom  */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "010100", /* topleft and top     */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "010010", /* topleft and topright*/ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "010001", /* topleft and right   */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "001100", /* bottom and top      */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } }, 
						{ "001010", /* bottom and topright */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } }, 
						{ "001001", /* bottom and right    */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "000110", /* top and topright    */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } },
						{ "000101", /* top and right       */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } }, 
						{ "000011", /* topright and right  */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, } }, 
					} },
				} },
				{ StepDeets.DanceDouble, new Dictionary<string, Dictionary<string, string[]>> {
					{ StepDeets.Left, new Dictionary<string, string[]>() { 
						// single steps reachable with the left foot while the right foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {
							"10000000", /*leftleft*/
							"01000000", /*leftbottom*/
							"00100000", /*lefttop*/
							"00010000", /*leftright*/
							"00001000", /*rightleft*/
							"00000100", /*rightbottom*/
							"00000010", /*righttop*/
						} },
						{ "10000000",/*leftleft             */ new string[] {T, T, T, T, T, T, T} },
						{ "01000000",/*leftbottom           */ new string[] {T, T, T, T, T, T, T} },
						{ "00100000",/*lefttop              */ new string[] {T, T, T, T, T, T, T} },
						{ "00010000",/*leftright            */ new string[] {T, T, T, T, T, T, T} },
						{ "00001000",/*rightleft            */ new string[] {T, T, T, T, T, T, T} },
						{ "00000100",/*rightbottom          */ new string[] {T, T, T, T, T, T, T} },
						{ "00000010",/*righttop             */ new string[] {T, T, T, T, T, T, T} },
						{ "00000001",/*rightright           */ new string[] {T, T, T, T, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {T, T, T, T, T, T, T} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {T, T, T, T, T, T, T} },
                        {"01010000",/*leftbottom leftright  */ new string[] {T, T, T, T, T, T, T} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {T, T, T, T, T, T, T} },
                        {"10010000",/*leftleft leftright    */ new string[] {T, T, T, T, T, T, T} },
                        {"10100000",/*leftleft lefttop      */ new string[] {T, T, T, T, T, T, T} }, // jumps on the left-hand side
                        {"00000011",/*righttop rightright   */ new string[] {T, T, T, T, T, T, T} },
                        {"00000110",/*rightbottom righttop  */ new string[] {T, T, T, T, T, T, T} },
                        {"00000101",/*rightbottom rightright*/ new string[] {T, T, T, T, T, T, T} },
                        {"00001100",/*rightleft rightbottom */ new string[] {T, T, T, T, T, T, T} },
                        {"00001001",/*rightleft rightright  */ new string[] {T, T, T, T, T, T, T} },
                        {"00001010",/*rightleft righttop    */ new string[] {T, T, T, T, T, T, T} }, // jumps on the right-hand side
                        {"10001000",/*leftleft rightleft    */ new string[] {T, T, T, T, T, T, T} }, // both left arrows
                        {"01000100",/*leftbottom rightbottom*/ new string[] {T, T, T, T, T, T, T} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {T, T, T, T, T, T, T} },// left bottom and right-left or right bottom
                        {"00100010",/*lefttop righttop      */ new string[] {T, T, T, T, T, T, T} },
                        {"00101000",/*lefttop rightleft     */ new string[] {T, T, T, T, T, T, T} },// left top and rightleft or right top
                        {"00011000",/*leftright rightleft   */ new string[] {T, T, T, T, T, T, T} },
                        {"00010100",/*leftright rightbottom */ new string[] {T, T, T, T, T, T, T} },
                        {"00010010",/*leftright righttop    */ new string[] {T, T, T, T, T, T, T} },
                        {"00010001",/*leftright rightright  */ new string[] {T, T, T, T, T, T, T} },
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {
							"01000000", /*leftbottom*/
							"00100000", /*lefttop*/
							"00010000", /*leftright*/
							"00001000", /*rightleft*/
							"00000100", /*rightbottom*/
							"00000010", /*righttop*/
							"00000001", /*rightright*/} },
						{ "10000000",/*leftleft             */ new string[] {T, T, T, T, T, T, T} },
						{ "01000000",/*leftbottom           */ new string[] {T, T, T, T, T, T, T} },
						{ "00100000",/*lefttop              */ new string[] {T, T, T, T, T, T, T} },
						{ "00010000",/*leftright            */ new string[] {T, T, T, T, T, T, T} },
						{ "00001000",/*rightleft            */ new string[] {T, T, T, T, T, T, T} },
						{ "00000100",/*rightbottom          */ new string[] {T, T, T, T, T, T, T} },
						{ "00000010",/*righttop             */ new string[] {T, T, T, T, T, T, T} },
						{ "00000001",/*rightright           */ new string[] {T, T, T, T, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {T, T, T, T, T, T, T} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {T, T, T, T, T, T, T} },
                        {"01010000",/*leftbottom leftright  */ new string[] {T, T, T, T, T, T, T} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {T, T, T, T, T, T, T} },
                        {"10010000",/*leftleft leftright    */ new string[] {T, T, T, T, T, T, T} },
                        {"10100000",/*leftleft lefttop      */ new string[] {T, T, T, T, T, T, T} }, // jumps on the left-hand side
                        {"00000011",/*righttop rightright   */ new string[] {T, T, T, T, T, T, T} },
                        {"00000110",/*rightbottom righttop  */ new string[] {T, T, T, T, T, T, T} },
                        {"00000101",/*rightbottom rightright*/ new string[] {T, T, T, T, T, T, T} },
                        {"00001100",/*rightleft rightbottom */ new string[] {T, T, T, T, T, T, T} },
                        {"00001001",/*rightleft rightright  */ new string[] {T, T, T, T, T, T, T} },
                        {"00001010",/*rightleft righttop    */ new string[] {T, T, T, T, T, T, T} }, // jumps on the right-hand side
                        {"10001000",/*leftleft rightleft    */ new string[] {T, T, T, T, T, T, T} }, // both left arrows
                        {"01000100",/*leftbottom rightbottom*/ new string[] {T, T, T, T, T, T, T} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {T, T, T, T, T, T, T} },// left bottom and right-left or right bottom
                        {"00100010",/*lefttop righttop      */ new string[] {T, T, T, T, T, T, T} },
                        {"00101000",/*lefttop rightleft     */ new string[] {T, T, T, T, T, T, T} },// left top and rightleft or right top
                        {"00011000",/*leftright rightleft   */ new string[] {T, T, T, T, T, T, T} },
                        {"00010100",/*leftright rightbottom */ new string[] {T, T, T, T, T, T, T} },
                        {"00010010",/*leftright righttop    */ new string[] {T, T, T, T, T, T, T} },
                        {"00010001",/*leftright rightright  */ new string[] {T, T, T, T, T, T, T} },
					} },
					{ StepDeets.Jump, new Dictionary<string, string[]>() { 
						// jump steps reachable from the key step: 
						{ StepDeets.Base, new string[] {
                     "00110000",
                     "01100000",
                     "01010000",
                     "11000000",
                     "10010000",
                     "10100000", // jumps on the left-hand side
                     "00000011",
                     "00000110",
                     "00000101",
                     "00001100",
                     "00001001",
                     "00001010", // jumps on the right-hand side
                     "10001000", // both left arrows
                     "01000100",
                     "01001000",// left bottom and right-left or right bottom
                     "00100010",
                     "00101000",// left top and rightleft or right top
                     "00011000",
                     "00010100",
                     "00010010",
                     "00010001"} },   // left right and right left, right top, right bottom or right right
						{ "10000000",/*leftleft             */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
						{ "01000000",/*leftbottom           */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
						{ "00100000",/*lefttop              */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
						{ "00010000",/*leftright            */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
						{ "00001000",/*rightleft            */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
						{ "00000100",/*rightbottom          */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
						{ "00000010",/*righttop             */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
						{ "00000001",/*rightright           */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"01010000",/*leftbottom leftright  */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"10010000",/*leftleft leftright    */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"10100000",/*leftleft lefttop      */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} }, // jumps on the left-hand side
                        {"00000011",/*righttop rightright   */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00000110",/*rightbottom righttop  */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00000101",/*rightbottom rightright*/ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00001100",/*rightleft rightbottom */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00001001",/*rightleft rightright  */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00001010",/*rightleft righttop    */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} }, // jumps on the right-hand side
                        {"10001000",/*leftleft rightleft    */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} }, // both left arrows
                        {"01000100",/*leftbottom rightbottom*/ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },// left bottom and right-left or right bottom
                        {"00100010",/*lefttop righttop      */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00101000",/*lefttop rightleft     */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },// left top and rightleft or right top
                        {"00011000",/*leftright rightleft   */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00010100",/*leftright rightbottom */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00010010",/*leftright righttop    */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00010001",/*leftright rightright  */ new string[] {T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T} },
					} },
				} },
			};

		public static bool fromJump(string dance_style, string laststep)
		{
			string[] jumpsteps = steps_3d_dictionary[dance_style][StepDeets.Jump][StepDeets.Base];
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
				for (int b = 0; b < steps_3d_dictionary[dance_style][foottype][StepDeets.Base].Count(); b++)
				{
					if (steps_3d_dictionary[dance_style][foottype][laststep][b].Equals(T))
					// look up the previous step in the dictionary and get the list of steps that we can go to from here
					{
						temp1.Add(steps_3d_dictionary[dance_style][foottype][StepDeets.Base][b]);
					}
				}
				return temp1.ToArray();
			}
			else // if the request is not for left, right, or jump, then we return the union of left and right as the set of single steps
			{
				for (int b = 0; b < steps_3d_dictionary[dance_style][StepDeets.Left][StepDeets.Base].Count(); b++)
				{
					if (steps_3d_dictionary[dance_style][StepDeets.Left][laststep][b].Equals(T))
					// look up the previous step in the dictionary and get the list of steps that we can go to from here
					{
						temp1.Add(steps_3d_dictionary[dance_style][StepDeets.Left][StepDeets.Base][b]);
					}
				}
				for (int b = 0; b < steps_3d_dictionary[dance_style][StepDeets.Right][StepDeets.Base].Count(); b++)
				{
					if (steps_3d_dictionary[dance_style][StepDeets.Right][laststep][b].Equals(T))
					// look up the previous step in the dictionary and get the list of steps that we can go to from here
					{
						temp2.Add(steps_3d_dictionary[dance_style][StepDeets.Right][StepDeets.Base][b]);
					}
				}
				temp1.AddRange(temp2);
				return temp1.ToArray();
			}
		}

		public static bool isUDUDSQuintuple(string dance_style, string steps_i, string steps_i_plus_one)
		// up-down-up-down-side quintuples are not allowed after jumps, because they are too easy to start on the wrong foot
		{
			if (dance_style.Equals(StepDeets.DanceSingle))
			{
				if (((steps_i == "0100") && (steps_i_plus_one == "0010")) ||
					((steps_i == "0010") && (steps_i_plus_one == "0100")))
				{
					return true;
				}
			}
			else if (dance_style.Equals(StepDeets.DanceSolo))
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
			else if (dance_style.Equals(StepDeets.DanceDouble))
			{
				if (((steps_i == "01000000") && (steps_i_plus_one == "00100000")) ||
					((steps_i == "00100000") && (steps_i_plus_one == "01000000")) ||
					((steps_i == "00000100") && (steps_i_plus_one == "00000010")) ||
					((steps_i == "00000010") && (steps_i_plus_one == "00000100")))
				{
					return true;
				}
			}
			else if (dance_style.Equals(StepDeets.PumpSingle))
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

		public static string emptyStep(string dance_style)
		{
			if (dance_style.Equals(StepDeets.DanceSingle))
			{
				return "0000";
			}
			else if (dance_style.Equals(StepDeets.DanceSolo))
			{
				return "000000";
			}
			else if (dance_style.Equals(StepDeets.DanceDouble))
			{
				return "00000000";
			}
			else //if (dance_style.Equals(StepDeets.PumpSingle))
			{
				return "00000";
			}
		}
	}
}

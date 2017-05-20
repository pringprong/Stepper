using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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
		public const string Novice = "novice";
		public const string Easy = "easy";
		public const string Medium = "medium";
		public const string Hard = "hard";
		public const string Expert = "expert";
		public const string SM = "SM";
		public const string SSC = "SSC";

		private static Dictionary<string, Dictionary<string, Dictionary<string, string[]>>>
			steps_3d_dictionary = new Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> {
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
						{ "10000000",/*leftleft             */ new string[] {F, F, T, T, F, F, F} },
						{ "01000000",/*leftbottom           */ new string[] {T, F, T, T, T, F, F} },
						{ "00100000",/*lefttop              */ new string[] {T, T, F, F, F, F, F} },
						{ "00010000",/*leftright            */ new string[] {T, T, T, F, F, F, F} },
						{ "00001000",/*rightleft            */ new string[] {T, T, T, T, F, F, F} },
						{ "00000100",/*rightbottom          */ new string[] {F, T, F, T, T, F, T} },
						{ "00000010",/*righttop             */ new string[] {F, F, T, T, T, T, F} },
						{ "00000001",/*rightright           */ new string[] {F, F, F, T, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {T, T, T, F, F, F, F} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {T, T, T, T, T, T, T} },
                        {"01010000",/*leftbottom leftright  */ new string[] {T, T, T, F, F, F, F} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {T, F, T, T, T, F, F} },
                        {"10010000",/*leftleft leftright    */ new string[] {T, T, T, F, F, F, F} },
                        {"10100000",/*leftleft lefttop      */ new string[] {T, T, F, T, F, F, F} }, // jumps on the left-hand side
                        {"00000011",/*righttop rightright   */ new string[] {F, F, F, T, T, T, T} },
                        {"00000110",/*rightbottom righttop  */ new string[] {F, T, T, T, T, T, T} },
                        {"00000101",/*rightbottom rightright*/ new string[] {F, F, F, T, T, T, T} },
                        {"00001100",/*rightleft rightbottom */ new string[] {F, T, F, T, T, F, T} },
                        {"00001001",/*rightleft rightright  */ new string[] {F, F, F, T, T, T, T} },
                        {"00001010",/*rightleft righttop    */ new string[] {F, F, T, T, T, T, F} }, // jumps on the right-hand side
                        {"10001000",/*leftleft rightleft    */ new string[] {T, T, T, T, F, F, F} }, // both left arrows
                        {"01000100",/*leftbottom rightbottom*/ new string[] {F, T, F, T, T, F, T} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {T, T, T, T, F, F, F} },// left bottom and right-left or right bottom
                        {"00100010",/*lefttop righttop      */ new string[] {F, T, T, T, T, T, F} },
                        {"00101000",/*lefttop rightleft     */ new string[] {T, T, T, T, F, F, F} },// left top and rightleft or right top
                        {"00011000",/*leftright rightleft   */ new string[] {T, T, T, T, F, F, F} },
                        {"00010100",/*leftright rightbottom */ new string[] {F, T, F, T, T, F, T} },
                        {"00010010",/*leftright righttop    */ new string[] {F, F, T, T, T, T, F} },
                        {"00010001",/*leftright rightright  */ new string[] {F, F, F, T, T, T, T} },
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
						{ "10000000",/*leftleft             */ new string[] {T, T, T, T, F, F, F} },
						{ "01000000",/*leftbottom           */ new string[] {F, T, T, T, T, F, F} },
						{ "00100000",/*lefttop              */ new string[] {T, F, T, T, F, T, F} },
						{ "00010000",/*leftright            */ new string[] {F, F, F, T, T, T, T} },
						{ "00001000",/*rightleft            */ new string[] {F, F, F, F, T, T, T} },
						{ "00000100",/*rightbottom          */ new string[] {F, F, F, F, F, T, T} },
						{ "00000010",/*righttop             */ new string[] {F, F, F, F, T, F, T} },
						{ "00000001",/*rightright           */ new string[] {T, T, T, T, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {T, F, T, T, F, T, F} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {T, T, T, T, T, T, F} },
                        {"01010000",/*leftbottom leftright  */ new string[] {F, T, T, T, T, F, F} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {T, T, T, T, F, F, F} },
                        {"10010000",/*leftleft leftright    */ new string[] {T, T, T, T, F, F, F} },
                        {"10100000",/*leftleft lefttop      */ new string[] {T, T, T, T, F, F, F} }, // jumps on the left-hand side
                        {"00000011",/*righttop rightright   */ new string[] {F, F, F, F, T, F, T} },
                        {"00000110",/*rightbottom righttop  */ new string[] {T, T, T, T, T, T, T} },
                        {"00000101",/*rightbottom rightright*/ new string[] {F, F, F, F, F, T, T} },
                        {"00001100",/*rightleft rightbottom */ new string[] {F, F, F, F, T, T, T} },
                        {"00001001",/*rightleft rightright  */ new string[] {F, F, F, F, T, T, T} },
                        {"00001010",/*rightleft righttop    */ new string[] {F, F, F, F, T, T, T} }, // jumps on the right-hand side
                        {"10001000",/*leftleft rightleft    */ new string[] {T, T, T, T, F, F, F} }, // both left arrows
                        {"01000100",/*leftbottom rightbottom*/ new string[] {F, T, T, T, T, F, F} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {F, T, T, T, T, F, F} },// left bottom and right-left or right bottom
                        {"00100010",/*lefttop righttop      */ new string[] {T, F, T, T, F, T, F} },
                        {"00101000",/*lefttop rightleft     */ new string[] {T, F, T, T, F, T, F} },// left top and rightleft or right top
                        {"00011000",/*leftright rightleft   */ new string[] {F, F, F, T, T, T, T} },
                        {"00010100",/*leftright rightbottom */ new string[] {F, F, F, T, T, T, T} },
                        {"00010010",/*leftright righttop    */ new string[] {F, F, F, T, T, T, T} },
                        {"00010001",/*leftright rightright  */ new string[] {F, F, F, T, T, T, T} },
					} },
					{ StepDeets.Jump, new Dictionary<string, string[]>() { 
						// jump steps reachable from the key step: 
						{ StepDeets.Base, new string[] {
                     "00110000",/*lefttop leftright     */
                     "01100000",/*leftbottom lefttop    */
                     "01010000",/*leftbottom leftright  */
                     "11000000",/*leftleft leftbottom   */
                     "10010000",/*leftleft leftright    */
                     "10100000",/*leftleft lefttop      */ // jumps on the left-hand side
                     "00000011",/*righttop rightright   */
                     "00000110",/*rightbottom righttop  */
                     "00000101",/*rightbottom rightright*/
                     "00001100",/*rightleft rightbottom */
                     "00001001",/*rightleft rightright  */
                     "00001010",/*rightleft righttop    */ // jumps on the right-hand side
                     "10001000",/*leftleft rightleft    */ // both left arrows
                     "01000100",/*leftbottom rightbottom*/
                     "01001000",/*leftbottom rightleft  */ // left bottom and right-left or right bottom
                     "00100010",/*lefttop righttop      */
                     "00101000",/*lefttop rightleft     */ // left top and rightleft or right top
                     "00011000",/*leftright rightleft   */
                     "00010100",/*leftright rightbottom */
                     "00010010",/*leftright righttop    */
                     "00010001"} }, /*leftright rightright  */  // left right and right left, right top, right bottom or right right
						{ "10000000",/*leftleft             */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, T, T, F, F, F, F} },
						{ "01000000",/*leftbottom           */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, T, T, F, F, F, F} },
						{ "00100000",/*lefttop              */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, T, T, F, F, F, F} },
						{ "00010000",/*leftright            */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, T, T, T, T, T, F} },
						{ "00001000",/*rightleft            */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, T, T, T, T, F, T, T, T, T} },
						{ "00000100",/*rightbottom          */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, T, F, F, F, F, T, T, F} },
						{ "00000010",/*righttop             */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, T, F, F, F, F, T} },
						{ "00000001",/*rightright           */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, T, F, F, F, F, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, F, T, T, T, T, T, F, F, F} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, F, F, F, F, F, F, F} },
                        {"01010000",/*leftbottom leftright  */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, T, F, F, T, T, T} },
//                        {"11000000",/*leftleft leftbottom   */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
//                        {"10010000",/*leftleft leftright    */ new string[] {0, 0, 0, 0, T, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
//                        {"10100000",/*leftleft lefttop      */ new string[] {0, 0, 0, 0, 0, T, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} }, 
//                        {"00000011",/*righttop rightright   */ new string[] {0, 0, 0, 0, 0, 0, T, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
//                        {"00000110",/*rightbottom righttop  */ new string[] {0, 0, 0, 0, 0, 0, 0, T, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
 //                       {"00000101",/*rightbottom rightright*/ new string[] {0, 0, 0, 0, 0, 0, 0, 0, T, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
 //                       {"00001100",/*rightleft rightbottom */ new string[] {0, 0, 0, 0, 0, 0, 0, 0, 0, T, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
   //                     {"00001001",/*rightleft rightright  */ new string[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, T, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
     //                   {"00001010",/*rightleft righttop    */ new string[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, T, 0, 0, 0, 0, 0, 0, 0, 0, 0} }, 
       //                 {"10001000",/*leftleft rightleft    */ new string[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, T, 0, 0, 0, 0, 0, 0, 0, 0} }, 
         //               {"01000100",/*leftbottom rightbottom*/ new string[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, T, 0, 0, 0, 0, 0, 0, 0} },
           //             {"01001000",/*leftbottom rightleft  */ new string[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, T, 0, 0, 0, 0, 0, 0} },
             //           {"00100010",/*lefttop righttop      */ new string[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, T, 0, 0, 0, 0, 0} },
               //         {"00101000",/*lefttop rightleft     */ new string[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, T, 0, 0, 0, 0} },
                 //       {"00011000",/*leftright rightleft   */ new string[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, T, 0, 0, 0} },
                   //     {"00010100",/*leftright rightbottom */ new string[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, T, 0, 0} },
                     //   {"00010010",/*leftright righttop    */ new string[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, T, 0} },
                       // {"00010001",/*leftright rightright  */ new string[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, T} },
					} },
				} },
				{ StepDeets.PumpSingle, new Dictionary<string, Dictionary<string, string[]>> {
					{ StepDeets.Left, new Dictionary<string, string[]>() { 
						// single steps reachable with the left foot while the right foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] { "10000"/*downleft*/, "01000"/*upleft*/, "00100"/*center*/, "00010"/*upright*/, "00001"/*downright*/ } },
						{ "10000" /*downleft          */, new string[] {T, T, T, T, T} },
						{ "01000" /*upleft            */, new string[] {T, T, T, T, T} },
						{ "00100" /*center            */, new string[] {T, T, T, T, T} },
						{ "00010" /*upright           */, new string[] {T, T, T, T, T} },
						{ "00001" /*downright         */, new string[] {T, T, T, T, T} },
						{ "00011" /*upright downright */, new string[] {T, T, T, T, T} },
						{ "00110" /*center upright    */, new string[] {T, T, T, F, T} }, // left foot can't go on upright
						{ "00101" /*center downright  */, new string[] {T, T, T, T, F} }, // left foot can't go on downright
						{ "01100" /*upleft center     */, new string[] {T, T, F, T, T} }, // left foot can't go on center
						{ "01001" /*upleft downright  */, new string[] {T, T, T, T, F} }, // left foot can't go on downright
						{ "01010" /*upleft upright    */, new string[] {T, T, T, F, T} }, // left foot can't go on upright because the right foot is on it
						{ "10001" /*downleft downright*/, new string[] {T, T, T, T, F} }, // left foot can't go on downright
						{ "10010" /*downleft upright  */, new string[] {T, T, T, F, T} }, // left foot can't go on upright
						{ "10100" /*downleft center   */, new string[] {T, T, F, T, T} }, // left foot can't go on center
						{ "11000" /*downleft upleft   */, new string[] {T, T, T, T, T} },
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] { "10000"/*downleft*/, "01000"/*upleft*/, "00100"/*center*/, "00010"/*upright*/, "00001"/*downright*/ } },
						{ "10000" /*downleft          */, new string[] {T, T, T, T, T} },
						{ "01000" /*upleft            */, new string[] {T, T, T, T, T} },
						{ "00100" /*center            */, new string[] {T, T, T, T, T} },
						{ "00010" /*upright           */, new string[] {T, T, T, T, T} },
						{ "00001" /*downright         */, new string[] {T, T, T, T, T} },
						{ "00011" /*upright downright */, new string[] {T, T, T, T, T} },
						{ "00110" /*center upright    */, new string[] {T, T, F, T, T} }, // right foot can't go on center
						{ "00101" /*center downright  */, new string[] {T, T, F, T, T} }, // right foot can't go on center
						{ "01100" /*upleft center     */, new string[] {T, F, T, T, T} }, // right foot can't go on upleft
						{ "01001" /*upleft downright  */, new string[] {T, F, T, T, T} }, // right foot can't go on upleft
						{ "01010" /*upleft upright    */, new string[] {T, F, T, T, T} }, // right foot can't go on upleft because the left foot is on it
						{ "10001" /*downleft downright*/, new string[] {F, T, T, T, T} }, // right foot can't go on downleft
						{ "10010" /*downleft upright  */, new string[] {F, T, T, T, T} }, // right foot can't go on downleft
						{ "10100" /*downleft center   */, new string[] {F, T, T, T, T} }, // right foot can't go on downleft
						{ "11000" /*downleft upleft   */, new string[] {T, T, T, T, T} },
					} },
					{ StepDeets.Jump, new Dictionary<string, string[]>() { 
						// jump steps reachable from the key step: all reachable
						{ StepDeets.Base, new string[] { 
							"00011"/*upright downright*/, 
							"00110"/*center upright*/, 
							"00101"/*center downright*/, 
							"01100"/*upleft center*/, 
							"01001"/*upleft downright*/, 
							"01010"/*upleft upright*/, 
							"10001"/*downleft downright*/, 
							"10010"/*downleft upright*/, 
							"10100"/*downleft center*/, 
							"11000"/*downleft upleft*/ } },
						{ "10000" /*downleft          */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "01000" /*upleft            */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "00100" /*center            */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "00010" /*upright           */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "00001" /*downright         */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "00011" /*upright downright */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "00110" /*center upright    */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "00101" /*center downright  */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "01100" /*upleft center     */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "01001" /*upleft downright  */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "01010" /*upleft upright    */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "10001" /*downleft downright*/, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "10010" /*downleft upright  */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "10100" /*downleft center   */, new string[] {T, T, T, T, T, T, T, T, T, T} },
						{ "11000" /*downleft upleft   */, new string[] {T, T, T, T, T, T, T, T, T, T} },
					} },
				} },

			};

		public static bool fromJump(string dance_style, string laststep)
		{
			string[] jumpsteps = steps_3d_dictionary[dance_style][StepDeets.Jump][StepDeets.Base];
			if (jumpsteps.Contains(laststep))
			{
				return true;
			}
			else
			{
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

		public static string stepTitle(string dance_style)
		{
			if (dance_style.Equals(StepDeets.DanceSingle))
			{
				return "Dance Single";
			}
			else if (dance_style.Equals(StepDeets.DanceSolo))
			{
				return "Dance Solo";
			}
			else if (dance_style.Equals(StepDeets.DanceDouble))
			{
				return "Dance Double";
			}
			else //if (dance_style.Equals(StepDeets.PumpSingle))
			{
				return "Pump Single";
			}
		}

		public static int numPlaces(string dance_style)
		{
			return emptyStep(dance_style).Length;
		}

		public static string[] getInitialStep(string dance_style)
		{
			if (dance_style.Equals(StepDeets.DanceSingle))
			{
				return new string[] { StepDeets.Left, "0001" };
			}
			else if (dance_style.Equals(StepDeets.DanceSolo))
			{
				return new string[] { StepDeets.Left, "000001" };
			}
			else if (dance_style.Equals(StepDeets.DanceDouble))
			{
				return new string[] { StepDeets.Left, "00000001" };
			}
			else //if (dance_style.Equals(StepDeets.PumpSingle))
			{
				return new string[] { StepDeets.Left, "00001" };
			}
		}

		public static string levelTitle(string level)
		{
			if (level.Equals(StepDeets.Novice))
			{
				return "Novice";
			}
			else if (level.Equals(StepDeets.Easy))
			{
				return "Easy";
			}
			else if (level.Equals(StepDeets.Medium))
			{
				return "Medium";
			}
			else if (level.Equals(StepDeets.Hard))
			{
				return "Hard";
			}
			else //if (level.Equals(StepDeets.Expert))
			{
				return "Expert";
			}
		}

		public static Color levelColor(string level)
		{
			if (level.Equals(StepDeets.Novice))
			{
				return System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			}
			else if (level.Equals(StepDeets.Easy))
			{
				return System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			}
			else if (level.Equals(StepDeets.Medium))
			{
				return System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			}
			else if (level.Equals(StepDeets.Hard))
			{
				return System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			}
			else //if (level.Equals(StepDeets.Expert))
			{
				return System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			}
		}

		public static Color labelColor(string level)
		{
			if (level.Equals(StepDeets.Novice))
			{
				return System.Drawing.Color.Fuchsia;
			}
			else if (level.Equals(StepDeets.Easy))
			{
				return System.Drawing.Color.LawnGreen;
			}
			else if (level.Equals(StepDeets.Medium))
			{
				return System.Drawing.Color.Khaki;
			}
			else if (level.Equals(StepDeets.Hard))
			{
				return System.Drawing.Color.OrangeRed;
			}
			else //if (level.Equals(StepDeets.Expert))
			{
				return System.Drawing.Color.DeepSkyBlue;
			}
		}
	}
}

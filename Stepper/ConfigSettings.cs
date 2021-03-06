﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Stepper
{
	[Serializable] public class ConfigSettings
	{

		public string DefaultSourceFolder { get; set; }
		public string DefaultDestinationFolder { get; set; }
		public Dictionary<string, Dictionary<string, NotesetParameters>> default_params { get; set; }
		public Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> steps_3d_dictionary {get; set;}
		public Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> steps_3d_dictionary_default { get; set; }
		public Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> steps_3d_dictionary_temp { get; set; }
		public string T = StepDeets.T;
		public string F = StepDeets.F;


		public ConfigSettings()
		{
			DefaultSourceFolder = "C:\\Games\\StepMania 5\\Songs\\Test";
			DefaultDestinationFolder = "C:\\Games\\StepMania 5\\Songs\\Test";
			default_params = new Dictionary<string, Dictionary<string, NotesetParameters>> {
				{ StepDeets.DanceSingle, new Dictionary<string, NotesetParameters> {
					{ StepDeets.Novice, new NotesetParameters(StepDeets.DanceSingle, StepDeets.Novice, true, true, 50, 100, 0, 20, true, true, 10, 100, false)},
					{ StepDeets.Easy, new NotesetParameters(StepDeets.DanceSingle, StepDeets.Easy, true, false, 100, 100, 0, 20, true, false, 15, 95, false)},
					{ StepDeets.Medium, new NotesetParameters(StepDeets.DanceSingle, StepDeets.Medium, false, true, 100, 100, 15, 20, false, true, 20, 90, false)},
					{ StepDeets.Hard, new NotesetParameters(StepDeets.DanceSingle, StepDeets.Hard, false, false, 100, 80, 0, 0, false, false, 25, 85, false)},
					{ StepDeets.Expert, new NotesetParameters(StepDeets.DanceSingle, StepDeets.Expert, true, false, 100, 85, 20, 50, true, true, 30, 80, true)}, 
				} },
				{ StepDeets.DanceSolo, new Dictionary<string, NotesetParameters> {
					{ StepDeets.Novice, new NotesetParameters(StepDeets.DanceSolo, StepDeets.Novice, true, false, 50, 100, 5, 20, true, false, 35, 75, false)},
					{ StepDeets.Easy, new NotesetParameters(StepDeets.DanceSolo, StepDeets.Easy, false, false, 100, 90, 0, 20, false, true, 40, 70, false)},
					{ StepDeets.Medium, new NotesetParameters(StepDeets.DanceSolo, StepDeets.Medium, false, true, 100, 100, 19, 20, false, false, 45, 65, false)},
					{ StepDeets.Hard, new NotesetParameters(StepDeets.DanceSolo, StepDeets.Hard, true, false, 100, 70, 0, 5, true, true, 50, 60, false)},
					{ StepDeets.Expert, new NotesetParameters(StepDeets.DanceSolo, StepDeets.Expert, true, true, 100, 80, 15, 30, true, true, 55, 55, true)}, 
				} },
				{ StepDeets.DanceDouble, new Dictionary<string, NotesetParameters> {
					{ StepDeets.Novice, new NotesetParameters(StepDeets.DanceDouble, StepDeets.Novice, false, true, 50, 90, 0, 20, false, true, 60, 50, false)},
					{ StepDeets.Easy, new NotesetParameters(StepDeets.DanceDouble, StepDeets.Easy, false, false, 90, 100, 3, 20, false, false, 65, 45, false)},
					{ StepDeets.Medium, new NotesetParameters(StepDeets.DanceDouble, StepDeets.Medium, true, false, 100, 100, 20, 20, true, true, 70, 40, false)},
					{ StepDeets.Hard, new NotesetParameters(StepDeets.DanceDouble, StepDeets.Hard, true, true, 100, 100, 90, 10, true, true, 75, 35, false)},
					{ StepDeets.Expert, new NotesetParameters(StepDeets.DanceDouble, StepDeets.Expert, true, false, 100, 60, 60, 20, true, false, 80, 30, true)}, 
				} },
				{ StepDeets.PumpSingle, new Dictionary<string, NotesetParameters> {
					{ StepDeets.Novice, new NotesetParameters(StepDeets.PumpSingle, StepDeets.Novice, false, false, 50, 100, 5, 100, true, true, 85, 25, false)},
					{ StepDeets.Easy, new NotesetParameters(StepDeets.PumpSingle, StepDeets.Easy, true, false, 100, 100, 0, 20, true, true, 90, 20, false)},
					{ StepDeets.Medium, new NotesetParameters(StepDeets.PumpSingle, StepDeets.Medium, true, true, 100, 100, 0, 20, true, true, 95, 15, false)},
					{ StepDeets.Hard, new NotesetParameters(StepDeets.PumpSingle, StepDeets.Hard, true, false, 100, 100, 0, 20, true, true, 100, 10, false)},
					{ StepDeets.Expert, new NotesetParameters(StepDeets.PumpSingle, StepDeets.Expert, false, true, 100, 100, 0, 20, true, true, 0, 5, true)}, 
				} },
			};

			steps_3d_dictionary = new Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> {
				{ StepDeets.DanceSingle, new Dictionary<string, Dictionary<string, string[]>> {
					{ StepDeets.Left, new Dictionary<string, string[]>() { 
						// single steps reachable with the left foot while the right foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"1000", "0100", "0010", "0001"} },
						{ "1000", new string[] {T, T, T, F} },
						{ "0100", new string[] {T, T, T, F} },
						{ "0010", new string[] {T, T, T, F} },
						{ "0001", new string[] {T, T, T, F} },
						{ "0011", new string[] {T, T, T, F} },
						{ "0101", new string[] {T, T, T, F} },
						{ "1001", new string[] {T, T, T, F} },
						{ "0110", new string[] {T, T, T, F} },
						{ "1010", new string[] {T, T, F, F} }, // top and left jump; don't try to put the left foot onto the top arrow, because the right foot is on it
						{ "1100", new string[] {T, F, T, F} }, // bottom and left jump don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"1000", "0100", "0010", "0001"} },
						{ "1000", new string[] {F, T, T, T} },
						{ "0100", new string[] {F, T, T, T} },
						{ "0010", new string[] {F, T, T, T} },
						{ "0001", new string[] {F, T, T, T} },
						{ "0011", new string[] {F, T, F, T} }, // top and right
						{ "0101", new string[] {F, F, T, T} }, // bottom and right
						{ "1001", new string[] {F, T, T, T} },
						{ "0110", new string[] {F, T, T, T} },
						{ "1010", new string[] {F, T, T, T} }, 
						{ "1100", new string[] {F, T, T, T} }, 
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
						{ StepDeets.Base, new string[] {"100000" /*left*/, "010000" /*topleft*/, "001000"/*bottom*/, "000100"/*top*/, "000010"/*topright*/, "000001"/*right*/} },
						{ "100000", /* left                */ new string[] {T, T, T, T, F, F} },
						{ "010000", /* topleft             */ new string[] {T, T, T, T, F, F} },
						{ "001000", /* bottom              */ new string[] {T, T, T, T, F, F} },
						{ "000100", /* top                 */ new string[] {T, T, T, T, F, F} },
						{ "000010", /* topright            */ new string[] {T, T, T, T, F, F} },
						{ "000001", /* right               */ new string[] {T, T, T, T, F, F} },
						{ "110000", /* left and topleft    */ new string[] {T, T, T, T, F, F} },
						{ "101000", /* left and bottom     */ new string[] {T, T, F, T, F, F} },// don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
						{ "100100", /* left and top        */ new string[] {T, T, T, F, F, F} },// don't try to put the left foot onto the top arrow, because the right foot is on it right now
						{ "100010", /* left and topright   */ new string[] {T, T, T, T, F, F} },
						{ "100001", /* left and right      */ new string[] {T, T, T, T, F, F} },
						{ "011000", /* topleft and bottom  */ new string[] {T, T, F, T, F, F} },// don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
						{ "010100", /* topleft and top     */ new string[] {T, T, T, F, F, F} },// don't try to put the left foot onto the top arrow, because the right foot is on it right now
						{ "010010", /* topleft and topright*/ new string[] {T, T, T, T, F, F} },
						{ "010001", /* topleft and right   */ new string[] {T, T, T, T, F, F} },
						{ "001100", /* bottom and top      */ new string[] {T, T, T, T, F, F} }, 
						{ "001010", /* bottom and topright */ new string[] {T, T, T, T, F, F} }, 
						{ "001001", /* bottom and right    */ new string[] {T, T, T, T, F, F} },
						{ "000110", /* top and topright    */ new string[] {T, T, T, T, F, F} },
						{ "000101", /* top and right       */ new string[] {T, T, T, T, F, F} }, 
						{ "000011", /* topright and right  */ new string[] {T, T, T, T, F, F} }, 
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"100000" /*left*/, "010000" /*topleft*/, "001000"/*bottom*/, "000100"/*top*/, "000010"/*topright*/, "000001"/*right*/} },
						{ "100000", /* left                */ new string[] {F, F, T, T, T, T} },
						{ "010000", /* topleft             */ new string[] {F, F, T, T, T, T} },
						{ "001000", /* bottom              */ new string[] {F, F, T, T, T, T} },
						{ "000100", /* top                 */ new string[] {F, F, T, T, T, T} },
						{ "000010", /* topright            */ new string[] {F, F, T, T, T, T} },
						{ "000001", /* right               */ new string[] {F, F, T, T, T, T} },
						{ "110000", /* left and topleft    */ new string[] {F, F, T, T, T, T} },
						{ "101000", /* left and bottom     */ new string[] {F, F, T, T, T, T} },
						{ "100100", /* left and top        */ new string[] {F, F, T, T, T, T} },
						{ "100010", /* left and topright   */ new string[] {F, F, T, T, T, T} },
						{ "100001", /* left and right      */ new string[] {F, F, T, T, T, T} },
						{ "011000", /* topleft and bottom  */ new string[] {F, F, T, T, T, T} },
						{ "010100", /* topleft and top     */ new string[] {F, F, T, T, T, T} },
						{ "010010", /* topleft and topright*/ new string[] {F, F, T, T, T, T} },
						{ "010001", /* topleft and right   */ new string[] {F, F, T, T, T, T} },
						{ "001100", /* bottom and top      */ new string[] {F, F, T, T, T, T} }, 
						{ "001010", /* bottom and topright */ new string[] {F, F, F, T, T, T} },// don't try to put the right foot onto the bottom arrow, because the left foot is on it right now 
						{ "001001", /* bottom and right    */ new string[] {F, F, F, T, T, T} },// don't try to put the right foot onto the bottom arrow, because the left foot is on it right now
						{ "000110", /* top and topright    */ new string[] {F, F, T, F, T, T} },// don't try to put the right foot onto the top arrow, because the left foot is on it right now
						{ "000101", /* top and right       */ new string[] {F, F, T, F, T, T} },// don't try to put the right foot onto the top arrow, because the left foot is on it right now 
						{ "000011", /* topright and right  */ new string[] {F, F, T, T, T, T} }, 
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
							"00000001", /*rightright*/
						} },
						{ "10000000",/*leftleft             */ new string[] {T, T, T, F, F, F, F, F} },
						{ "01000000",/*leftbottom           */ new string[] {T, T, T, F, F, F, F, F} },
						{ "00100000",/*lefttop              */ new string[] {T, T, T, F, F, F, F, F} },
						{ "00010000",/*leftright            */ new string[] {T, T, T, T, F, F, F, F} },
						{ "00001000",/*rightleft            */ new string[] {T, T, T, T, T, F, F, F} },
						{ "00000100",/*rightbottom          */ new string[] {F, T, F, T, T, T, T, F} },
						{ "00000010",/*righttop             */ new string[] {F, F, T, T, T, F, T, F} },
						{ "00000001",/*rightright           */ new string[] {F, F, F, T, T, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {T, T, T, F, F, F, F, F} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {T, T, T, F, F, F, F, F} },
                        {"01010000",/*leftbottom leftright  */ new string[] {T, T, T, F, F, F, F, F} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {T, F, T, F, F, F, F, F} },
                        {"10010000",/*leftleft leftright    */ new string[] {T, T, T, F, F, F, F, F} },
                        {"10100000",/*leftleft lefttop      */ new string[] {T, T, F, F, F, F, F, F} }, // jumps on the left-hand side
                        {"00000011",/*righttop rightright   */ new string[] {F, F, F, T, T, T, T, F} },
                        {"00000110",/*rightbottom righttop  */ new string[] {F, F, F, T, T, T, T, F} },
                        {"00000101",/*rightbottom rightright*/ new string[] {F, F, F, T, T, T, T, F} },
                        {"00001100",/*rightleft rightbottom */ new string[] {F, T, F, T, T, F, T, F} },
                        {"00001001",/*rightleft rightright  */ new string[] {F, F, F, T, T, T, T, F} },
                        {"00001010",/*rightleft righttop    */ new string[] {F, F, T, T, T, T, F, F} }, // jumps on the right-hand side
                        {"10001000",/*leftleft rightleft    */ new string[] {T, T, T, T, F, F, F, F} }, // both left arrows
                        {"01000100",/*leftbottom rightbottom*/ new string[] {F, T, F, T, T, F, T, F} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {T, T, T, T, F, F, F, F} },// left bottom and right-left or right bottom
                        {"00100010",/*lefttop righttop      */ new string[] {F, F, T, T, T, T, F, F} },
                        {"00101000",/*lefttop rightleft     */ new string[] {T, T, T, T, F, F, F, F} },// left top and rightleft or right top
                        {"00011000",/*leftright rightleft   */ new string[] {T, T, T, T, F, F, F, F} },
                        {"00010100",/*leftright rightbottom */ new string[] {F, T, F, T, T, F, T, F} },
                        {"00010010",/*leftright righttop    */ new string[] {F, F, T, T, T, T, F, F} },
                        {"00010001",/*leftright rightright  */ new string[] {F, F, F, T, T, T, T, F} },
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {
							"10000000", /*leftleft*/
							"01000000", /*leftbottom*/
							"00100000", /*lefttop*/
							"00010000", /*leftright*/
							"00001000", /*rightleft*/
							"00000100", /*rightbottom*/
							"00000010", /*righttop*/
							"00000001", /*rightright*/} },
						{ "10000000",/*leftleft             */ new string[] {T, T, T, T, T, F, F, F} },
						{ "01000000",/*leftbottom           */ new string[] {F, T, T, T, T, T, F, F} },
						{ "00100000",/*lefttop              */ new string[] {F, T, T, T, T, F, T, F} },
						{ "00010000",/*leftright            */ new string[] {F, F, F, T, T, T, T, T} },
						{ "00001000",/*rightleft            */ new string[] {F, F, F, F, T, T, T, T} },
						{ "00000100",/*rightbottom          */ new string[] {F, F, F, F, F, T, T, T} },
						{ "00000010",/*righttop             */ new string[] {F, F, F, F, F, T, T, T} },
						{ "00000001",/*rightright           */ new string[] {F, F, F, F, F, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {F, T, F, T, T, F, T, F} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {F, T, T, T, T, F, F, F} },
                        {"01010000",/*leftbottom leftright  */ new string[] {F, F, T, T, T, T, F, F} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {F, T, T, T, T, F, F, F} },
                        {"10010000",/*leftleft leftright    */ new string[] {F, T, T, T, T, F, F, F} },
                        {"10100000",/*leftleft lefttop      */ new string[] {F, T, T, T, T, F, F, F} }, // jumps on the left-hand side
                        {"00000011",/*righttop rightright   */ new string[] {F, F, F, F, F, T, F, T} },
                        {"00000110",/*rightbottom righttop  */ new string[] {F, F, F, F, F, T, T, T} },
                        {"00000101",/*rightbottom rightright*/ new string[] {F, F, F, F, F, F, T, T} },
                        {"00001100",/*rightleft rightbottom */ new string[] {F, F, F, F, F, T, T, T} },
                        {"00001001",/*rightleft rightright  */ new string[] {F, F, F, F, F, T, T, T} },
                        {"00001010",/*rightleft righttop    */ new string[] {F, F, F, F, F, T, T, T} }, // jumps on the right-hand side
                        {"10001000",/*leftleft rightleft    */ new string[] {F, T, T, T, T, F, F, F} }, // both left arrows
                        {"01000100",/*leftbottom rightbottom*/ new string[] {F, F, T, T, T, T, F, F} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {F, F, T, T, T, T, F, F} },// left bottom and right-left or right bottom
                        {"00100010",/*lefttop righttop      */ new string[] {F, T, F, T, T, F, T, F} },
                        {"00101000",/*lefttop rightleft     */ new string[] {F, T, F, T, T, F, T, F} },// left top and rightleft or right top
                        {"00011000",/*leftright rightleft   */ new string[] {F, F, F, F, T, T, T, T} },
                        {"00010100",/*leftright rightbottom */ new string[] {F, F, F, F, T, T, T, T} },
                        {"00010010",/*leftright righttop    */ new string[] {F, F, F, F, T, T, T, T} },
                        {"00010001",/*leftright rightright  */ new string[] {F, F, F, F, T, T, T, T} },
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
                     "00010001" /*leftright rightright  */} },   // left right and right left, right top, right bottom or right right
						{"10000000",/*leftleft              */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, F, T, F, F, F, F} },
						{"01000000",/*leftbottom            */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, F, T, F, F, F, F} },
						{"00100000",/*lefttop               */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, T, T, F, F, F, F} },
						{"00010000",/*leftright             */ new string[] {T, T, T, T, T, T, F, F, F, T, F, T, T, T, T, T, T, T, T, T, T} },
						{"00001000",/*rightleft             */ new string[] {T, F, T, F, F, F, T, T, T, T, T, T, T, T, T, T, F, T, T, T, T} },
						{"00000100",/*rightbottom           */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, T, F, F, F, F, T, T, T} },
						{"00000010",/*righttop              */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, T, F, F, T, T, T} },
						{"00000001",/*rightright            */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, F, F, F, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, T, T, T, T, T, F} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, F, F, F, T, T, T, F} },
                        {"01010000",/*leftbottom leftright  */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, F, F, T, T, T, F} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, F, T, T, F, F, F} },
                        {"10010000",/*leftleft leftright    */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, F, T, T, F, F, F} },
                        {"10100000",/*leftleft lefttop      */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, F, T, T, F, F, F} }, 
                        {"00000011",/*righttop rightright   */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, F, F, T, T, T, T} },
                        {"00000110",/*rightbottom righttop  */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, T, F, T, T, F, F, T} },
                        {"00000101",/*rightbottom rightright*/ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, F, F, T, T, T, T} },
                        {"00001100",/*rightleft rightbottom */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, T, T, F, T, T, T, T, T} },
                        {"00001001",/*rightleft rightright  */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, F, F, T, T, T, T} },
                        {"00001010",/*rightleft righttop    */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, T, T, T, T, T, T, T} }, 
                        {"10001000",/*leftleft rightleft    */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, T, T, T, F, F, F} }, 
                        {"01000100",/*leftbottom rightbottom*/ new string[] {F, T, T, F, F, F, F, T, F, T, F, F, T, T, T, F, F, T, T, F, T} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {T, T, T, T, T, F, T, F, T, T, F, T, T, T, T, F, T, T, T, T, T} },
                        {"00100010",/*lefttop righttop      */ new string[] {T, T, F, F, F, F, F, T, F, F, F, T, T, T, F, T, T, T, F, T, T} },
                        {"00101000",/*lefttop rightleft     */ new string[] {T, T, T, F, T, T, T, F, T, T, F, T, T, F, T, T, T, T, T, T, T} },
                        {"00011000",/*leftright rightleft   */ new string[] {T, T, T, F, T, F, F, T, F, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00010100",/*leftright rightbottom */ new string[] {T, F, T, T, F, T, F, T, T, T, T, T, T, T, T, F, T, T, T, T, T} },
                        {"00010010",/*leftright righttop    */ new string[] {T, F, T, T, F, T, T, T, F, T, T, T, T, F, T, T, T, T, T, T, T } },
                        {"00010001",/*leftright rightright  */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, T, F, T, F, T, T, T, T} },
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


			steps_3d_dictionary_default = new Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> {
				{ StepDeets.DanceSingle, new Dictionary<string, Dictionary<string, string[]>> {
					{ StepDeets.Left, new Dictionary<string, string[]>() { 
						// single steps reachable with the left foot while the right foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"1000", "0100", "0010", "0001"} },
						{ "1000", new string[] {T, T, T, F} },
						{ "0100", new string[] {T, T, T, F} },
						{ "0010", new string[] {T, T, T, F} },
						{ "0001", new string[] {T, T, T, F} },
						{ "0011", new string[] {T, T, T, F} },
						{ "0101", new string[] {T, T, T, F} },
						{ "1001", new string[] {T, T, T, F} },
						{ "0110", new string[] {T, T, T, F} },
						{ "1010", new string[] {T, T, F, F} }, // top and left jump; don't try to put the left foot onto the top arrow, because the right foot is on it
						{ "1100", new string[] {T, F, T, F} }, // bottom and left jump don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"1000", "0100", "0010", "0001"} },
						{ "1000", new string[] {F, T, T, T} },
						{ "0100", new string[] {F, T, T, T} },
						{ "0010", new string[] {F, T, T, T} },
						{ "0001", new string[] {F, T, T, T} },
						{ "0011", new string[] {F, T, F, T} }, // top and right
						{ "0101", new string[] {F, F, T, T} }, // bottom and right
						{ "1001", new string[] {F, T, T, T} },
						{ "0110", new string[] {F, T, T, T} },
						{ "1010", new string[] {F, T, T, T} }, 
						{ "1100", new string[] {F, T, T, T} }, 
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
						{ StepDeets.Base, new string[] {"100000" /*left*/, "010000" /*topleft*/, "001000"/*bottom*/, "000100"/*top*/, "000010"/*topright*/, "000001"/*right*/} },
						{ "100000", /* left                */ new string[] {T, T, T, T, F, F} },
						{ "010000", /* topleft             */ new string[] {T, T, T, T, F, F} },
						{ "001000", /* bottom              */ new string[] {T, T, T, T, F, F} },
						{ "000100", /* top                 */ new string[] {T, T, T, T, F, F} },
						{ "000010", /* topright            */ new string[] {T, T, T, T, F, F} },
						{ "000001", /* right               */ new string[] {T, T, T, T, F, F} },
						{ "110000", /* left and topleft    */ new string[] {T, T, T, T, F, F} },
						{ "101000", /* left and bottom     */ new string[] {T, T, F, T, F, F} },// don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
						{ "100100", /* left and top        */ new string[] {T, T, T, F, F, F} },// don't try to put the left foot onto the top arrow, because the right foot is on it right now
						{ "100010", /* left and topright   */ new string[] {T, T, T, T, F, F} },
						{ "100001", /* left and right      */ new string[] {T, T, T, T, F, F} },
						{ "011000", /* topleft and bottom  */ new string[] {T, T, F, T, F, F} },// don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
						{ "010100", /* topleft and top     */ new string[] {T, T, T, F, F, F} },// don't try to put the left foot onto the top arrow, because the right foot is on it right now
						{ "010010", /* topleft and topright*/ new string[] {T, T, T, T, F, F} },
						{ "010001", /* topleft and right   */ new string[] {T, T, T, T, F, F} },
						{ "001100", /* bottom and top      */ new string[] {T, T, T, T, F, F} }, 
						{ "001010", /* bottom and topright */ new string[] {T, T, T, T, F, F} }, 
						{ "001001", /* bottom and right    */ new string[] {T, T, T, T, F, F} },
						{ "000110", /* top and topright    */ new string[] {T, T, T, T, F, F} },
						{ "000101", /* top and right       */ new string[] {T, T, T, T, F, F} }, 
						{ "000011", /* topright and right  */ new string[] {T, T, T, T, F, F} }, 
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"100000" /*left*/, "010000" /*topleft*/, "001000"/*bottom*/, "000100"/*top*/, "000010"/*topright*/, "000001"/*right*/} },
						{ "100000", /* left                */ new string[] {F, F, T, T, T, T} },
						{ "010000", /* topleft             */ new string[] {F, F, T, T, T, T} },
						{ "001000", /* bottom              */ new string[] {F, F, T, T, T, T} },
						{ "000100", /* top                 */ new string[] {F, F, T, T, T, T} },
						{ "000010", /* topright            */ new string[] {F, F, T, T, T, T} },
						{ "000001", /* right               */ new string[] {F, F, T, T, T, T} },
						{ "110000", /* left and topleft    */ new string[] {F, F, T, T, T, T} },
						{ "101000", /* left and bottom     */ new string[] {F, F, T, T, T, T} },
						{ "100100", /* left and top        */ new string[] {F, F, T, T, T, T} },
						{ "100010", /* left and topright   */ new string[] {F, F, T, T, T, T} },
						{ "100001", /* left and right      */ new string[] {F, F, T, T, T, T} },
						{ "011000", /* topleft and bottom  */ new string[] {F, F, T, T, T, T} },
						{ "010100", /* topleft and top     */ new string[] {F, F, T, T, T, T} },
						{ "010010", /* topleft and topright*/ new string[] {F, F, T, T, T, T} },
						{ "010001", /* topleft and right   */ new string[] {F, F, T, T, T, T} },
						{ "001100", /* bottom and top      */ new string[] {F, F, T, T, T, T} }, 
						{ "001010", /* bottom and topright */ new string[] {F, F, F, T, T, T} },// don't try to put the right foot onto the bottom arrow, because the left foot is on it right now 
						{ "001001", /* bottom and right    */ new string[] {F, F, F, T, T, T} },// don't try to put the right foot onto the bottom arrow, because the left foot is on it right now
						{ "000110", /* top and topright    */ new string[] {F, F, T, F, T, T} },// don't try to put the right foot onto the top arrow, because the left foot is on it right now
						{ "000101", /* top and right       */ new string[] {F, F, T, F, T, T} },// don't try to put the right foot onto the top arrow, because the left foot is on it right now 
						{ "000011", /* topright and right  */ new string[] {F, F, T, T, T, T} }, 
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
							"00000001", /*rightright*/
						} },
						{ "10000000",/*leftleft             */ new string[] {T, T, T, F, F, F, F, F} },
						{ "01000000",/*leftbottom           */ new string[] {T, T, T, F, F, F, F, F} },
						{ "00100000",/*lefttop              */ new string[] {T, T, T, F, F, F, F, F} },
						{ "00010000",/*leftright            */ new string[] {T, T, T, T, F, F, F, F} },
						{ "00001000",/*rightleft            */ new string[] {T, T, T, T, T, F, F, F} },
						{ "00000100",/*rightbottom          */ new string[] {F, T, F, T, T, T, T, F} },
						{ "00000010",/*righttop             */ new string[] {F, F, T, T, T, F, T, F} },
						{ "00000001",/*rightright           */ new string[] {F, F, F, T, T, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {T, T, T, F, F, F, F, F} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {T, T, T, F, F, F, F, F} },
                        {"01010000",/*leftbottom leftright  */ new string[] {T, T, T, F, F, F, F, F} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {T, F, T, F, F, F, F, F} },
                        {"10010000",/*leftleft leftright    */ new string[] {T, T, T, F, F, F, F, F} },
                        {"10100000",/*leftleft lefttop      */ new string[] {T, T, F, F, F, F, F, F} }, // jumps on the left-hand side
                        {"00000011",/*righttop rightright   */ new string[] {F, F, F, T, T, T, T, F} },
                        {"00000110",/*rightbottom righttop  */ new string[] {F, F, F, T, T, T, T, F} },
                        {"00000101",/*rightbottom rightright*/ new string[] {F, F, F, T, T, T, T, F} },
                        {"00001100",/*rightleft rightbottom */ new string[] {F, T, F, T, T, F, T, F} },
                        {"00001001",/*rightleft rightright  */ new string[] {F, F, F, T, T, T, T, F} },
                        {"00001010",/*rightleft righttop    */ new string[] {F, F, T, T, T, T, F, F} }, // jumps on the right-hand side
                        {"10001000",/*leftleft rightleft    */ new string[] {T, T, T, T, F, F, F, F} }, // both left arrows
                        {"01000100",/*leftbottom rightbottom*/ new string[] {F, T, F, T, T, F, T, F} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {T, T, T, T, F, F, F, F} },// left bottom and right-left or right bottom
                        {"00100010",/*lefttop righttop      */ new string[] {F, F, T, T, T, T, F, F} },
                        {"00101000",/*lefttop rightleft     */ new string[] {T, T, T, T, F, F, F, F} },// left top and rightleft or right top
                        {"00011000",/*leftright rightleft   */ new string[] {T, T, T, T, F, F, F, F} },
                        {"00010100",/*leftright rightbottom */ new string[] {F, T, F, T, T, F, T, F} },
                        {"00010010",/*leftright righttop    */ new string[] {F, F, T, T, T, T, F, F} },
                        {"00010001",/*leftright rightright  */ new string[] {F, F, F, T, T, T, T, F} },
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {
							"10000000", /*leftleft*/
							"01000000", /*leftbottom*/
							"00100000", /*lefttop*/
							"00010000", /*leftright*/
							"00001000", /*rightleft*/
							"00000100", /*rightbottom*/
							"00000010", /*righttop*/
							"00000001", /*rightright*/} },
						{ "10000000",/*leftleft             */ new string[] {T, T, T, T, T, F, F, F} },
						{ "01000000",/*leftbottom           */ new string[] {F, T, T, T, T, T, F, F} },
						{ "00100000",/*lefttop              */ new string[] {F, T, T, T, T, F, T, F} },
						{ "00010000",/*leftright            */ new string[] {F, F, F, T, T, T, T, T} },
						{ "00001000",/*rightleft            */ new string[] {F, F, F, F, T, T, T, T} },
						{ "00000100",/*rightbottom          */ new string[] {F, F, F, F, F, T, T, T} },
						{ "00000010",/*righttop             */ new string[] {F, F, F, F, F, T, T, T} },
						{ "00000001",/*rightright           */ new string[] {F, F, F, F, F, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {F, T, F, T, T, F, T, F} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {F, T, T, T, T, F, F, F} },
                        {"01010000",/*leftbottom leftright  */ new string[] {F, F, T, T, T, T, F, F} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {F, T, T, T, T, F, F, F} },
                        {"10010000",/*leftleft leftright    */ new string[] {F, T, T, T, T, F, F, F} },
                        {"10100000",/*leftleft lefttop      */ new string[] {F, T, T, T, T, F, F, F} }, // jumps on the left-hand side
                        {"00000011",/*righttop rightright   */ new string[] {F, F, F, F, F, T, F, T} },
                        {"00000110",/*rightbottom righttop  */ new string[] {F, F, F, F, F, T, T, T} },
                        {"00000101",/*rightbottom rightright*/ new string[] {F, F, F, F, F, F, T, T} },
                        {"00001100",/*rightleft rightbottom */ new string[] {F, F, F, F, F, T, T, T} },
                        {"00001001",/*rightleft rightright  */ new string[] {F, F, F, F, F, T, T, T} },
                        {"00001010",/*rightleft righttop    */ new string[] {F, F, F, F, F, T, T, T} }, // jumps on the right-hand side
                        {"10001000",/*leftleft rightleft    */ new string[] {F, T, T, T, T, F, F, F} }, // both left arrows
                        {"01000100",/*leftbottom rightbottom*/ new string[] {F, F, T, T, T, T, F, F} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {F, F, T, T, T, T, F, F} },// left bottom and right-left or right bottom
                        {"00100010",/*lefttop righttop      */ new string[] {F, T, F, T, T, F, T, F} },
                        {"00101000",/*lefttop rightleft     */ new string[] {F, T, F, T, T, F, T, F} },// left top and rightleft or right top
                        {"00011000",/*leftright rightleft   */ new string[] {F, F, F, F, T, T, T, T} },
                        {"00010100",/*leftright rightbottom */ new string[] {F, F, F, F, T, T, T, T} },
                        {"00010010",/*leftright righttop    */ new string[] {F, F, F, F, T, T, T, T} },
                        {"00010001",/*leftright rightright  */ new string[] {F, F, F, F, T, T, T, T} },
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
                     "00010001" /*leftright rightright  */} },   // left right and right left, right top, right bottom or right right
						{"10000000",/*leftleft              */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, F, T, F, F, F, F} },
						{"01000000",/*leftbottom            */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, F, T, F, F, F, F} },
						{"00100000",/*lefttop               */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, T, T, F, F, F, F} },
						{"00010000",/*leftright             */ new string[] {T, T, T, T, T, T, F, F, F, T, F, T, T, T, T, T, T, T, T, T, T} },
						{"00001000",/*rightleft             */ new string[] {T, F, T, F, F, F, T, T, T, T, T, T, T, T, T, T, F, T, T, T, T} },
						{"00000100",/*rightbottom           */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, T, F, F, F, F, T, T, T} },
						{"00000010",/*righttop              */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, T, F, F, T, T, T} },
						{"00000001",/*rightright            */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, F, F, F, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, T, T, T, T, T, F} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, F, F, F, T, T, T, F} },
                        {"01010000",/*leftbottom leftright  */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, F, F, T, T, T, F} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, F, T, T, F, F, F} },
                        {"10010000",/*leftleft leftright    */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, F, T, T, F, F, F} },
                        {"10100000",/*leftleft lefttop      */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, F, T, T, F, F, F} }, 
                        {"00000011",/*righttop rightright   */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, F, F, T, T, T, T} },
                        {"00000110",/*rightbottom righttop  */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, T, F, T, T, F, F, T} },
                        {"00000101",/*rightbottom rightright*/ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, F, F, T, T, T, T} },
                        {"00001100",/*rightleft rightbottom */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, T, T, F, T, T, T, T, T} },
                        {"00001001",/*rightleft rightright  */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, F, F, T, T, T, T} },
                        {"00001010",/*rightleft righttop    */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, T, T, T, T, T, T, T} }, 
                        {"10001000",/*leftleft rightleft    */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, T, T, T, F, F, F} }, 
                        {"01000100",/*leftbottom rightbottom*/ new string[] {F, T, T, F, F, F, F, T, F, T, F, F, T, T, T, F, F, T, T, F, T} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {T, T, T, T, T, F, T, F, T, T, F, T, T, T, T, F, T, T, T, T, T} },
                        {"00100010",/*lefttop righttop      */ new string[] {T, T, F, F, F, F, F, T, F, F, F, T, T, T, F, T, T, T, F, T, T} },
                        {"00101000",/*lefttop rightleft     */ new string[] {T, T, T, F, T, T, T, F, T, T, F, T, T, F, T, T, T, T, T, T, T} },
                        {"00011000",/*leftright rightleft   */ new string[] {T, T, T, F, T, F, F, T, F, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00010100",/*leftright rightbottom */ new string[] {T, F, T, T, F, T, F, T, T, T, T, T, T, T, T, F, T, T, T, T, T} },
                        {"00010010",/*leftright righttop    */ new string[] {T, F, T, T, F, T, T, T, F, T, T, T, T, F, T, T, T, T, T, T, T } },
                        {"00010001",/*leftright rightright  */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, T, F, T, F, T, T, T, T} },
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
			steps_3d_dictionary_temp = new Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> {
				{ StepDeets.DanceSingle, new Dictionary<string, Dictionary<string, string[]>> {
					{ StepDeets.Left, new Dictionary<string, string[]>() { 
						// single steps reachable with the left foot while the right foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"1000", "0100", "0010", "0001"} },
						{ "1000", new string[] {T, T, T, F} },
						{ "0100", new string[] {T, T, T, F} },
						{ "0010", new string[] {T, T, T, F} },
						{ "0001", new string[] {T, T, T, F} },
						{ "0011", new string[] {T, T, T, F} },
						{ "0101", new string[] {T, T, T, F} },
						{ "1001", new string[] {T, T, T, F} },
						{ "0110", new string[] {T, T, T, F} },
						{ "1010", new string[] {T, T, F, F} }, // top and left jump; don't try to put the left foot onto the top arrow, because the right foot is on it
						{ "1100", new string[] {T, F, T, F} }, // bottom and left jump don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"1000", "0100", "0010", "0001"} },
						{ "1000", new string[] {F, T, T, T} },
						{ "0100", new string[] {F, T, T, T} },
						{ "0010", new string[] {F, T, T, T} },
						{ "0001", new string[] {F, T, T, T} },
						{ "0011", new string[] {F, T, F, T} }, // top and right
						{ "0101", new string[] {F, F, T, T} }, // bottom and right
						{ "1001", new string[] {F, T, T, T} },
						{ "0110", new string[] {F, T, T, T} },
						{ "1010", new string[] {F, T, T, T} }, 
						{ "1100", new string[] {F, T, T, T} }, 
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
						{ StepDeets.Base, new string[] {"100000" /*left*/, "010000" /*topleft*/, "001000"/*bottom*/, "000100"/*top*/, "000010"/*topright*/, "000001"/*right*/} },
						{ "100000", /* left                */ new string[] {T, T, T, T, F, F} },
						{ "010000", /* topleft             */ new string[] {T, T, T, T, F, F} },
						{ "001000", /* bottom              */ new string[] {T, T, T, T, F, F} },
						{ "000100", /* top                 */ new string[] {T, T, T, T, F, F} },
						{ "000010", /* topright            */ new string[] {T, T, T, T, F, F} },
						{ "000001", /* right               */ new string[] {T, T, T, T, F, F} },
						{ "110000", /* left and topleft    */ new string[] {T, T, T, T, F, F} },
						{ "101000", /* left and bottom     */ new string[] {T, T, F, T, F, F} },// don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
						{ "100100", /* left and top        */ new string[] {T, T, T, F, F, F} },// don't try to put the left foot onto the top arrow, because the right foot is on it right now
						{ "100010", /* left and topright   */ new string[] {T, T, T, T, F, F} },
						{ "100001", /* left and right      */ new string[] {T, T, T, T, F, F} },
						{ "011000", /* topleft and bottom  */ new string[] {T, T, F, T, F, F} },// don't try to put the left foot onto the bottom arrow, because the right foot is on it right now
						{ "010100", /* topleft and top     */ new string[] {T, T, T, F, F, F} },// don't try to put the left foot onto the top arrow, because the right foot is on it right now
						{ "010010", /* topleft and topright*/ new string[] {T, T, T, T, F, F} },
						{ "010001", /* topleft and right   */ new string[] {T, T, T, T, F, F} },
						{ "001100", /* bottom and top      */ new string[] {T, T, T, T, F, F} }, 
						{ "001010", /* bottom and topright */ new string[] {T, T, T, T, F, F} }, 
						{ "001001", /* bottom and right    */ new string[] {T, T, T, T, F, F} },
						{ "000110", /* top and topright    */ new string[] {T, T, T, T, F, F} },
						{ "000101", /* top and right       */ new string[] {T, T, T, T, F, F} }, 
						{ "000011", /* topright and right  */ new string[] {T, T, T, T, F, F} }, 
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {"100000" /*left*/, "010000" /*topleft*/, "001000"/*bottom*/, "000100"/*top*/, "000010"/*topright*/, "000001"/*right*/} },
						{ "100000", /* left                */ new string[] {F, F, T, T, T, T} },
						{ "010000", /* topleft             */ new string[] {F, F, T, T, T, T} },
						{ "001000", /* bottom              */ new string[] {F, F, T, T, T, T} },
						{ "000100", /* top                 */ new string[] {F, F, T, T, T, T} },
						{ "000010", /* topright            */ new string[] {F, F, T, T, T, T} },
						{ "000001", /* right               */ new string[] {F, F, T, T, T, T} },
						{ "110000", /* left and topleft    */ new string[] {F, F, T, T, T, T} },
						{ "101000", /* left and bottom     */ new string[] {F, F, T, T, T, T} },
						{ "100100", /* left and top        */ new string[] {F, F, T, T, T, T} },
						{ "100010", /* left and topright   */ new string[] {F, F, T, T, T, T} },
						{ "100001", /* left and right      */ new string[] {F, F, T, T, T, T} },
						{ "011000", /* topleft and bottom  */ new string[] {F, F, T, T, T, T} },
						{ "010100", /* topleft and top     */ new string[] {F, F, T, T, T, T} },
						{ "010010", /* topleft and topright*/ new string[] {F, F, T, T, T, T} },
						{ "010001", /* topleft and right   */ new string[] {F, F, T, T, T, T} },
						{ "001100", /* bottom and top      */ new string[] {F, F, T, T, T, T} }, 
						{ "001010", /* bottom and topright */ new string[] {F, F, F, T, T, T} },// don't try to put the right foot onto the bottom arrow, because the left foot is on it right now 
						{ "001001", /* bottom and right    */ new string[] {F, F, F, T, T, T} },// don't try to put the right foot onto the bottom arrow, because the left foot is on it right now
						{ "000110", /* top and topright    */ new string[] {F, F, T, F, T, T} },// don't try to put the right foot onto the top arrow, because the left foot is on it right now
						{ "000101", /* top and right       */ new string[] {F, F, T, F, T, T} },// don't try to put the right foot onto the top arrow, because the left foot is on it right now 
						{ "000011", /* topright and right  */ new string[] {F, F, T, T, T, T} }, 
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
							"00000001", /*rightright*/
						} },
						{ "10000000",/*leftleft             */ new string[] {T, T, T, F, F, F, F, F} },
						{ "01000000",/*leftbottom           */ new string[] {T, T, T, F, F, F, F, F} },
						{ "00100000",/*lefttop              */ new string[] {T, T, T, F, F, F, F, F} },
						{ "00010000",/*leftright            */ new string[] {T, T, T, T, F, F, F, F} },
						{ "00001000",/*rightleft            */ new string[] {T, T, T, T, T, F, F, F} },
						{ "00000100",/*rightbottom          */ new string[] {F, T, F, T, T, T, T, F} },
						{ "00000010",/*righttop             */ new string[] {F, F, T, T, T, F, T, F} },
						{ "00000001",/*rightright           */ new string[] {F, F, F, T, T, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {T, T, T, F, F, F, F, F} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {T, T, T, F, F, F, F, F} },
                        {"01010000",/*leftbottom leftright  */ new string[] {T, T, T, F, F, F, F, F} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {T, F, T, F, F, F, F, F} },
                        {"10010000",/*leftleft leftright    */ new string[] {T, T, T, F, F, F, F, F} },
                        {"10100000",/*leftleft lefttop      */ new string[] {T, T, F, F, F, F, F, F} }, // jumps on the left-hand side
                        {"00000011",/*righttop rightright   */ new string[] {F, F, F, T, T, T, T, F} },
                        {"00000110",/*rightbottom righttop  */ new string[] {F, F, F, T, T, T, T, F} },
                        {"00000101",/*rightbottom rightright*/ new string[] {F, F, F, T, T, T, T, F} },
                        {"00001100",/*rightleft rightbottom */ new string[] {F, T, F, T, T, F, T, F} },
                        {"00001001",/*rightleft rightright  */ new string[] {F, F, F, T, T, T, T, F} },
                        {"00001010",/*rightleft righttop    */ new string[] {F, F, T, T, T, T, F, F} }, // jumps on the right-hand side
                        {"10001000",/*leftleft rightleft    */ new string[] {T, T, T, T, F, F, F, F} }, // both left arrows
                        {"01000100",/*leftbottom rightbottom*/ new string[] {F, T, F, T, T, F, T, F} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {T, T, T, T, F, F, F, F} },// left bottom and right-left or right bottom
                        {"00100010",/*lefttop righttop      */ new string[] {F, F, T, T, T, T, F, F} },
                        {"00101000",/*lefttop rightleft     */ new string[] {T, T, T, T, F, F, F, F} },// left top and rightleft or right top
                        {"00011000",/*leftright rightleft   */ new string[] {T, T, T, T, F, F, F, F} },
                        {"00010100",/*leftright rightbottom */ new string[] {F, T, F, T, T, F, T, F} },
                        {"00010010",/*leftright righttop    */ new string[] {F, F, T, T, T, T, F, F} },
                        {"00010001",/*leftright rightright  */ new string[] {F, F, F, T, T, T, T, F} },
					} },
					{ StepDeets.Right, new Dictionary<string, string[]>() { 
						// single steps reachable with the right foot while the left foot is on the key step (or both feet for jumps)
						{ StepDeets.Base, new string[] {
							"10000000", /*leftleft*/
							"01000000", /*leftbottom*/
							"00100000", /*lefttop*/
							"00010000", /*leftright*/
							"00001000", /*rightleft*/
							"00000100", /*rightbottom*/
							"00000010", /*righttop*/
							"00000001", /*rightright*/} },
						{ "10000000",/*leftleft             */ new string[] {T, T, T, T, T, F, F, F} },
						{ "01000000",/*leftbottom           */ new string[] {F, T, T, T, T, T, F, F} },
						{ "00100000",/*lefttop              */ new string[] {F, T, T, T, T, F, T, F} },
						{ "00010000",/*leftright            */ new string[] {F, F, F, T, T, T, T, T} },
						{ "00001000",/*rightleft            */ new string[] {F, F, F, F, T, T, T, T} },
						{ "00000100",/*rightbottom          */ new string[] {F, F, F, F, F, T, T, T} },
						{ "00000010",/*righttop             */ new string[] {F, F, F, F, F, T, T, T} },
						{ "00000001",/*rightright           */ new string[] {F, F, F, F, F, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {F, T, F, T, T, F, T, F} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {F, T, T, T, T, F, F, F} },
                        {"01010000",/*leftbottom leftright  */ new string[] {F, F, T, T, T, T, F, F} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {F, T, T, T, T, F, F, F} },
                        {"10010000",/*leftleft leftright    */ new string[] {F, T, T, T, T, F, F, F} },
                        {"10100000",/*leftleft lefttop      */ new string[] {F, T, T, T, T, F, F, F} }, // jumps on the left-hand side
                        {"00000011",/*righttop rightright   */ new string[] {F, F, F, F, F, T, F, T} },
                        {"00000110",/*rightbottom righttop  */ new string[] {F, F, F, F, F, T, T, T} },
                        {"00000101",/*rightbottom rightright*/ new string[] {F, F, F, F, F, F, T, T} },
                        {"00001100",/*rightleft rightbottom */ new string[] {F, F, F, F, F, T, T, T} },
                        {"00001001",/*rightleft rightright  */ new string[] {F, F, F, F, F, T, T, T} },
                        {"00001010",/*rightleft righttop    */ new string[] {F, F, F, F, F, T, T, T} }, // jumps on the right-hand side
                        {"10001000",/*leftleft rightleft    */ new string[] {F, T, T, T, T, F, F, F} }, // both left arrows
                        {"01000100",/*leftbottom rightbottom*/ new string[] {F, F, T, T, T, T, F, F} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {F, F, T, T, T, T, F, F} },// left bottom and right-left or right bottom
                        {"00100010",/*lefttop righttop      */ new string[] {F, T, F, T, T, F, T, F} },
                        {"00101000",/*lefttop rightleft     */ new string[] {F, T, F, T, T, F, T, F} },// left top and rightleft or right top
                        {"00011000",/*leftright rightleft   */ new string[] {F, F, F, F, T, T, T, T} },
                        {"00010100",/*leftright rightbottom */ new string[] {F, F, F, F, T, T, T, T} },
                        {"00010010",/*leftright righttop    */ new string[] {F, F, F, F, T, T, T, T} },
                        {"00010001",/*leftright rightright  */ new string[] {F, F, F, F, T, T, T, T} },
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
                     "00010001" /*leftright rightright  */} },   // left right and right left, right top, right bottom or right right
						{"10000000",/*leftleft              */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, F, T, F, F, F, F} },
						{"01000000",/*leftbottom            */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, F, T, F, F, F, F} },
						{"00100000",/*lefttop               */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, T, T, F, F, F, F} },
						{"00010000",/*leftright             */ new string[] {T, T, T, T, T, T, F, F, F, T, F, T, T, T, T, T, T, T, T, T, T} },
						{"00001000",/*rightleft             */ new string[] {T, F, T, F, F, F, T, T, T, T, T, T, T, T, T, T, F, T, T, T, T} },
						{"00000100",/*rightbottom           */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, T, F, F, F, F, T, T, T} },
						{"00000010",/*righttop              */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, T, F, F, T, T, T} },
						{"00000001",/*rightright            */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, F, F, F, T, T, T} },
                        {"00110000",/*lefttop leftright     */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, T, T, T, T, T, F} },
                        {"01100000",/*leftbottom lefttop    */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, F, F, F, T, T, T, F} },
                        {"01010000",/*leftbottom leftright  */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, F, F, T, T, T, F} },
                        {"11000000",/*leftleft leftbottom   */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, F, T, T, F, F, F} },
                        {"10010000",/*leftleft leftright    */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, F, T, T, F, F, F} },
                        {"10100000",/*leftleft lefttop      */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, F, T, F, T, T, F, F, F} }, 
                        {"00000011",/*righttop rightright   */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, F, F, T, T, T, T} },
                        {"00000110",/*rightbottom righttop  */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, T, F, T, T, F, F, T} },
                        {"00000101",/*rightbottom rightright*/ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, F, F, T, T, T, T} },
                        {"00001100",/*rightleft rightbottom */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, T, T, F, T, T, T, T, T} },
                        {"00001001",/*rightleft rightright  */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, F, F, F, T, T, T, T} },
                        {"00001010",/*rightleft righttop    */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, F, T, T, T, T, T, T, T} }, 
                        {"10001000",/*leftleft rightleft    */ new string[] {T, T, T, T, T, T, F, F, F, F, F, F, T, T, T, T, T, T, F, F, F} }, 
                        {"01000100",/*leftbottom rightbottom*/ new string[] {F, T, T, F, F, F, F, T, F, T, F, F, T, T, T, F, F, T, T, F, T} },
                        {"01001000",/*leftbottom rightleft  */ new string[] {T, T, T, T, T, F, T, F, T, T, F, T, T, T, T, F, T, T, T, T, T} },
                        {"00100010",/*lefttop righttop      */ new string[] {T, T, F, F, F, F, F, T, F, F, F, T, T, T, F, T, T, T, F, T, T} },
                        {"00101000",/*lefttop rightleft     */ new string[] {T, T, T, F, T, T, T, F, T, T, F, T, T, F, T, T, T, T, T, T, T} },
                        {"00011000",/*leftright rightleft   */ new string[] {T, T, T, F, T, F, F, T, F, T, T, T, T, T, T, T, T, T, T, T, T} },
                        {"00010100",/*leftright rightbottom */ new string[] {T, F, T, T, F, T, F, T, T, T, T, T, T, T, T, F, T, T, T, T, T} },
                        {"00010010",/*leftright righttop    */ new string[] {T, F, T, T, F, T, T, T, F, T, T, T, T, F, T, T, T, T, T, T, T } },
                        {"00010001",/*leftright rightright  */ new string[] {F, F, F, F, F, F, T, T, T, T, T, T, F, T, F, T, F, T, T, T, T} },
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
		}

		public void setStepList(string ds, string foot)
		{
			foreach (string step in steps_3d_dictionary_temp[ds][foot].Keys)
			{
				string[] string_array = steps_3d_dictionary_temp[ds][foot][step];
				for (int i = 0; i < string_array.Count(); i++)
				{
					steps_3d_dictionary[ds][foot][step][i] = steps_3d_dictionary_temp[ds][foot][step][i];
				}
			}
		}

		public  void resetStepList(string ds, string foot)
		{
			foreach (string step in steps_3d_dictionary_temp[ds][foot].Keys)
			{
				string[] string_array = steps_3d_dictionary_temp[ds][foot][step];
				for (int i = 0; i < string_array.Count(); i++)
				{
					steps_3d_dictionary_temp[ds][foot][step][i] = steps_3d_dictionary_default[ds][foot][step][i];
				}
			}
		}

		public void cancelTempStepList(string ds, string foot)
		{
			foreach (string step in steps_3d_dictionary_temp[ds][foot].Keys)
			{
				string[] string_array = steps_3d_dictionary_temp[ds][foot][step];
				for (int i = 0; i < string_array.Count(); i++)
				{
					steps_3d_dictionary_temp[ds][foot][step][i] = steps_3d_dictionary[ds][foot][step][i];
				}
			}
		}

		public bool fromJump(string dance_style, string laststep)
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

		public string[] getStepList(string dance_style, string foottype, string laststep)
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

		public Dictionary<string, string[]> getTempStepGrid(string dance_style, string foot)
		{
			return steps_3d_dictionary_temp[dance_style][foot];
		}



	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Stepper
{
//	[Serializable]
//	public sealed class Config
	[Serializable] public  class Config
	{

		public string DefaultSourceFolder { get; set; }
		public string DefaultDestinationFolder { get; set; }
		[XmlIgnore] public string notSerialized { get; set; }
		[XmlIgnore] public Dictionary<string, Dictionary<string, NotesetParameters>> default_params { get; set; }

	//	[XmlIgnore]	private static Config config_instance = new Config();

		public Config()
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
		}
	}
}

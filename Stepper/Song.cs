using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepper
{
    class Song
    {
		public string type { get; private set; }
		public string path { get; private set; }
		public List<string> header { get; private set; }
		public int num_measures { get; private set; }
		public int min_BPM { get; private set; }
		public int max_BPM { get; private set; }
		public int BPM_changes { get; private set; }
		public int num_stops { get; private set; }
		public int num_notesets { get; private set; }
		public int min_measures { get; private set; }
		public int max_measures { get; private set; }

        public Song(string t, string p, List<string> h, int nm, int minbpm, int maxbpm, int numbpmchanges, int numstops, 
			int numnotesets, int minmeasures, int maxmeasures)
        {
            type = t;
            path = p;
            header = h;
            num_measures = nm;
            min_BPM = minbpm;
            max_BPM = maxbpm;
            BPM_changes = numbpmchanges;
            num_stops = numstops;
            num_notesets = numnotesets;
            min_measures = minmeasures;
            max_measures = maxmeasures;
        }
    }
}

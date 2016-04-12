using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepper
{
    class Song
    {
        string type;
        string path;
        List<string> header;
        int num_measures;
        int min_BPM;
        int max_BPM;
        int BPM_changes;
        int num_stops;
        int num_notesets;
        int min_measures;
        int max_measures;

        public Song()
        {

        }

        public Song(string t, string p, List<string> h, int nm, int minbpm, int maxbpm, int numbpmchanges, int numstops, int numnotesets, int minmeasures, int maxmeasures)
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

        public string getType()
        {
            return type;
        }

        public string getPath()
        {
            return path;
        }

        public List<string> getHeader()
        {
            return header;
        }

        public int getNumMeasures()
        {
            return num_measures;
        }

        public int getMinBPM()
        {
            return min_BPM;
        }

        public int getMaxBPM()
        {
            return max_BPM;
        }

        public int getBPMChanges()
        {
            return BPM_changes;
        }

        public int getNumStops()
        {
            return num_stops;
        }

        public int getNumNotesets()
        {
            return num_notesets;
        }

        public int getMinMeasures()
        {
            return min_measures;
        }

        public int getMaxMeasures()
        {
            return max_measures;
        }
    }
}

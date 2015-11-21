using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepper
{
    class Song
    {
        string path;
        List<string> header;
        int num_measures;

        public Song()
        {

        }

        public Song(string p, List<string> h, int i)
        {
            path = p;
            header = h;
            num_measures = i;
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
    }
}

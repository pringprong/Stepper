using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stepper
{
	/* decided to use a class instead of a struct because this item needs more than 16 bytes, I think */
	[Serializable]
	public class NotesetParameters
	{
		public string dance_style { get; set; }
		public string dance_level { get; set; }
		public bool alternating_foot { get; set; }
		public bool repeat_arrows { get;  set; }
		public int percent_stepfill { get;  set; }
		public int percent_onbeat { get;  set; }
		public int percent_jumps { get;  set; }
		public int percent_quintuples { get;  set; }
		public bool triples_on_1_only { get;  set; }
		public bool quintuples_on_1_only { get;  set; }
		public int triple_type { get;  set; }
		public int quintuple_type { get;  set; }
		public bool full8th { get;  set; }

		public NotesetParameters(string ds, string dl, bool af, bool ra, int sf, int ob, int j, int q, bool t1o, bool q1o, int tr, int qu, bool f)
		{
			dance_style = ds;
			dance_level = dl;
			alternating_foot = af;
			repeat_arrows = ra;
			percent_stepfill = sf;
			percent_onbeat = ob;
			percent_jumps = j;
			percent_quintuples = q;
			triples_on_1_only = t1o;
			quintuples_on_1_only = q1o;
			triple_type = tr;
			quintuple_type = qu;
			full8th = f;
		}
	}
}

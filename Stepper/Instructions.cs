using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stepper
{
    public partial class Instructions : Form
    {
        public Instructions()
        {
            InitializeComponent();
            label1.Text = @"Stepper overwrites existing Stepmania .sm stepfiles with automatically generated steps. 

Instructions:
1. Before running Stepper, open C:\Games\StepMania 5\Songs and create a new song group folder such as 'Cardio'
2. Copy songs (entire folders containing .mp3, .sm, etc.) from your other song group folders into the new one.
3. Run Stepper, browse to the new folder and click 'Get Info'. The table will show the songs, max and min beats per minute, number of stops, etc. in the selected song group folder
4. Change the settings for the 5 difficulty levels as desired
5. Click 'Overwrite stepfiles' to overwrite the existing stepfiles and create 5 single-pad dance steps per song according to the settings.
6. Open Stepmania and try out your new steps!

Warnings: 
1. Although Stepper will create a backup of the old stepfile, there is no quick way to restore from backup. It's better to copy the song folders to a new song group folder before you start instead of changing the old .sm files in place
2. Stepmania stores information about each song in a cache. If Stepmania doesn't display all 5 song levels created by Stepper, go to C:\Users\<your username>\AppData\Roaming\StepMania 5\Cache and delete everything, then restart Stepmania to refresh the cache
3. Some songs use a .dwi file instead of a .sm file to store step information. Stepper does not work for .dwi format stepfiles";
            ToolTip toolTip1 = new ToolTip();
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 200;
            toolTip1.ReshowDelay = 200;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.button1, "Close this window");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

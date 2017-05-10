using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Stepper
{
    public partial class SampleWindow : Form
    {

        private Stepper parent;
        private String dance_class;
  
        public SampleWindow()
        {
            InitializeComponent();
        }

        public SampleWindow(Stepper p, String dc)
        {
            InitializeComponent();
            parent = p;
            dance_class = dc;
            if (dc.Equals("dance-single"))
            {
                this.sampleDGV.ColumnCount = 7;
                sampleDGV.RowCount = 17;
                foreach (DataGridViewColumn c in sampleDGV.Columns)
                {
                    c.Width = sampleDGV.Width / sampleDGV.ColumnCount;
                }
                foreach (DataGridViewRow r in sampleDGV.Rows)
                {
                    r.Height = sampleDGV.Height / sampleDGV.RowCount;
                }
                sampleDGV.Rows[0].Cells[0].Value = "Measure";
                sampleDGV.Rows[0].Cells[1].Value = "Beat";
                sampleDGV.Rows[0].Cells[2].Value = "Foot";
            }

            sampleDGV.ClearSelection();
            sampleDGV.CurrentCell = null;
        }

        public void setParentWindow(Stepper s)
        {
            parent = s;
        }



        private void sampleDGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == 0 && e.ColumnIndex==3 && dance_class.Equals("dance-single")) {
                            // Create a Graphics object

            // Create two AdjustableArrowCap objects
            AdjustableArrowCap cap2 = new AdjustableArrowCap(2, 1);

            // Set cap properties
            cap2.WidthScale = 1;
            cap2.BaseCap = LineCap.Square;
            cap2.Height = 1;

            // Create a pen
            Pen blackPen = new Pen(Color.Black, 15);

            // Set CustomStartCap and CustomEndCap properties
            //blackPen.CustomStartCap = cap1;
            blackPen.CustomEndCap = cap2;

            // Draw line
            e.Graphics.DrawLine(blackPen, 
                e.CellBounds.X+40, 
                e.CellBounds.Y+30, 
                e.CellBounds.X, 
                e.CellBounds.Y+30);

            // Dispose of objects
            e.PaintContent(e.ClipBounds);
            e.Handled = true;
            blackPen.Dispose();
            
            }


        }
    }
}

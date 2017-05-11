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
         private Pen blackpen;
        private Pen redpen;
        private Pen bluepen;
        int scalefactor = 3;
        int nummeasures;
        char[] feet;
        string[] steps;
  
        public SampleWindow()
        {
            InitializeComponent();
        }

        public SampleWindow(Stepper p, String dc, int nm, char[] f, string[] s, Pen b, Pen red, Pen bl)
        {
            InitializeComponent();
            parent = p;
            dance_class = dc;
            nummeasures = nm;
            feet = f;
            steps = s;
            blackpen = b;
            redpen = red;
            bluepen = bl;

            int numrows = nummeasures * 8 + 1;
            sampleDGV.RowCount = numrows;

                if (dc.Equals("dance-single"))
                {
                    this.sampleDGV.ColumnCount = 7;
                    foreach (DataGridViewColumn c in sampleDGV.Columns)
                    {
                        if (c.Index == 0)
                        {
                            c.Width = sampleDGV.Width * 2 / (sampleDGV.ColumnCount + 1);

                        }
                        else
                        {
                            c.Width = sampleDGV.Width / (sampleDGV.ColumnCount + 1);
                        }
                    }
                    foreach (DataGridViewRow r in sampleDGV.Rows)
                    {
                        r.Height = sampleDGV.Height / sampleDGV.RowCount;
                    }
                    sampleDGV.Rows[0].Cells[0].Value = "Measure";
                    sampleDGV.Rows[0].Cells[1].Value = "Beat";
                    sampleDGV.Rows[0].Cells[2].Value = "Foot";
                    for (int r = 1; r< numrows; r++)
                    {
                        sampleDGV.Rows[r].Cells[2].Value = feet[r - 1];
                        string step = steps[r-1];
                        for (int c = 0; c < 4; c++)
                        {
                            if (step[c] != '0')
                            {
                                sampleDGV.Rows[r].Cells[c + 3].Value = "";
                            }
                        }
                    }
                }
                int measurecount = 1;
                int beatcount = 1;
                for (int i = 0; i < numrows; i++)
                {
                    if ((i - 1) % 8 == 0)
                    {
                        sampleDGV.Rows[i].Cells[0].Value = measurecount;
                        measurecount++;
                    }
                    if (((i - 1) % 2) == 0)
                    {
                        sampleDGV.Rows[i].Cells[1].Value = beatcount;
                        beatcount++;
                        if (beatcount > 4) { beatcount = 1; }
                    }
                }

            sampleDGV.ClearSelection();
            sampleDGV.CurrentCell = null;
        }

        private void sampleDGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (dance_class.Equals("dance-single")) {
                if (e.RowIndex == 0)
                {
                    drawDanceSingleArrow(blackpen, e);
                }

                else if (((e.RowIndex % 2) == 0) && (e.ColumnIndex > 2) && sampleDGV[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    drawDanceSingleArrow(bluepen, e);
                    //sampleDGV[e.ColumnIndex, e.RowIndex].Value = null;
                }
                else if (((e.RowIndex % 2) == 1) && (e.ColumnIndex > 2) && sampleDGV[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    drawDanceSingleArrow(redpen, e);
                    //sampleDGV[e.ColumnIndex, e.RowIndex].Value = null;
                }
 
            }

            // Paint 
            e.PaintContent(e.ClipBounds);
            e.Handled = true;
        }

        private void drawDanceSingleArrow(Pen p, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2,
                e.CellBounds.X + e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2);
            }
            else if (e.ColumnIndex == 4)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y + e.CellBounds.Height);
            }
            else if (e.ColumnIndex == 5)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y + e.CellBounds.Height,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y);
            }
            else if (e.ColumnIndex == 6)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2);
            }
        }
    }
}

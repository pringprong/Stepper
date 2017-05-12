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
        private String level;
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

        public SampleWindow(Stepper p, String dc, String l, int nm, char[] f, string[] s, Pen b, Pen red, Pen bl)
        {
            InitializeComponent();
            parent = p;
            dance_class = dc;
            level = l;
            nummeasures = nm;
            feet = f;
            steps = s;
            blackpen = b;
            redpen = red;
            bluepen = bl;

            int numrows = nummeasures * 8 + 1;
            sampleDGV.RowCount = numrows;
            foreach (DataGridViewRow r in sampleDGV.Rows)
            {
                r.Height = sampleDGV.Height / sampleDGV.RowCount;
            }
            if (dance_class.Equals("dance-single"))
            {
                    this.Text = "Dance Single " + level ;
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
            else if (dance_class.Equals("dance-solo"))
            {
                this.Width = 642;
                this.Text = "Dance Solo " + level;
                this.sampleDGV.ColumnCount = 9;
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
                for (int r = 1; r < numrows; r++)
                {
                    sampleDGV.Rows[r].Cells[2].Value = feet[r - 1];
                    string step = steps[r - 1];
                    for (int c = 0; c < 6; c++)
                    {
                        if (step[c] != '0')
                        {
                            sampleDGV.Rows[r].Cells[c + 3].Value = "";
                        }
                    }
                }
            }

            else if (dance_class.Equals("dance-double"))
            {
                this.Width = 771;
                this.Text = "Dance Double " + level;
                this.sampleDGV.ColumnCount = 11;
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
                for (int r = 1; r < numrows; r++)
                {
                    sampleDGV.Rows[r].Cells[2].Value = feet[r - 1];
                    string step = steps[r - 1];
                    for (int c = 0; c < 8; c++)
                    {
                        if (step[c] != '0')
                        {
                            sampleDGV.Rows[r].Cells[c + 3].Value = "";
                        }
                    }
                }
            }
            else if (dance_class.Equals("pump-single"))
            {
                this.Width = 578;
                this.Text = "Pump Single " + level;
                this.sampleDGV.ColumnCount = 8;
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
                for (int r = 1; r < numrows; r++)
                {
                    sampleDGV.Rows[r].Cells[2].Value = feet[r - 1];
                    string step = steps[r - 1];
                    for (int c = 0; c < 5; c++)
                    {
                        if (step[c] != '0')
                        {
                            sampleDGV.Rows[r].Cells[c + 3].Value = "";
                        }
                    }
                }
            }

            sampleDGV.Rows[0].Cells[0].Value = "Measure";
            sampleDGV.Rows[0].Cells[1].Value = "Beat";
            sampleDGV.Rows[0].Cells[2].Value = "Foot";
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
        }

        private void sampleDGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (dance_class.Equals("dance-single"))
            {
                if (e.RowIndex == 0)
                {
                    drawDanceSingleArrow(blackpen, e);
                }
                else if (((e.RowIndex % 2) == 0) && (e.ColumnIndex > 2) && sampleDGV[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    drawDanceSingleArrow(bluepen, e);
                }
                else if (((e.RowIndex % 2) == 1) && (e.ColumnIndex > 2) && sampleDGV[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    drawDanceSingleArrow(redpen, e);
                }
            }
            else if (dance_class.Equals("dance-solo"))
            {
                if (e.RowIndex == 0)
                {
                    drawDanceSoloArrow(blackpen, e);
                }
                else if (((e.RowIndex % 2) == 0) && (e.ColumnIndex > 2) && sampleDGV[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    drawDanceSoloArrow(bluepen, e);
                }
                else if (((e.RowIndex % 2) == 1) && (e.ColumnIndex > 2) && sampleDGV[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    drawDanceSoloArrow(redpen, e);
                }
            }
            else if (dance_class.Equals("dance-double"))
            {
                if (e.RowIndex == 0)
                {
                    drawDanceDoubleArrow(blackpen, e);
                }
                else if (((e.RowIndex % 2) == 0) && (e.ColumnIndex > 2) && sampleDGV[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    drawDanceDoubleArrow(bluepen, e);
                }
                else if (((e.RowIndex % 2) == 1) && (e.ColumnIndex > 2) && sampleDGV[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    drawDanceDoubleArrow(redpen, e);
                }
            }
            else if (dance_class.Equals("pump-single"))
            {
                if (e.RowIndex == 0)
                {
                    drawPumpSingleArrow(blackpen, e);
                }
                else if (((e.RowIndex % 2) == 0) && (e.ColumnIndex > 2) && sampleDGV[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    drawPumpSingleArrow(bluepen, e);
                }
                else if (((e.RowIndex % 2) == 1) && (e.ColumnIndex > 2) && sampleDGV[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    drawPumpSingleArrow(redpen, e);
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
        private void drawDanceSoloArrow(Pen p, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2,
                e.CellBounds.X + e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2);
            }
            if (e.ColumnIndex == 4)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height,
                e.CellBounds.X + e.CellBounds.Width / scalefactor + e.CellBounds.Height / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height/ scalefactor);
            }
            else if (e.ColumnIndex == 5)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y + e.CellBounds.Height);
            }
            else if (e.ColumnIndex == 6)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y + e.CellBounds.Height,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y);
            }
            else if (e.ColumnIndex == 7)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / scalefactor + e.CellBounds.Height / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / scalefactor);
            }
            else if (e.ColumnIndex == 8)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2);
            }
        }
        private void drawDanceDoubleArrow(Pen p, DataGridViewCellPaintingEventArgs e)
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
            if (e.ColumnIndex == 7)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2,
                e.CellBounds.X + e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2);
            }
            else if (e.ColumnIndex == 8)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y + e.CellBounds.Height);
            }
            else if (e.ColumnIndex == 9)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y + e.CellBounds.Height,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y);
            }
            else if (e.ColumnIndex == 10)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2);
            }
        }
        private void drawPumpSingleArrow(Pen p, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / scalefactor,
                e.CellBounds.X + e.CellBounds.Width / scalefactor + e.CellBounds.Height / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height );
            }
            if (e.ColumnIndex == 4)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height,
                e.CellBounds.X + e.CellBounds.Width / scalefactor + e.CellBounds.Height / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / scalefactor);
            }
            else if (e.ColumnIndex == 5)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y + e.CellBounds.Height / 2 + e.CellBounds.Height / 5,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y + e.CellBounds.Height + e.CellBounds.Height / 5);
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y + e.CellBounds.Height - e.CellBounds.Height / 5,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y + e.CellBounds.Height / 2 - e.CellBounds.Height / 5);
            }
            else if (e.ColumnIndex == 6)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / scalefactor + e.CellBounds.Height / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / scalefactor);
            }
            else if (e.ColumnIndex == 7)
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / scalefactor + e.CellBounds.Height / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / scalefactor,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height );
            }
        }
    }
}

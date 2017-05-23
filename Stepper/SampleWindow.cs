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
    public class SampleWindow : Form
    {
		//private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataGridView sampleDGV;
		private String dance_style;
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

        public SampleWindow(String dc, String l, int nm, char[] f, string[] s, Pen b, Pen red, Pen bl)
        {
			dance_style = dc;
			Width = (StepDeets.numPlaces(dance_style) + 4) * 64;
			InitializeComponent();
            level = l;
            nummeasures = nm;
            feet = f;
            steps = s;
            blackpen = b;
            redpen = red;
            bluepen = bl;

            int numrows = nummeasures * StepDeets.beats_per_measure*2 + 1;
            sampleDGV.RowCount = numrows;
            foreach (DataGridViewRow r in sampleDGV.Rows)
            {
                r.Height = sampleDGV.Height / sampleDGV.RowCount;
            }
			Text = StepDeets.stepTitle(dance_style) + " " + level;
			sampleDGV.ColumnCount = StepDeets.numPlaces(dance_style) + 3;
			Width = (StepDeets.numPlaces(dance_style) + 4) * 64;
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
				for (int c = 0; c < StepDeets.numPlaces(dance_style); c++)
				{
					if (step[c] != '0')
					{
						sampleDGV.Rows[r].Cells[c + 3].Value = "";
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
                if (((i - 1) % (StepDeets.beats_per_measure*2)) == 0)
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
            if (e.RowIndex == 0)
            {
                drawArrow(StepDeets.getArrow(dance_style, e.ColumnIndex), blackpen, e);
            }
            else if (((e.RowIndex % 2) == 0) && (e.ColumnIndex > 2) && sampleDGV[e.ColumnIndex, e.RowIndex].Value != null)
            {
				drawArrow(StepDeets.getArrow(dance_style, e.ColumnIndex), bluepen, e);
            }
            else if (((e.RowIndex % 2) == 1) && (e.ColumnIndex > 2) && sampleDGV[e.ColumnIndex, e.RowIndex].Value != null)
            {
				drawArrow(StepDeets.getArrow(dance_style, e.ColumnIndex), redpen, e);
            }
            e.PaintContent(e.ClipBounds);
            e.Handled = true;
        }

        private void drawArrow(string type, Pen p, DataGridViewCellPaintingEventArgs e)
        {
            if (type.Equals(StepDeets.LeftArrow))
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2,
                e.CellBounds.X + e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2);
            }
            else if (type.Equals(StepDeets.DownArrow))
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y + e.CellBounds.Height);
            }
            else if (type.Equals(StepDeets.UpArrow))
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y + e.CellBounds.Height,
                e.CellBounds.X + e.CellBounds.Width / 2,
                e.CellBounds.Y);
            }
            else if (type.Equals(StepDeets.RightArrow))
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / 2);
            }
            else if (type.Equals(StepDeets.UpLeftArrow))
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height,
                e.CellBounds.X + e.CellBounds.Width / scalefactor + e.CellBounds.Height / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / scalefactor);
            }
            else if (type.Equals(StepDeets.UpRightArrow))
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / scalefactor + e.CellBounds.Height / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / scalefactor);
            }
            else if (type.Equals(StepDeets.DownLeftArrow))
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / scalefactor,
                e.CellBounds.X + e.CellBounds.Width / scalefactor + e.CellBounds.Height / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height);
            }
            else if (type.Equals(StepDeets.DownRightArrow))
            {
                e.Graphics.DrawLine(p,
                e.CellBounds.X + e.CellBounds.Width / scalefactor + e.CellBounds.Height / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height / scalefactor,
                e.CellBounds.X + e.CellBounds.Width - e.CellBounds.Width / scalefactor,
                e.CellBounds.Y + e.CellBounds.Height);
            }
            else if (type.Equals(StepDeets.CenterArrow))
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
        }

	/*	protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}*/

		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.sampleDGV = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.sampleDGV)).BeginInit();
			this.SuspendLayout();
			// 
			// sampleDGV
			// 
			this.sampleDGV.AllowUserToAddRows = false;
			this.sampleDGV.AllowUserToDeleteRows = false;
			this.sampleDGV.AllowUserToResizeColumns = false;
			this.sampleDGV.AllowUserToResizeRows = false;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
			this.sampleDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
			this.sampleDGV.BackgroundColor = System.Drawing.SystemColors.Control;
			this.sampleDGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.sampleDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.sampleDGV.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			this.sampleDGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.sampleDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.sampleDGV.ColumnHeadersVisible = false;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.sampleDGV.DefaultCellStyle = dataGridViewCellStyle4;
			this.sampleDGV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sampleDGV.Enabled = false;
			this.sampleDGV.EnableHeadersVisualStyles = false;
			this.sampleDGV.Location = new System.Drawing.Point(0, 0);
			this.sampleDGV.Name = "sampleDGV";
			this.sampleDGV.ReadOnly = true;
			this.sampleDGV.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.sampleDGV.RowHeadersVisible = false;
			this.sampleDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.sampleDGV.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.sampleDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.sampleDGV.ShowCellErrors = false;
			this.sampleDGV.ShowCellToolTips = false;
			this.sampleDGV.ShowEditingIcon = false;
			this.sampleDGV.ShowRowErrors = false;
			this.sampleDGV.Size = new System.Drawing.Size(498, 785);
			this.sampleDGV.TabIndex = 1;
			this.sampleDGV.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.sampleDGV_CellPainting);
			// 
			// SampleWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(498, 785);
			this.Controls.Add(this.sampleDGV);
			this.Name = "SampleWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SampleWindow";
			this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			((System.ComponentModel.ISupportInitialize)(this.sampleDGV)).EndInit();
			this.ResumeLayout(false);
		}
    }
}

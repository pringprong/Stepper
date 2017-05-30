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
	class StepConfigTabPage : TabPage
	{
		DataGridView dgv;
		private string dance_style;
		private string foot;
		Dictionary<string, string[]> stepgrid;
		public int num_rows { get; private set; }
		public int num_columns { get; private set; }
		private Pen blackpen;
		private Pen graypen;
		private SolidBrush blackbrush;
		private SolidBrush graybrush;
		private SolidBrush whitebrush;

		public StepConfigTabPage(string ds, string f, Dictionary<string, string[]> sg, int w, int h, Pen bp, Pen gp, SolidBrush bb, SolidBrush gb, SolidBrush wb)
		{
			dance_style = ds;
			foot = f;
			this.Width = w;
			this.Height = h;
			blackpen = bp;
			graypen = gp;
			blackbrush = bb;
			graybrush = gb;
			whitebrush = wb;
			stepgrid = sg;
			num_rows = stepgrid.Keys.Count();
			num_columns = stepgrid[StepDeets.Base].Count() + 1;
			InitializeComponent();
			fill();
		}

		private void InitializeComponent()
		{
			dgv = new DataGridView();

			this.Text = "From single step and jump (rows) to " + foot + " (columns)";
			//
			// cell style
			//
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.Padding = new Padding(0, 0, 0, 0);
			// 
			// dgv
			// 
			this.dgv.Width = this.Width;
			this.dgv.Height = this.Height;
			this.dgv.Location = new Point(3, 3);
			this.dgv.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.dgv.AllowUserToAddRows = false;
			this.dgv.AllowUserToDeleteRows = false;
			this.dgv.AllowUserToResizeColumns = false;
			this.dgv.AllowUserToResizeRows = false;
			this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgv.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgv.ColumnHeadersVisible = false;
			this.dgv.DefaultCellStyle = dataGridViewCellStyle4;
			this.dgv.EnableHeadersVisualStyles = false;
			this.dgv.ReadOnly = true;
			this.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgv.RowHeadersVisible = false;
			this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dgv.ShowCellErrors = false;
			this.dgv.ShowCellToolTips = false;
			this.dgv.ShowEditingIcon = false;
			this.dgv.ShowRowErrors = false;
			this.dgv.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_CellPainting);
			this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
			this.dgv.Resize += new EventHandler(this.dgv_Resize);
			this.Controls.Add(this.dgv);
		}

		private void fill()
		{
			dgv.RowCount = num_rows;
			dgv.ColumnCount = num_columns;
			dgv_Resize(null, null);

			string[] keys = stepgrid.Keys.ToArray();
			string[] column_headers = stepgrid[StepDeets.Base].ToArray();
			for (int r = 0; r < num_rows; r++)
			{
				for (int c = 0; c < num_columns; c++)
				{
					if (r == 0 && c > 0)
					{
						dgv.Rows[r].Cells[c].Value = column_headers[c - 1];
					}
					else
					{
						if (c == 0)
						{
							dgv.Rows[r].Cells[c].Value = keys[r];
						}
						else
						{
							dgv.Rows[r].Cells[c].Value = stepgrid[keys[r]][c - 1];
						}
					}
				}
			}
		}

		private void dgv_Resize(object sender, EventArgs e)
		{
			int width = Math.Max(((dgv.Width - 10) / dgv.ColumnCount), 50);
			int height = Math.Max(((dgv.Height - 10) / dgv.RowCount), 25);
			foreach (DataGridViewColumn c in dgv.Columns)
			{
				c.Width = width;
			}
			foreach (DataGridViewRow r in dgv.Rows)
			{
				r.Height = height;
			}
		}

		
		private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			e.Graphics.FillRectangle(whitebrush, new Rectangle(
				e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height));
			int scalefactor = 2;
			if (dance_style.Equals(StepDeets.DanceSingle))
			{
				if (e.RowIndex == 0)
				{
					e.Graphics.FillRectangle(blackbrush, new Rectangle(
						e.CellBounds.X + e.CellBounds.Width / scalefactor,   // x
						e.CellBounds.Y + e.CellBounds.Height / scalefactor,  // y
						e.CellBounds.Width / scalefactor,  // width
						e.CellBounds.Height / scalefactor));   // height
				}
				if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
				{
					if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals(StepDeets.T))
					{
						e.Graphics.FillRectangle(blackbrush, new Rectangle(
							e.CellBounds.X + e.CellBounds.Width / scalefactor,   // x
							e.CellBounds.Y + e.CellBounds.Height / scalefactor,  // y
							e.CellBounds.Width / scalefactor,  // width
							e.CellBounds.Height / scalefactor));   // height
					}
					else if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals(StepDeets.F))
					{
						e.Graphics.DrawRectangle(graypen, new Rectangle(
							e.CellBounds.X + e.CellBounds.Width / scalefactor,   // x
							e.CellBounds.Y + e.CellBounds.Height / scalefactor,  // y
							e.CellBounds.Width / scalefactor,  // width
							e.CellBounds.Height / scalefactor));   // height
					}
				}

			}
			e.PaintContent(e.ClipBounds);
			e.Handled = true;
		}

		private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			string v = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			if (v.Equals(StepDeets.F))
			{
				dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = StepDeets.T;
			}
			else if (v.Equals(StepDeets.T))
			{
				dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = StepDeets.F;
			}
		}

		public void saveSettings()
		{
			string[] keys = stepgrid.Keys.ToArray();
			string[] column_headers = stepgrid[StepDeets.Base].ToArray();
			for (int r = 2; r < num_rows; r++)
			{
				for (int c = 1; c < num_columns; c++)
				{
					stepgrid[keys[r - 1]][c - 1] = (string)dgv.Rows[r].Cells[c].Value;
				}
			}
		}

		public void resetSettings()
		{
			StepDeets.resetStepList(dance_style, foot);
			fill();
		}

		public void cancelSettings()
		{
			StepDeets.resetTempStepList(dance_style, foot);
			fill();
		}
	}
}

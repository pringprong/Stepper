﻿using System;
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
		private Dictionary<string, string[]> stepgrid;
		public int num_rows { get; private set; }
		public int num_columns { get; private set; }
		private Pen blackpen;
		private Pen graypen;
		private Pen lightgraypen;
		private SolidBrush blackbrush;
		private SolidBrush graybrush;
		private SolidBrush lightgraybrush;
		private SolidBrush whitebrush;
		private ConfigSettings config;

		public StepConfigTabPage(string ds, string f, Dictionary<string, string[]> sg, int w, int h, 
			Pen bp, Pen gp, Pen lgp, SolidBrush bb, SolidBrush gb, SolidBrush lgb, SolidBrush wb, ConfigSettings c)
		{
			dance_style = ds;
			foot = f;
			this.Width = w;
			this.Height = h;
			blackpen = bp;
			graypen = gp;
			lightgraypen = lgp;
			blackbrush = bb;
			graybrush = gb;
			lightgraybrush = lgb;
			whitebrush = wb;
			stepgrid = sg;
			num_rows = stepgrid.Keys.Count();
			num_columns = stepgrid[StepDeets.Base].Count() + 1;
			config = c;
			InitializeComponent();
			fill();
		}

		private void InitializeComponent()
		{
			dgv = new DataGridView();

			this.Text = "From single step and jump (rows) to " + foot + " (columns)";
			
			// cell style
			//
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		//	dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.Control;
		//	dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
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
						dgv.Rows[r].Cells[c].Tag = column_headers[c - 1];
					}
					else
					{
						if (c == 0)
						{
							dgv.Rows[r].Cells[c].Tag = keys[r];
						}
						else
						{
							dgv.Rows[r].Cells[c].Tag = stepgrid[keys[r]][c - 1];
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
			// 1. Get the number of squares : 4 for dance single, 6 for dance solo, 8 for dance double, 5 for pump single
			// 2. Get the size of the squares from StepDeets
			// 3. Get the placement of the squares from StepDeets
			// 4. Whether they are open or filled
			//		get string from column header
			//		split up string into 1 and 0
			//		associate 1 and 0 with square index
			//		1 = filled, 0 = open
			// 5. Color of the squares:  later I changed the Pens and Brushes colors to Blue-Black-Grey
 			//		index column and row: black
			//		value T:  dark gray
			//      value F:  light gray
			
			// first draw a white background square
			e.Graphics.FillRectangle(whitebrush, new Rectangle(
				e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height));

			int numsquares = StepDeets.emptyStep(dance_style).Count();
			int squareheight = (int)(e.CellBounds.Height / StepDeets.getConfigSquareHeightScaleFactor(dance_style));
			int squarewidth = (int)(e.CellBounds.Width / StepDeets.getConfigSquareWidthScaleFactor(dance_style)); ;
			double[] xcoords = StepDeets.getConfigSquareXCoordinateScaleFactor(dance_style);
			double[] ycoords = StepDeets.getConfigSquareYCoordinateScaleFactor(dance_style);

			string[] types = new string[] { "open", "open", "open", "open", "open", "open", "open", "open", "open", "open"};
			string indexstring = "dummy";
			if (e.ColumnIndex > 0)
			{
				indexstring = (string)dgv.Rows[0].Cells[e.ColumnIndex].Tag;
			}
			else if (e.ColumnIndex == 0) {
				indexstring = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
			}
			if (!indexstring.Equals("dummy"))
			{
				for (int j = 0; j < indexstring.Length; j++)
				{
					if (indexstring[j].Equals('1'))
					{
						types[j] = "filled";
					}
				}
			}

			string color = "black";
			if ((e.RowIndex == 0 && e.ColumnIndex == 0) || dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag == null)
			{
				e.PaintContent(e.ClipBounds);
				e.Handled = true;
				return;
			}
			else if (e.RowIndex == 0 || e.ColumnIndex == 0)
			{
				color = "black";
			}
			else if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.Equals(StepDeets.T))
			{
				color = "dg";
			}
			else if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.Equals(StepDeets.F))
			{
				color = "lg";
			}
			for (int i = 0; i < numsquares; i++)
			{
				int x = (int)(e.CellBounds.X + e.CellBounds.Width * xcoords[i]);
				int y = (int)(e.CellBounds.Y + e.CellBounds.Height * ycoords[i]);
				draw(types[i], color, x, y, squarewidth, squareheight, e);

			}
			e.PaintContent(e.ClipBounds);
			e.Handled = true;
		}

		private void draw(string type, string color, int x, int y, int w, int h, DataGridViewCellPaintingEventArgs e)
		{
			if (type.Equals("open"))
			{
				if (color.Equals("black"))
				{
					e.Graphics.DrawRectangle(blackpen, new Rectangle(x, y, w, h));
				}
				else if (color.Equals("dg"))
				{
					e.Graphics.DrawRectangle(graypen, new Rectangle(x, y, w, h));
				}
				else if (color.Equals("lg"))
				{
					e.Graphics.DrawRectangle(lightgraypen, new Rectangle(x, y, w, h));
				}
			}
			else if (type.Equals("filled"))
			{
				if (color.Equals("black"))
				{
					e.Graphics.FillRectangle(blackbrush, new Rectangle(x, y, w, h));
				}
				else if (color.Equals("dg"))
				{
					e.Graphics.FillRectangle(graybrush, new Rectangle(x, y, w, h));
				}
				else if (color.Equals("lg"))
				{
					e.Graphics.FillRectangle(lightgraybrush, new Rectangle(x, y, w, h));
				}
			}
		}

		private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			string v = (string)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
			if (v.Equals(StepDeets.F))
			{
				dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = StepDeets.T;
			}
			else if (v.Equals(StepDeets.T))
			{
				dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = StepDeets.F;
			}
		}

		public void saveSettings()
		{
			// write the current state of the datagridview  to the temp step dictionary
			string[] keys = stepgrid.Keys.ToArray();
			string[] column_headers = stepgrid[StepDeets.Base].ToArray();
			for (int rr = 1; rr < num_rows; rr++)
			{
				for (int cc = 1; cc < num_columns; cc++)
				{
					stepgrid[keys[rr]][cc - 1] = (string)dgv.Rows[rr].Cells[cc].Tag;
				}
			}
			// then write the temp step dictionary to the current running step dictionary
			config.setStepList(dance_style, foot);
		}

		public void resetSettings()
		{
			// write the default step dictionary to the temp step dictionary
			//StepDeets.resetStepList(dance_style, foot);
			config.resetStepList(dance_style, foot);
			// then read the temp step dictionary into the datagridview
			fill();
			dgv.Refresh();
		}

		public void cancelSettings()
		{
			// write the current running step dictionary back over the temp step dictionary
			//StepDeets.cancelTempStepList(dance_style, foot);
			config.cancelTempStepList(dance_style, foot);
			// then read the temp step dictionary to the datagridview
			fill();
			dgv.Refresh();
		}

		public void set_config(ConfigSettings c)
		{
			stepgrid = c.getTempStepGrid(dance_style, foot);
			config = c;
			fill();
			dgv.Refresh();
		}
	}
}

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

		public StepConfigTabPage(string ds, string f, Dictionary<string, string[]> sg, int w, int h)
		{
			dance_style = ds;
			foot = f;
			this.Width = w;
			this.Height = h;
			dgv = new DataGridView();
			stepgrid = sg;
			num_rows = stepgrid.Keys.Count() + 1;
			num_columns = stepgrid[StepDeets.Base].Count() + 1;
			Initialize();
			dgv.Width = this.Width - 10;
			dgv.Height = this.Height - 10;
			dgv.RowCount = num_rows;
			dgv.ColumnCount = num_columns;
			foreach (DataGridViewColumn c in dgv.Columns)
			{
				c.Width = dgv.Width / dgv.ColumnCount;
			}
			foreach (DataGridViewRow r in dgv.Rows)
			{
				r.Height = (int)Math.Floor((double)dgv.Height / (double)dgv.RowCount) - 1;
			}
			load();
		}

		private void Initialize()
		{

			this.Text = "To " + foot;
			this.Dock = System.Windows.Forms.DockStyle.Fill;
			this.UseVisualStyleBackColor = true;
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			// 
			// dgv
			// 
			this.dgv.AllowUserToAddRows = false;
			this.dgv.AllowUserToDeleteRows = false;
			this.dgv.AllowUserToResizeColumns = false;
			this.dgv.AllowUserToResizeRows = false;
			this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
			//	this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dgv.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgv.ColumnHeadersVisible = false;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewCellStyle4.Padding = new Padding(0, 0, 0, 0);
			this.dgv.DefaultCellStyle = dataGridViewCellStyle4;
			//		this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
			//		this.dgv.Enabled = false;
			this.dgv.EnableHeadersVisualStyles = false;
			this.dgv.ReadOnly = true;
			this.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dgv.RowHeadersVisible = false;
			this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dgv.ShowCellErrors = false;
			this.dgv.ShowCellToolTips = false;
			this.dgv.ShowEditingIcon = false;
			this.dgv.ShowRowErrors = false;
			this.dgv.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_CellPainting);
			this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
			this.Controls.Add(this.dgv);
		}

		private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
/*			if (e.RowIndex == 0)
			{
			}
			else {
			}
			e.PaintContent(e.ClipBounds);
			e.Handled = true; */
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
			load();
		}

		private void load()
		{
			string[] keys = stepgrid.Keys.ToArray();
			string[] column_headers = stepgrid[StepDeets.Base].ToArray();
			for (int r = 0; r < num_rows; r++)
			{
				for (int c = 0; c < num_columns; c++)
				{
					if (r == 0)
					{
						if (c == 0)
						{
							dgv.Rows[r].Cells[c].Value = "to:";
						}
						else
						{
							dgv.Rows[r].Cells[c].Value = column_headers[c - 1];
						}
					}
					else if (r == 1)
					{
						if (c == 0)
						{
							dgv.Rows[r].Cells[c].Value = "from:";
						}
					}
					else
					{
						if (c == 0)
						{
							dgv.Rows[r].Cells[c].Value = keys[r - 1];
						}
						else
						{
							dgv.Rows[r].Cells[c].Value = stepgrid[keys[r - 1]][c - 1];
						}
					}
				}
			}
		}
	}
}

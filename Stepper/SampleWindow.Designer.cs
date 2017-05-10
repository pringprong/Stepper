namespace Stepper
{
    partial class SampleWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.sampleDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.sampleDGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sampleDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sampleDGV.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.sampleDGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sampleDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sampleDGV.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.sampleDGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.sampleDGV.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.sampleDGV.Size = new System.Drawing.Size(782, 785);
            this.sampleDGV.TabIndex = 1;
            this.sampleDGV.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.sampleDGV_CellPainting);
            // 
            // SampleWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 785);
            this.Controls.Add(this.sampleDGV);
            this.Name = "SampleWindow";
            this.Text = "SampleWindow";
            ((System.ComponentModel.ISupportInitialize)(this.sampleDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView sampleDGV;
    }
}
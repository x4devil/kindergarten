namespace Lab3
{
    partial class FormEat
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtEatingDate = new System.Windows.Forms.DateTimePicker();
            this.dgEatingList = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEatingList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtEatingDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 29);
            this.panel1.TabIndex = 1;
            // 
            // dtEatingDate
            // 
            this.dtEatingDate.Location = new System.Drawing.Point(455, 5);
            this.dtEatingDate.Name = "dtEatingDate";
            this.dtEatingDate.Size = new System.Drawing.Size(137, 20);
            this.dtEatingDate.TabIndex = 2;
            this.dtEatingDate.ValueChanged += new System.EventHandler(this.dtEatingDate_ValueChanged);
            // 
            // dgEatingList
            // 
            this.dgEatingList.AllowUserToAddRows = false;
            this.dgEatingList.AllowUserToDeleteRows = false;
            this.dgEatingList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgEatingList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgEatingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEatingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgEatingList.Location = new System.Drawing.Point(0, 29);
            this.dgEatingList.Name = "dgEatingList";
            this.dgEatingList.RowTemplate.Height = 23;
            this.dgEatingList.Size = new System.Drawing.Size(595, 446);
            this.dgEatingList.TabIndex = 1;
            this.dgEatingList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEatingList_CellValueChanged);
            // 
            // FormEat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 475);
            this.Controls.Add(this.dgEatingList);
            this.Controls.Add(this.panel1);
            this.Name = "FormEat";
            this.Text = "Список питавшихся";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEat_FormClosing);
            this.Load += new System.EventHandler(this.FormEat_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgEatingList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtEatingDate;
        private System.Windows.Forms.DataGridView dgEatingList;
    }
}
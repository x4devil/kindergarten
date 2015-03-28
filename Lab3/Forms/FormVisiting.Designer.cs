namespace Lab3
{
    partial class FormVisiting
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
            this.dtVisitingDay = new System.Windows.Forms.DateTimePicker();
            this.btnTakeOut = new System.Windows.Forms.Button();
            this.btnTakeIn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgVisitingList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVisitingList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtVisitingDay);
            this.panel1.Controls.Add(this.btnTakeOut);
            this.panel1.Controls.Add(this.btnTakeIn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 29);
            this.panel1.TabIndex = 0;
            // 
            // dtVisitingDay
            // 
            this.dtVisitingDay.Location = new System.Drawing.Point(455, 5);
            this.dtVisitingDay.Name = "dtVisitingDay";
            this.dtVisitingDay.Size = new System.Drawing.Size(137, 20);
            this.dtVisitingDay.TabIndex = 3;
            this.dtVisitingDay.ValueChanged += new System.EventHandler(this.dtVisitingDay_ValueChanged);
            // 
            // btnTakeOut
            // 
            this.btnTakeOut.Location = new System.Drawing.Point(84, 3);
            this.btnTakeOut.Name = "btnTakeOut";
            this.btnTakeOut.Size = new System.Drawing.Size(75, 23);
            this.btnTakeOut.TabIndex = 2;
            this.btnTakeOut.Text = "Забрали";
            this.btnTakeOut.UseVisualStyleBackColor = true;
            this.btnTakeOut.Click += new System.EventHandler(this.btnTakeOut_Click);
            // 
            // btnTakeIn
            // 
            this.btnTakeIn.Location = new System.Drawing.Point(3, 3);
            this.btnTakeIn.Name = "btnTakeIn";
            this.btnTakeIn.Size = new System.Drawing.Size(75, 23);
            this.btnTakeIn.TabIndex = 1;
            this.btnTakeIn.Text = "Привели";
            this.btnTakeIn.UseVisualStyleBackColor = true;
            this.btnTakeIn.Click += new System.EventHandler(this.btnTakeIn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgVisitingList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(595, 446);
            this.panel2.TabIndex = 1;
            // 
            // dgVisitingList
            // 
            this.dgVisitingList.AllowUserToAddRows = false;
            this.dgVisitingList.AllowUserToDeleteRows = false;
            this.dgVisitingList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgVisitingList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgVisitingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVisitingList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dgVisitingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVisitingList.Location = new System.Drawing.Point(0, 0);
            this.dgVisitingList.Name = "dgVisitingList";
            this.dgVisitingList.RowTemplate.Height = 23;
            this.dgVisitingList.Size = new System.Drawing.Size(595, 446);
            this.dgVisitingList.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ФИО ребенка";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Привели";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Забрали";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Доверенное лицо";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Воспитатель";
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Номер";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // FormVisiting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 475);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormVisiting";
            this.Text = "Учет посещаемости";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVisiting_FormClosing);
            this.Load += new System.EventHandler(this.FormVisiting_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgVisitingList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtVisitingDay;
        private System.Windows.Forms.Button btnTakeIn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgVisitingList;
        private System.Windows.Forms.Button btnTakeOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column4;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}
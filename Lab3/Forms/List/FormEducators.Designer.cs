namespace Lab3
{
    partial class FormEducators
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
            this.btnEditEducator = new System.Windows.Forms.Button();
            this.btnDeleteEducator = new System.Windows.Forms.Button();
            this.btnAddEducator = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgEducatorList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEducatorList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnEditEducator);
            this.panel1.Controls.Add(this.btnDeleteEducator);
            this.panel1.Controls.Add(this.btnAddEducator);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 29);
            this.panel1.TabIndex = 0;
            // 
            // btnEditEducator
            // 
            this.btnEditEducator.Location = new System.Drawing.Point(84, 3);
            this.btnEditEducator.Name = "btnEditEducator";
            this.btnEditEducator.Size = new System.Drawing.Size(75, 23);
            this.btnEditEducator.TabIndex = 2;
            this.btnEditEducator.Text = "Изменить";
            this.btnEditEducator.UseVisualStyleBackColor = true;
            this.btnEditEducator.Click += new System.EventHandler(this.btnEditEducator_Click);
            // 
            // btnDeleteEducator
            // 
            this.btnDeleteEducator.Location = new System.Drawing.Point(165, 3);
            this.btnDeleteEducator.Name = "btnDeleteEducator";
            this.btnDeleteEducator.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteEducator.TabIndex = 3;
            this.btnDeleteEducator.Text = "Удалить";
            this.btnDeleteEducator.UseVisualStyleBackColor = true;
            this.btnDeleteEducator.Click += new System.EventHandler(this.btnDeleteEducator_Click);
            // 
            // btnAddEducator
            // 
            this.btnAddEducator.Location = new System.Drawing.Point(3, 3);
            this.btnAddEducator.Name = "btnAddEducator";
            this.btnAddEducator.Size = new System.Drawing.Size(75, 23);
            this.btnAddEducator.TabIndex = 1;
            this.btnAddEducator.Text = "Добавить";
            this.btnAddEducator.UseVisualStyleBackColor = true;
            this.btnAddEducator.Click += new System.EventHandler(this.btnAddEducator_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgEducatorList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(595, 446);
            this.panel2.TabIndex = 1;
            // 
            // dgEducatorList
            // 
            this.dgEducatorList.AllowUserToAddRows = false;
            this.dgEducatorList.AllowUserToDeleteRows = false;
            this.dgEducatorList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgEducatorList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgEducatorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEducatorList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dgEducatorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgEducatorList.GridColor = System.Drawing.SystemColors.Control;
            this.dgEducatorList.Location = new System.Drawing.Point(0, 0);
            this.dgEducatorList.Name = "dgEducatorList";
            this.dgEducatorList.ReadOnly = true;
            this.dgEducatorList.RowTemplate.Height = 23;
            this.dgEducatorList.Size = new System.Drawing.Size(595, 446);
            this.dgEducatorList.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Фамилия";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Имя";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Отчество";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Телефон";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Место проживания";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Номер";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // FormEducators
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 475);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormEducators";
            this.Text = "Список воспитателей";
            this.Load += new System.EventHandler(this.FormEducators_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgEducatorList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEditEducator;
        private System.Windows.Forms.Button btnDeleteEducator;
        private System.Windows.Forms.Button btnAddEducator;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgEducatorList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}
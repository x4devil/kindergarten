namespace Lab3
{
    partial class FormGroup
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgGroupList = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGroupList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgGroupList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(595, 475);
            this.panel2.TabIndex = 2;
            // 
            // dgGroupList
            // 
            this.dgGroupList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgGroupList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgGroupList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGroupList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgGroupList.Location = new System.Drawing.Point(0, 0);
            this.dgGroupList.Name = "dgGroupList";
            this.dgGroupList.RowTemplate.Height = 23;
            this.dgGroupList.Size = new System.Drawing.Size(595, 475);
            this.dgGroupList.TabIndex = 0;
            this.dgGroupList.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgGroupList_UserDeletingRow);
            // 
            // FormGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 475);
            this.Controls.Add(this.panel2);
            this.Name = "FormGroup";
            this.Text = "Список групп";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGroup_FormClosing);
            this.Load += new System.EventHandler(this.FormGroup_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgGroupList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgGroupList;
    }
}
namespace Lab3
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.управлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокГруппToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокВоспитателейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.учетПосещаемостиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оплатаПосещенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.учетПитанияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.стоимостьПитанияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подключениеКБДToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.времяПитанияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbGroupNames = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMove = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgChildList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.личнаяКартаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оплатаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgChildList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.управлениеToolStripMenuItem,
            this.настройкиToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(595, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // управлениеToolStripMenuItem
            // 
            this.управлениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокГруппToolStripMenuItem,
            this.списокВоспитателейToolStripMenuItem,
            this.учетПосещаемостиToolStripMenuItem,
            this.оплатаПосещенияToolStripMenuItem,
            this.учетПитанияToolStripMenuItem,
            this.стоимостьПитанияToolStripMenuItem});
            this.управлениеToolStripMenuItem.Name = "управлениеToolStripMenuItem";
            this.управлениеToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.управлениеToolStripMenuItem.Text = "Управление";
            // 
            // списокГруппToolStripMenuItem
            // 
            this.списокГруппToolStripMenuItem.Name = "списокГруппToolStripMenuItem";
            this.списокГруппToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.списокГруппToolStripMenuItem.Text = "Список групп";
            this.списокГруппToolStripMenuItem.Click += new System.EventHandler(this.списокГруппToolStripMenuItem_Click);
            // 
            // списокВоспитателейToolStripMenuItem
            // 
            this.списокВоспитателейToolStripMenuItem.Name = "списокВоспитателейToolStripMenuItem";
            this.списокВоспитателейToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.списокВоспитателейToolStripMenuItem.Text = "Список воспитателей";
            this.списокВоспитателейToolStripMenuItem.Click += new System.EventHandler(this.списокВоспитателейToolStripMenuItem_Click);
            // 
            // учетПосещаемостиToolStripMenuItem
            // 
            this.учетПосещаемостиToolStripMenuItem.Name = "учетПосещаемостиToolStripMenuItem";
            this.учетПосещаемостиToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.учетПосещаемостиToolStripMenuItem.Text = "Учет посещаемости";
            this.учетПосещаемостиToolStripMenuItem.Click += new System.EventHandler(this.учетПосещаемостиToolStripMenuItem_Click);
            // 
            // оплатаПосещенияToolStripMenuItem
            // 
            this.оплатаПосещенияToolStripMenuItem.Name = "оплатаПосещенияToolStripMenuItem";
            this.оплатаПосещенияToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.оплатаПосещенияToolStripMenuItem.Text = "Оплата посещения";
            this.оплатаПосещенияToolStripMenuItem.Click += new System.EventHandler(this.оплатаПосещенияToolStripMenuItem_Click);
            // 
            // учетПитанияToolStripMenuItem
            // 
            this.учетПитанияToolStripMenuItem.Name = "учетПитанияToolStripMenuItem";
            this.учетПитанияToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.учетПитанияToolStripMenuItem.Text = "Учет питания";
            this.учетПитанияToolStripMenuItem.Click += new System.EventHandler(this.учетПитанияToolStripMenuItem_Click);
            // 
            // стоимостьПитанияToolStripMenuItem
            // 
            this.стоимостьПитанияToolStripMenuItem.Name = "стоимостьПитанияToolStripMenuItem";
            this.стоимостьПитанияToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.стоимостьПитанияToolStripMenuItem.Text = "Стоимость питания";
            this.стоимостьПитанияToolStripMenuItem.Click += new System.EventHandler(this.стоимостьПитанияToolStripMenuItem_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.подключениеКБДToolStripMenuItem,
            this.времяПитанияToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // подключениеКБДToolStripMenuItem
            // 
            this.подключениеКБДToolStripMenuItem.Name = "подключениеКБДToolStripMenuItem";
            this.подключениеКБДToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.подключениеКБДToolStripMenuItem.Text = "Подключение к БД";
            this.подключениеКБДToolStripMenuItem.Click += new System.EventHandler(this.подключениеКБДToolStripMenuItem_Click);
            // 
            // времяПитанияToolStripMenuItem
            // 
            this.времяПитанияToolStripMenuItem.Name = "времяПитанияToolStripMenuItem";
            this.времяПитанияToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.времяПитанияToolStripMenuItem.Text = "Время питания";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbGroupNames);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnMove);
            this.panel1.Controls.Add(this.btnRemove);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 29);
            this.panel1.TabIndex = 1;
            // 
            // cbGroupNames
            // 
            this.cbGroupNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroupNames.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbGroupNames.FormattingEnabled = true;
            this.cbGroupNames.Location = new System.Drawing.Point(471, 5);
            this.cbGroupNames.Name = "cbGroupNames";
            this.cbGroupNames.Size = new System.Drawing.Size(121, 21);
            this.cbGroupNames.TabIndex = 2;
            this.cbGroupNames.SelectedIndexChanged += new System.EventHandler(this.cbGroupNames_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(425, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Группа";
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(165, 3);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(75, 23);
            this.btnMove.TabIndex = 0;
            this.btnMove.Text = "Перевести";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(84, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 0;
            this.btnRemove.Text = "Удалить";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgChildList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(595, 271);
            this.panel2.TabIndex = 2;
            // 
            // dgChildList
            // 
            this.dgChildList.AllowUserToAddRows = false;
            this.dgChildList.AllowUserToDeleteRows = false;
            this.dgChildList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgChildList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgChildList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgChildList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgChildList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgChildList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgChildList.Location = new System.Drawing.Point(0, 0);
            this.dgChildList.Name = "dgChildList";
            this.dgChildList.ReadOnly = true;
            this.dgChildList.RowTemplate.Height = 23;
            this.dgChildList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgChildList.Size = new System.Drawing.Size(595, 271);
            this.dgChildList.TabIndex = 0;
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
            this.Column4.HeaderText = "Дата рождения";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.личнаяКартаToolStripMenuItem,
            this.оплатаToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 48);
            // 
            // личнаяКартаToolStripMenuItem
            // 
            this.личнаяКартаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.просмотрToolStripMenuItem,
            this.редактированиеToolStripMenuItem});
            this.личнаяКартаToolStripMenuItem.Name = "личнаяКартаToolStripMenuItem";
            this.личнаяКартаToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.личнаяКартаToolStripMenuItem.Text = "Личная карта";
            // 
            // просмотрToolStripMenuItem
            // 
            this.просмотрToolStripMenuItem.Name = "просмотрToolStripMenuItem";
            this.просмотрToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.просмотрToolStripMenuItem.Text = "Просмотр";
            // 
            // редактированиеToolStripMenuItem
            // 
            this.редактированиеToolStripMenuItem.Name = "редактированиеToolStripMenuItem";
            this.редактированиеToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.редактированиеToolStripMenuItem.Text = "Редактирование";
            this.редактированиеToolStripMenuItem.Click += new System.EventHandler(this.редактированиеToolStripMenuItem_Click);
            // 
            // оплатаToolStripMenuItem
            // 
            this.оплатаToolStripMenuItem.Name = "оплатаToolStripMenuItem";
            this.оплатаToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.оплатаToolStripMenuItem.Text = "Оплата";
            this.оплатаToolStripMenuItem.Click += new System.EventHandler(this.оплатаToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 324);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Детский сад";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgChildList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem управлениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокГруппToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокВоспитателейToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbGroupNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgChildList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.ToolStripMenuItem учетПосещаемостиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оплатаПосещенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem учетПитанияToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem личнаяКартаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem просмотрToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактированиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оплатаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.ToolStripMenuItem стоимостьПитанияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem подключениеКБДToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem времяПитанияToolStripMenuItem;

    }
}


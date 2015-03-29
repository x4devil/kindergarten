using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class FormVisiting : Form
    {
        public FormVisiting()
        {
            InitializeComponent();
        }
        DateTime cur_date;
        /// <summary>
        /// Первоначальная инициализация формы
        /// </summary>
        public void init()
        {
            cur_date = this.dtVisitingDay.Value;
            loadData();
        }

        /// <summary>
        /// Загрузка данных о посещаемости
        /// </summary>
        public void loadData()
        {
            if (this.dgVisitingList.Columns.Count > 0)
            {
                this.dgVisitingList.Columns.Clear();
            }

            if (this.dgVisitingList.Rows.Count > 0)
            {
                this.dgVisitingList.Rows.Clear();
            }
            //получаем таблицу уже имеющихся в базе записей
            DataTable main_table = GlobalVars.connection.getVisitingListByDate(cur_date);

            //получаем таблицу детей
            DataTable add_table = GlobalVars.connection.getVisitingBabyList();

            //добавляем в таблицу неотмеченых детей
            for (int i = 0; i < add_table.Rows.Count; i++)
            {
                bool contains = false;
                for (int j = 0; j < main_table.Rows.Count; j++)
                {
                    if (add_table.Rows[i]["Номер ребенка"].Equals(main_table.Rows[j]["Номер ребенка"]))
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                {
                    main_table.ImportRow(add_table.Rows[i]);
                }
            }

            this.dgVisitingList.DataSource = main_table;
            this.dgVisitingList.Columns[0].Visible = false;
            this.dgVisitingList.Columns[1].Visible = false;

            //отключаем сортировку, включает ридонли
            foreach (DataGridViewColumn column in this.dgVisitingList.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.ReadOnly = true;
            }

            //получаем список воспитателей
            DataTable educators_list = GlobalVars.connection.getEducatorLoV();


            for (int i = 0; i < this.dgVisitingList.Rows.Count; i++)
            {
                //для каждой строки устанавливаем список воспитаталей...
                DataGridViewComboBoxCell educators_cell = new DataGridViewComboBoxCell(); 
                educators_cell.DataSource = educators_list;
                educators_cell.DisplayMember = "ФИО";
                educators_cell.ValueMember = "Номер";
                this.dgVisitingList[5, i] = educators_cell;
                this.dgVisitingList[5, i].ReadOnly = false;

                //и уникальный список доверенных лиц
                DataGridViewComboBoxCell truestee_cell = new DataGridViewComboBoxCell();
                DataTable trustee_table = GlobalVars.connection.getTrusteeLoV(
                    Convert.ToInt32(this.dgVisitingList[1, i].Value));
                DataRow null_row_t = trustee_table.NewRow();
                null_row_t["ФИО"] = " ";
                null_row_t["Номер"] = DBNull.Value;
                trustee_table.Rows.InsertAt(null_row_t, 0);
                truestee_cell.DataSource = trustee_table;
                truestee_cell.DisplayMember = "ФИО";
                truestee_cell.ValueMember = "Номер";
                this.dgVisitingList[6, i] = truestee_cell;
                this.dgVisitingList[6, i].ReadOnly = false;
            }
        }
        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void FormVisiting_Load(object sender, EventArgs e)
        {
            init();
        }

        /// <summary>
        /// Обработчик изменения даты в dtVisitingDay
        /// </summary>
        private void dtVisitingDay_ValueChanged(object sender, EventArgs e)
        {
            DialogResult dresult = MessageBox.Show("Сохранить изменения?",
                "Сохранение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dresult == DialogResult.Yes)
            {
                saveData();
            }
            cur_date = this.dtVisitingDay.Value;
            loadData();
        }

        /// <summary>
        /// Сохранение изменений в таблице
        /// </summary>
        public void saveData()
        {
            int rowCount = this.dgVisitingList.Rows.Count;
            for(int i = 0; i < rowCount; i++)
            {
                if (this.dgVisitingList[0, i].Value.Equals(DBNull.Value) || this.dgVisitingList[0, i].Value.Equals(""))
                {
                    if (!this.dgVisitingList[3,i].Value.Equals(DBNull.Value))
                    {
                        String date = cur_date.ToString("yyyy-MM-dd");

                        String e;
                        if (this.dgVisitingList[4, i].Value.ToString().Equals(""))
                        {
                            e = "null";
                        }
                        else
                        {
                            e = "'" + this.dgVisitingList[4, i].Value.ToString() + "'";
                        }


                        String ed;
                        if (this.dgVisitingList[5,i].Value.Equals(DBNull.Value))
                        {
                            ed = "null"; 
                        }
                        else
                        {
                            ed = "'" + this.dgVisitingList[5, i].Value.ToString() + "'";
                        }

                        String tr;
                        if (this.dgVisitingList[6, i].Value.Equals(DBNull.Value))
                        {
                            tr = "null";
                        }
                        else
                        {
                            tr = "'" + this.dgVisitingList[6, i].Value.ToString() + "'";
                        }

                        GlobalVars.connection.insertVisiting(
                            Convert.ToInt32(this.dgVisitingList[1,i].Value),
                            date,
                            this.dgVisitingList[3,i].Value.ToString(),
                            e,
                            ed,
                            tr
                            );
                    }
                }
                else
                {
                    String e;
                    if (this.dgVisitingList[4, i].Value.ToString().Equals(""))
                    {
                        e = "null";
                    }
                    else
                    {
                        e = "'" + this.dgVisitingList[4, i].Value.ToString() + "'";
                    }

                    String ed;
                    if (this.dgVisitingList[5, i].Value.Equals(DBNull.Value))
                    {
                        ed = "null";
                    }
                    else
                    {
                        ed = "'" + this.dgVisitingList[5, i].Value.ToString() + "'";
                    }

                    String tr;
                    if (this.dgVisitingList[6, i].Value.Equals(DBNull.Value))
                    {
                        tr = "null";
                    }
                    else
                    {
                        tr = "'" + this.dgVisitingList[6, i].Value.ToString() + "'";
                    }

                    GlobalVars.connection.updateVisiting(
                        Convert.ToInt32(
                        this.dgVisitingList[0, i].Value),
                        this.dgVisitingList[3, i].Value.ToString(),
                        e,
                        ed,
                        tr
                        );
                }
            }
        }
        /// <summary>
        /// Обработчик кнопки "Привести"
        /// </summary>
        private void btnTakeIn_Click(object sender, EventArgs e)
        {
            int rowIndex = this.dgVisitingList.CurrentCellAddress.Y;
            if (!this.dgVisitingList[ 3,rowIndex].Value.Equals(DBNull.Value))
            {
                GlobalVars.showWarningMsgBox(this, "Этот ребенок уже отмечен.");
            }
            else
            {
                this.dgVisitingList[3, rowIndex].Value = DateTime.Now;
            }
        }

        /// <summary>
        /// Обработчик кнопки "Забрали"
        /// </summary>
        private void btnTakeOut_Click(object sender, EventArgs e)
        {
            int rowIndex = this.dgVisitingList.CurrentCellAddress.Y;
            if (this.dgVisitingList[3, rowIndex].Value.Equals(DBNull.Value))
            {
                GlobalVars.showWarningMsgBox(this, "Этого ребенка еще не привели.");
            }
            else 
            {
                if (!this.dgVisitingList[4, rowIndex].Value.Equals(DBNull.Value))
                {
                    GlobalVars.showWarningMsgBox(this, "Этого ребенка уже забрали.");
                }
                else
                {
                    if (this.dgVisitingList[5, rowIndex].Value.Equals(DBNull.Value))
                    {
                        GlobalVars.showWarningMsgBox(this, "Необходимо выбрать воспитателя.");
                    }
                    else
                    {
                        this.dgVisitingList[4, rowIndex].Value = DateTime.Now;
                    }
                }
            } 
        }


        /// <summary>
        /// Обработка закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormVisiting_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dresult = MessageBox.Show("Сохранить изменения?",
                "Сохранение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dresult == DialogResult.Yes)
            {
                saveData();
            }
        }
    }
}

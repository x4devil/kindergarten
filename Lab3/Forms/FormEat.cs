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
    public partial class FormEat : Form
    {
        public FormEat()
        {
            InitializeComponent();
        }
        DateTime cur_date;
        DataTable main_table;
        DataTable secondary_table;
        int main_rows_count = 0;
        bool locked = false;
        bool[,] change_matrix;
        bool loaded = false;
        /// <summary>
        /// Первоначальная инициализация формы
        /// </summary>
        public void init()
        {
            cur_date = this.dtEatingDate.Value;
            checkDate();
            initDataGrid();
            loadData();
        }

        /// <summary>
        /// Программное создание стобцов в dataGrid
        /// </summary>
        public void initDataGrid()
        {
            DataGridViewTextBoxColumn firstColumn = new DataGridViewTextBoxColumn();
            firstColumn.HeaderText = "ID";
            firstColumn.Name = "Baby ID";
            DataGridViewTextBoxColumn secondColumn = new DataGridViewTextBoxColumn();
            secondColumn.HeaderText = "ФИО Ребенка";
            secondColumn.Name = "Baby Name";
            DataGridViewCheckBoxColumn thirdColumn = new DataGridViewCheckBoxColumn();
            thirdColumn.Name = "Breakfast";
            thirdColumn.HeaderText = "Завтрак";
            DataGridViewCheckBoxColumn fourtColumn = new DataGridViewCheckBoxColumn();
            fourtColumn.Name = "Lunch";
            fourtColumn.HeaderText = "Обед";
            DataGridViewCheckBoxColumn fifthColumn = new DataGridViewCheckBoxColumn();
            fifthColumn.Name = "Snack";
            fifthColumn.HeaderText = "Полдник";
            DataGridViewCheckBoxColumn sixColumn = new DataGridViewCheckBoxColumn();
            sixColumn.Name = "Dinner";
            sixColumn.HeaderText = "Ужин";

            this.dgEatingList.Columns.Add(firstColumn);
            this.dgEatingList.Columns.Add(secondColumn);
            this.dgEatingList.Columns.Add(thirdColumn);
            this.dgEatingList.Columns.Add(fourtColumn);
            this.dgEatingList.Columns.Add(fifthColumn);
            this.dgEatingList.Columns.Add(sixColumn);
        }

        /// <summary>
        /// Загрузка данных о питании
        /// </summary>
        public void loadData()
        {
            loaded = false;

            if (this.dgEatingList.Rows.Count > 0)
            {
                this.dgEatingList.Rows.Clear();
            }

            //загружаем уже имеющиеся в базе записи для данной даты
            main_table = GlobalVars.connection.getEatingListByDate(cur_date);

            //сортируем таблицу по id ребенка
            main_table.DefaultView.Sort = "Baby ID";
            main_table = main_table.DefaultView.ToTable();

            //заполняем datagrid
            int old_id = -1;
            int curr_row = -1;
            main_rows_count = 0;
            for (int i = 0; i < main_table.Rows.Count; i++)
            {
                if ( !old_id.Equals(main_table.Rows[i].Field<int>("Baby ID")) ) 
                {
                    old_id = main_table.Rows[i].Field<int>("Baby ID");

                    //создает строку для нового ребенка
                    DataGridViewCell firstCell = new DataGridViewTextBoxCell();
                    DataGridViewCell secondCell = new DataGridViewTextBoxCell();
                    DataGridViewCell thirdCell = new DataGridViewCheckBoxCell();
                    DataGridViewCell fourtCell = new DataGridViewCheckBoxCell();
                    DataGridViewCell fifthCell = new DataGridViewCheckBoxCell();
                    DataGridViewCell sixCell = new DataGridViewCheckBoxCell();
                    DataGridViewRow row = new DataGridViewRow();
                    firstCell.Value = main_table.Rows[i].Field<int>("Baby ID");
                    secondCell.Value = main_table.Rows[i].Field<String>("Baby Name");
                    thirdCell.Value = false;
                    fourtCell.Value = false;
                    fifthCell.Value = false;
                    sixCell.Value = false;
                    switch (main_table.Rows[i].Field<int>("N ID"))
                    {
                        case 1:
                            thirdCell.Value = true;
                            break;
                        case 2:
                            fourtCell.Value = true;
                            break;
                        case 3:
                            fifthCell.Value = true;
                            break;
                        case 4:
                            sixCell.Value = true;
                            break;
                    }
                    row.Cells.AddRange(firstCell, secondCell, thirdCell, fourtCell, fifthCell, sixCell);
                    curr_row = this.dgEatingList.Rows.Add(row);
                    main_rows_count++;
                }
                //проставляем галочки для данного ребенка
                else
                {
                    switch (main_table.Rows[i].Field<int>("N ID"))
                    {
                        case 1:
                            this.dgEatingList.Rows[curr_row].Cells[2].Value = true;
                            break;
                        case 2:
                            this.dgEatingList.Rows[curr_row].Cells[3].Value = true;
                            break;
                        case 3:
                            this.dgEatingList.Rows[curr_row].Cells[4].Value = true;
                            break;
                        case 4:
                            this.dgEatingList.Rows[curr_row].Cells[5].Value = true;
                            break;
                    }
                }
            }

            //загружаем остальных детей, посещавших в этот день
            secondary_table = GlobalVars.connection.getVisitingListByDate(cur_date);
            for (int i = 0; i < secondary_table.Rows.Count; i++)
            {
                bool contains = false;
                for (int j = 0; j < this.dgEatingList.Rows.Count; j++)
                {
                    if ( this.dgEatingList.Rows[j].Cells[0].Value.Equals( secondary_table.Rows[i].Field<int>("Номер ребенка")))
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                {
                    //создает строку для нового ребенка
                    DataGridViewCell firstCell = new DataGridViewTextBoxCell();
                    DataGridViewCell secondCell = new DataGridViewTextBoxCell();
                    DataGridViewCell thirdCell = new DataGridViewCheckBoxCell();
                    DataGridViewCell fourtCell = new DataGridViewCheckBoxCell();
                    DataGridViewCell fifthCell = new DataGridViewCheckBoxCell();
                    DataGridViewCell sixCell = new DataGridViewCheckBoxCell();
                    DataGridViewRow row = new DataGridViewRow();
                    firstCell.Value = secondary_table.Rows[i].Field<int>("Номер ребенка");
                    secondCell.Value = secondary_table.Rows[i].Field<String>("ФИО ребенка");
                    thirdCell.Value = false;
                    fourtCell.Value = false;
                    fifthCell.Value = false;
                    sixCell.Value = false;
                    row.Cells.AddRange(firstCell, secondCell, thirdCell, fourtCell, fifthCell, sixCell);
                    this.dgEatingList.Rows.Add(row);
                }
            }


            //блокируем редактирование для прошедших дат
            if (cur_date < DateTime.Today)
            {
                locked = true;
                this.dgEatingList.ReadOnly = true;
                this.dgEatingList.Enabled = false;
            }
            else
            {
                locked = false;
                this.dgEatingList.ReadOnly = false;
                this.dgEatingList.Enabled = true;
            }

            //скрываем лишнии столбцы, устанавливаем ридонли для имен
            this.dgEatingList.Columns[0].Visible = false;
            this.dgEatingList.Columns[1].ReadOnly = true;

            //создаем матрицу для проверки изменений
            change_matrix = new bool[main_rows_count, 4];

            loaded = true;
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void FormEat_Load(object sender, EventArgs e)
        {
            init();
        }

        /// <summary>
        /// Обработчик изменения даты в dtEatingDate
        /// </summary>
        private void dtEatingDate_ValueChanged(object sender, EventArgs e)
        {
            if (!locked) 
            {
                this.dgEatingList.EndEdit();
                DialogResult dresult = MessageBox.Show("Сохранить изменения?",
                    "Сохранение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dresult == DialogResult.Yes)
                {
                    saveEatingData();
                }
            }
            cur_date = this.dtEatingDate.Value;
            loadData();
        }

        /// <summary>
        /// Сохранение данных из таблице
        /// </summary>
        private void saveEatingData()
        {
            String date = cur_date.ToString("yyyy-MM-dd");

            double[] cost = GlobalVars.connection.getEatingCost();

            //обновляем или удаляем уже имеющиеся записи с помощью матрицы изменений
            for(int i = 0; i < main_rows_count; i++)
            {
                //завтрак
                if (change_matrix[i,0])
                {
                    if (this.dgEatingList.Rows[i].Cells[2].Value.Equals(true))
                    {
                        GlobalVars.connection.insertEating(Convert.ToInt32(this.dgEatingList.Rows[i].Cells[0].Value),
                            date, 1, cost[0]);
                    }
                    else
                    {
                        GlobalVars.connection.deleteEating(Convert.ToInt32(this.dgEatingList.Rows[i].Cells[0].Value),
                            1, date);
                    }
                }
                //обед
                if (change_matrix[i, 1])
                {
                    if (this.dgEatingList.Rows[i].Cells[3].Value.Equals(true))
                    {
                        GlobalVars.connection.insertEating(Convert.ToInt32(this.dgEatingList.Rows[i].Cells[0].Value),
                            date, 2, cost[1]);
                    }
                    else
                    {
                        GlobalVars.connection.deleteEating(Convert.ToInt32(this.dgEatingList.Rows[i].Cells[0].Value),
                            2, date);
                    }
                }
                //полдник
                if (change_matrix[i, 2])
                {
                    if (this.dgEatingList.Rows[i].Cells[4].Value.Equals(true))
                    {
                        GlobalVars.connection.insertEating(Convert.ToInt32(this.dgEatingList.Rows[i].Cells[0].Value),
                            date, 3, cost[2]);
                    }
                    else
                    {
                        GlobalVars.connection.deleteEating(Convert.ToInt32(this.dgEatingList.Rows[i].Cells[0].Value),
                            3, date);
                    }
                }
                //ужин
                if (change_matrix[i, 3])
                {
                    if (this.dgEatingList.Rows[i].Cells[5].Value.Equals(true))
                    {
                        GlobalVars.connection.insertEating(Convert.ToInt32(this.dgEatingList.Rows[i].Cells[0].Value),
                            date, 4, cost[3]);
                    }
                    else
                    {
                        GlobalVars.connection.deleteEating(Convert.ToInt32(this.dgEatingList.Rows[i].Cells[0].Value),
                            4, date);
                    }
                }
            }
            //вставляем полностью новые записи
            for(int i = main_rows_count; i < this.dgEatingList.Rows.Count; i++)
            {
                //вставка завтрака
                if (this.dgEatingList.Rows[i].Cells[2].Value.Equals(true))
                {
                    GlobalVars.connection.insertEating( Convert.ToInt32(this.dgEatingList.Rows[i].Cells[0].Value),
                        date, 1, cost[0]);
                }
                //вставка обеда
                if (this.dgEatingList.Rows[i].Cells[3].Value.Equals(true))
                {
                    GlobalVars.connection.insertEating(Convert.ToInt32(this.dgEatingList.Rows[i].Cells[0].Value),
                        date, 2, cost[1]);
                }
                //вставка полдника
                if (this.dgEatingList.Rows[i].Cells[4].Value.Equals(true))
                {
                    GlobalVars.connection.insertEating(Convert.ToInt32(this.dgEatingList.Rows[i].Cells[0].Value),
                        date, 3, cost[2]);
                }
                //вставка ужина
                if (this.dgEatingList.Rows[i].Cells[5].Value.Equals(true))
                {
                    GlobalVars.connection.insertEating(Convert.ToInt32(this.dgEatingList.Rows[i].Cells[0].Value),
                        date, 4, cost[3]);
                }
            }
        }

        /// <summary>
        /// Проверка, не устарела ли стомость, вывод формы настройки стоимости по желанию
        /// </summary>
        private void checkDate()
        {
            DateTime ending_date = GlobalVars.connection.getEatscheduleEndDate();
            if (ending_date.Date < cur_date)
            {
                DialogResult dresult = MessageBox.Show("Стоимость питания устарела, желаете задать новую?",
                    "Сохранение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dresult == DialogResult.Yes)
                {
                    Form f = new FormEatCoas();
                    f.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Событие, сохраняющиее индексы изменившихся ячеек в матрице изменений
        /// </summary>
        private void dgEatingList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (loaded)
            {
                if (e.RowIndex < main_rows_count)
                {
                    change_matrix[e.RowIndex, e.ColumnIndex - 2] = !change_matrix[e.RowIndex, e.ColumnIndex - 2];
                }
            }
        }

        /// <summary>
        /// Обработка закрытия формы
        /// </summary>
        private void FormEat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!locked)
            {
                this.dgEatingList.EndEdit();
                DialogResult dresult = MessageBox.Show("Сохранить изменения?",
                    "Сохранение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dresult == DialogResult.Yes)
                {
                    saveEatingData();
                }
            }
        }            
    }
}

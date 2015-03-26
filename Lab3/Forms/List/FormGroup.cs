using MySql.Data.MySqlClient;
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
    public partial class FormGroup : Form
    {
        public bool isEnd = false;

        public FormGroup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Первоначальная инициализация программы
        /// </summary>
        public void init()
        {
            loadData();
        }

        /// <summary>
        /// Загрузка данных в DataGrid dgGroupList
        /// </summary>
        public void loadData()
        {
            if (this.dgGroupList.Columns.Count > 0)
            {
                this.dgGroupList.Columns.Clear();
            }

            if (this.dgGroupList.Rows.Count > 0)
            {
                this.dgGroupList.Rows.Clear();
            }
            this.dgGroupList.DataSource = GlobalVars.connection.getGroupList();
            this.dgGroupList.Columns[0].Visible = false; //Скрываем ид
        }
        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGroup_Load(object sender, EventArgs e)
        {
            init();
        }

        /// <summary>
        /// Обработка удаления пользователем строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgGroupList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dresult = GlobalVars.showQuestionMsgBox("Вы уверены, что хотите удалить группу?");
            if (dresult == DialogResult.Yes)
            {
                int rowIndex = e.Row.Index;
                int groupId = -1;

                if (this.dgGroupList[0, rowIndex].Value != null)
                {
                    String tmp = this.dgGroupList[0, rowIndex].FormattedValue.ToString();
                    if (!tmp.Equals(""))
                    {
                        groupId = Convert.ToInt32(tmp);
                    }
                }

                if (groupId != -1)
                {
                    if (!GlobalVars.connection.deleteGroup(groupId))
                    {
                        GlobalVars.showWarningMsgBox("Невозможно удалить группу. Есть связанные данные.");
                    }
                    else
                    {
                        loadData();
                    }
                }
            }
        }

        /// <summary>
        /// Обработка закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isEnd)
            {
                DialogResult dresult = GlobalVars.showQuestionMsgBox("Сохранить изменения?");
                if (dresult == DialogResult.Yes)
                {
                    if (saveData())
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        /// <summary>
        /// Проверяет на валидность введенный возраст
        /// </summary>
        /// <param name="val">возраст</param>
        /// <returns></returns>
        private bool isValidateAge(double val)
        {
            String buf = val.ToString();

            //Проверяем есть ли у нас дробная часть
            if (buf.IndexOf(',') >= 0)
            {
                String intPart = buf.Split(new char[] { ',' })[0];
                String fractpart = buf.Split(new char[] { ',' })[1];

                if (Convert.ToInt32(intPart) > 6)
                {
                    return false;
                }
                if (Convert.ToInt32(fractpart) % 5 != 0)
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (val <= 6)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверка данных на валидность
        /// </summary>
        /// <returns>Возвращает true если все данные валидны иначе возвращает false</returns>
        private bool validateData()
        {
            int rowsCount = this.dgGroupList.Rows.Count;
            int columnsCount = this.dgGroupList.Columns.Count;

            if (rowsCount == 1)
            {
                //GlobalVars.showWarningMsgBox("Необходимо заполнить все поля");
                return true;
            }

            List<String> names = new List<String>();
            for (int i = 0; i < rowsCount - 1; i++)
            {
                if (this.dgGroupList[1, i].Value == null || !GlobalVars.isValidateString(this.dgGroupList[1, i].Value.ToString()))
                {
                    GlobalVars.showWarningMsgBox("Укажите корректное название группы");
                    this.dgGroupList.CurrentCell = this.dgGroupList[1, i];
                    return false;
                }
                else
                {
                    if (names.Contains(this.dgGroupList[1, i].Value.ToString()))
                    {
                        this.dgGroupList.CurrentCell = this.dgGroupList[1, i];
                        GlobalVars.showWarningMsgBox("Названия групп должны быть уникальными");
                        return false;
                    }
                    else
                    {
                        names.Add(this.dgGroupList[1, i].Value.ToString());
                    }
                }

                if (this.dgGroupList[2, i].Value.ToString().Equals("") ||
                    !isValidateAge(Convert.ToDouble(this.dgGroupList[2, i].Value)))
                {
                    GlobalVars.showWarningMsgBox("Укажите корректный возраст");
                    this.dgGroupList.CurrentCell = this.dgGroupList[2, i];
                    return false;
                }
                if (this.dgGroupList[3, i].Value.ToString().Equals("") ||
                    !isValidateAge(Convert.ToDouble(this.dgGroupList[3, i].Value)))
                {
                    GlobalVars.showWarningMsgBox("Укажите корректный возраст");
                    this.dgGroupList.CurrentCell = this.dgGroupList[3, i];
                    return false;
                }
                if (this.dgGroupList[4, i].Value.ToString().Equals("") ||
                    Convert.ToInt32(this.dgGroupList[4, i].Value) <= 0)
                {
                    GlobalVars.showWarningMsgBox("Укажите корректную вместимость группы");
                    this.dgGroupList.CurrentCell = this.dgGroupList[4, i];
                    return false;
                }
                if (this.dgGroupList[5, i].Value.ToString().Equals("") ||
                    Convert.ToDouble(this.dgGroupList[5, i].Value) <= 0)
                {
                    GlobalVars.showWarningMsgBox("Укажите корректную стоимость");
                    this.dgGroupList.CurrentCell = this.dgGroupList[5, i];
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Сохранение изменений в таблице
        /// </summary>
        private bool saveData()
        {
            if (validateData())
            {
                int rowCount = this.dgGroupList.Rows.Count;
                for (int i = 0; i < rowCount - 1; i++)
                {
                    if (this.dgGroupList[0, i].Value == null || this.dgGroupList[0, i].Value.ToString().Equals(""))
                    {
                        GlobalVars.connection.insertGroup(
                            this.dgGroupList[1, i].Value.ToString(),
                            Convert.ToDouble(this.dgGroupList[3, i].Value),
                            Convert.ToDouble(this.dgGroupList[2, i].Value),
                            Convert.ToInt32(this.dgGroupList[4, i].Value),
                            Convert.ToDouble(this.dgGroupList[5, i].Value)
                            );
                    }
                    else
                    {
                        GlobalVars.connection.updateGroup(
                            Convert.ToInt32(this.dgGroupList[0, i].Value),
                            this.dgGroupList[1, i].Value.ToString(),
                            Convert.ToDouble(this.dgGroupList[3, i].Value),
                            Convert.ToDouble(this.dgGroupList[2, i].Value),
                            Convert.ToInt32(this.dgGroupList[4, i].Value),
                            Convert.ToDouble(this.dgGroupList[5, i].Value)
                            );
                    }
                }
                isEnd = true;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

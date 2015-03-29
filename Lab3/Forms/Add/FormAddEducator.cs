using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3.Add
{
    public partial class FormAddEducator : Form
    {
        int educator_id;

        /// <summary>
        /// Конструктор формы добавления/редактирования воспитателя
        /// </summary>
        /// <param name="id">Первичный ключ воспитателя</param>
        public FormAddEducator(int id)
        {
            InitializeComponent();
            educator_id = id;
        }

        /// <summary>
        /// Инициализация формы для добавления воспитателя
        /// </summary>
        public void InitializeAdd()
        {
            btnSave.Text = "Добавить";
        }

        /// <summary>
        /// Инициализация формы для редактирования воспитателя
        /// </summary>
        public void InitializeEdit()
        {
            this.Text = "Изменить воспитателя";

            DataTable dt = GlobalVars.connection.getEducatorById(educator_id);

            this.tbSecondname.Text = dt.Rows[0].ItemArray[0].ToString();
            this.tbFirstname.Text = dt.Rows[0].ItemArray[1].ToString();
            this.tbThirdname.Text = dt.Rows[0].ItemArray[2].ToString();
            this.tbPhone.Text = dt.Rows[0].ItemArray[3].ToString();
            this.tbLocation.Text = dt.Rows[0].ItemArray[4].ToString();
            this.cbGroup.SelectedValue = dt.Rows[0].ItemArray[5];
        }

        /// <summary>
        /// Загрузка списка групп в cbGroup
        /// </summary>
        public void getGroupList()
        {
            this.cbGroup.DataSource = GlobalVars.connection.getGroupLoV();
            this.cbGroup.DisplayMember = "Название";
            this.cbGroup.ValueMember = "Номер";
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAddEducator_Load(object sender, EventArgs e)
        {
            getGroupList();
            if (educator_id == -1) 
            {
                InitializeAdd();
            }
            else
            {
                InitializeEdit();
            }
        }

        /// <summary>
        /// Проверка имени на валидность
        /// </summary>
        /// <param name="str">Строка, которую необходимо проверить</param>
        /// <returns>Возвращает true если строка не содержит лишних символов, иначе возвращает false</returns>
        private bool isValidName(String str)
        {
            char[] stopChar = { '%', '$', '!', '^', '#', '@', '\'', '\"', '\\', '|', '/', '*', '&', '~', '=', '<', '>',
                              '0','1','2','3','4','5','6','7','8','9'};
            if (str.IndexOfAny(stopChar) >= 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Проверка имени на пустоту
        /// </summary>
        /// <param name="str">Строка, которую необходимо проверить</param>
        /// <returns>Возвращает true если строка не пуста, иначе возвращает false</returns>
        private bool isEmptyName(String str)
        {
            if (str == null || str.Equals("") || str.Equals(" "))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Проверка введеных данных на валидность
        /// </summary>
        /// <returns>Возвращает true если все данные валидны иначе возвращает false</returns>
        private bool validateData()
        {
            if ( !isValidName(this.tbSecondname.Text.ToString()) ||
                    !isEmptyName(this.tbSecondname.Text.ToString()) )
            {
                GlobalVars.showWarningMsgBox(this, "Фамилия указана некорректно");
                this.tbSecondname.Focus();
                return false;
            }
            if (!isValidName(this.tbFirstname.Text.ToString()) ||
                    !isEmptyName(this.tbFirstname.Text.ToString()))
            {
                GlobalVars.showWarningMsgBox(this, "Имя указано некорректно");
                this.tbFirstname.Focus();
                return false;
            }
            if ( !isValidName(this.tbThirdname.Text.ToString()) )
            {
                GlobalVars.showWarningMsgBox(this, "Отчество указано некорректно");
                this.tbThirdname.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Добавление нового воспитателя в список
        /// </summary>
        private void addEducator()
        {
            GlobalVars.connection.insertEducator(
                this.tbFirstname.Text.ToString(),
                this.tbSecondname.Text.ToString(),
                this.tbThirdname.Text.ToString(),
                this.tbPhone.Text.ToString(),
                this.tbLocation.Text.ToString(),
                Convert.ToInt32(cbGroup.SelectedValue)
                );
        }

        /// <summary>
        /// Обновление информации о воспитатели
        /// </summary>
        private void updateEducator()
        {
            GlobalVars.connection.updateEducator(
                this.tbFirstname.Text.ToString(),
                this.tbSecondname.Text.ToString(),
                this.tbThirdname.Text.ToString(),
                this.tbPhone.Text.ToString(),
                this.tbLocation.Text.ToString(),
                Convert.ToInt32(cbGroup.SelectedValue),
                educator_id
                );
        }

        /// <summary>
        /// Обработчик кнопки "Сохранить/Добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validateData())
            {
                if (educator_id == -1)
                {
                    addEducator();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    updateEducator();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}

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
    public partial class FormEducators : Form
    {
        public FormEducators()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вызывает форму в диалоговом режиме
        /// </summary>
        /// <param name="form">Форма, которую нужно открыть</param>
        /// <returns>Возвращает DialogResult формы</returns>
        private DialogResult showForm(Form form)
        {
            while (form.DialogResult != DialogResult.OK && form.DialogResult != DialogResult.Cancel)
            {
                try
                {
                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return form.DialogResult;
        }

        /// <summary>
        /// Первоначальная инициализация формы
        /// </summary>
        public void init()
        {
            loadData();
        }
        /// <summary>
        /// Загрузка данных о воспитателях в DataGrid dgEducatorList
        /// </summary>
        public void loadData()
        {
            if (this.dgEducatorList.Columns.Count > 0)
            {
                this.dgEducatorList.Columns.Clear();
            }

            if (this.dgEducatorList.Rows.Count > 0)
            {
                this.dgEducatorList.Rows.Clear();
            }
            this.dgEducatorList.DataSource = GlobalVars.connection.getEducatorList();
            this.dgEducatorList.Columns[5].Visible = false; 
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormEducators_Load(object sender, EventArgs e)
        {
            init();
        }

        /// <summary>
        /// Открытие формы добавления нового воспитателя и обработка результата её работы 
        /// </summary>
        public void addEducator()
        {
            DialogResult result = showForm(new Add.FormAddEducator(-1));
            if (result == DialogResult.OK)
            {
                loadData();
            }
        }

        /// <summary>
        /// Открытие формы редактирования выделеного воспитателя и обработка результата её работы 
        /// </summary>
        public void editEducator()
        {
            int rowIndex = this.dgEducatorList.CurrentCellAddress.Y;
            int educator_id = Convert.ToInt32(this.dgEducatorList[5, rowIndex].Value);
            DialogResult result = showForm(new Add.FormAddEducator(educator_id));
            if (result == DialogResult.OK)
            {
                loadData();
            }
        }

        /// <summary>
        /// Удаление выделеного в dgEducatorList воспитателя
        /// </summary>
        public void deleteSelectedEducator()
        {
            DialogResult dresult = GlobalVars.showQuestionMsgBox("Вы уверены, что хотите удалить этого воспитателя?");
            if (dresult == DialogResult.Yes)
            {
                int rowIndex = this.dgEducatorList.CurrentCellAddress.Y;
                int educatorId = -1;

                if (this.dgEducatorList[5, rowIndex].Value != null)
                {
                    String tmp = this.dgEducatorList[5, rowIndex].FormattedValue.ToString();
                    if (!tmp.Equals(""))
                    {
                        educatorId = Convert.ToInt32(tmp);
                    }
                }

                if (educatorId != -1)
                {
                    if (!GlobalVars.connection.deleteEducator(educatorId))
                    {
                        GlobalVars.showWarningMsgBox("Невозможно удалить воспитателя. Есть связанные данные.");
                    }
                    else
                    {
                        loadData();
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик кнопки "Добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEducator_Click(object sender, EventArgs e)
        {
            addEducator();
        }

        /// <summary>
        /// Обработчик кнопки "Изменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditEducator_Click(object sender, EventArgs e)
        {
            editEducator();
        }

        /// <summary>
        /// Обработчик кнопки "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteEducator_Click(object sender, EventArgs e)
        {
            deleteSelectedEducator();
        }
    }
}

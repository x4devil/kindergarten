using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab3.Forms;
using System.Threading;

namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
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
        /// Загрузка сервера, имени пользователя и пароля из файла конфигурации
        /// </summary>
        private void initConfiguration()
        {
            GlobalVars.server = ConfigurationManager.AppSettings["server"].ToString();
            GlobalVars.user = ConfigurationManager.AppSettings["user"].ToString();
            GlobalVars.password = ConfigurationManager.AppSettings["password"].ToString();
        }

        /// <summary>
        /// Первоначальная инициализация программы
        /// </summary>
        private void init()
        {
            initConfiguration();
            GlobalVars.connection = new MySQLConnector(GlobalVars.server, GlobalVars.user, GlobalVars.password);

            Form box = new ConnectMsgBox();

            Thread th = new Thread(() =>
            {
                box.ShowDialog();
            });
            th.Start();
            if (!GlobalVars.connection.openConnect())
            {
                th.Abort();
                //Если не удалось подключиться, то открываем форму настройки БД
                DialogResult result = showForm(new FormSettDB());

                if (result == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
            th.Abort();

            //Проверяем есть ли группы
            if (!GlobalVars.connection.isHasGroup())
            {
                GlobalVars.showWarningMsgBox("Необходимо создать хотя бы одну группу");
                DialogResult result = showForm(new FormGroup());

                if (result == DialogResult.Cancel && !GlobalVars.connection.isHasGroup())
                {
                    this.Close();
                }
            }

            //Загружаем названия групп
            initComboBoxGroupNames();
            //Загружаем список детей
            initChildList(GlobalVars.activeGroupId);
        }

        /// <summary>
        /// Загружаем названия групп в cbGroupNames
        /// </summary>
        private void initComboBoxGroupNames()
        {

            List<String> names = GlobalVars.connection.getGroupNamesList();
            int countNames = names.Count;
            for (int i = 0; i < countNames; i++)
            {
                this.cbGroupNames.Items.Add(names[i]);
            }
            this.cbGroupNames.SelectedIndex = 0;
        }

        /// <summary>
        /// Загружает список детей для определенной группы
        /// </summary>
        /// <param name="groupId">Ид группы</param>
        private void initChildList(int groupId)
        {
            if (this.dgChildList.Columns.Count > 0)
            {
                this.dgChildList.Columns.Clear();
            }

            if (this.dgChildList.Rows.Count > 0)
            {
                this.dgChildList.Rows.Clear();
            }
            this.dgChildList.DataSource = GlobalVars.connection.getChildList(groupId);
        }
        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            init();
        }

        /// <summary>
        /// Загрузка формы настройки БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void подключениеКБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = showForm(new FormSettDB());

            if (result == DialogResult.Cancel)
            {
                if (!GlobalVars.connection.connectionIsOpen())
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Загрузка формы со списком групп
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void списокГруппToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new FormGroup());
        }

        /// <summary>
        /// Загрузка формы со списком воспитателей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void списокВоспитателейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new FormEducators());
        }

        /// <summary>
        /// Обработка изменения выбранного элемента в cbGroupNames
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbGroupNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectIndex = this.cbGroupNames.SelectedIndex;
            if (selectIndex >= 0)
            {
                String name = this.cbGroupNames.Items[selectIndex].ToString();
                GlobalVars.activeGroupId = GlobalVars.connection.getGroupIdByName(name);
                initChildList(GlobalVars.activeGroupId);
            }
        }

        /// <summary>
        /// Обработка удаления выбранного ребенка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.dgChildList.CurrentRow != null)
            {
                int rowIndex = this.dgChildList.CurrentRow.Index;
                int babyId = Convert.ToInt32(this.dgChildList[0, rowIndex].Value);
                
                DialogResult dresult = GlobalVars.showQuestionMsgBox("Вы уверены, что хотите удалить всю информацию о ребенке?");
                if (dresult == DialogResult.Yes)
                {
                    GlobalVars.connection.deleteBaby(babyId);
                    initChildList(GlobalVars.activeGroupId);
                }
            }
        }

        /// <summary>
        /// Обработка перевода ребенка в другую группу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMove_Click(object sender, EventArgs e)
        {
            if (GlobalVars.connection.getGroupCount() > 1)
            {
                if (this.dgChildList.CurrentRow != null)
                {
                    int rowIndex = this.dgChildList.CurrentRow.Index;
                    GlobalVars.activeBabyId = Convert.ToInt32(this.dgChildList[0, rowIndex].Value);

                    showForm(new FormMove());
                    initChildList(GlobalVars.activeGroupId);
                }
            }
            else
            {
                GlobalVars.showWarningMsgBox("У вас нет других групп");
            }
        }
    }
}

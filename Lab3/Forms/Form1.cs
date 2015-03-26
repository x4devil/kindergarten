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
    }
}

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
using System.Xml;

namespace Lab3.Forms
{
    public partial class FormSettDB : Form
    {
        public FormSettDB()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка сервера, имени пользователя и пароля из файла конфигурации
        /// </summary>
        private void initConfiguration()
        {
            this.tbServer.Text = ConfigurationManager.AppSettings["server"].ToString();
            this.tbUser.Text = ConfigurationManager.AppSettings["user"].ToString();
            this.tbPassword.Text = ConfigurationManager.AppSettings["password"].ToString();
        }
        /// <summary>
        /// Вывод предупреждающего сообщения
        /// </summary>
        /// <param name="msg">Текст сообщения</param>
        private void showWarningMsgBox(String msg)
        {
            MessageBox.Show(this, msg, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Проверка данных на валидность
        /// </summary>
        /// <returns>Возвращает true если все данные валидны иначе возвращает false</returns>
        private bool validateData()
        {
            if (this.tbUser.Text.Equals("") || this.tbUser.Text.Equals(" "))
            {
                showWarningMsgBox("Необходимо указать имя пользователя");
                this.tbUser.Focus();
                return false;
            }
            else if (this.tbPassword.Equals("") || this.tbPassword.Equals(" "))
            {
                showWarningMsgBox("Необходимо указать имя пароль");
                this.tbPassword.Focus();
                return false;
            }
            else
            {
                if (this.tbServer.Equals("") || this.tbServer.Equals(" "))
                {
                    showWarningMsgBox("Необходимо указать IP адрес сервера");
                    this.tbServer.Focus();
                    return false;
                }
                else
                {
                    String txt = this.tbServer.Text;
                    int size = txt.Length;
                    int count = 1;
                    for (int j = 0, i = 0; i < size; i++)
                    {
                        if (txt[i] != '.')
                        {
                            j++;
                            if (j > 3)
                            {
                                showWarningMsgBox("Необходимо указать корректный IP адрес сервера");
                            }
                        }
                        else
                        {
                            j = 0;
                            count++;
                        }
                    }

                    if (count > 4)
                    {
                        showWarningMsgBox("Необходимо указать корректный IP адрес сервера");
                    }
                }
            }

            return true;
        }
        
        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormSettDB_Load(object sender, EventArgs e)
        {
            initConfiguration();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Подключиться"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (validateData())
            {
                GlobalVars.connection = new MySQLConnector(this.tbServer.Text, this.tbUser.Text, this.tbPassword.Text);

                if (GlobalVars.connection.openConnect())
                {
                    MessageBox.Show(this, "Подключение к БД прошло успешно","Подключение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Записываем новые данные в конфиг
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    foreach (XmlElement element in xmlDoc.DocumentElement)
                    {
                        if (element.Name.Equals("appSettings"))
                        {
                            foreach (XmlNode node in element.ChildNodes)
                            {
                                if (node.Attributes[0].Value.Equals("server"))
                                {
                                    node.Attributes[1].Value = this.tbServer.Text;
                                }

                                if (node.Attributes[0].Value.Equals("user"))
                                {
                                    node.Attributes[1].Value = this.tbUser.Text;
                                }

                                if (node.Attributes[0].Value.Equals("password"))
                                {
                                    node.Attributes[1].Value = this.tbPassword.Text;
                                }
                            }
                        }
                    }

                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    ConfigurationManager.RefreshSection("appSettings");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Обработчик ввода имени сервера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbServer_TextChanged(object sender, EventArgs e)
        {
            int size = this.tbServer.Text.Length;
            if (size == 0)
            {
                return;
            }
            if (this.tbServer.Text[size - 1] < '0' || this.tbServer.Text[size - 1] > '9')
            {
                if (this.tbServer.Text[size - 1] != '.')
                {
                    this.tbServer.Text = this.tbServer.Text.Remove(size - 1);
                    this.tbServer.SelectionStart = this.tbServer.Text.Length;
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Отменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

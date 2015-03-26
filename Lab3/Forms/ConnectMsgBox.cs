using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3.Forms
{
    public partial class ConnectMsgBox : Form
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ConnectMsgBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectMsgBox_Load(object sender, EventArgs e)
        {
            this.pbLoad.Value = 0;
            this.timer1.Start();
        }

        /// <summary>
        /// Действие по таймеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.pbLoad.Value < 100)
            {
                this.pbLoad.Value += 1;
            }
            else
            {
                this.pbLoad.Value = 0;
            }
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectMsgBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Stop();
        }

    }
}

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
    public partial class FormPay : Form
    {
        public FormPay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPay_Load(object sender, EventArgs e)
        {
            this.tbSurname.Text = GlobalVars.babyPay.surname;
            this.tbName.Text = GlobalVars.babyPay.name;
            this.tbPatronomic.Text = GlobalVars.babyPay.patronomic;
            this.dtBirthday.Value = GlobalVars.babyPay.birthday;
        }

        /// <summary>
        /// Обработка нажатия кнопки оплатить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (this.nudBalance.Value > 0)
            {
                double balance = GlobalVars.connection.getBabyBalance(GlobalVars.babyPay.id);
                balance += (double)this.nudBalance.Value;
                GlobalVars.connection.updateBabyPayment(GlobalVars.babyPay.id, balance);
                GlobalVars.babyPay.balance = balance;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                GlobalVars.showWarningMsgBox(this, "Сумма платежа не может быть нулевой");
                this.nudBalance.Focus();
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки отмена
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

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
    public partial class FormCoast : Form
    {
        public FormCoast()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Расчитывает задолженность ребенка за пребывание 
        /// </summary>
        /// <param name = "babyId">Ид ребенка</param>>
        /// <returns>Задолженость ребенка за пребывание</returns>
        public double countVisitingDebt(int babyId)
        {
            DataTable visiting = GlobalVars.connection.getVisitingByBabyId(babyId);
            if (visiting != null && visiting.Rows.Count > 0)
            {
                double debt = 0;
                int rowsCount = visiting.Rows.Count;

                for (int i = 0; i < rowsCount; i++)
                {
                    try
                    {
                        DateTime begin = Convert.ToDateTime(visiting.Rows[i][0]);
                        DateTime end = Convert.ToDateTime(visiting.Rows[i][1]);
                        double coast = (Convert.ToDouble(visiting.Rows[i][2]) / 60); //Стоимость минуты

                        TimeSpan span = end - begin;
                        int minutes = (span.Hours * 60);
                        if (!Settings.isFullHour)
                        {
                            minutes += span.Minutes;
                        }

                        debt += (coast * minutes);
                    }
                    catch
                    {

                    }
                }

                return debt;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Расчитывает задолженность ребенка за питание 
        /// </summary>
        /// <param name = "babyId">Ид ребенка</param>>
        /// <returns>Задолженость ребенка за питание</returns>
        public double countEatingDebt(int babyId)
        {
            DataTable table = GlobalVars.connection.getEatingByBabyId(babyId);
            if (table != null && table.Rows.Count > 0)
            {
                double debt = 0;
                int rowsCount = table.Rows.Count;
                for (int i = 0; i < rowsCount; i++)
                {
                    int coast = Convert.ToInt32(table.Rows[i][0]);
                    debt += coast;
                }
                return debt;
            }
            else
            {
                return 0;
            }
        }
        
        /// <summary>
        /// Первоначальная инициализация
        /// </summary>
        public void init()
        {
            DataTable table = GlobalVars.connection.getPaymentList();
            if (table != null && table.Rows.Count > 0)
            {
                int activeBabyIndex = -1;
                int rowsCount = table.Rows.Count;
                int id;
                double balance, debt;

                for (int i = 0; i < rowsCount; i++)
                {
                    this.dgPaymentList.Rows.Add();
                    id = Convert.ToInt32(table.Rows[i][0]);
                    if (GlobalVars.babyIsFind && id == GlobalVars.activeBabyId)
                    {
                        activeBabyIndex = i;
                    }
                    this.dgPaymentList[0, i].Value = id;
                    this.dgPaymentList[1, i].Value = table.Rows[i][1].ToString();
                    this.dgPaymentList[2, i].Value = table.Rows[i][2].ToString();
                    this.dgPaymentList[3, i].Value = table.Rows[i][3].ToString();
                    this.dgPaymentList[4, i].Value = Convert.ToDateTime(table.Rows[i][4]);
                    this.dgPaymentList[5, i].Value = table.Rows[i][5].ToString();

                    balance = Convert.ToDouble(table.Rows[i][6]);
                    debt = countVisitingDebt(id);
                    debt += countEatingDebt(id);
                    this.dgPaymentList[6, i].Value = balance - debt;
                }

                if (activeBabyIndex != -1)
                {
                    this.dgPaymentList.CurrentCell = this.dgPaymentList[1, activeBabyIndex];
                }
            }

        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCoast_Load(object sender, EventArgs e)
        {
            init();
        }

        /// <summary>
        /// Обработка нажатия кнопки оплатить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (this.dgPaymentList.CurrentRow != null)
            {
                int rowIndex = this.dgPaymentList.CurrentRow.Index;
                int babyId = Convert.ToInt32(this.dgPaymentList[0, rowIndex].Value);

                GlobalVars.babyPay = new Forms.Baby();

                GlobalVars.babyPay.id = babyId;
                GlobalVars.babyPay.surname = this.dgPaymentList[1, rowIndex].Value.ToString();
                GlobalVars.babyPay.name = this.dgPaymentList[2, rowIndex].Value.ToString();
                GlobalVars.babyPay.patronomic = this.dgPaymentList[3, rowIndex].Value.ToString();
                GlobalVars.babyPay.birthday = Convert.ToDateTime(this.dgPaymentList[4, rowIndex].Value);
                Form payForm = new FormPay();

                while(payForm.DialogResult != DialogResult.OK &&
                    payForm.DialogResult != DialogResult.Cancel)
                {
                    payForm.ShowDialog();
                }

                if (payForm.DialogResult == DialogResult.OK)
                {
                    double debt = countVisitingDebt(babyId);
                    debt += countEatingDebt(babyId);
                    double balance = GlobalVars.babyPay.balance - debt;
                    this.dgPaymentList[6, rowIndex].Value = balance;
                }

                GlobalVars.babyPay = null;
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки отчет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReport_Click(object sender, EventArgs e)
        {
            String fname = "Отчет_" + DateTime.Now.Date.ToString("dd_MM_yyyy") + ".txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                int rowsCount = this.dgPaymentList.Rows.Count;
                String str = "";
                for (int i = 0; i < rowsCount; i++)
                {
                    str = "";
                    str += (i + 1);
                    str += " " + this.dgPaymentList[1, i].Value.ToString();
                    str += " " + this.dgPaymentList[2, i].Value.ToString();
                    str += " " + this.dgPaymentList[3, i].Value.ToString();
                    str += " " + this.dgPaymentList[4, i].Value.ToString();
                    str += " " + this.dgPaymentList[5, i].Value.ToString();
                    str += " " + this.dgPaymentList[6, i].Value.ToString();
                    file.WriteLine(str);
                }
            }
            try
            {
                if (GlobalVars.showQuestionMsgBox(this, "Файл сохранен в папке: " + Environment.CurrentDirectory + ". Хотите открыть его?") == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(fname);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
    }
}

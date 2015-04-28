using Lab3.Objects;
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
    public partial class FormEatCoas : Form
    {
        private EatShedule shedule;
        public FormEatCoas()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Первоначальная инициализация
        /// </summary>
        private void init()
        {
            shedule = GlobalVars.connection.getEatShedule();
            if (shedule != null)
            {
                this.dtBegin.Value = shedule.dateBegin;
                this.dtEnd.Value = shedule.dateEnd;

                this.udCoast1.Value = shedule.breakfast.coast;
                this.udCoast2.Value = shedule.snack.coast;
                this.udCoast3.Value = shedule.lunch.coast;
                this.udCoast4.Value = shedule.dinner.coast;

                this.dtBegin1.Value = new DateTime(1990, 1, 1, shedule.breakfast.timeBegin.Hours, shedule.breakfast.timeBegin.Minutes, shedule.breakfast.timeBegin.Seconds);
                this.dtEnd1.Value = new DateTime(1995, 1, 1, shedule.breakfast.timeEnd.Hours, shedule.breakfast.timeEnd.Minutes, shedule.breakfast.timeEnd.Seconds);

                this.dtBegin2.Value = new DateTime(1990, 1, 1, shedule.snack.timeBegin.Hours, shedule.snack.timeBegin.Minutes, shedule.snack.timeBegin.Seconds);
                this.dtEnd2.Value = new DateTime(1995, 1, 1, shedule.snack.timeEnd.Hours, shedule.snack.timeEnd.Minutes, shedule.snack.timeEnd.Seconds);

                this.dtBegin3.Value = new DateTime(1990, 1, 1, shedule.lunch.timeBegin.Hours, shedule.lunch.timeBegin.Minutes, shedule.lunch.timeBegin.Seconds);
                this.dtEnd3.Value = new DateTime(1995, 1, 1, shedule.lunch.timeEnd.Hours, shedule.lunch.timeEnd.Minutes, shedule.lunch.timeEnd.Seconds);

                this.dtBegin4.Value = new DateTime(1990, 1, 1, shedule.dinner.timeBegin.Hours, shedule.dinner.timeBegin.Minutes, shedule.dinner.timeBegin.Seconds);
                this.dtEnd4.Value = new DateTime(1995, 1, 1, shedule.dinner.timeEnd.Hours, shedule.dinner.timeEnd.Minutes, shedule.dinner.timeEnd.Seconds);
            }
        }
        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormEatCoas_Load(object sender, EventArgs e)
        {
            init();
        }

        /// <summary>
        /// Проверка данных на валидность
        /// </summary>
        /// <returns></returns>
        private bool validateData()
        {
            if (this.dtBegin.Value > this.dtEnd.Value)
            {
                GlobalVars.showWarningMsgBox(this, "Дата начала не может быть больше даты окончания");
                this.dtBegin.Focus();
                return false;
            }

            if (this.dtBegin1.Value.TimeOfDay > this.dtEnd1.Value.TimeOfDay)
            {
                GlobalVars.showWarningMsgBox(this, "Время начала не может быть больше времени окончания");
                this.dtBegin1.Focus();
                return false;
            }
            if (this.dtEnd1.Value.TimeOfDay > this.dtBegin2.Value.TimeOfDay)
            {
                GlobalVars.showWarningMsgBox(this, "Время не может пересекаться");
                this.dtBegin2.Focus();
                return false;
            }

            if (this.dtBegin2.Value.TimeOfDay > this.dtEnd2.Value.TimeOfDay)
            {
                GlobalVars.showWarningMsgBox(this, "Время начала не может быть больше времени окончания");
                this.dtBegin2.Focus();
                return false;
            }

            if (this.dtEnd2.Value.TimeOfDay > this.dtBegin3.Value.TimeOfDay)
            {
                GlobalVars.showWarningMsgBox(this, "Время не может пересекаться");
                this.dtBegin3.Focus();
                return false;
            }

            if (this.dtBegin3.Value.TimeOfDay > this.dtEnd3.Value.TimeOfDay)
            {
                GlobalVars.showWarningMsgBox(this, "Время начала не может быть больше времени окончания");
                this.dtBegin3.Focus();
                return false;
            }

            if (this.dtEnd3.Value.TimeOfDay > this.dtBegin4.Value.TimeOfDay)
            {
                GlobalVars.showWarningMsgBox(this, "Время не может пересекаться");
                this.dtBegin4.Focus();
                return false;
            }

            if (this.dtBegin4.Value.TimeOfDay > this.dtEnd4.Value.TimeOfDay)
            {
                GlobalVars.showWarningMsgBox(this, "Время начала не может быть больше времени окончания");
                this.dtBegin4.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Сохранение информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validateData())
            {
                this.DialogResult = DialogResult.OK;
                shedule.dateBegin = this.dtBegin.Value;
                shedule.dateEnd = this.dtEnd.Value;

                shedule.breakfast.coast = Convert.ToInt32(this.udCoast1.Value);
                shedule.breakfast.timeBegin = this.dtBegin1.Value.TimeOfDay;
                shedule.breakfast.timeEnd = this.dtEnd1.Value.TimeOfDay;

                shedule.snack.coast = Convert.ToInt32(this.udCoast2.Value);
                shedule.snack.timeBegin = this.dtBegin2.Value.TimeOfDay;
                shedule.snack.timeEnd = this.dtEnd2.Value.TimeOfDay;

                shedule.lunch.coast = Convert.ToInt32(this.udCoast3.Value);
                shedule.lunch.timeBegin = this.dtBegin3.Value.TimeOfDay;
                shedule.lunch.timeEnd = this.dtEnd3.Value.TimeOfDay;

                shedule.dinner.coast = Convert.ToInt32(this.udCoast4.Value);
                shedule.dinner.timeBegin = this.dtBegin4.Value.TimeOfDay;
                shedule.dinner.timeEnd = this.dtEnd4.Value.TimeOfDay;

                GlobalVars.connection.insertEatShedule(shedule);
                this.Close();
            }
        }

        /// <summary>
        /// Отменить сохранение
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

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
    public partial class MoveForm : Form
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MoveForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загружаем названия групп в cbGroupNames
        /// </summary>
        private void initComboBoxGroupNames()
        {
            String name = GlobalVars.connection.getGroupNameById(GlobalVars.activeGroupId);
            List<String> names = GlobalVars.connection.getGroupNamesList();
            this.cbGroupNames.Items.Clear();
            int countNames = names.Count;
            for (int i = 0; i < countNames; i++)
            {
                if (!name.Equals(names[i]))
                {
                    this.cbGroupNames.Items.Add(names[i]);
                }
            }
            this.cbGroupNames.SelectedIndex = 0;
        }

        /// <summary>
        /// Обработка нажатия кнопки Отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Обработка нажатия кнопки Перевести
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int selectIndex = this.cbGroupNames.SelectedIndex;
            if (selectIndex >= 0)
            {
                String name = this.cbGroupNames.Items[selectIndex].ToString();
                int groupId = GlobalVars.connection.getGroupIdByName(name);
                

                double maxAge = GlobalVars.connection.getMaxAgeGroup(groupId) * 365;
                double minAge = GlobalVars.connection.getMinAgeGroup(groupId) * 365;

                TimeSpan span = DateTime.Now.Date - GlobalVars.birthdayActiveBaby.Date;
                String buf = "" +  span.Days ;
                double age = Convert.ToDouble(buf);

                DialogResult result = DialogResult.Cancel;
                if (maxAge < age)
                {
                    result = GlobalVars.showQuestionMsgBox(this, "Возраст ребенка превышает максимальный возраст в группе. Хотите продолжить?");
                }
                else if(minAge > age)
                {
                    result = GlobalVars.showQuestionMsgBox(this, "Возраст ребенка меньше минимального возраста в группе. Хотите продолжить?");
                }
                if (result == DialogResult.Yes)
                {
                    GlobalVars.connection.moveChild(GlobalVars.activeBabyId, groupId);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveForm_Load(object sender, EventArgs e)
        {
            initComboBoxGroupNames();
        }
    }
}

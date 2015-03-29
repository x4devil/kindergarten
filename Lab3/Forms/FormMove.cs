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
    public partial class FormMove : Form
    {
        public FormMove()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Загружаем названия групп в cbGroupNames
        /// </summary>
        private void initComboBoxGroupNames()
        {
            String activeName = GlobalVars.connection.getGroupNameById(GlobalVars.activeGroupId);

            List<String> names = GlobalVars.connection.getGroupNamesList();
            int countNames = names.Count;
            for (int i = 0; i < countNames; i++)
            {
                if (!names[i].Equals(activeName))
                {
                    this.cbGroupNames.Items.Add(names[i]);
                } 
            }
            this.cbGroupNames.SelectedIndex = 0;
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMove_Load(object sender, EventArgs e)
        {
            initComboBoxGroupNames();
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            int selectIndex = this.cbGroupNames.SelectedIndex;
            if (selectIndex >= 0)
            {
                String name = this.cbGroupNames.Items[selectIndex].ToString();
                int groupId = GlobalVars.connection.getGroupIdByName(name);

                GlobalVars.connection.moveBaby(GlobalVars.activeBabyId, groupId);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}

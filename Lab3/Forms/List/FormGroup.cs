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
    public partial class FormGroup : Form
    {
        public FormGroup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Первоначальная инициализация программы
        /// </summary>
        public void init()
        {
            this.dgGroupList.DataSource = GlobalVars.connection.getGroupList();
        }
        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGroup_Load(object sender, EventArgs e)
        {
            
        }
    }
}

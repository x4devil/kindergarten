using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3.List
{
    public partial class FormAddBaby : Form
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public FormAddBaby()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конвертирует DateTime в строку формата yyy-MM-dd
        /// </summary>
        /// <param name="dt">Дата</param>
        /// <returns></returns>
        public String convertData(DateTime dt)
        {
            return dt.ToString("yyyy.MM.dd");
        }

        /// <summary>
        /// Конвертирует TimeSpan в строку формата hh:mm:ss
        /// </summary>
        /// <param name="time">Время</param>
        /// <returns></returns>
        private String convertTime(TimeSpan time)
        {
            return time.ToString("hh:mm:ss");
        }
        /// <summary>
        /// Первоначальная инициализация
        /// </summary>
        private void init()
        {

        }
        
        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAddBaby_Load(object sender, EventArgs e)
        {
            if (GlobalVars.babyIsEdit)
            {
                init();
            }
            else
            {
                this.dtEnd.Value = this.dtBegin.Value.Date.AddDays(1);
                this.dgTrustee.Columns[0].Visible = false;
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки Отменить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Обработка нажатия клавиши сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (GlobalVars.babyIsEdit)
            {

            } else {
                insertData();
            }
        }

        /// <summary>
        /// Проверка данных на валидность
        /// </summary>
        /// <returns>true если данные валидны иначе false</returns>
        private bool validateData()
        {
            ///tabPage1
            if (!GlobalVars.isValiString(this.tbBabySurname.Text))
            {
                GlobalVars.showWarningMsgBox(this, "Необходимо указать фамилию ребенка");
                this.tabControl1.SelectedIndex = 0;
                this.tbBabySurname.Focus();
                return false;
            }
            if (!GlobalVars.isValiString(this.tbBabyName.Text))
            {
                GlobalVars.showWarningMsgBox(this, "Необходимо указать имя ребенка");
                this.tabControl1.SelectedIndex = 0;
                this.tbBabyName.Focus();
                return false;
            }
            if (this.dtBabyBirthday.Value.Date >= DateTime.Now.Date)
            {
                TimeSpan span = DateTime.Now.Date - this.dtBabyBirthday.Value.Date;

                if (span.Days < 365)
                {
                    GlobalVars.showWarningMsgBox(this, "Ребенку должен быть хотя бы год");
                    this.tabControl1.SelectedIndex = 0;
                    this.dtBabyBirthday.Focus();
                    return false;
                }
            }

            TimeSpan nulltime = new DateTime(1, 1, 1, 0, 0, 0).TimeOfDay;

            if (this.dtBegin1.Value.TimeOfDay >= this.dtEnd1.Value.TimeOfDay)
            {
                if (this.dtBegin1.Value.TimeOfDay != nulltime && this.dtEnd1.Value.TimeOfDay != nulltime)
                {
                    GlobalVars.showWarningMsgBox(this, "Время прихода не может быть больше времени ухода");
                    this.tabControl1.SelectedIndex = 0;
                    this.dtBegin1.Focus();
                    return false;
                }
                
            }

            if (this.dtBegin2.Value.TimeOfDay >= this.dtEnd2.Value.TimeOfDay)
            {
                if (this.dtBegin2.Value.TimeOfDay != nulltime && this.dtEnd2.Value.TimeOfDay != nulltime)
                {
                    GlobalVars.showWarningMsgBox(this, "Время прихода не может быть больше времени ухода");
                    this.tabControl1.SelectedIndex = 0;
                    this.dtBegin2.Focus();
                    return false;
                }
            }

            if (this.dtBegin3.Value.TimeOfDay >= this.dtEnd3.Value.TimeOfDay)
            {
                if (this.dtBegin3.Value.TimeOfDay != nulltime && this.dtEnd3.Value.TimeOfDay != nulltime)
                {
                    GlobalVars.showWarningMsgBox(this, "Время прихода не может быть больше времени ухода");
                    this.tabControl1.SelectedIndex = 0;
                    this.dtBegin3.Focus();
                    return false;
                }
            }

            if (this.dtBegin4.Value.TimeOfDay >= this.dtEnd4.Value.TimeOfDay)
            {
                if (this.dtBegin4.Value.TimeOfDay != nulltime && this.dtEnd4.Value.TimeOfDay != nulltime)
                {
                    GlobalVars.showWarningMsgBox(this, "Время прихода не может быть больше времени ухода");
                    this.tabControl1.SelectedIndex = 0;
                    this.dtBegin4.Focus();
                    return false;
                }
            }

            if (this.dtBegin5.Value.TimeOfDay >= this.dtEnd5.Value.TimeOfDay)
            {
                if (this.dtBegin5.Value.TimeOfDay != nulltime && this.dtEnd5.Value.TimeOfDay != nulltime)
                {
                    GlobalVars.showWarningMsgBox(this, "Время прихода не может быть больше времени ухода");
                    this.tabControl1.SelectedIndex = 0;
                    this.dtBegin1.Focus();
                    return false;
                }
            }

            if (this.dtBegin6.Value.TimeOfDay >= this.dtEnd6.Value.TimeOfDay)
            {
                if (this.dtBegin6.Value.TimeOfDay != nulltime && this.dtEnd6.Value.TimeOfDay != nulltime)
                {
                    GlobalVars.showWarningMsgBox(this, "Время прихода не может быть больше времени ухода");
                    this.tabControl1.SelectedIndex = 0;
                    this.dtBegin1.Focus();
                    return false;
                }
            }

            if (this.dtBegin7.Value.TimeOfDay >= this.dtEnd7.Value.TimeOfDay)
            {
                if (this.dtBegin7.Value.TimeOfDay != nulltime && this.dtEnd7.Value.TimeOfDay != nulltime)
                {
                    GlobalVars.showWarningMsgBox(this, "Время прихода не может быть больше времени ухода");
                    this.tabControl1.SelectedIndex = 0;
                    this.dtBegin7.Focus();
                    return false;
                }
            }

            if (this.dtBegin.Value.Date >= this.dtEnd.Value.Date)
            {
                GlobalVars.showWarningMsgBox(this, "Дата начала планируемого графика не может быть больше даты окончания");
                this.tabControl1.SelectedIndex = 0;
                this.dtBegin.Focus();
                return false;
            }

            //tabPage2
            if (!validateParent())
            {
                return false;
            }

            //tabPage3

            if (!validateTrustee())
            {
                return false;
            }

            bool parentEmpty = parentIsEmpty();
            bool trusteeEmpty  = trusteeIsEmpty(); 
            if ( parentEmpty &&  trusteeEmpty)
            {
                GlobalVars.showWarningMsgBox(this, "Укажите родителей или довереных лиц");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Проверяет данные о родителях на пустоту
        /// </summary>
        /// <returns></returns>
        private bool parentIsEmpty()
        {
            TextBox[] mom = {
                                this.tbMomSurname,
                                this.tbMomName,
                                this.tbMomPatronomic,
                                this.tbMomPhone,
                                this.tbMomWorkPhone,
                                this.tbMomHome,
                                this.tbMomWork
            };

            TextBox[] dad = { 
                            this.tbDadSurname,
                            this.tbDadName,
                            this.tbDadPatronomic,
                            this.tbDadPhone,
                            this.tbDadWorkPhone,
                            this.tbDadHome,
                            this.tbDadWork
            };

            int size = mom.Length;
            for (int i = 0; i < size; i++)
            {
                if (!mom[i].Text.Equals("") || !dad[i].Text.Equals(""))
                {
                    return false;
                }
            }
            return true;
        }
        
        /// <summary>
        /// Проверка данных о родителях на валидность
        /// </summary>
        /// <returns>true если данные валидны иначе false</returns>
        private bool validateParent()
        {
            TextBox[] mom = {
                                this.tbMomSurname,
                                this.tbMomName,
                                this.tbMomPatronomic,
                                this.tbMomPhone,
                                this.tbMomWorkPhone,
                                this.tbMomHome,
                                this.tbMomWork
            };

            TextBox[] dad = { 
                            this.tbDadSurname,
                            this.tbDadName,
                            this.tbDadPatronomic,
                            this.tbDadPhone,
                            this.tbDadWorkPhone,
                            this.tbDadHome,
                            this.tbDadWork
            };

            int size = mom.Length;

            bool momIsEmpty = true;
            //Если вся информация о матери пустая, то не нужно ее проверять на корректность
            for (int i = 0; i < size; i++)
            {
                if (!mom[i].Text.Equals(""))
                {
                    momIsEmpty = false;
                }

            }

            if (!momIsEmpty)
            {
                if (!GlobalVars.isValiString(mom[0].Text))
                {
                    GlobalVars.showWarningMsgBox(this, "Необходимо указать фамилию матери");
                    this.tabControl1.SelectedIndex = 1;
                    mom[0].Focus();
                    return false;
                }

                if (!GlobalVars.isValiString(mom[1].Text))
                {
                    GlobalVars.showWarningMsgBox(this, "Необходимо указать имя матери");
                    this.tabControl1.SelectedIndex = 1;
                    mom[1].Focus();
                    return false;
                }

                if (mom[3].Text.Equals(""))
                {
                    GlobalVars.showWarningMsgBox(this, "Необходимо указать телефон матери");
                    this.tabControl1.SelectedIndex = 1;
                    mom[3].Focus();
                    return false;
                }

                if (!GlobalVars.isValiString(mom[5].Text))
                {
                    GlobalVars.showWarningMsgBox(this, "Необходимо указать место проживания матери");
                    this.tabControl1.SelectedIndex = 1;
                    mom[5].Focus();
                    return false;
                }
            }

            //Если вся информация об отце пустая, то не нужно ее проверять на корректность
            bool dadIsEmpty = true;

            for (int i = 0; i < size; i++)
            {
                if (!dad[i].Text.Equals(""))
                {
                    dadIsEmpty = false;
                    break;
                }
            }

            if (!dadIsEmpty)
            {
                if (!GlobalVars.isValiString(dad[0].Text))
                {
                    GlobalVars.showWarningMsgBox(this, "Необходимо указать фамилию отца");
                    this.tabControl1.SelectedIndex = 1;
                    dad[0].Focus();
                    return false;
                }

                if (!GlobalVars.isValiString(dad[1].Text))
                {
                    GlobalVars.showWarningMsgBox(this, "Необходимо указать имя отца");
                    this.tabControl1.SelectedIndex = 1;
                    dad[1].Focus();
                    return false;
                }

                if (dad[3].Text.Equals(""))
                {
                    GlobalVars.showWarningMsgBox(this, "Необходимо указать телефон отца");
                    this.tabControl1.SelectedIndex = 1;
                    dad[3].Focus();
                    return false;
                }

                if (!GlobalVars.isValiString(dad[5].Text))
                {
                    GlobalVars.showWarningMsgBox(this, "Необходимо указать место проживания отца");
                    this.tabControl1.SelectedIndex = 1;
                    dad[5].Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Проверка данных о доверенных лицах на пустоту
        /// </summary>
        /// <returns></returns>
        private bool trusteeIsEmpty()
        {
            int rowCount = this.dgTrustee.RowCount;
            int columnCount = this.dgTrustee.ColumnCount;
            if (rowCount == 1)
            {
                return true;
            }
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (this.dgTrustee[j, i].Value != null && !this.dgTrustee[j, i].Value.ToString().Equals(""))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        /// <summary>
        /// Проверка данных о доверенных лицах на валидность
        /// </summary>
        /// <returns>true если данные валидны иначе false</returns>
        private bool validateTrustee()
        {
            //Если есть информация о доверенных лицах, то проверим ее
            if (!trusteeIsEmpty())
            {
                int rowCount = this.dgTrustee.RowCount;
                if (rowCount == 1)
                {
                    return true;
                }
                int columnCount = this.dgTrustee.ColumnCount;

                for (int i = 0; i < rowCount -1; i++)
                {
                    for (int j = 1; j < columnCount; j++)
                    {
                        if (this.dgTrustee[j, i].Value != null && this.dgTrustee[j, i].Value.ToString().Equals(""))
                        {
                            GlobalVars.showWarningMsgBox(this, "Все поля о довереном лице должны быть заполнены");
                            this.dgTrustee.CurrentCell = this.dgTrustee[j, i];
                            this.tabControl1.SelectedIndex = 2;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        
        /// <summary>
        /// Вставка новой информации о ребенке
        /// </summary>
        private void insertData()
        {
            if (validateData())
            {
                //Сохраняем инфу о ребенке
                GlobalVars.connection.insertBaby(
                    this.tbBabySurname.Text,
                    this.tbBabyName.Text, 
                    this.tbBabyPatronomic.Text, 
                    convertData(this.dtBabyBirthday.Value.Date),
                    Convert.ToInt32(this.cbHealthCertificate.Checked),
                    GlobalVars.activeGroupId);

                int babyId = GlobalVars.connection.getMaxBabyId();
                //Сохраняем инфу о прививках
                GlobalVars.connection.insertImmunization(
                    babyId, 
                    convertData(this.dtDtp.Value.Date),
                    convertData(this.dtParotits.Value.Date),
                    convertData(this.dtTuberculosis.Value.Date),
                    convertData(this.dtPoli.Value.Date)
                    );

                //Сохраняем график 
                GlobalVars.connection.insertSheduleVisit(babyId, convertData(this.dtBegin.Value.Date), convertData(this.dtEnd.Value.Date));
                int shedulevisitId = GlobalVars.connection.getMaxSheduleVisitId();

                TimeSpan nullTime = new DateTime(1, 1, 1, 0, 0, 0).TimeOfDay;

                if (this.dtBegin1.Value.TimeOfDay != nullTime && this.dtEnd1.Value.TimeOfDay != nullTime)
                {
                    GlobalVars.connection.insertSheduleVisitString(
                        shedulevisitId, 
                        1, 
                        Convert.ToInt32(this.cbEatDay1.Checked),
                        Convert.ToInt32(this.cbEatEvening1.Checked),
                        this.dtBegin1.Value.TimeOfDay.ToString(),
                        this.dtEnd1.Value.TimeOfDay.ToString(),
                        babyId
                        );
                }

                if (this.dtBegin2.Value.TimeOfDay != nullTime && this.dtEnd2.Value.TimeOfDay != nullTime)
                {
                    GlobalVars.connection.insertSheduleVisitString(
                        shedulevisitId,
                        2,
                        Convert.ToInt32(this.cbEatDay2.Checked),
                        Convert.ToInt32(this.cbEatEvening2.Checked),
                        this.dtBegin2.Value.TimeOfDay.ToString(),
                        this.dtEnd2.Value.TimeOfDay.ToString(),
                        babyId
                        );
                }

                if (this.dtBegin3.Value.TimeOfDay != nullTime && this.dtEnd3.Value.TimeOfDay != nullTime)
                {
                    GlobalVars.connection.insertSheduleVisitString(
                        shedulevisitId,
                        3,
                        Convert.ToInt32(this.cbEatDay3.Checked),
                        Convert.ToInt32(this.cbEatEvening3.Checked),
                        this.dtBegin3.Value.TimeOfDay.ToString(),
                        this.dtEnd3.Value.TimeOfDay.ToString(),
                        babyId
                        );
                }

                if (this.dtBegin4.Value.TimeOfDay != nullTime && this.dtEnd4.Value.TimeOfDay != nullTime)
                {
                    GlobalVars.connection.insertSheduleVisitString(
                        shedulevisitId,
                        4,
                        Convert.ToInt32(this.cbEatDay4.Checked),
                        Convert.ToInt32(this.cbEatEvening4.Checked),
                        this.dtBegin4.Value.TimeOfDay.ToString(),
                        this.dtEnd4.Value.TimeOfDay.ToString(),
                        babyId
                        );
                }

                if (this.dtBegin5.Value.TimeOfDay != nullTime && this.dtEnd5.Value.TimeOfDay != nullTime)
                {
                    GlobalVars.connection.insertSheduleVisitString(
                        shedulevisitId,
                        5,
                        Convert.ToInt32(this.cbEatDay5.Checked),
                        Convert.ToInt32(this.cbEatEvening5.Checked),
                        this.dtBegin5.Value.TimeOfDay.ToString(),
                        this.dtEnd5.Value.TimeOfDay.ToString(),
                        babyId
                        );
                }

                if (this.dtBegin6.Value.TimeOfDay != nullTime && this.dtEnd6.Value.TimeOfDay != nullTime)
                {
                    GlobalVars.connection.insertSheduleVisitString(
                        shedulevisitId,
                        6,
                        Convert.ToInt32(this.cbEatDay6.Checked),
                        Convert.ToInt32(this.cbEatEvening6.Checked),
                        this.dtBegin6.Value.TimeOfDay.ToString(),
                        this.dtEnd6.Value.TimeOfDay.ToString(),
                        babyId
                        );
                }

                if (this.dtBegin7.Value.TimeOfDay != nullTime && this.dtEnd7.Value.TimeOfDay != nullTime)
                {
                    GlobalVars.connection.insertSheduleVisitString(
                        shedulevisitId,
                        7,
                        Convert.ToInt32(this.cbEatDay7.Checked),
                        Convert.ToInt32(this.cbEatEvening7.Checked),
                        this.dtBegin7.Value.TimeOfDay.ToString(),
                        this.dtEnd7.Value.TimeOfDay.ToString(),
                        babyId
                        );
                }

                //Сохраняем инфу о родителях
                if (!parentIsEmpty())
                {
                    TextBox[] mom = {
                                this.tbMomSurname,
                                this.tbMomName,
                                this.tbMomPatronomic,
                                this.tbMomPhone,
                                this.tbMomWorkPhone,
                                this.tbMomHome,
                                this.tbMomWork
                    };
                    
                    TextBox[] dad = { 
                            this.tbDadSurname,
                            this.tbDadName,
                            this.tbDadPatronomic,
                            this.tbDadPhone,
                            this.tbDadWorkPhone,
                            this.tbDadHome,
                            this.tbDadWork
                    };
                    int size = mom.Count();
                    bool momIsEmpty = true;
                    for (int i = 0; i < size; i++)
                    {
                        if (!mom[i].Text.Equals(""))
                        {
                            momIsEmpty = false;
                            break;
                        }
                    }

                    if (!momIsEmpty)
                    {
                        GlobalVars.connection.insertTrustee(mom[0].Text, mom[1].Text, mom[2].Text, mom[3].Text, "Мать",babyId);
                        GlobalVars.connection.insertParent(mom[0].Text, mom[1].Text, mom[2].Text, mom[3].Text, mom[4].Text, mom[5].Text, mom[6].Text, "Мать", babyId);
                    }

                    bool dadIsEmpty = true;
                    for (int i = 0; i < size; i++)
                    {
                        if (!dad[i].Text.Equals(""))
                        {
                            dadIsEmpty = false;
                            break;
                        }
                    }

                    if (!dadIsEmpty)
                    {
                        GlobalVars.connection.insertTrustee(dad[0].Text, dad[1].Text, dad[2].Text, dad[3].Text, "Отец", babyId);
                        GlobalVars.connection.insertParent(dad[0].Text, dad[1].Text, dad[2].Text, dad[3].Text, dad[4].Text, dad[5].Text, dad[6].Text, "Отец", babyId);
                    }
                }

                //Сохраняем инфу о доверенных лицах
                if (!trusteeIsEmpty())
                {
                    int rowCount = this.dgTrustee.RowCount;
                    int columnCount = this.dgTrustee.ColumnCount;

                    if (rowCount == 1)
                    {

                    }
                    else
                    {
                        for (int i = 0; i < rowCount; i++)
                        {
                            bool rowIsEmpty = true;
                            for (int j = 0; j < columnCount; j++)
                            {
                                if (this.dgTrustee[j, i].Value != null && !this.dgTrustee[j, i].Value.ToString().Equals(""))
                                {
                                    rowIsEmpty = false;
                                    break;
                                }
                            }

                            if (!rowIsEmpty)
                            {
                                GlobalVars.connection.insertTrustee(
                                    this.dgTrustee[1, i].Value.ToString(),
                                    this.dgTrustee[2, i].Value.ToString(),
                                    this.dgTrustee[3, i].Value.ToString(),
                                    this.dgTrustee[4, i].Value.ToString(),
                                    this.dgTrustee[5, i].Value.ToString(),
                                    babyId);
                            }
                        }
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}

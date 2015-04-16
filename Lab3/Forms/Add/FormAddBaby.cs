using Lab3.Forms;
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

        private static Baby baby;
        private static Immunization imun;
        private static SheduleVisit visit;
        private static Parent father, mother;
        private static List<int> deleteTrustee;
        /// <summary>
        /// Первоначальная инициализация
        /// </summary>
        private void init()
        {
            baby = GlobalVars.connection.getBaby(GlobalVars.activeBabyId);
            this.tbBabySurname.Text = baby.surname;
            this.tbBabyName.Text = baby.name;
            this.tbBabyPatronomic.Text = baby.patronomic;
            this.cbHealthCertificate.Checked = Convert.ToBoolean(baby.healthCertificate);
            this.dtBabyBirthday.Value = baby.birthday;

            //Прививки
            imun = GlobalVars.connection.getImmun(GlobalVars.activeBabyId);
            this.dtDtp.Value = imun.dtp;
            this.dtParotits.Value = imun.parotits;
            this.dtTuberculosis.Value = imun.tuber;
            this.dtPoli.Value = imun.poli;

            //Шапка графика посещений
            visit = GlobalVars.connection.getSheduleVisit(GlobalVars.activeBabyId);
            this.dtBegin.Value = visit.dateBegin;
            this.dtEnd.Value = visit.dateEnd;

            //Строки графика посещений
            initStringSheduleVisit(GlobalVars.connection.getStringSheduleVisit(GlobalVars.activeBabyId));

            //Родители
            initParent(GlobalVars.connection.getParents(GlobalVars.activeBabyId));

            //Довереный лица 
            if (this.dgTrustee.RowCount > 0)
            {
                this.dgTrustee.Rows.Clear();
            }
            if (this.dgTrustee.ColumnCount > 0)
            {
                this.dgTrustee.Columns.Clear();
            }
            this.dgTrustee.DataSource = GlobalVars.connection.getTrustee(GlobalVars.activeBabyId);
            this.dgTrustee.Columns[0].Visible = false;

            deleteTrustee = new List<int>();
        }

        /// <summary>
        /// Инициализация графика посещения
        /// </summary>
        /// <param name="list">Строки графика посещений</param>
        private void initStringSheduleVisit(List<StringSheduleVisit> val)
        {
            DateTimePicker[] dateBegin = {
                                this.dtBegin1,
                                this.dtBegin2,
                                this.dtBegin3,
                                this.dtBegin4,
                                this.dtBegin5,
                                this.dtBegin6,
                                this.dtBegin7
            };

            DateTimePicker[] dateEnd = {
                                this.dtEnd1,
                                this.dtEnd2,
                                this.dtEnd3,
                                this.dtEnd4,
                                this.dtEnd5,
                                this.dtEnd6,
                                this.dtEnd7
            };

            CheckBox[] eatBreakfast = {
                                        this.cbBreakfast1,
                                        this.cbBreakfast2,
                                        this.cbBreakfast3,
                                        this.cbBreakfast4,
                                        this.cbBreakfast5,
                                        this.cbBreakfast6,
                                        this.cbBreakfast7
                                    };
            CheckBox[] eatSnack = {
                                        this.cbSnack1,
                                        this.cbSnack2,
                                        this.cbSnack3,
                                        this.cbSnack4,
                                        this.cbSnack5,
                                        this.cbSnack6,
                                        this.cbSnack7
                                    };
            CheckBox[] eatLunch = {
                                        this.cbLunch1,
                                        this.cbLunch2,
                                        this.cbLunch3,
                                        this.cbLunch4,
                                        this.cbLunch5,
                                        this.cbLunch6,
                                        this.cbLunch7

                                  };
            CheckBox[] eatDinner = {
                                       this.cbDinner1,
                                       this.cbDinner2,
                                       this.cbDinner3,
                                       this.cbDinner4,
                                       this.cbDinner5,
                                       this.cbDinner6,
                                       this.cbDinner7
                                   };
            DateTime timeNull = new DateTime(1755, 1, 1, 0, 0, 0);

            int count = dateBegin.Count();
            for (int i = 0; i < count; i++)
            {
                dateBegin[i].Value = timeNull;
                dateEnd[i].Value = timeNull;
            }

            int listCount = val.Count;
            for (int i = 0; i < listCount && i < count; i++)
            {
                StringSheduleVisit visit = val[i];
                int index = val[i].dayWeekId - 1;
                dateBegin[index].Value = visit.timeBegin;
                dateEnd[index].Value = visit.timeEnd;
                eatBreakfast[index].Checked = Convert.ToBoolean(val[i].eatBreakfast);
                eatSnack[index].Checked = Convert.ToBoolean(val[i].eatSnack);
                eatLunch[index].Checked = Convert.ToBoolean(val[i].eatLunch);
                eatDinner[index].Checked = Convert.ToBoolean(val[i].eatDinner);
            }
        }

        /// <summary>
        /// Инициализация родителей
        /// </summary>
        /// <param name="parents">Список родителей</param>
        private void initParent(List<Parent> parents)
        {
            if (parents == null)
            {
                return;
            }

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

            int count = parents.Count();
            for(int i = 0; i < count; i++) {
                Parent parent = parents[i];
                if (parent.parentInfo.Equals("Отец"))
                {
                    father = parent;
                    dad[0].Text = parent.surname;
                    dad[1].Text = parent.name;
                    dad[2].Text = parent.patronomic;
                    dad[3].Text = parent.phone;
                    dad[4].Text = parent.workPhone;
                    dad[5].Text = parent.location;
                    dad[6].Text = parent.work;
                }
                else
                {
                    mother = parent;
                    mom[0].Text = parent.surname;
                    mom[1].Text = parent.name;
                    mom[2].Text = parent.patronomic;
                    mom[3].Text = parent.phone;
                    mom[4].Text = parent.workPhone;
                    mom[5].Text = parent.location;
                    mom[6].Text = parent.work;
                }
            } 
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
                updateData();

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
            if (this.dtDtp.Value.Date <= DateTime.Now)
            {
                GlobalVars.showWarningMsgBox(this, "Укажите корректную дату");
                this.tabControl1.SelectedIndex = 0;
                this.dtDtp.Focus();
                return false;
            }

            if (this.dtPoli.Value.Date <= DateTime.Now)
            {
                GlobalVars.showWarningMsgBox(this, "Укажите корректную дату");
                this.tabControl1.SelectedIndex = 0;
                this.dtPoli.Focus();
                return false;
            }

            if (this.dtTuberculosis.Value.Date <= DateTime.Now)
            {
                GlobalVars.showWarningMsgBox(this, "Укажите корректную дату");
                this.tabControl1.SelectedIndex = 0;
                this.dtTuberculosis.Focus();
                return false;
            }

            if (this.dtParotits.Value.Date <= DateTime.Now)
            {
                GlobalVars.showWarningMsgBox(this, "Укажите корректную дату");
                this.tabControl1.SelectedIndex = 0;
                this.dtParotits.Focus();
                return false;
            }
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
                else
                {
                    double maxAge = GlobalVars.connection.getMaxAgeGroup(GlobalVars.activeGroupId) * 365;
                    double minAge = GlobalVars.connection.getMinAgeGroup(GlobalVars.activeGroupId) * 365;
                    DialogResult result = DialogResult.Cancel;
                    if (maxAge < span.Days)
                    {
                        result = GlobalVars.showQuestionMsgBox(this, "Возраст ребенка превышает максимальный возраст в группе. Хотите продолжить?");
                    }
                    else if (minAge > span.Days)
                    {
                        result = GlobalVars.showQuestionMsgBox(this, "Возраст ребенка меньше минимального возраста в группе. Хотите продолжить?");
                    }

                    if (result == DialogResult.Cancel)
                    {
                        return false;
                    }
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
        /// Проверка питания на валидность 
        /// (Функция не используется, если заставят проверять галочки на валидность, то ее нужно дописать)
        /// </summary>
        /// <returns></returns>
        private bool validateStringSheduleVisit()
        {
            DateTimePicker[] dateBegin = {
                                this.dtBegin1,
                                this.dtBegin2,
                                this.dtBegin3,
                                this.dtBegin4,
                                this.dtBegin5,
                                this.dtBegin6,
                                this.dtBegin7
            };

            DateTimePicker[] dateEnd = {
                                this.dtEnd1,
                                this.dtEnd2,
                                this.dtEnd3,
                                this.dtEnd4,
                                this.dtEnd5,
                                this.dtEnd6,
                                this.dtEnd7
            };

            CheckBox[] eatBreakfast = {
                                        this.cbBreakfast1,
                                        this.cbBreakfast2,
                                        this.cbBreakfast3,
                                        this.cbBreakfast4,
                                        this.cbBreakfast5,
                                        this.cbBreakfast6,
                                        this.cbBreakfast7
                                    };
            CheckBox[] eatSnack = {
                                        this.cbSnack1,
                                        this.cbSnack2,
                                        this.cbSnack3,
                                        this.cbSnack4,
                                        this.cbSnack5,
                                        this.cbSnack6,
                                        this.cbSnack7
                                    };
            CheckBox[] eatLunch = {
                                        this.cbLunch1,
                                        this.cbLunch2,
                                        this.cbLunch3,
                                        this.cbLunch4,
                                        this.cbLunch5,
                                        this.cbLunch6,
                                        this.cbLunch7

                                  };
            CheckBox[] eatDinner = {
                                       this.cbDinner1,
                                       this.cbDinner2,
                                       this.cbDinner3,
                                       this.cbDinner4,
                                       this.cbDinner5,
                                       this.cbDinner6,
                                       this.cbDinner7
                                   };
            TimeSpan timeNull = new DateTime(1755, 1, 1, 0, 0, 0).TimeOfDay;
            
            int count = dateBegin.Count();
            for (int i = 0; i < count; i++)
            {
                if (dateBegin[i].Value.TimeOfDay != timeNull && dateEnd[i].Value.TimeOfDay != timeNull)
                {
                    if (dateBegin[i].Value.TimeOfDay >= dateEnd[i].Value.TimeOfDay)
                    {
                        GlobalVars.showWarningMsgBox(this, "Время прихода не может быть больше времени ухода");
                        this.tabControl1.SelectedIndex = 0;
                        dateBegin[i].Focus();
                        return false;
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    if (dateBegin[i].Value.TimeOfDay == timeNull && dateEnd[i].Value.TimeOfDay != timeNull)
                    {
                        GlobalVars.showWarningMsgBox(this, "Укажите корректное время");
                        this.tabControl1.SelectedIndex = 0;
                        dateBegin[i].Focus();
                        return false;
                    }
                    else
                    {
                        GlobalVars.showWarningMsgBox(this, "Укажите корректное время");
                        this.tabControl1.SelectedIndex = 0;
                        dateEnd[i].Focus();
                        return false;
                    }
                }
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
                            this.tabControl1.SelectedIndex = 2;
                            this.dgTrustee.CurrentCell = this.dgTrustee[j, i];
                            
                            return false;
                        }
                        else
                        {
                            if (j == columnCount - 1)
                            {
                                if (this.dgTrustee[j, i].Value.ToString().Equals("Мать") || this.dgTrustee[j, i].Value.ToString().Equals("Отец") ||
                                    this.dgTrustee[j, i].Value.ToString().Equals("мать") || this.dgTrustee[j, i].Value.ToString().Equals("отец"))
                                {
                                    GlobalVars.showWarningMsgBox(this, "Родители автоматически добавляются в довереные лица");
                                    this.tabControl1.SelectedIndex = 2;
                                    this.dgTrustee.CurrentCell = this.dgTrustee[j, i];

                                    return false;
                                }
                            }
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

                //Сохраняем строки графика
                insertSheduleVisitString(shedulevisitId, babyId);



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

                //Добавляем в списки оплаты
                GlobalVars.connection.insertBabyInPaymentList(babyId);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Обновляет информацию в БД
        /// </summary>
        private void updateData()
        {
            if (validateData())
            {
                //Обновляем ребенка
                GlobalVars.connection.updateBaby(
                    GlobalVars.activeBabyId,
                    this.tbBabySurname.Text,
                    this.tbBabyName.Text,
                    this.tbBabyPatronomic.Text,
                    Convert.ToInt32(this.cbHealthCertificate.Checked),
                    convertData(this.dtBabyBirthday.Value.Date)
                    );

                //Сохраняем инфу о прививках
                GlobalVars.connection.updateImmunization(
                    GlobalVars.activeBabyId,
                    convertData(this.dtDtp.Value.Date),
                    convertData(this.dtParotits.Value.Date),
                    convertData(this.dtTuberculosis.Value.Date),
                    convertData(this.dtPoli.Value.Date)
                    );
                
                //Сохраняем график посещения
                updateSheduleVisit();

                //Сохраняем родителей
                updateParent();

                //обновляем довереные лица
                updateTrustee();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Обновляет график посещения
        /// </summary>
        private void updateSheduleVisit()
        {
            GlobalVars.connection.updateSheduleVisit(GlobalVars.activeBabyId, convertData(this.dtBegin.Value.Date), convertData(this.dtEnd.Value.Date));
            GlobalVars.connection.deleteSheduleVisitString(GlobalVars.activeBabyId);
            insertSheduleVisitString(visit.id, GlobalVars.activeBabyId);
        }

        /// <summary>
        /// Вставка строк графика посещения
        /// </summary>
        /// <param name="shedulevisitId"></param>
        /// <param name="babyId"></param>
        private void insertSheduleVisitString(int shedulevisitId, int babyId)
        {
            TimeSpan nullTime = new DateTime(1, 1, 1, 0, 0, 0).TimeOfDay;
            if (this.dtBegin1.Value.TimeOfDay != nullTime && this.dtEnd1.Value.TimeOfDay != nullTime)
            {
                GlobalVars.connection.insertSheduleVisitString(
                    shedulevisitId,
                    1,
                    Convert.ToInt32(this.cbBreakfast1.Checked),
                    Convert.ToInt32(this.cbSnack1.Checked),
                    Convert.ToInt32(this.cbLunch1.Checked),
                    Convert.ToInt32(this.cbDinner1.Checked),
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
                    Convert.ToInt32(this.cbBreakfast2.Checked),
                    Convert.ToInt32(this.cbSnack2.Checked),
                    Convert.ToInt32(this.cbLunch2.Checked),
                    Convert.ToInt32(this.cbDinner2.Checked),
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
                    Convert.ToInt32(this.cbBreakfast3.Checked),
                    Convert.ToInt32(this.cbSnack3.Checked),
                    Convert.ToInt32(this.cbLunch3.Checked),
                    Convert.ToInt32(this.cbDinner3.Checked),
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
                    Convert.ToInt32(this.cbBreakfast4.Checked),
                    Convert.ToInt32(this.cbSnack4.Checked),
                    Convert.ToInt32(this.cbLunch4.Checked),
                    Convert.ToInt32(this.cbDinner4.Checked),
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
                    Convert.ToInt32(this.cbBreakfast5.Checked),
                    Convert.ToInt32(this.cbSnack5.Checked),
                    Convert.ToInt32(this.cbLunch5.Checked),
                    Convert.ToInt32(this.cbDinner5.Checked),
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
                    Convert.ToInt32(this.cbBreakfast6.Checked),
                    Convert.ToInt32(this.cbSnack6.Checked),
                    Convert.ToInt32(this.cbLunch6.Checked),
                    Convert.ToInt32(this.cbDinner6.Checked),
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
                    Convert.ToInt32(this.cbBreakfast7.Checked),
                    Convert.ToInt32(this.cbSnack7.Checked),
                    Convert.ToInt32(this.cbLunch7.Checked),
                    Convert.ToInt32(this.cbDinner7.Checked),
                    this.dtBegin7.Value.TimeOfDay.ToString(),
                    this.dtEnd7.Value.TimeOfDay.ToString(),
                    babyId
                    );
            }
        }

        /// <summary>
        /// Обновление информации о родителях
        /// </summary>
        private void updateParent()
        {
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
                    if (mother != null)
                    {
                        GlobalVars.connection.updateParent(mother.id, mom[0].Text, mom[1].Text, mom[2].Text, mom[3].Text, mom[4].Text, mom[5].Text, mom[6].Text);
                        GlobalVars.connection.updateTrustee(GlobalVars.connection.getTrusteeId(GlobalVars.activeBabyId, "Мать"), mom[0].Text, mom[1].Text, mom[2].Text, mom[3].Text, "Мать");
                    }
                    else
                    {
                        GlobalVars.connection.insertTrustee(mom[0].Text, mom[1].Text, mom[2].Text, mom[3].Text, "Мать", GlobalVars.activeBabyId);
                        GlobalVars.connection.insertParent(mom[0].Text, mom[1].Text, mom[2].Text, mom[3].Text, mom[4].Text, mom[5].Text, mom[6].Text, "Мать", GlobalVars.activeBabyId);
                    }
                   
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
                    if (father != null)
                    {
                        GlobalVars.connection.updateParent(father.id, dad[0].Text, dad[1].Text, dad[2].Text, dad[3].Text, dad[4].Text, dad[5].Text, dad[6].Text);
                        GlobalVars.connection.updateTrustee(GlobalVars.connection.getTrusteeId(GlobalVars.activeBabyId, "Отец"), dad[0].Text, dad[1].Text, dad[2].Text, dad[3].Text, "Отец");
                    }
                    else
                    {
                        GlobalVars.connection.insertTrustee(dad[0].Text, dad[1].Text, dad[2].Text, dad[3].Text, "Отец", GlobalVars.activeBabyId);
                        GlobalVars.connection.insertParent(dad[0].Text, dad[1].Text, dad[2].Text, dad[3].Text, dad[4].Text, dad[5].Text, dad[6].Text, "Отец", GlobalVars.activeBabyId);
                    }
                    
                }
                if (father != null && dadIsEmpty)
                {
                    int trusteeId = GlobalVars.connection.getTrusteeId(GlobalVars.activeBabyId, "Отец");
                    if (GlobalVars.connection.deleteParent(father.id))
                    {
                        GlobalVars.connection.deleteTrustee(trusteeId);
                    }
                    else
                    {
                        GlobalVars.showWarningMsgBox(this, "Невозможно удалить информацию об отце, есть связанные данные");
                    }
                }

                if (mother != null && momIsEmpty)
                {
                    int trusteeId = GlobalVars.connection.getTrusteeId(GlobalVars.activeBabyId, "Мать");
                    if (GlobalVars.connection.deleteParent(mother.id))
                    {
                        GlobalVars.connection.deleteTrustee(trusteeId);
                    }
                    else
                    {
                        GlobalVars.showWarningMsgBox(this, "Невозможно удалить информацию о матери, есть связанные данные");
                    }
                }
            }
            else
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

                bool dadIsEmpty = true;
                for (int i = 0; i < size; i++)
                {
                    if (!dad[i].Text.Equals(""))
                    {
                        dadIsEmpty = false;
                        break;
                    }
                }
                if (father != null && dadIsEmpty) 
                {
                    int trusteeId = GlobalVars.connection.getTrusteeId(GlobalVars.activeBabyId, "Отец");
                    if (GlobalVars.connection.deleteParent(father.id))
                    {
                        GlobalVars.connection.deleteTrustee(trusteeId);
                    }
                    else
                    {
                        GlobalVars.showWarningMsgBox(this, "Невозможно удалить информацию об отце, есть связанные данные");
                    }
                }

                if (mother != null && momIsEmpty)
                {
                    int trusteeId = GlobalVars.connection.getTrusteeId(GlobalVars.activeBabyId, "Мать");
                    if (GlobalVars.connection.deleteParent(mother.id))
                    {
                        GlobalVars.connection.deleteTrustee(trusteeId);
                    }
                    else
                    {
                        GlobalVars.showWarningMsgBox(this, "Невозможно удалить информацию о матери, есть связанные данные");
                    }
                }
            }
        }


        /// <summary>
        /// Обновление информации о доверенных лицах
        /// </summary>
        private void updateTrustee()
        {
            int rowCount = this.dgTrustee.RowCount - 1;

            for (int i = 0; i < rowCount; i++)
            {
                
                if (this.dgTrustee[0, i].Value != null && !this.dgTrustee[0, i].Value.ToString().Equals(""))
                {
                    //Обновляем довереное лицо
                    int trusteeId = Convert.ToInt32(this.dgTrustee[0, i].Value);
                    GlobalVars.connection.updateTrustee(
                        trusteeId, 
                        this.dgTrustee[1, i].Value.ToString(),
                        this.dgTrustee[2, i].Value.ToString(),
                        this.dgTrustee[3, i].Value.ToString(),
                        this.dgTrustee[4, i].Value.ToString(),
                        this.dgTrustee[5, i].Value.ToString());
                }
                else
                {
                    //Вставляем новое довереное лицо
                    GlobalVars.connection.insertTrustee(this.dgTrustee[1, i].Value.ToString(),
                        this.dgTrustee[2, i].Value.ToString(),
                        this.dgTrustee[3, i].Value.ToString(),
                        this.dgTrustee[4, i].Value.ToString(),
                        this.dgTrustee[5, i].Value.ToString(),
                        GlobalVars.activeBabyId);
                }
            }

            bool isGood = true;
            //Удаляем доверенных лиц
            int count = deleteTrustee.Count();
            for (int i = 0; i < count; i++)
            {
                if (!GlobalVars.connection.deleteTrustee(deleteTrustee[i]))
                {
                    isGood = false;
                }
            }

            if (!isGood)
            {
                GlobalVars.showWarningMsgBox(this, "Не удалось удалить некоторых довереных лиц, так как есть связанные данные");
            }
        }

        private void dgTrustee_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dresult = GlobalVars.showQuestionMsgBox(this, "Вы уверены, что хотите удалить довереное лицо?");
            if (dresult == DialogResult.Yes)
            {
                int rowIndex = e.Row.Index;
                int trusteeId = -1;
                int pos = e.Row.Index;
                if (this.dgTrustee[0, rowIndex].Value != null && !this.dgTrustee[0, rowIndex].Value.ToString().Equals(""))
                {
                    trusteeId = Convert.ToInt32(this.dgTrustee[0, rowIndex].Value);
                }

                if (trusteeId != -1)
                {
                    deleteTrustee.Add(trusteeId);
                }
            }

        }

    }
}

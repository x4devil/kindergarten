using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Lab3.Forms;
using Lab3.Objects;

namespace Lab3
{
    /// <summary>
    /// Основной класс для работы с БД
    /// </summary>
    class MySQLConnector
    {
        public MySqlConnection connection; //Подключение к БД 

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="server">IP сервера</param>
        /// <param name="uid">Логин</param>
        /// <param name="pass">Пароль</param>
        public MySQLConnector(String server, String uid, String pass)
        {
            String db = "childdb";
            String connectionString = "Server=" + server +
                                        ";Database=" + db +
                                        ";Uid=" + uid +
                                        ";Pwd=" + pass +
                                        ";CharSet = utf8;";
            connection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Открытие подключения к БД
        /// </summary>
        /// <param name="server">IP сервера</param>
        /// <param name="db">Название БД</param>
        /// <param name="uid">Логин</param>
        /// <param name="pass">Пароль</param>
        /// <returns>Возвращает true если установлено подключение иначе возвращает false.</returns>
        public bool openConnect()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Не удалось подключится к БД. Текст ошибки: " + ex.Message); //DEBUG
                return false;
            }
        }

        /// <summary>
        /// Закрытие подключения к БД
        /// </summary>
        /// <returns>Возвращает true если подключение закрыто иначе возвращает false.</returns>
        public bool closeConnect()
        {
            if (connection.State == ConnectionState.Open)
            {
                try
                {
                    connection.Close();
                    return true;
                }
                catch (MySqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show("Нет удалось закрыть подключение к БД. Текст ошибки: " + ex.Message); //DEBUG
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Проверяет открыто ли подключение к БД
        /// </summary>
        /// <returns>Возвращает true если подключение открыто иначе возвращает false </returns>
        public bool connectionIsOpen()
        {
            if (connection.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Выполняет выборку данных по заданному sql запросу
        /// </summary>
        /// <param name="sql">sql запрос</param>
        /// <returns>Выборка данных или null если нет подключения </returns>
        public DataTable select(String sql)
        {
            try
            {
                if (connectionIsOpen())
                {
                    DataTable table = new DataTable();
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(table);
                    return table;
                }
                else
                {
                    return null;
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message); //DEBUG
                return null;
            }
        }

        /// <summary>
        /// Выполняет вставку, удаление, обновление данных по заданному sql запросу
        /// </summary>
        /// <param name="sql">sql запрос</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool execute(String sql)
        {
            try
            {
                if (connectionIsOpen())
                {
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message); //DEBUG
                return false;
            }
        }

        /// <summary>
        /// Конвертирует время в дату и время
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public DateTime convertTimeToDateTime(String time)
        {
            String[] buf = time.Split(new char[] { ':' });
            return new DateTime(1755, 1, 1, Convert.ToInt32(buf[0]), Convert.ToInt32(buf[1]), Convert.ToInt32(buf[2]));
        }
        /*****************************************************************************************
         ************************* Методы связанные с выборкой данных***************************** 
         *****************************************************************************************
         */

        public double getMaxAgeGroup(int groupId)
        {
            String sql = String.Format("select max_age from grouptable where group_id = {0}", groupId);
            DataTable table = select(sql);

            if (table != null && table.Rows.Count > 0)
            {
                return Convert.ToDouble(table.Rows[0][0]);
            }
            else
            {
                return -1;
            }
        }

        public double getMinAgeGroup(int groupId)
        {
            String sql = String.Format("select min_age from grouptable where group_id = {0}", groupId);
            DataTable table = select(sql);

            if (table != null && table.Rows.Count > 0)
            {
                return Convert.ToDouble(table.Rows[0][0]);
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// Получает список групп 
        /// </summary>
        /// <returns>Список групп или null если нет подключения</returns>
        public DataTable getGroupList()
        {
            String sql = "select group_id, group_name as \"Название\"," +
                " max_age \"Max_возраст\"," +
                " min_age \"Min_возраст\", " +
                " group_size \"Вместимость\"," +
                " group_coast \"Стоимость\" " +
                "from grouptable";
            return select(sql);
        }

        /// <summary>
        /// Получает первичный ключ последней добавленной записи
        /// </summary>
        /// <returns></returns>
        public int getMaxGroupId()
        {
            String sql = "select max(group_id) from grouptable";
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Получает список детей для определенной группы
        /// </summary>
        /// <param name="groupId">Ид группы</param>
        /// <returns>Список детей</returns>
        public DataTable getChildList(int groupId)
        {
            String sql = "select baby_id as \"ИД\", " +
                "baby_surname as \"Фамилия\", " +
                "baby_name as \"Имя\", " +
                "baby_patronomic as \"Отчество\", " +
                "baby_birthday as \"Дата_рождения\" " +
                "from baby";
            sql = String.Format(sql + " where group_id = {0}", groupId);

            return select(sql);
        }

        /// <summary>
        /// Проверяет наличие групп
        /// </summary>
        /// <returns>true если есть группы, иначе false</returns>
        public bool isHasGroup()
        {
            String sql = "select group_id from grouptable";
            DataTable table = select(sql);

            if (table != null && table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Получает список названий групп 
        /// </summary>
        /// <returns>Список названий групп</returns>
        public List<String> getGroupNamesList()
        {
            String sql = "select group_name from grouptable";
            DataTable table = select(sql);

            List<String> result = new List<String>();
            if (table == null)
            {
                return result;
            }

            int rowsCount = table.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                result.Add(table.Rows[i][0].ToString());
                Console.WriteLine(table.Rows[i][0].ToString());
            }
            return result;
        }

        /// <summary>
        /// Получает ид группы по ее названию
        /// </summary>
        /// <param name="name">Название группы</param>
        /// <returns>Возвращает ид группы или -1 если такое название не найдено</returns>
        public int getGroupIdByName(String name)
        {
            String sql = String.Format("select group_id from grouptable where group_name like '{0}'", name);
            DataTable table = select(sql);

            if (table != null && table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }

            return -1;
        }

        /// <summary>
        /// Получает список пар groupname/groupid 
        /// </summary>
        /// <returns>Список пар или null если нет подключения</returns>
        public DataTable getGroupLoV() //LoV - list of values
        {
            String sql = "select group_name as \"Название\"," +
                " group_id \"Номер\" " +
                " from grouptable";
            return select(sql);
        }

        /// <summary>
        /// Получает список воспитателей 
        /// </summary>
        /// <returns>Список воспитателей или null если нет подключения</returns>
        public DataTable getEducatorList()
        {
            String sql = "select educator_surname as \"Фамилия\"," +
                " educator_name \"Имя\"," +
                " educator_patronomic \"Отчество\"," +
                " educator_phone \"Телефон\"," +
                " educator_location \"Место проживания\"," +
                " educator_id \"Номер\" " +
                " from educator";
            return select(sql);
        }

        /// <summary>
        /// Получает воспитателя с заданым id
        /// </summary>
        /// <returns>Строка с воспитателем или null</returns>
        public DataTable getEducatorById(int id)
        {
            String sql = String.Format("select educator_surname as \"Фамилия\"," +
                " educator_name \"Имя\"," +
                " educator_patronomic \"Отчество\"," +
                " educator_phone \"Телефон\"," +
                " educator_location \"Место проживания\"," +
                " group_id \"Номер группы\" " +
                " from educator" +
                " where educator_id = {0}",
                id);
            return select(sql);
        }

        /// <summary>
        /// Получает список посещений на заданную дату
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns>Список посещений или null</returns>
        public DataTable getVisitingListByDate(DateTime date)
        {
            String d = date.ToString("yyyy-MM-dd");
            String sql = String.Format("select " +
                " v.visiting_id as \"Номер\"," +
                " v.baby_id as \"Номер ребенка\", " +
                " (select concat_ws(\" \", b.baby_surname, b.baby_name, b.baby_patronomic)" +
                "   from baby b" +
                "   where b.baby_id = v.baby_id " +
                " ) as \"ФИО ребенка\"," +
                " v.visiting_timebegin as \"Привели\"," +
                " v.visiting_timeend as \"Забрали\"," +
                " v.educator_id as \"Воспитатель\"," +
                " v.trustee_id as \"Кто забрал\"," +
                " v.visiting_cost as \"Стомость часа\" " +
                " from visiting v" +
                " where visiting_date = '{0}'",
                d);
            return select(sql);
        }
        /// <summary>
        /// Получает список детей форматированый под табоицу посещения
        /// </summary>
        /// <returns>Список посещений или null</returns>
        public DataTable getVisitingBabyList()
        {
            String sql = String.Format("select " +
                " null as \"Номер\"," +
                " baby_id as \"Номер ребенка\", " +
                " concat_ws(\" \", baby_surname, baby_name, baby_patronomic) as \"ФИО ребенка\"," +
                " null as \"Привели\"," +
                " null as \"Забрали\"," +
                " null as \"Воспитатель\"," +
                " null as \"Доверенное лицо\" " +
                " from baby ");
            return select(sql);
        }
        /// <summary>
        /// Получает список пар имя воспитателя/номер воспитателя
        /// </summary>
        /// <returns>Список пар или null если нет подключения</returns>
        public DataTable getEducatorLoV()
        {
            String sql = "select concat_ws(\" \", educator_surname, educator_name) as \"ФИО\"," +
                " educator_id as \"Номер\" " +
                " from educator";
            return select(sql);
        }
        /// <summary>
        /// Получает список пар доверенное лицо/номер лица для ребенка с заданым id
        /// </summary>
        /// <param name="id">Номер ребенка</param>
        /// <returns>Список пар или null если нет подключения</returns>
        public DataTable getTrusteeLoV(int id) //LoV - list of values
        {
            String sql = String.Format("select concat_ws(\" \",trustee_surname,trustee_name) as \"ФИО\"," +
                " trustee_id as \"Номер\" " +
                " from trustee" +
                " where baby_id = '{0}'",
                id);
            return select(sql);
        }

        /// <summary>
        /// Получает название группы по ид группы
        /// </summary>
        /// <param name="groupId">ИД группы</param>
        /// <returns>Название группы или null, если нет группы с таким ИД</returns>
        public String getGroupNameById(int groupId)
        {
            String sql = String.Format("select group_name from grouptable where group_id = {0}", groupId);
            DataTable table = select(sql);

            if (table != null && table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Получает количество групп
        /// </summary>
        /// <returns>Количество групп или 0 если групп нет</returns>
        public int getGroupCount()
        {
            String sql = "select count(group_id) from grouptable";
            DataTable table = select(sql);

            if (table != null && table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Получает максимальный ид ребенка
        /// </summary>
        /// <returns>максималь ид ребенка или -1 если таблица пуста</returns>
        public int getMaxBabyId()
        {
            String sql = "select max(baby_id) from baby";
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            return -1;
        }

        /// <summary>
        /// Получает максимальный ид планируемого графика посещения
        /// </summary>
        /// <returns>максималь ид графика или -1 если таблица пуста</returns>
        public int getMaxSheduleVisitId()
        {
            String sql = "select max(shedulevisit_id) from shedulevisit";
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            return -1;
        }

        /// <summary>
        /// Возвращает информацию о ребенке
        /// </summary>
        /// <param name="babyId">ИД ребенка</param>
        /// <returns>Экземпляр класса Baby или null если ничего не найдено</returns>
        public Baby getBaby(int babyId)
        {
            String sql = String.Format("select baby_surname, baby_name, baby_patronomic, health_certificat, baby_birthday, baby_id "  +
                "from baby where baby_id = {0}", babyId);
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                Baby baby = new Baby();
                baby.surname = table.Rows[0][0].ToString();
                baby.name = table.Rows[0][1].ToString();
                baby.patronomic = table.Rows[0][2].ToString();
                bool buf = Convert.ToBoolean(table.Rows[0][3].ToString());
                baby.healthCertificate = Convert.ToInt32(buf);
                baby.birthday = Convert.ToDateTime(table.Rows[0][4]);
                baby.id = Convert.ToInt32(table.Rows[0][5]);

                return baby;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Возвращает информацию о прививках
        /// </summary>
        /// <param name="babyId">ИД ребенка</param>
        /// <returns>Информация о ребенке или null если информация не найдена</returns>
        public Immunization getImmun(int babyId)
        {
            String sql = String.Format("select immunizations_id, dtp, parotits, tuberculosis, poli from immunization where baby_id = {0}", babyId);
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                Immunization imun = new Immunization();
                imun.id = Convert.ToInt32(table.Rows[0][0]);
                imun.dtp = Convert.ToDateTime(table.Rows[0][1]);
                imun.parotits = Convert.ToDateTime(table.Rows[0][2]);
                imun.tuber = Convert.ToDateTime(table.Rows[0][3]);
                imun.poli = Convert.ToDateTime(table.Rows[0][4]);

                return imun;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Возвращает информацию о графике посещения
        /// </summary>
        /// <param name="babyId">ИД ребенка</param>
        /// <returns>Информация о графике посещения или null если информация не найдена</returns>
        public SheduleVisit getSheduleVisit(int babyId)
        {
            String sql = String.Format("select shedulevisit_id, date_begin, date_end from shedulevisit where baby_id = {0}", babyId);
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                SheduleVisit visit = new SheduleVisit();
                visit.id = Convert.ToInt32(table.Rows[0][0]);
                visit.dateBegin = Convert.ToDateTime(table.Rows[0][1]);
                visit.dateEnd = Convert.ToDateTime(table.Rows[0][2]);

                return visit;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Возвращает строки графика посещения
        /// </summary>
        /// <param name="babyId">ИД ребенка</param>
        /// <returns>Строки графика посещения или null если информация не найдена</returns>
        public List<StringSheduleVisit> getStringSheduleVisit(int babyId)
        {
            String sql = String.Format("select string_id, eat_breakfast, eat_snack, eat_lunch, eat_dinner, time_begin, time_end, dayweek_id from string_shedulevisit where baby_id = {0}", babyId);
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                int rowCount = table.Rows.Count;
                List<StringSheduleVisit> result = new List<StringSheduleVisit>();
                for (int i = 0; i < rowCount; i++)
                {
                    StringSheduleVisit str = new StringSheduleVisit();
                    str.id = Convert.ToInt32(table.Rows[i][0]);
                    
                    str.eatBreakfast = Convert.ToInt32(table.Rows[i][1]);
                    str.eatSnack = Convert.ToInt32(table.Rows[i][2]);
                    str.eatLunch = Convert.ToInt32(table.Rows[i][3]);
                    str.eatDinner = Convert.ToInt32(table.Rows[i][4]);

                    str.timeBegin = convertTimeToDateTime(table.Rows[i][5].ToString());
                    str.timeEnd = convertTimeToDateTime(table.Rows[i][6].ToString());
                    str.dayWeekId = Convert.ToInt32(table.Rows[i][7]);

                    result.Add(str);
                }

                return result;

            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Возвращает информацию о родителях
        /// </summary>
        /// <param name="babyId">ИД ребенка</param>
        /// <returns>Информация о родителях или null если информация не найдена</returns>
        public List<Parent> getParents(int babyId)
        {
            String sql = String.Format("select parent_id, parent_surname, parent_name, parent_patronomic, parent_phone, parent_workphone, parent_location, parent_work, parent_info " + 
                " from parent where baby_id = {0}", babyId);
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                int rowCount = table.Rows.Count;
                List<Parent> result = new List<Parent>();
                for (int i = 0; i < rowCount; i++)
                {
                    Parent parent = new Parent();
                    parent.id = Convert.ToInt32(table.Rows[i][0]);
                    parent.surname = table.Rows[i][1].ToString();
                    parent.name = table.Rows[i][2].ToString();
                    parent.patronomic = table.Rows[i][3].ToString();
                    parent.phone = table.Rows[i][4].ToString();
                    parent.workPhone = table.Rows[i][5].ToString();
                    parent.location = table.Rows[i][6].ToString();
                    parent.work = table.Rows[i][7].ToString();
                    parent.parentInfo = table.Rows[i][8].ToString();

                    result.Add(parent);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Возвращает информацию о довереных лицах
        /// </summary>
        /// <param name="babyId">ИД ребенка</param>
        /// <returns>Информация о довереных лицах</returns>
        public DataTable getTrustee(int babyId)
        {
            String sql = String.Format("select trustee_id , trustee_surname as \"Фамилия\", trustee_name as \"Имя\", trustee_patronomic as \"Отчество\", trustee_phone \"Телефон\", trustee_caption \"Кем приходится\"from trustee where baby_id = {0}" +
                " and trustee_caption not like 'Отец' and trustee_caption not like 'Мать'", babyId);
            return select(sql);
        }

        /// <summary>
        /// Ищет ид довереного лица по ИД ребенка и описанию
        /// </summary>
        /// <param name="babyId">ИД ребенка</param>
        /// <param name="caption">Описание</param>
        /// <returns>ИД довереного лица или -1 если ничего не нашли</returns>
        public int getTrusteeId(int babyId, String caption)
        {
            String sql = String.Format("select trustee_id from trustee where baby_id = {0} and trustee_caption like '{1}'",
                babyId, caption);
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            else
            {
                return -1;
            }
        }

        public int getTrusteeId(String surname, String name)
        {
            String sql = String.Format("select educator_id from educator where educator_surname like = {0} and educator_name like = {1}",surname, name);
            DataTable table = select(sql);

            if (table != null && table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Возвращает стоимость часа для группы, в которой находится заданный ребенок
        /// </summary>
        /// <param name="babyId">ИД ребенка</param>
        /// <returns>Стоимость часа</returns>
        public int getGroupCostByBaby(int babyId)
        {
            String sql = String.Format("select group_coast as \"Cost\" " +
                " from grouptable g, baby b " +
                " where g.group_id = b.group_id " +
                " and b.baby_id = {0}",
                babyId);
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Возвращает список оплаты пребывания ребенка
        /// </summary>
        /// <returns>Списки оплаты</returns>
        public DataTable getPaymentList()
        {
            String sql = "select pay_id, baby_surname as \"Фамилия\", " + 
                " baby_name as \"Имя\", " + 
                " baby_patronomic as \"Отчество\", " +
                " baby_birthday as \"Дата рождения\", " + 
                " group_name as \"Группа\", " + 
                " pay_balance as \"Баланс\" " + 
                " from pay, grouptable, baby " + 
                " where pay.baby_id = baby.baby_id and baby.group_id = grouptable.group_id";

            return select(sql);
        }

        /// <summary>
        /// Получает информацию о посещении ребенка
        /// </summary>
        /// <param name="babyId">Ид ребенка</param>
        /// <returns>Информация о посещении ребенка</returns>
        public DataTable getVisitingByBabyId(int babyId)
        {
            String sql = String.Format("select visiting_timebegin, visiting_timeend, visiting_cost" +
                " from visiting " + 
                " where baby_id = {0}", babyId);
            return select(sql);
        }

        /// <summary>
        /// Возвращает баланс ребенка
        /// </summary>
        /// <param name="babyId"></param>
        /// <returns></returns>
        public double getBabyBalance(int babyId)
        {
            String sql = String.Format("select pay_balance from pay where baby_id = {0}", babyId);
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                return Convert.ToDouble(table.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Получает список питания на заданную дату
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns>Список питания или null</returns>
        public DataTable getEatingListByDate(DateTime date)
        {
            String d = date.ToString("yyyy-MM-dd");
            String sql = String.Format("select " +
                " e.eating_id as \"ID\"," +
                " e.baby_id as \"Baby ID\", " +
                " (select concat_ws(\" \", b.baby_surname, b.baby_name, b.baby_patronomic)" +
                "   from baby b" +
                "   where b.baby_id = e.baby_id " +
                " ) as \"Baby name\"," +
                " e.nutrition_type_id as \"N ID\"," +
                " e.coast as \"Cost\" " +
                " from eating e" +
                " where e.eating_date = '{0}'",
                d);
            return select(sql);
        }

        /// <summary>
        /// Получает текущую стомость питания
        /// </summary>
        public double[] getEatingCost()
        {
            String sql = ("select " +
                " eatshedule_id as \"ID\", " +
                " eating_cost as \"Cost\" " +
                " from eatshedule");
            DataTable data = select(sql);
            double[] costs = new double[4];
            for (int i = 0; i < 4; i++ )
            {
                costs[i] = data.Rows[i].Field<double>("Cost");
            }
            return costs;
        }
        /// <summary>
        /// Получает дату окончания графика питания
        /// </summary>
        public DateTime getEatscheduleEndDate()
        {
            String sql = ("select " +
                " eatschedule_date_end as \"Date\" " +
                " from eatshedule " +
                " where eatshedule_id = 1");
            DataTable data = select(sql);
            return data.Rows[0].Field<DateTime>("Date");
        }

        /// <summary>
        /// Получает информацию о питании ребенка
        /// </summary>
        /// <param name="babyId">Ид ребенка</param>
        /// <returns>Информация о питании ребенка</returns>
        public DataTable getEatingByBabyId(int babyId)
        {
            String sql = String.Format("select coast from eating where baby_id = {0}", babyId);
            return select(sql);
        }

        /// <summary>
        /// Проверяет есть ли расписание питания
        /// </summary>
        /// <returns>true - если расписание есть иначе false</returns>
        public bool isHasEatShedule()
        {
            String sql = String.Format("select eatshedule_id from eatshedule");
            DataTable table = select(sql);

            if (table != null && table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Получает информацию о питании
        /// </summary>
        /// <param name="eatId"></param>
        /// <returns>Информация о питании</returns>
        public Eat getEatById(int eatId)
        {
            Eat result = new Eat();
            String sql = String.Format("select eatshedule_time_begin, eatshedule_time_end, eating_cost from eatshedule where nutrition_type_id = {0}", eatId);
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                result.id = eatId;
                result.timeBegin = Convert.ToDateTime(table.Rows[0][0]).TimeOfDay;
                result.timeEnd = Convert.ToDateTime(table.Rows[0][1]).TimeOfDay;
                result.coast = Convert.ToInt32(table.Rows[0][2]);
                return result;
            }
            return null;
        }
        
        /// <summary>
        /// Получает расписание питания
        /// </summary>
        /// <returns>Расписание питания</returns>
        public EatShedule getEatShedule()
        {
            if (isHasEatShedule())
            {
                String sql = "select eatschedule_date_begin, eatschedule_date_end from eatshedule";
                DataTable table = select(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    EatShedule shedule = new EatShedule();
                    shedule.dateBegin = Convert.ToDateTime(table.Rows[0][0]);
                    shedule.dateEnd = Convert.ToDateTime(table.Rows[0][1]);

                    shedule.breakfast = getEatById(1);
                    shedule.snack = getEatById(2);
                    shedule.lunch = getEatById(3);
                    shedule.dinner = getEatById(4);
                    
                    return shedule;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Стоиость часа в группе
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>Стоимость часа</returns>
        public int getGroupCoast(int groupId)
        {
            String sql = String.Format("select group_coast from grouptable where group_id = {0}", groupId);
            DataTable table = select(sql);
            if (table != null && table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }
        /*****************************************************************************************
         ************************* Методы связанные с вставкой данных***************************** 
         *****************************************************************************************
         */
        /// <summary>
        /// Вставка новой группы
        /// </summary>
        /// <param name="name">Название группы</param>
        /// <param name="min">Минимальный возраст</param>
        /// <param name="max">Максимальный возраст</param>
        /// <param name="size">Вместимость группы</param>
        /// <param name="coast">Стоимость часа</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool insertGroup(String name, double min, double max, int size, double coast)
        {
            String minAge = min.ToString().Replace(",", ".");
            String maxAge = max.ToString().Replace(",", ".");
            String scoast = coast.ToString().Replace(",", ".");
            String sql = String.Format("insert into grouptable (group_name, max_age, min_age, group_size, group_coast) values('{0}', '{1}', '{2}', {3}, '{4}')",
            name, maxAge, minAge, size, scoast);
            return execute(sql);
        }

        /// <summary>
        /// Вставка нового воспитателя
        /// </summary>
        /// <param name="first_name">Имя</param>
        /// <param name="second_name">Фамилия</param>
        /// <param name="third_name">Отчество</param>
        /// <param name="phone">Номер телефона</param>
        /// <param name="location">Место жительства</param>
        /// <param name="group">Номер группы</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool insertEducator(String first_name, String second_name, String third_name, String phone, String location, int group)
        {
            String sql = String.Format("insert into educator" + 
                " (educator_surname, educator_name, educator_patronomic, educator_phone, educator_location, group_id)" + 
                " values('{0}', '{1}', '{2}', '{3}', '{4}','{5}')",
            second_name, first_name, third_name, phone, location, group);
            return execute(sql);
        }
        /// <summary>
        /// Вставка нового посещения
        /// </summary>
        /// <param name="baby_id">Id ребенка</param>
        /// <param name="date">Дата</param>
        /// <param name="begin">Время привода</param>
        /// <param name="end">Время ухода</param>
        /// <param name="educator_id">Id воспитателя</param>
        /// <param name="trustee_id">Id доверенного лица</param>
        /// <param name="cost"> Стоимость часа</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool insertVisiting(int baby_id, String date, String begin, String end, String educator_id, String trustee_id, String cost)
        {
            String sql = String.Format("insert into visiting" +
                " (visiting_date, visiting_timebegin, visiting_timeend, educator_id, baby_id, trustee_id, visiting_cost) " +
                " values('{0}', STR_TO_DATE('{1}','%d.%m.%Y %H:%i:%s'), " +
                " STR_TO_DATE({2},'%d.%m.%Y %H:%i:%s'), " +
                " {3},{4},{5},{6})",
                date, begin, end, educator_id, baby_id, trustee_id, cost);
            return execute(sql);
        }

        /// <summary>
        /// Вставка нового ребенка
        /// </summary>
        /// <param name="surname">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="patronomic">Отчество</param>
        /// <param name="birthday">День рождения</param>
        /// <param name="healtCertificate">Наличие справки о здоровье</param>
        /// <param name="groupId">Ид группы</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool insertBaby(String surname, String name, String patronomic, String birthday, int healtCertificate, int groupId)
        {
            String sql = String.Format("insert into baby (baby_surname, baby_name, baby_patronomic, health_certificat, group_id, baby_birthday)" +
                " values ('{0}', '{1}', '{2}', {3}, {4}, STR_TO_DATE('{5}', '%Y.%m.%d') )",
                surname, name, patronomic,healtCertificate, groupId, birthday);
            return execute(sql);
        }

        /// <summary>
        /// Вставка информации о прививках
        /// </summary>
        /// <param name="babyId">Ид ребенка</param>
        /// <param name="dtp">Дата для АКДС</param>
        /// <param name="parotits">Дата для паротита</param>
        /// <param name="tuber">Дата для туберкулеза</param>
        /// <param name="pol">Дата для полимелита</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool insertImmunization(int babyId, String dtp, String parotits, String tuber, String poli)
        {
            String sql = String.Format("insert into immunization (baby_id, dtp, parotits, poli, tuberculosis) " +
                "values({0}, STR_TO_DATE('{1}', '%Y.%m.%d'), STR_TO_DATE('{2}', '%Y.%m.%d'), STR_TO_DATE('{3}', '%Y.%m.%d'), STR_TO_DATE('{4}', '%Y.%m.%d'))",
                babyId, dtp, parotits,tuber, poli);
            return execute(sql);
        }

        /// <summary>
        /// Вставка информации о планируемом графике посещения
        /// </summary>
        /// <param name="babyId">ИД ребенка</param>
        /// <param name="dateBegin">Дата начала графика</param>
        /// <param name="dateEnd">Дата окончания графика</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool insertSheduleVisit(int babyId, String dateBegin, String dateEnd)
        {
            String sql = String.Format("insert into shedulevisit (baby_id, date_begin, date_end) " +
                " values({0}, STR_TO_DATE('{1}', '%Y.%m.%d'), STR_TO_DATE('{2}', '%Y.%m.%d'))", babyId, dateBegin, dateEnd);
            return execute(sql);
        }

        /// <summary>
        /// Вставка информации о строке планируемого графика посещения
        /// </summary>
        /// <param name="shedulevisitId">ИД графика</param>
        /// <param name="dayWeekId">ИД дня недели</param>
        /// <param name="eatMorning">Питание утром</param>
        /// <param name="eatEvening">Питание вечером</param>
        /// <param name="timeBegin">Время прихода</param>
        /// <param name="timeEnd">Время ухода</param>
        /// <param name="babyId">ИД ребенка</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool insertSheduleVisitString(int shedulevisitId, int dayWeekId, int eatBreakfast, int eatSnack, int eatLunch, int eatDinner, String timeBegin, String timeEnd, int babyId)
        {
            String sql = String.Format("insert into string_shedulevisit (shedulevisit_id, dayweek_id, eat_breakfast, eat_snack, eat_lunch, eat_dinner, time_begin, time_end, baby_id) " +
                " values({0}, {1}, {2}, {3}, {4}, {5}, '{6}', '{7}', {8})",
                shedulevisitId, dayWeekId, eatBreakfast, eatSnack, eatLunch, eatDinner, timeBegin, timeEnd, babyId);
            return execute(sql);
        }

        /// <summary>
        /// Вставка родителя
        /// </summary>
        /// <param name="surname">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="patronomic">Отчество</param>
        /// <param name="location">Место проживания</param>
        /// <param name="phone">Телефон</param>
        /// <param name="workphone">Рабочий телефон</param>
        /// <param name="work">Место работы</param>
        /// <param name="parent_info">Информация</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool insertParent(String surname, String name, String patronomic, String phone, String workphone, String location, String work, String parent_info, int babyId)
        {
            String sql = String.Format("insert into parent (parent_surname, parent_name, parent_patronomic, parent_phone, parent_workphone, parent_location, parent_work, parent_info, baby_id)" +
                " values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8} )",
                surname, name, patronomic, phone, workphone, location, work, parent_info, babyId);
            return execute(sql);
        }
        /// <summary>
        /// Вставка довереного лица
        /// </summary>
        /// <param name="surname">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="patronomic">Отчество</param>
        /// <param name="phone">Телефон</param>
        /// <param name="caption">Описание</param>
        /// <param name="babyId">ИД ребенка</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool insertTrustee(String surname, String name, String patronomic, String phone, String caption, int babyId)
        {
            String sql = String.Format("insert into trustee (trustee_surname, trustee_name, trustee_patronomic, trustee_phone, trustee_caption, baby_id)" + 
                "values ('{0}', '{1}', '{2}', '{3}', '{4}', {5})", 
                surname, name, patronomic, phone, caption, babyId);
            return execute(sql);
        }

        /// <summary>
        /// Вставка информации о ребенке в список оплаты пребывания
        /// </summary>
        /// <param name="babyId"></param>
        /// <returns></returns>
        public bool insertBabyInPaymentList(int babyId)
        {
            String sql = String.Format("insert into pay (baby_id, pay_balance) values({0}, {1})", babyId, 0);
            return execute(sql);
        }

        /// <summary>
        /// Вставка нового питания
        /// </summary>
        /// <param name="baby_id">Id ребенка</param>
        /// <param name="date">Дата</param>
        /// <param name="nut_id">ID типа питания</param>
        /// <param name="cost">Текущая стоимость</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool insertEating(int baby_id, String date, int nut_id, double cost)
        {
            String sql = String.Format("insert into eating" +
                " (eating_date, nutrition_type_id, baby_id, coast) " +
                " values('{0}',{1},{2},{3})",
                date, nut_id, baby_id, cost);
            return execute(sql);
        }

        public bool insertEatShedule(EatShedule shedule)
        {
            deleteEatShedule();

            String sql = String.Format("INSERT INTO eatshedule (eatshedule_time_begin, eatshedule_time_end, eating_cost, eatschedule_date_begin, eatschedule_date_end, nutrition_type_id) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                new DateTime(1990, 1, 1, shedule.breakfast.timeBegin.Hours, shedule.breakfast.timeBegin.Minutes, shedule.breakfast.timeBegin.Seconds).ToString("yyyy-MM-dd HH:mm:ss"),
                new DateTime(1990, 1, 1, shedule.breakfast.timeEnd.Hours, shedule.breakfast.timeEnd.Minutes, shedule.breakfast.timeEnd.Seconds).ToString("yyyy-MM-dd HH:mm:ss"),
                shedule.breakfast.coast, shedule.dateBegin.ToString("yyyy-MM-dd"), shedule.dateEnd.ToString("yyyy-MM-dd"), 1);
            execute(sql);

            sql = String.Format("INSERT INTO eatshedule (eatshedule_time_begin, eatshedule_time_end, eating_cost, eatschedule_date_begin, eatschedule_date_end, nutrition_type_id) VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                new DateTime(1990, 1, 1, shedule.snack.timeBegin.Hours, shedule.snack.timeBegin.Minutes, shedule.snack.timeBegin.Seconds).ToString("yyyy-MM-dd HH:mm:ss"),
                new DateTime(1990, 1, 1, shedule.snack.timeEnd.Hours, shedule.snack.timeEnd.Minutes, shedule.snack.timeEnd.Seconds).ToString("yyyy-MM-dd HH:mm:ss"),
                shedule.snack.coast, shedule.dateBegin.ToString("yyyy-MM-dd"), shedule.dateEnd.ToString("yyyy-MM-dd"), 2);
            execute(sql);

            sql = String.Format("INSERT INTO eatshedule (eatshedule_time_begin, eatshedule_time_end, eating_cost, eatschedule_date_begin, eatschedule_date_end, nutrition_type_id) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                new DateTime(1990, 1, 1, shedule.lunch.timeBegin.Hours, shedule.lunch.timeBegin.Minutes, shedule.lunch.timeBegin.Seconds).ToString("yyyy-MM-dd HH:mm:ss"),
                new DateTime(1990, 1, 1, shedule.lunch.timeEnd.Hours, shedule.lunch.timeEnd.Minutes, shedule.lunch.timeEnd.Seconds).ToString("yyyy-MM-dd HH:mm:ss"),
                shedule.lunch.coast, shedule.dateBegin.ToString("yyyy-MM-dd"), shedule.dateEnd.ToString("yyyy-MM-dd"), 3);
            execute(sql);

            sql = String.Format("INSERT INTO eatshedule (eatshedule_time_begin, eatshedule_time_end, eating_cost, eatschedule_date_begin, eatschedule_date_end, nutrition_type_id) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                new DateTime(1990, 1, 1, shedule.dinner.timeBegin.Hours, shedule.dinner.timeBegin.Minutes, shedule.dinner.timeBegin.Seconds).ToString("yyyy-MM-dd HH:mm:ss"),
                new DateTime(1990, 1, 1, shedule.dinner.timeEnd.Hours, shedule.dinner.timeEnd.Minutes, shedule.dinner.timeEnd.Seconds).ToString("yyyy-MM-dd HH:mm:ss"),
                shedule.dinner.coast, shedule.dateBegin.ToString("yyyy-MM-dd"), shedule.dateEnd.ToString("yyyy-MM-dd"), 4);
            execute(sql);
            return true;
        }
        /*****************************************************************************************
         ************************* Методы связанные с обновлением данных***************************** 
         *****************************************************************************************
         */
        /// <summary>
        /// Обновление группы
        /// </summary>
        /// <param name="name">Название группы</param>
        /// <param name="min">Минимальный возраст</param>
        /// <param name="max">Максимальный возраст</param>
        /// <param name="size">Вместимость группы</param>
        /// <param name="coast">Стоимость часа</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool updateGroup(int id, String name, double min, double max, int size, double coast)
        {
            String minAge = min.ToString().Replace(",", ".");
            String maxAge = max.ToString().Replace(",", ".");
            String scoast = coast.ToString().Replace(",", ".");
            String sql = String.Format("update grouptable set group_name = '{0}', min_age = '{1}', max_age = '{2}', group_size = '{3}', group_coast = '{4}' where group_id = {5}",
                name, minAge, maxAge, size, scoast, id);
            return execute(sql);
        }

        /// <summary>
        /// Обновление воспитателя с заданным id 
        /// </summary>
        /// <param name="first_name">Имя</param>
        /// <param name="second_name">Фамилия</param>
        /// <param name="third_name">Отчество</param>
        /// <param name="phone">Номер телефона</param>
        /// <param name="location">Место жительства</param>
        /// <param name="group">Номер группы</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool updateEducator(String first_name, String second_name, String third_name, String phone, String location, int group, int educator_id)
        {
            String sql = String.Format("update educator" + 
                " set educator_surname = '{0}', educator_name = '{1}', educator_patronomic = '{2}', educator_phone = '{3}', educator_location = '{4}', group_id = '{5}' " + 
                " where educator_id = {6}",
            second_name, first_name, third_name, phone, location, group, educator_id);
            return execute(sql);
        }

        /// <summary>
        /// Обновление посещения
        /// </summary>
        /// <param name="v_id">Id строки посещения</param>
        /// <param name="begin">Время привода</param>
        /// <param name="end">Время ухода</param>
        /// <param name="educator_id">Id воспитателя</param>
        /// <param name="trustee_id">Id доверенного лица</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool updateVisiting(int v_id, String begin, String end, String educator_id, String trustee_id)
        {
            String sql = String.Format("update visiting" +
                " set visiting_timebegin = STR_TO_DATE('{0}','%d.%m.%Y %H:%i:%s'), " +
                " visiting_timeend = STR_TO_DATE({1},'%d.%m.%Y %H:%i:%s'), " +
                " educator_id = {2}," +
                " trustee_id = {3}" +
                " where visiting_id = {4}",
                begin, end, educator_id, trustee_id, v_id);
            return execute(sql);
        }


        /// <summary>
        /// Обновление информации о ребенке
        /// </summary>
        /// <param name="babyId">ИД ребенка</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="patronomic">Отчество</param>
        /// <param name="health">Наличие справки</param>
        /// <param name="birthday">День рождения</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool updateBaby(int babyId, String surname, String name, String patronomic, int health, String birthday)
        {
            String sql = String.Format("update baby" + 
                " set baby_surname = '{0}'," +
                " baby_name = '{1}'," +
                " baby_patronomic = '{2}'," +
                " health_certificat = {3}," +
                " baby_birthday = STR_TO_DATE('{4}', '%Y.%m.%d')" +
                " where baby_id = {5}",
                surname, name, patronomic, health, birthday, babyId);
            return execute(sql);
        }

        /// <summary>
        /// Обновление информации о прививках
        /// </summary>
        /// <param name="babyId">ИД ребенка</param>
        /// <param name="dtp">АКДС</param>
        /// <param name="parotits">Паротит</param>
        /// <param name="tuber">Туберкулез</param>
        /// <param name="poli">Полимелит</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool updateImmunization(int babyId, String dtp, String parotits, String tuber, String poli)
        {
            String sql = String.Format("update immunization set " +
                " dtp = STR_TO_DATE('{0}', '%Y.%m.%d')," +
                " parotits = STR_TO_DATE('{1}', '%Y.%m.%d')," +
                " tuberculosis = STR_TO_DATE('{2}', '%Y.%m.%d')," +
                " poli = STR_TO_DATE('{3}', '%Y.%m.%d')" + 
                " where baby_id = {4}",
                dtp, parotits, tuber, poli, babyId);
            return execute(sql);
        }

        /// <summary>
        /// Обновление графика посещения
        /// </summary>
        /// <param name="babyId">ИД ребенка</param>
        /// <param name="dateBegin">Дата начала</param>
        /// <param name="dateEnd">Дата окончания</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool updateSheduleVisit(int babyId, String dateBegin, String dateEnd)
        {
            String sql = String.Format("update shedulevisit set " +
                " date_begin = STR_TO_DATE('{0}', '%Y.%m.%d')," +
                " date_end = STR_TO_DATE('{1}', '%Y.%m.%d')" +
                " where baby_id = {2}", dateBegin, dateEnd, babyId);
            return execute(sql);
        }

        /// <summary>
        /// Обновление информации о родителях
        /// </summary>
        /// <param name="parentId">ИД родителя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="patronomic">Отчество</param>
        /// <param name="phone">Телефон</param>
        /// <param name="workPhone">Дополнительный телефон</param>
        /// <param name="location">Место проживания</param>
        /// <param name="work">Место работы</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool updateParent(int parentId, String surname, String name, String patronomic, String phone, String workPhone, String location, String work)
        {
            String sql = String.Format("update parent set " + 
                " parent_surname = '{0}'," +
                " parent_name = '{1}'," +
                " parent_patronomic = '{2}'," +
                " parent_phone = '{3}'," +
                " parent_workphone = '{4}'," +
                " parent_location = '{5}'," +
                " parent_work = '{6}' " +
                " where parent_id = {7}",
                surname, name, patronomic, phone, workPhone, location, work, parentId);

            return execute(sql);
        }

        /// <summary>
        /// Обновление информации о довереных лицах
        /// </summary>
        /// <param name="trusteeId">ИД довереного лица</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="patronomic">Отчество</param>
        /// <param name="phone">Телефон</param>
        /// <param name="caption">Описание</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool updateTrustee(int trusteeId, String surname, String name, String patronomic, String phone, String caption)
        {
            String sql = String.Format("update trustee set " + 
                " trustee_surname = '{0}'," +
                " trustee_name = '{1}'," +
                " trustee_patronomic = '{2}'," +
                " trustee_phone = '{3}', " +
                " trustee_caption = '{4}' " + 
                " where trustee_id = {5}",
                surname, name, patronomic, phone, caption, trusteeId);
            return execute(sql);
        }
        
        /// <summary>
        /// Перевод ребенка в другую группу
        /// </summary>
        /// <param name="childId">Ид ребенка, которого нужно перевести</param>
        /// <param name="groupId">Ид группы в  которую переводим ребенка</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool moveChild(int childId, int groupId)
        {
            String sql = String.Format("update baby set group_id = {0} where baby_id = {1} ", groupId, childId);
            return execute(sql);
        }

        /// <summary>
        /// Обновление информации о платежах 
        /// </summary>
        /// <param name="babyId">Ид ребенка</param>
        /// <param name="sum">Сумма внесения</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool updateBabyPayment(int babyId, double sum)
        {
            String sql = String.Format("update pay set pay_balance = {0} where baby_id = {1}", sum, babyId);
            return execute(sql);
        }
        
        /*****************************************************************************************
         ************************* Методы связанные с удалением данных***************************** 
         *****************************************************************************************
         */
        /// <summary>
        /// Удаление группы по заданному id
        /// </summary>
        /// <param name="id">id группы, которую необходимо удалить </param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool deleteGroup(int id)
        {
            String sql = String.Format("delete from grouptable where group_id = '{0}'", id);
            return execute(sql);
        }

        /// <summary>
        /// Удаление воспитателя по заданному id
        /// </summary>
        /// <param name="id">id воспитателя, которого необходимо удалить </param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool deleteEducator(int id)
        {
            String sql = String.Format("delete from educator where educator_id = '{0}'", id);
            return execute(sql);
        }

        /// <summary>
        /// Удаление информации о ребенке
        /// </summary>
        /// <param name="babyId">ИД ребенка для удаления </param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool deleteBaby(int babyId)
        {
            String sql = String.Format("delete from baby where baby_id = {0}", babyId);
            return execute(sql);
        }

        /// <summary>
        /// Удаляет строки графика посещения
        /// </summary>
        /// <param name="babyId"></param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool deleteSheduleVisitString(int babyId)
        {
            String sql = String.Format("delete from string_shedulevisit where baby_id = {0}", babyId);
            return execute(sql);
        }

        /// <summary>
        /// Удаление информации о довереном лице
        /// </summary>
        /// <param name="trusteeId">ИД довереного лица</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool deleteTrustee(int trusteeId)
        {
            if (isHasDate(trusteeId)) return false;
            String sql = String.Format("delete from trustee where trustee_id = {0}", trusteeId);
            return execute(sql);
        }

        /// <summary>
        /// Удаление информации о родителях
        /// </summary>
        /// <param name="parentId">ИД родителя</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool deleteParent(int parentId)
        {
            String sql = String.Format("delete from parent where parent_id = {0}", parentId);
            return execute(sql);
        }

        public bool isHasDate(int trusteeId)
        {
            String sql = String.Format("select visiting_id_id from visitint where trustee_id = {0}",trusteeId);
            DataTable table = select(sql);

            if (table != null && table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Удаление питания по типу, дате и ребенку
        /// </summary>
        /// <param name="id">id воспитателя, которого необходимо удалить </param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool deleteEating(int baby_id, int nut_id, String date)
        {
            String sql = String.Format("delete from eating" +
                " where nutrition_type_id = {0} " +
                " and baby_id = {1} " +
                " and eating_date = '{2}'",
                nut_id, baby_id, date);
            return execute(sql);
        }

        /// <summary>
        /// Удаление расписания
        /// </summary>
        /// <returns></returns>
        public bool deleteEatShedule()
        {
            for (int i = 0; i < 4; i++)
            {
                String sql = String.Format("delete from eatshedule where nutrition_type_id = {0}", i + 1);
                execute(sql);
            }
            return true;
        }
    }
}

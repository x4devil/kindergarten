using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

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

        /*****************************************************************************************
         ************************* Методы связанные с выборкой данных***************************** 
         *****************************************************************************************
         */
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
                " v.trustee_id as \"Доверенное лицо\" " +
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
                " educator_id \"Номер\" " +
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
                " trustee_id \"Номер\" " +
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
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool insertVisiting(int baby_id, String date, String begin, String end, String educator_id, String trustee_id)
        {
            String sql = String.Format("insert into visiting" +
                " (visiting_date, visiting_timebegin, visiting_timeend, educator_id, baby_id, trustee_id) " +
                " values('{0}', STR_TO_DATE('{1}','%d.%m.%Y %H:%i:%s'), " +
                " STR_TO_DATE({2},'%d.%m.%Y %H:%i:%s'), " +
                " {3},{4},{5})",
                date, begin, end, educator_id, baby_id, trustee_id);
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
        public bool insertSheduleVisitString(int shedulevisitId, int dayWeekId, int eatMorning, int eatEvening, String timeBegin, String timeEnd, int babyId)
        {
            String sql = String.Format("insert into string_shedulevisit (shedulevisit_id, dayweek_id, eating_morning, eating_evening, time_begin, time_end, baby_id) " +
                " values({0}, {1}, {2}, {3}, '{4}', '{5}', {6})",
                shedulevisitId, dayWeekId, eatMorning, eatEvening, timeBegin, timeEnd, babyId);
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
    }
}

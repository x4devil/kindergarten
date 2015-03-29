﻿using System;
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
        /// Получает количество групп
        /// </summary>
        /// <returns>Возвращает количество групп или -1 если групп нет</returns>
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
                return -1;
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
        /// Получает название группы по ее ИД
        /// </summary>
        /// <param name="id">ИД группы</param>
        /// <returns>Название группы или null</returns>
        public String getGroupNameById(int id)
        {
            String sql = String.Format("select group_name from grouptable where group_id = {0} ", id);
            DataTable table = select(sql);

            if (table != null && table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }

            return null;
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
        /// Перевод ребенка в другую группу
        /// </summary>
        /// <param name="babyId">ИД ребенка, которого переводим</param>
        /// <param name="groupId">ИД группы в которую переводим ребенка </param>
        /// <returns></returns>
        public bool moveBaby(int babyId, int groupId)
        {
            String sql = String.Format("update baby set group_id = {0} where baby_id = {1}", groupId, babyId);
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
        /// Удаление ребенка по заданому id
        /// </summary>
        /// <param name="id">ИД ребенка, которого необходимо удалить</param>
        /// <returns>true если операция выполнена успешно иначе false</returns>
        public bool deleteBaby(int id)
        {
            String sql = String.Format("delete from baby where baby_id = '{0}'", id);
            return execute(sql);
        }
    }
}

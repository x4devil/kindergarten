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
    }
}

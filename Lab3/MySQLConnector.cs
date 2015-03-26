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
    /// Основной класс для работы с   БД 
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
            if (connectionIsOpen())
            {
                MySqlCommand comm = new MySqlCommand(sql, connection);
                IDataReader reader = comm.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                return table;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Получает список групп 
        /// </summary>
        /// <returns>Список групп или null если нет подключения</returns>
        public DataTable getGroupList()
        {
            String sql = "select group_id as 'ИД', group_name, max_age, min_age, group_size, group_coast" + 
                "from group";
            return select(sql);
        }
    }
}

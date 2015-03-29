using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    /// <summary>
    /// Класс для хранения глобальных данных
    /// </summary>
    class GlobalVars
    {
        //172.28.128.3
        /// <summary>
        /// Основной класс для работы с БД
        /// </summary>
        public static MySQLConnector connection = null;

        /// <summary>
        /// IP сервера с БД
        /// </summary>
        public static String server = null;
        /// <summary>
        /// Логин для подключения к БД
        /// </summary>
        public static String user = null;
        /// <summary>
        /// Пароль для подключения к БД
        /// </summary>
        public static String password = null;

        /// <summary>
        /// Активная группа 
        /// </summary>
        public static int activeGroupId = -1;

        public static int activeBabyId = -1;

        /// <summary>
        /// Выводит предупреждающее диалоговое окно
        /// </summary>
        /// <param name="msg">Текст сообщения</param>
        public static void showWarningMsgBox(String msg)
        {
            MessageBox.Show(null, msg, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Выводит вопросительное диалоговое окно
        /// </summary>
        /// <param name="msg">Текст сообщения</param>
        /// <returns>Возвращает результат диалога. Yes - Если нажата кнопка "Да", No - иначе</returns>
        public static DialogResult showQuestionMsgBox(String msg)
        {
            return MessageBox.Show(null, msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Проверяет строку на валидность
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool isValidateString(String str)
        {

            if (str == null || str.Equals("") || str.Equals(" "))
            {
                return false;
            }
            char[] stopChar = { '%', '$', '!', '^', '#', '@', '\'', '\"', '\\', '|', '/', '*', '&', '~', '=', '<', '>' };
            if (str.IndexOfAny(stopChar) >= 0)
            {
                return false;
            }
            if (str[0] >= '0' && str[0] <= '9')
            {
                return false;
            }
            return true;
        }
    }
}

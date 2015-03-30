using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Objects
{
    /// <summary>
    /// Родители
    /// </summary>
    class Parent
    {
        /// <summary>
        /// ИД родителя
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public String surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public String patronomic { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public String phone { get; set; }

        /// <summary>
        /// Рабочий телефон
        /// </summary>
        public String workPhone { get; set; }

        /// <summary>
        /// Место проживания
        /// </summary>
        public String location { get; set; }

        /// <summary>
        /// Место работы
        /// </summary>
        public String work { get; set; }

        /// <summary>
        /// Информация
        /// </summary>
        public String parentInfo { get; set; }
    }
}

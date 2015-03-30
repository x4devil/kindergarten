using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Objects
{
    /// <summary>
    /// Довереное лицо
    /// </summary>
    class Trustee
    {
        /// <summary>
        /// ИД довереного лица
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
        /// Описание
        /// </summary>
        public String caption { get; set; }
    }
}

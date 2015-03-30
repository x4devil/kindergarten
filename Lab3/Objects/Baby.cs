using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Forms
{
    /// <summary>
    /// Класс хранящий информацию о ребенке
    /// </summary>
    class Baby
    {
        /// <summary>
        /// Ид ребенка
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Фамилия ребенка
        /// </summary>
        public String surname { get; set; }

        /// <summary>
        /// Имя ребенка
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// Отчество ребенка
        /// </summary>
        public String patronomic { get; set; }

        /// <summary>
        /// Наличие справки о здоровье (0 - нет; 1 - есть)
        /// </summary>
        public int healthCertificate { get; set; }

        /// <summary>
        /// ИД группы
        /// </summary>
        public int groupId { get; set; }

        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime birthday { get; set; }
    }
}

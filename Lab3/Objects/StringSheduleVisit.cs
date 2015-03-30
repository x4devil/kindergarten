using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Objects
{
    /// <summary>
    /// Строка графика посещения
    /// </summary>
    class StringSheduleVisit
    {
        /// <summary>
        /// ИД строки
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Питанрие утром
        /// </summary>
        public int eatMorning { get; set; }

        /// <summary>
        /// Питание вечером
        /// </summary>
        public int eatEvening { get; set; }

        /// <summary>
        /// Время прихода
        /// </summary>
        public DateTime timeBegin { get; set; }

        /// <summary>
        /// Время ухода
        /// </summary>
        public DateTime timeEnd { get; set; }

        /// <summary>
        /// День недели
        /// </summary>
        public int dayWeekId { get; set; }
    }
}

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
        /// Завтрак
        /// </summary>
        public int eatBreakfast { get; set; }

        /// <summary>
        /// Полдник
        /// </summary>
        public int eatSnack { get; set; }

        /// <summary>
        /// Обед
        /// </summary>
        public int eatLunch { get; set; }

        /// <summary>
        /// Завтрак
        /// </summary>
        public int eatDinner { get; set; }

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

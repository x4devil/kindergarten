using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Objects
{
    /// <summary>
    /// Расписание питания
    /// </summary>
    class EatShedule
    {
        /// <summary>
        /// Ид расписания
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Дата начала расписания
        /// </summary>
        public DateTime dateBegin { get; set; }

        /// <summary>
        /// Дата окончания расписания
        /// </summary>
        public DateTime dateEnd { get; set; }

        /// <summary>
        /// Завтрак
        /// </summary>
        public Eat breakfast { get; set; }

        /// <summary>
        /// Полдник
        /// </summary>
        public Eat snack { get; set; }

        /// <summary>
        /// Обед
        /// </summary>
        public Eat lunch { get; set; }

        /// <summary>
        /// Ужин
        /// </summary>
        public Eat dinner { get; set; }
    }
}

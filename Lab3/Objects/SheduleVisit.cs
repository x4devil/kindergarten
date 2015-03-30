using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Objects
{
    /// <summary>
    /// Планируемый график посещений
    /// </summary>
    class SheduleVisit
    {
        /// <summary>
        /// ИД графика
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Дата начала графика
        /// </summary>
        public DateTime dateBegin { get; set; }

        /// <summary>
        /// Дата окончания графика
        /// </summary>
        public DateTime dateEnd { get; set; }
    }
}

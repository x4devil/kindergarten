using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Objects
{
    /// <summary>
    /// Питание
    /// </summary>
    class Eat
    {
        /// <summary>
        /// Ид типа питания (завтрак, полдник, обед, ужин)
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Стоимость питания
        /// </summary>
        public int coast { get; set; }
        /// <summary>
        /// Время начала
        /// </summary>
        public TimeSpan timeBegin { get; set; }
        /// <summary>
        /// Время окончания
        /// </summary>
        public TimeSpan timeEnd { get; set; }
    }
}

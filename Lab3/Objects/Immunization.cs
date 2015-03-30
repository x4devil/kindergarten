using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Objects
{
    /// <summary>
    /// График прививок
    /// </summary>
    class Immunization
    {
        /// <summary>
        /// ИД графика прививок
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// АКДС
        /// </summary>
        public DateTime dtp { get; set; }

        /// <summary>
        /// Паротит
        /// </summary>
        public DateTime parotits { get; set; }

        /// <summary>
        /// Туберкулез
        /// </summary>
        public DateTime tuber { get; set; }

        /// <summary>
        /// Полимелит
        /// </summary>
        public DateTime poli { get; set; }
    }
}

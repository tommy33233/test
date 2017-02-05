using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace perf.Import
{
    public class Perfume
    {
        [Key]
        public int LineId { get; set; }

        public int? Number { get; set; }

        [StringLength(200)]
        public string Product { get; set; }
        [StringLength(200)]
        public string Producer { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }
        public DateTime? Date { get; set; }
        [StringLength(1)]
        public string IsSampleAvailable { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [StringLength(1)]
        public string IsFullSize { get; set; }
        [StringLength(250)]
        public string Note { get; set; }
        [StringLength(100)]
        public  string MagDate { get; set; }

    }
}
